using Autofac;
using Autofac.Integration.Mvc;
using Owin;
using System.Web.Mvc;
using System.Web.Routing;

namespace Acme.Dressing.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new CoreModule());
            builder.RegisterControllers(typeof(Startup).Assembly);

            var container = builder.Build();

            RouteTable.Routes.MapMvcAttributeRoutes();
            RouteTable.Routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Default", action = "Index", id = UrlParameter.Optional }
            );
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            app.UseAutofacMiddleware(container);
            app.UseAutofacMvc();
        }
    }
}
