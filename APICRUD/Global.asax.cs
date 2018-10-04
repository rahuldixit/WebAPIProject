using APICRUD.Models;
using APICRUD.Models.DAL;
using Autofac;
using Autofac.Integration.WebApi;
using System.Data.Entity;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;

namespace APICRUD
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {   
            AreaRegistration.RegisterAllAreas();
            RegisterDependencyInjector(GlobalConfiguration.Configuration);

            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // do seeding here

            Database.SetInitializer<UserContext>(new UserDBInitializer());

            //var AdoEmployeeService = container.Resolve<IRepository<Employee>>();
            // when you do this then the repo should be injected into controllers
        }

        public static void RegisterDependencyInjector(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();

            // register dependencies here
            // two options:
            // 1. use the same project and user autofac
            // 2. create a new .net core web api and use .net built in dependency injector
            //registering interface with class that implemented            
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<EFRepository>()
                   .As<IRepository<User>>()
                   .InstancePerRequest();

            builder.RegisterType<UserContext>()
                   .AsSelf()
                   .InstancePerRequest();

            //Resolving interface with autofac
            var container = builder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);            
        }
    }
}
