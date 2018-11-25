using ClockRestoration.BusinessLogic.Services;
using ClockRestoration.Infrustructure;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Ninject.Modules;
using System.Web;
using Microsoft.AspNet.Identity;
using ClockRestoration.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using ClockRestoration.Data.Context;
using ClockRestoration.DataAccess.Context;
using System.Web.ModelBinding;
using ClockRestoration.DataAccess.Interfaces;
using ClockRestoration.DataAccess.Repositories;

namespace ClockRestoration.App_Start
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Unbind<ModelValidatorProvider>();

            Bind<IOrderService>().To<OrderService>();

            Bind<IBrandRepository>().To<BrandRepository>();
            Bind<IClockTypeRepository>().To<ClockTypeRepository>();
            Bind<IDeliveryRepository>().To<DeliveryRepository>();
            Bind<IOrderRepository>().To<OrderRepository>();
            Bind<IPaymentRepository>().To<PaymentRepository>();

            Bind<IAuthenticationManager>().ToMethod((context) => System.Web.HttpContext.Current.GetOwinContext().Authentication);
            Bind<IApplicationUserManager>().To<ApplicationUserManager>();
            Bind<IUserStore<ApplicationUser>>().To<ApplicationUserStore>();

            Bind<ApplicationUserManager>()
                .ToMethod(ctx => HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>());


            Bind<IdentityFactoryOptions<ApplicationUserManager>>()
                 .ToMethod(x => new IdentityFactoryOptions<ApplicationUserManager>()
                 {
                     DataProtectionProvider = Startup.DataProtectionProvider
                 });

            Bind<ClockRestorationContext>().ToSelf();

    
        }

        public class ApplicationUserStore : UserStore<ApplicationUser>
        {
            public ApplicationUserStore(IDataContext context)
                : base(context as ClockRestorationContext)
            {
            }
        }
    }
}
