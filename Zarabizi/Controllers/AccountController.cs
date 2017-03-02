using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Zarabizi.Models;

namespace Zarabizi.Controllers
{
    public class AccountController : Controller
    {

        //
        // GET: /Account/LogOn
        public ActionResult LogOn()
        {
            return View();
        }

        //
        // POST: /Account/LogOn

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        ////Buscar idUsuario en tabla idSocio ////
                        
                        ZarabiziEntities db = new ZarabiziEntities();
                        Guid keyUser = (Guid)Membership.GetUser(model.UserName).ProviderUserKey;
                        int count = db.Socio.Where(s => s.idUsuario == keyUser).Count();                        

                        if (count == 0)
                        {
                            return RedirectToAction("Create", "Socio");
                        }
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "El nombre o contraseña introducidos son incorrectos.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus;
                Membership.CreateUser(model.UserName, model.Password, model.Email, null, null, true, null, out createStatus);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false /* createPersistentCookie */);
                    //return RedirectToAction("Index", "Home");
                    return RedirectToAction("Create", "Socio");
                }
                else
                {
                    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePassword

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
                    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "La contraseña actual es incorrecta o la nueva contraseña no es válida.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePasswordSuccess

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "El nombre de usuario ya existe. Por favor introduzca un nombre de usuario diferente.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "El nombre de usuario para ésta dirección de correo electrónico ya existe. Por favor introduzca una dirección de correo electrónico diferente.";

                case MembershipCreateStatus.InvalidPassword:
                    return "La contraseña introducida es inválida. Por favor introduzca una contraseña válida.";

                case MembershipCreateStatus.InvalidEmail:
                    return "La dirección de correo electrónico es invláida. Por favor corrijala y vuelava a introducirla.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "La respuesta de recuperación de contraseña proporcionada no es válida. Por favor, compruebe el valor y vuelva a intentarlo.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "La pregunta de recuperación de contraseña proporcionada no es válida. Por favor, compruebe el valor y vuelva a intentarlo.";

                case MembershipCreateStatus.InvalidUserName:
                    return "El nombre de usuario proporcionado no es válido. Por favor, compruebe el valor y vuelva a intentarlo.";

                case MembershipCreateStatus.ProviderError:
                    return "Proveedor de autenticación ha devuelto un error. Por favor, compruebe los datos introducidos y vuelve a intentarlo. Si el problema persiste, póngase en contacto con el administrador del sistema.";

                case MembershipCreateStatus.UserRejected:
                    return "La solicitud de creación de usuario ha sido cancelado. Por favor, compruebe los datos introducidos y vuelve a intentarlo. Si el problema persiste, póngase en contacto con el administrador del sistema.";

                default:
                    return "Ocurrió un error desconocido. Por favor, compruebe los datos introducidos y vuelve a intentarlo. Si el problema persiste, póngase en contacto con el administrador del sistema.";
            }
        }
        #endregion
    }
}
