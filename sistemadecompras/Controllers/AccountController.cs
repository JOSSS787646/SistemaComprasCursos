using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using sistemadecompras.Encript;
using sistemadecompras.Models;
using sistemadecompras.Models.Domain;
using sistemadecompras.Services.Account;
using sistemadecompras.Services.Infraestructura;
using sistemadecompras.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;



namespace sistemadecompras.Controllers
{
    [RoutePrefix("Account")]
    public class AccountController : Controller
    {
        //private readonly AvatarService avatarServices;
        private readonly IUsuariosServicios userServices = null;



        [HttpPost]
        //[ExceptionLoggin]
        public async Task<JsonResult> Login([Bind(Include = "UserName,Password")] LoginViewModel loginView)
        {
            //var avatarModel = await avatarService.GetRandomAvatarAsync();
            return Json(loginView.UserName.ToUpper(), JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        [Route("security/access/login")]
        public ActionResult Security()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Security([Bind(Include = "UserName ,Password")] LoginViewModel loginview)
        {
            LoginDomainModel login = new LoginDomainModel();
            login.UserName = loginview.UserName;
            login.Password = loginview.Password;
            IUsuariosServicios serviciosIUsuarios = new UsuariosServicios();

            var usuarios = serviciosIUsuarios.Login(login);
            return SigInUser(usuarios, false, "");



        }


        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register([Bind(Include ="NombreUsuario,Password,Gmail")]
        RegisterAccountDm registeraccount)
        {
            RegisterAccountDm accountDm = new RegisterAccountDm
            {
                UserName = registeraccount.UserName,

                Password = Funciones.Encrypt(registeraccount.Password),
            };
            LoginDomainModel loginDm = new LoginDomainModel();
            AutoMapper.Mapper.Map(accountDm, loginDm);
            userServices.Insert(loginDm);
            if (userServices.Insert(loginDm))
            {
                Session.Add("userRegister", accountDm);
                return RedirectToAction("About", "Home");
            }
            return RedirectToAction("Security", "Account");


        }
        public ActionResult PasswordRecovery()
        {
            return View();
        }



        #region Metodo Claims para Seguridad

        private ActionResult SigInUser(LoginDomainModel loginDM, bool rememberMe, string returnUrl)
        {
            ActionResult result;

            if (loginDM == null || string.IsNullOrEmpty(loginDM.UserName))
            {
                // Si loginDM es nulo o el nombre de usuario es nulo o vacío, redirigir a una página de error
                TempData["ErrorMessage"] = "Credenciales incorrectas.";
                return RedirectToAction("Security", "Account");
            }

            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, loginDM.Id.ToString()));

            // Verificar si el nombre de usuario no es nulo o vacío antes de agregarlo como reclamación
            if (!string.IsNullOrWhiteSpace(loginDM.UserName))
            {
                claims.Add(new Claim(ClaimTypes.Email, loginDM.UserName));
                claims.Add(new Claim(ClaimTypes.Name, loginDM.UserName));
            }

            if (loginDM.Roles != null)
            {
                claims.Add(new Claim(ClaimTypes.Role, loginDM.Roles.StrValor));
            }

            var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignIn(new AuthenticationProperties { IsPersistent = rememberMe }, identity);

            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                returnUrl = Url.Action("Index", "Home");
            }

            result = Redirect(returnUrl);
            return result;
        }

        public ActionResult LogOff()
        {
            IAuthenticationManager authenticationManager =
                HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            return RedirectToAction("Index", "Home");
        }


        #endregion
            [HttpPost]
            public ActionResult ExternalLogin(string provider)
            {
           
                HttpContext.GetOwinContext().Authentication.Challenge(new AuthenticationProperties
                {
                    RedirectUri = Url.Action("ExternalLoginCallback", "Account")
                }, provider);

                return new HttpUnauthorizedResult();
            }

            [AllowAnonymous]

            public ActionResult ExternalLoginCallback()
            {
                var loginInfo = AuthenticationManager.GetExternalLoginInfo();
                if (loginInfo == null)
                {
                    return RedirectToAction("Security", "Account");
                }


                var userName = loginInfo.DefaultUserName;


                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, userName),

            new Claim(ClaimTypes.Role, "Administrador"),

        };

                var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

                AuthenticationManager.SignIn(new AuthenticationProperties
                {
                    IsPersistent = false
                }, identity);


                return RedirectToAction("Index", "Home");
            }


            private IAuthenticationManager AuthenticationManager
            {
                get { return HttpContext.GetOwinContext().Authentication; }
            }
        }
    }