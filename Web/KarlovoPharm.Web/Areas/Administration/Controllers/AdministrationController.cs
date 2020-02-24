namespace KarlovoPharm.Web.Areas.Administration.Controllers
{
    using KarlovoPharm.Common;
    using KarlovoPharm.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
