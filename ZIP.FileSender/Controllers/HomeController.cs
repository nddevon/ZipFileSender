using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Principal;
using ZIP.FileSender.Filters;
using ZIP.FileSender.ViewModel;

namespace ZIP.FileSender.Controllers {
    [ControlPanelExceptionFilter]
    public class HomeController: BaseController {
        public IActionResult Index() {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel zipFile) {

            if (!ModelState.IsValid) {
                return RedirectToAction("Index");
            }
            var userName = "admin";
            var password = "123";

            var isValid = (zipFile.UserName.Equals(userName) && zipFile.Password.Equals(password));
            if (!isValid) {
                ModelState.AddModelError("", "username or password is invalid");
                return RedirectToAction("Index");
            }

            HttpContext.Session.SetString("UserName", userName);
            HttpContext.Session.SetString("Password", password);
            string[] roles = new string[1];
            var user = new GenericPrincipal(new ClaimsIdentity(userName), roles);     
            HttpContext.User = user;

            return RedirectToAction("Index", new { Controller = "ZipFileSend" });
        }
    }
}
