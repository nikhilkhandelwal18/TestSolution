using Resolver;
using System;
using System.ComponentModel.Composition;

namespace BusinessServices
{
    [Export(typeof(IComponent))]
    class DependencyResolver : IComponent
    {
        public void SetUp(IRegisterComponent registerComponent)
        {
            registerComponent.RegisterType<IProductServices, ProductServices>();
            registerComponent.RegisterType<IUserServices, UserServices>();
            registerComponent.RegisterType<ITokenServices, TokenServices>();
        }
    }
}
