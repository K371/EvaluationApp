using System;
using System.Web.Http.Dependencies;
using Ninject;
using Ninject.Syntax;

namespace API.App_Start
{
	/// <summary>
	/// Provides a Ninject implementation of IDependencyScope
	/// which resolves services using the Ninject container.
	/// </summary>
	public class NinjectDependencyScope : IDependencyScope
	{
		IResolutionRoot _resolver;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="resolver"></param>
		public NinjectDependencyScope(IResolutionRoot resolver)
		{
			_resolver = resolver;
		}

		/// <summary>
		/// GetService - I have absolutely no idea what this does...
		/// </summary>
		/// <param name="serviceType"></param>
		/// <returns></returns>
		public object GetService(Type serviceType)
		{
			if (_resolver == null)
			{
				throw new ObjectDisposedException("this", "This scope has been disposed");
			}

			return _resolver.TryGet(serviceType);
		}

		/// <summary>
		/// GetServices - I have no idea what this does.
		/// </summary>
		/// <param name="serviceType"></param>
		/// <returns></returns>
		public System.Collections.Generic.IEnumerable<object> GetServices(Type serviceType)
		{
			if (_resolver == null)
			{
				throw new ObjectDisposedException("this", "This scope has been disposed");
			}
			return _resolver.GetAll(serviceType);
		}

		/// <summary>
		/// Standard dispose function
		/// </summary>
		public void Dispose()
		{
			var disposable = _resolver as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}

			_resolver = null;
		}
	}

	/// <summary>
	/// This class is the resolver, but it is also the global scope
	/// so we derive from NinjectScope.
	/// </summary>
	public class NinjectDependencyResolver : NinjectDependencyScope, IDependencyResolver
	{
		private readonly IKernel _kernel;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="kernel"></param>
		public NinjectDependencyResolver(IKernel kernel)
			: base(kernel)
		{
			_kernel = kernel;
		}

		/// <summary>
		/// I have no idea what this does.
		/// </summary>
		/// <returns></returns>
		public IDependencyScope BeginScope()
		{
			return new NinjectDependencyScope(_kernel.BeginBlock());
		}
	}
}