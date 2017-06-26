using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using FluentValidation.WebApi;
using Gorgias.Infrastruture.Filters;
using Gorgias.Infrastruture.Handlers;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using Gorgias.Infrastruture.Core;
using System.Web.Http.Cors;

namespace Gorgias
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            //Fluent Custome Classes 
            config.Filters.Add(new ValidateModelStateFilter());
            config.MessageHandlers.Add(new ResponseWrappingHandler());

            //config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            //Web API routes
            config.MapHttpAttributeRoutes();

            //Must be here ;)
            FluentValidationModelValidatorProvider.Configure(config);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                //handler: new ResponseWrappingHandler(),
                //constraints: null,
                defaults: new { id = RouteParameter.Optional }
            );

            //var corsAttr = new EnableCorsAttribute("http://localhost:22551/", "*", "*");
            //config.EnableCors(corsAttr);
            //var enableCorsAttribute = new EnableCorsAttribute("*",
            //                                   "Origin, Content-Type, Accept",
            //                                   "GET, PUT, POST, DELETE, OPTIONS");
            var enableCorsAttribute = new EnableCorsAttribute("*",
                                   "*",
                                   "*");
            config.EnableCors(enableCorsAttribute);
            //config.EnableCors();
            //config.Routes.MapHttpRoute(
            //    name: "DefaultDataApi",
            //    routeTemplate: "api/datatable/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            //var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            //jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            //config.Formatters.Add(new BrowserJsonFormatter());

        }
    }
}
