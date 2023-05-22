using System.Web.Mvc;

namespace NeoSistem.Trinnk.Management.Models.Authentication
{
    [Authorize(Roles = "Admin")]
    public class AdminBase : Controller
    {

    }
}