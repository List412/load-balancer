using System.Web.Http;

namespace load_balancer
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.EnableCors();
            
            config.Routes.MapHttpRoute(
                name: "Proxy",
                routeTemplate: "api/{resource}/{id}",
                defaults: new { controller = "ServerController", id = RouteParameter.Optional }
            );
        }
    }
}