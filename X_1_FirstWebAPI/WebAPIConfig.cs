using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;

//引入命名空间
using System.Web.Http;
using System.Web.Http.Cors;
using Newtonsoft.Json;

namespace X_1_FirstWebAPI
{
    public class WebAPIConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //注册路由映射
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );

            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter
            {
                SerializerSettings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore
                }
            });

            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));
        }
    }
}