using Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;

[assembly: OwinStartup(typeof(AspNetIdentityDemo.App_Start.OWINStartup))]

namespace AspNetIdentityDemo.App_Start
{
    public class OWINStartup
    {
        public void Configuration(IAppBuilder app)
        {
            CookieAuthenticationOptions options = new CookieAuthenticationOptions();
            options.AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie;
            options.LoginPath = new PathString("/account/login");
            app.UseCookieAuthentication(options);
        }
    }
}