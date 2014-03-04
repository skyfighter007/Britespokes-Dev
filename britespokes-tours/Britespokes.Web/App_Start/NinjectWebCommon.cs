using System.Web.Http;
using System.Web.Mvc;
using Britespokes.Core;
using Britespokes.Core.Data;
using Britespokes.Data;
using Britespokes.Services.Authentication;
using Britespokes.Web.Infrastructure.Filters;
using Britespokes.Web.Infrastructure.Logging;
using Britespokes.Web.Infrastructure.Uploaders;
using Britespokes.Web.Mailers;
using Ninject.Web.Mvc.FilterBindingSyntax;
using Ninject.Extensions.Conventions;

[assembly: WebActivator.PreApplicationStartMethod(typeof(Britespokes.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(Britespokes.Web.App_Start.NinjectWebCommon), "Stop")]

namespace Britespokes.Web.App_Start
{
  using System;
  using System.Web;

  using Microsoft.Web.Infrastructure.DynamicModuleHelper;

  using Ninject;
  using Ninject.Web.Common;

  public static class NinjectWebCommon
  {
    private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

    /// <summary>
    /// Starts the application
    /// </summary>
    public static void Start()
    {
      DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
      DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
      Bootstrapper.Initialize(CreateKernel);
    }

    /// <summary>
    /// Stops the application.
    /// </summary>
    public static void Stop()
    {
      Bootstrapper.ShutDown();
    }

    /// <summary>
    /// Creates the kernel that will manage your application.
    /// </summary>
    /// <returns>The created kernel.</returns>
    private static IKernel CreateKernel()
    {
      var kernel = new StandardKernel();
      kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
      kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

      RegisterServices(kernel);
      // Install our Ninject-based IDependencyResolver into the Web API config
      GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);
      Container.Initialize(kernel);

      return kernel;
    }

    /// <summary>
    /// Load your modules or register your services here!
    /// </summary>
    /// <param name="kernel">The kernel.</param>
    private static void RegisterServices(IKernel kernel)
    {
      // logging
      kernel.Bind<ILogger>().To<NLogLogger>().InSingletonScope();

      // context
      kernel.Bind<EfDbContext>().To<EfDbContext>().InRequestScope();

      // filters
      kernel.BindFilter<OrganizationAuthorize>(FilterScope.Controller, 0);
      kernel.BindFilter<ConfirmationRequired>(FilterScope.Controller, null);
      kernel.BindFilter<GuestRequired>(FilterScope.Controller, null);
      kernel.BindFilter<LogUserActivityAttribute>(FilterScope.Controller, null);

      // convention based binding
      kernel.Bind(
        x => x.FromAssembliesMatching("Britespokes.Services.dll", "Britespokes.Data.dll", "Britespokes.Web.dll")
          .SelectAllClasses().BindDefaultInterface());

      kernel.Bind(
        x =>
          x.FromAssembliesMatching("Britespokes.Data.dll")
            .SelectAllClasses()
            .InheritedFrom(typeof (IRepository<>))
            .BindAllInterfaces());

      // mailers
      kernel.Bind<OrderNotificationMailerController>().To<OrderNotificationMailerController>()
            .InRequestScope()
            .WithConstructorArgument("orderNotificationEmailAddress", EnvironmentConfig.OrderNotificationEmailAddress);

      kernel.Bind<GiftOrderNotificationMailerController>().To<GiftOrderNotificationMailerController>()
          .InRequestScope()
          .WithConstructorArgument("giftorderNotificationEmailAddress", EnvironmentConfig.GiftOrderNotificationEmailAddress);

      kernel.Bind<StudentInquiryMailerController>().To<StudentInquiryMailerController>()
            .InRequestScope()
            .WithConstructorArgument("studentInquiryNotificationEmailAddress", EnvironmentConfig.StudentInquiryNotificationEmailAddress);

      // services
      kernel.Bind<IAuthenticationService>().To<FormsAuthenticationService>().InRequestScope();

      // uploaders
      kernel.Bind<IImageUploader>().To<AzureImageUploader>().InRequestScope();
    }
  }
}