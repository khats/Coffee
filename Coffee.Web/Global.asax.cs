using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Http;
using System.Web.Http.Dependencies;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Coffee.Controllers;
using Coffee.Controllers.Helpers;
using Coffee.Shared.Configuration;
using Coffee.Web.App_Start;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using StructureMap;

namespace Coffee.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var configurationService = new ConfigurationService(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            ObjectFactory.Initialize(registry =>
                                         {
                                             registry.For<IConfigurationService>().Use(configurationService);
                                             registry.Scan(x =>
                                                               {

                                                                   x.AssembliesFromApplicationBaseDirectory();
                                                                   x.WithDefaultConventions();
                                                               });
                                         });

            ObjectFactory.Container.Inject(typeof(IHttpControllerActivator), new StructureMapHttpControllerActivator(ObjectFactory.Container));
            GlobalConfiguration.Configuration.DependencyResolver = new IoCContainer(ObjectFactory.Container);
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings = new JsonSerializerSettings
                                                                                                {
                                                                                                    ContractResolver =
                                                                                                        new LowercaseContractResolver
                                                                                                        ()
                                                                                                };
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
    public class LowercaseContractResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            return propertyName.ToLower();
        }
    }

    
}