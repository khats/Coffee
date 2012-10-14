using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using StructureMap;

namespace Coffee.Controllers.Helpers
{
    public class StructureMapHttpControllerActivator : System.Web.Http.Dispatcher.IHttpControllerActivator
    {
        private readonly IContainer _container;

         public StructureMapHttpControllerActivator(IContainer container)
        {
            _container = container;
        }

        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            return (IHttpController)_container.GetInstance(controllerType);
        }
    }
}