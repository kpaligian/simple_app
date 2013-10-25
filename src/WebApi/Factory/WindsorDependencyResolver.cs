using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;
using Castle.Windsor;

namespace Simple.Web.Windsor.Owin.Factory
{
    public class WindsorDependencyScope : IDependencyScope
    {
        private IWindsorContainer _container;

        public WindsorDependencyScope(IWindsorContainer container)
        {
            if (container == null) throw new ArgumentNullException("container");
            this._container = container;
        }

        public object GetService(Type serviceType)
        {
            if (_container == null)
                throw new ObjectDisposedException("this", "This scope has already been disposed.");

            if (!_container.Kernel.HasComponent(serviceType))
                return null;

            return _container.Resolve(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            if (_container == null)
                throw new ObjectDisposedException("this", "This scope has already been disposed.");

            if (!_container.Kernel.HasComponent(serviceType))
                return Enumerable.Empty<object>();

            return _container.ResolveAll(serviceType).Cast<object>();
        }

        public void Dispose()
        {
            _container.Dispose();
            _container = null;
        }
    }

    public class WindsorResolver : WindsorDependencyScope, IDependencyResolver
    {
        private readonly IWindsorContainer container;

        public WindsorResolver(IWindsorContainer container)
            : base(container)
        {
            if (container == null)
                throw new ArgumentNullException("container");

            this.container = container;
        }

        public IDependencyScope BeginScope()
        {
            var scope = new WindsorContainer();
            container.AddChildContainer(scope);
            return new WindsorDependencyScope(scope);
        }
    }
}