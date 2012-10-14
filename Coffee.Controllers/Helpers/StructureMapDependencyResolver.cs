using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using StructureMap;

namespace Coffee.Controllers.Helpers
{
    public class StructureMapScopeContainer : IDependencyScope
    {
        protected readonly IContainer Container;

        public StructureMapScopeContainer(IContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }
            
            Container = container;
        }

        public void Dispose()
        {
            Container.Dispose();
        }

        public object GetService(Type serviceType)
        {
            return Container.TryGetInstance(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return Container.GetAllInstances(serviceType).Cast<object>();
        }
    }

    public class IoCContainer : StructureMapScopeContainer, IDependencyResolver
    {
        public IoCContainer(IContainer container) : base(container)
        {
        }

        public IDependencyScope BeginScope()
        {
            return new StructureMapScopeContainer(ObjectFactory.Container);
        }
    }
}