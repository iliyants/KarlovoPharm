namespace KarlovoPharm.Common
{
    public class ValidationMessages
    {
        public const string RequiredFieldErrorMessage = "Това поле е задължително.";

        public const string CategoryLenghtErrorMessage = "Името на категорията трябва да бъде между 3 и 30 символа.";

        public const string SubCategoryLenghtErrorMessage = "Името на подкатегорията трябва да бъде между 3 и 30 символа.";

        public const string CategoryUniqieNameErrorMessage = "Вече съществува категория с това име!";

        public const string SubCategoryUniqieNameErrorMessage = "Вече съществува подкатегория с това име!";

        public const string CategoryCannotBeDeletedErrorMessage = "Категорията не може да бъде изтрита!";

        public const string SubCategoryCannotBeDeletedErrorMessage = "Подкатегорията не може да бъде изтрита!";

        public const string ProductNameLenghtErrorMessage = "Името на продукта трябва да бъде между 3 и 100 исмвола.";

        public const string ProductDeleteErrorMEssage = "Възникна грешка ! Продуктът не може да бъде изтрит!";

        public const string RequiredAllFieldsErrorMessage = "Моля попълнете всички полета обозначени с този знак *";


        public const string ProductSpecificationLengthErrorMessage = "Спецификацията на продукта трябва да бъде между 10 и 500 символа";

        public const string ProductDescriptionLengthErrorMessage = "Описанието на продукта трябва да бъде между 10 и 500 символа";

        public const string ProductPriceNegativeErrorMessage = "Цената не може да бъде негативно число или нула";


        public const string ProductNameNotUniqueErrorMessage = "Вече съществува продукт с това име!";

        public const string ProductDesignationLengthErrorMessage = "Предназначението на продукта трябва да е между 10 и 500 символа.";

        public const string ProductEffectLengthErrorMessage = "Действието на продукта трябва да е между 10 и 500 символа.";

        public const string ProductWayOfUseLengthErrorMessage = "Начина на употреба на продукта трябва да е между 10 и 500 символа.";

        public const string ProductManufacturerLengthErrorMessage = "Производителя на продукта трябва да е между 3 и 50 символа.";

        public const string ProductCountryOfOriginLengthErrorMessage = "Страната на произхода на продукта трябва да е между 3 и 50 символа.";



        public const string UsernameLengthErrorMessage = "Потребителското име трябва да съдържа между 3 и 10 символа.";

        public const string UsernameNotUniqueErrorMessage = "Моля изберете друго потребителско име !";

        public const string NameLenghtErrorMessage = "Името трябва да е между 3 и 30 символа";

        public const string LastNameLenghtErrorMessage = "Фамилията трябва да е между 3 и 30 символа";

        public const string EmailValidationErrorMessage = "Моля въведете валиден имейл адрес.";

        public const string PasswordValidationErrorMessage = "Паролата трябва да бъде между 5 и 15 символа.";

        public const string PasswordAndConfirmPasswordDoNotMatchErrorMessage = "Паролите трябва да съвпадат!";

        public const string TownLengthErrorMessage = "Паролите трябва да съвпадат!";

        public const string UserNameExistsErrorMessage = "Вече съществува потребител с това потребителско име !";

        public const string EmailExistsErrorMessage = "Вече съществува потребител с този имейл адрес !";


        public const string CityLengthErrorMessage = "Градът трябва да е между 2 и 20 символа !";

        public const string StreetLengthErrorMessage = "Улицата трябва да е между 5 и 40 символа !";

        public const string BuildingNumberLengthErrorMessage = "Номерът на сградата трябва да е между 1 и 10 символа !";

        public const string PostCodeLengthErrorMessage = "Пощенският код трябва да е между 2 и 10 символа !";

        public const string DescriptionLengthErrorMessage = "Описанието трябва да е между 10 и 500 символа !";

        public const string PhoneNumberLengthErrorMessage = "Моля въдевете валиден телефонен номер !";

    }
}
