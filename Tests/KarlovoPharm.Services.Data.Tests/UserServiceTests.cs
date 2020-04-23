namespace KarlovoPharm.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using KarlovoPharm.Data;
    using KarlovoPharm.Data.Models.Common;
    using KarlovoPharm.Data.Repositories;
    using KarlovoPharm.Services.Data.Tests.Common;
    using KarlovoPharm.Services.Data.Tests.Common.Seeders;
    using KarlovoPharm.Services.Data.Users;
    using KarlovoPharm.Web.InputModels.Users;
    using KarlovoPharm.Web.ViewModels.User;
    using Microsoft.AspNetCore.Identity;
    using Moq;
    using Xunit;

    public class UserServiceTests
    {
        [Fact]
        public async Task UserHasThreeAdresses_ReturnsCorrectBooleanResult()
        {
            var errorMessage = "UserService UserHasThreeAddresses() method does not return the correct boolean result";

            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(context);
            var userService = this.GetUserService(userRepository, context);
            var seeder = new UserTestSeeder();
            await seeder.SeedUsersWithAddressesAsync(context);

            var userWithoutAddresses = context.Users.First(x => x.UserName == "UserWithoutAddresses");
            var userWithThreeAddresses = context.Users.First(x => x.UserName == "UserWithThreeAddresses");

            var resultNoAdresses = await userService.UserHasThreeAddresses(userWithoutAddresses.Id);
            var resultThreeAdresses = await userService.UserHasThreeAddresses(userWithThreeAddresses.Id);

            Assert.False(resultNoAdresses, errorMessage);
            Assert.True(resultThreeAdresses, errorMessage);
        }

        [Fact]
        public async Task EditProfileAsync_ThrowsNullException_IfTheUserIsNull()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(context);
            var userService = this.GetUserService(userRepository, context);
            var profileEditInputModel = new ProfileEdintInputModel();

            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await userService.EditProfileAsync(profileEditInputModel);
            });
        }

        [Fact]
        public async Task EditProfileAsync_ReturnsFalse_IfUsernameAlreadyExists()
        {
            var errorMessage = "EditProfileAsync did not return false when a user tried to change his username to an already existing one.";

            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(context);
            var userService = this.GetUserService(userRepository, context);
            var seeder = new UserTestSeeder();
            await seeder.SeedUsersWithAddressesAsync(context);

            var userWithoutAddresses = context.Users.First(x => x.UserName == "UserWithoutAddresses");

            var profileEditInputModel = new ProfileEdintInputModel()
            {
                Id = userWithoutAddresses.Id,
                Username = "UserWithThreeAddresses",
            };

            var result = await userService.EditProfileAsync(profileEditInputModel);

            Assert.False(result, errorMessage);

        }

        [Fact]
        public async Task EditProfileAsync_ReturnsTrueAndMakesChanges_IfTheModelIsCorrect()
        {
            var errorMessageBool = "EditProfileAsync did not return true";

            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(context);
            var userService = this.GetUserService(userRepository, context);
            var seeder = new UserTestSeeder();
            await seeder.SeedUsersWithAddressesAsync(context);

            var userWithoutAddresses = context.Users.First(x => x.UserName == "UserWithoutAddresses");

            var profileEditInputModel = new ProfileEdintInputModel()
            {
                Id = userWithoutAddresses.Id,
                Username = "Changed",
            };

            var result = await userService.EditProfileAsync(profileEditInputModel);

            Assert.True(result, errorMessageBool);
            Assert.Equal("Changed", userWithoutAddresses.UserName);
        }

        [Fact]
        public async Task GetUserInfo_WorksCorectly()
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(context);
            var userService = this.GetUserService(userRepository, context);
            var userTestSeeder = new UserTestSeeder();

            await userTestSeeder.SeedUsersWithAddressesAsync(context);

            var result = await userService.GetUserInfo<UserProfileViewModel>("UserId1");

            Assert.NotNull(result);

            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                 await userService.GetUserInfo<ProfileEdintInputModel>(null);
            });
        }

        [Fact]
        public async Task GetUserWithAllPropertiesById_WorksCorectly()
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(context);
            var userService = this.GetUserService(userRepository, context);
            var userTestSeeder = new UserTestSeeder();

            await userTestSeeder.SeedUsersWithAddressesAsync(context);

            var result = await userService.GetUserWithAllPropertiesById("UserId1");

            Assert.NotNull(result);

            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await userService.GetUserWithAllPropertiesById(null);
            });
        }

        private UserService GetUserService(EfDeletableEntityRepository<ApplicationUser> userRepository, ApplicationDbContext context)
        {
            // UserManager
            var userManagerMock = this.GetUserManagerMock(context);

            var userService = new UserService(
                userRepository,
                userManagerMock.Object);

            return userService;
        }

        private Mock<UserManager<ApplicationUser>> GetUserManagerMock(ApplicationDbContext context)
        {
            var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
            var userManagerMock = new Mock<UserManager<ApplicationUser>>(
                userStoreMock.Object, null, null, null, null, null, null, null, null);
            userManagerMock
                .Setup(x => x.UpdateAsync(It.IsAny<ApplicationUser>()))
                .Callback((ApplicationUser user) =>
                {
                    context.Update(user);
                })
                .ReturnsAsync(IdentityResult.Success);

            return userManagerMock;
        }
    }
}
