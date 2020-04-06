using System.Web.Mvc;

namespace NeoSistem.MakinaTurkiye.Management.Models.Authentication
{
    [Authorize(Roles = "Admin")]
  public class AdminBase : Controller
  {

  }
}