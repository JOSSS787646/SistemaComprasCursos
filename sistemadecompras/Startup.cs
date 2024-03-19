using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using Owin;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(sistemadecompras.Startup))]

namespace sistemadecompras
{
    public class Startup
    {

        public void Configuration(IAppBuilder app)
        {
            // Configuración de autenticación de cookies
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Security")
            });

            // Configuración de autenticación externa
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Configuración de autenticación de Google
            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions
            {
                ClientId = "754464376797-gc86147p65inuf93sgnqa1u0v49f94j6.apps.googleusercontent.com",
                ClientSecret = "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"
            });

            // Configuración de autenticación de Facebook
            app.UseFacebookAuthentication(new FacebookAuthenticationOptions
            {
                AppId = "1575094226583057",
                AppSecret = "8926fe7902bacc549211c65ac9c704d2",
            });
        }

    }


}
    


