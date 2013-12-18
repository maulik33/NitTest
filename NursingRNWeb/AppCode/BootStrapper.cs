using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Configuration;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using NursingLibrary.Common;
using NursingLibrary.DAO;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters.Controller;
using NursingLibrary.Presenters.Navigation;
using NursingLibrary.Presenters.StateManagement;
using NursingLibrary.Services;
using NursingLibrary.Utilities;

namespace NursingRNWeb
{
    public class BootStrapper
    {
        public static void Boot()
        {
            BuildContainer();
            KTPApp.Initialize();
            AssignStartupParams();
        }

        private static void AssignStartupParams()
        {
            PresentationHelper.AuthorizationRulesCallBackDelegate = DIHelper.GetAuthorizationRules;
            Global.IsProductionApp = KTPApp.IsProductionApp;
            KTPApp.RefreshAppSettings(DIHelper.GetServiceHelperInstance());
        }

        private static void BuildContainer()
        {
            IUnityContainer container = new UnityContainer();

            var adminPresenters = KTPApp.GetAdminPresenterTypes();

            var presenters = Assembly.GetAssembly(typeof(IDataContext)).GetTypes().Where(
                  t => t.Name.IndexOf("Presenter") != -1).Except(adminPresenters).ToList();

            var matchingInfos = new List<MatchingInfo>();
            matchingInfos.AddRange(presenters.Select(t => new MatchingInfo(t.Name)));
            matchingInfos.AddRange(new[]
                                       {
                                           new MatchingInfo(typeof(IDataContext).Name),
                                           new MatchingInfo(typeof(IStudentRepository).Name),
                                           new MatchingInfo(typeof(IQuestionRepository).Name),
                                           new MatchingInfo(typeof(ITestRepository).Name),
                                           new MatchingInfo(typeof(IAdminRepository).Name),
                                           new MatchingInfo(typeof(ICMSRepository).Name),
                                           new MatchingInfo(typeof(IReportRepository).Name),
                                           new MatchingInfo(typeof(IStudentService).Name),
                                           new MatchingInfo(typeof(IStudentAppController).Name),
                                           new MatchingInfo(typeof(IUnitOfWork).Name)
                                       });
            container.AddNewExtension<Interception>().Configure<Interception>()
                .AddPolicy("Logging policy")
                .AddMatchingRule(new TypeMatchingRule(matchingInfos.ToArray()))
                .AddCallHandler(typeof(LoggingHandler), new HttpRequestLifetimeManager(), new InjectionConstructor())
                .Container
                .RegisterType<ICacheManagement, CacheManagement>(new ContainerControlledLifetimeManager())
                .RegisterType<IDataContext, DataContext>(new HttpRequestLifetimeManager(),
                                                         new InjectionConstructor(
                                                             WebConfigurationManager.ConnectionStrings["Nursing"]
                                                                 .ConnectionString),
                                                         new Interceptor<InterfaceInterceptor>(),
                                                         new InterceptionBehavior<PolicyInjectionBehavior>())
                .RegisterType<IDataContext, DataContext>("LiveAppDataContext", new HttpRequestLifetimeManager(),
                                                         new InjectionConstructor(
                                                             WebConfigurationManager.ConnectionStrings["NursingLive"]
                                                                 .ConnectionString),
                                                         new Interceptor<InterfaceInterceptor>(),
                                                         new InterceptionBehavior<PolicyInjectionBehavior>())
                 .RegisterType<IDataContext, DataContext>("ReportDataContext", new HttpRequestLifetimeManager(),
                                                         new InjectionConstructor(
                                                             WebConfigurationManager.ConnectionStrings["NursingReport"]
                                                                 .ConnectionString),
                                                         new Interceptor<InterfaceInterceptor>(),
                                                         new InterceptionBehavior<PolicyInjectionBehavior>())
                .RegisterType<IStudentRepository, StudentRepository>(new HttpRequestLifetimeManager(),
                                                                     new Interceptor<InterfaceInterceptor>(),
                                                                     new InterceptionBehavior<PolicyInjectionBehavior>())
                .RegisterType<IQuestionRepository, QuestionRepository>(new HttpRequestLifetimeManager(),
                                                                       new Interceptor<InterfaceInterceptor>(),
                                                                       new InterceptionBehavior<PolicyInjectionBehavior>())
                .RegisterType<ICMSRepository, CMSRepository>(new HttpRequestLifetimeManager(),
                        new InjectionConstructor(new ResolvedParameter<IDataContext>(),
                            new ResolvedParameter<IDataContext>("LiveAppDataContext")),
                                                                       new Interceptor<InterfaceInterceptor>(),
                                                                       new InterceptionBehavior<PolicyInjectionBehavior>())

                 .RegisterType<IReportRepository, ReportRepository>(new HttpRequestLifetimeManager(),
                                          new InjectionConstructor(new ResolvedParameter<IDataContext>(),
                                                   new ResolvedParameter<IDataContext>("ReportDataContext")),
                                                                       new Interceptor<InterfaceInterceptor>(),
                                                                       new InterceptionBehavior<PolicyInjectionBehavior>())
                .RegisterType<IUnitOfWork, UnitOfWork>("LiveProdUnitOfWork", new HttpRequestLifetimeManager(),
                            new InjectionConstructor(new ResolvedParameter<IDataContext>("LiveAppDataContext")),
                                                       new Interceptor<InterfaceInterceptor>(),
                                                       new InterceptionBehavior<PolicyInjectionBehavior>())
                .RegisterType<IStudentService, StudentService>(new HttpRequestLifetimeManager(),
                                                               new Interceptor<InterfaceInterceptor>(),
                                                               new InterceptionBehavior<PolicyInjectionBehavior>())
                .RegisterType<IAdminService, AdminService>(new HttpRequestLifetimeManager(),
                                                               new Interceptor<InterfaceInterceptor>(),
                                                               new InterceptionBehavior<PolicyInjectionBehavior>(),
                                                               new InjectionProperty("CacheManager"))
                .RegisterType<ICMSService, CMSService>(new HttpRequestLifetimeManager(),
                        new InjectionConstructor(new ResolvedParameter<ICMSRepository>(),
                            new ResolvedParameter<IUnitOfWork>(),
                            new ResolvedParameter<IAdminService>(),
                            new ResolvedParameter<IReportDataService>(),
                            new ResolvedParameter<IUnitOfWork>("LiveProdUnitOfWork")),
                        new Interceptor<InterfaceInterceptor>(),
                        new InterceptionBehavior<PolicyInjectionBehavior>(),
                        new InjectionProperty("CacheManager"))
                .RegisterType<IReportDataService, ReportDataService>(new HttpRequestLifetimeManager(),
                                                               new Interceptor<InterfaceInterceptor>(),
                                                               new InterceptionBehavior<PolicyInjectionBehavior>(),
                                                               new InjectionProperty("CacheManager"))
                .RegisterType<IStudentAppController, StudentAppController>(new HttpRequestLifetimeManager(),
                                                                           new Interceptor<InterfaceInterceptor>(),
                                                                           new InterceptionBehavior<PolicyInjectionBehavior>())
                .RegisterType<IPageNavigator, PageNavigator>(new HttpRequestLifetimeManager())
                .RegisterType<ICookieManagement, CookieManagement>(new HttpRequestLifetimeManager())
                .RegisterType<ISessionManagement, SessionManagement>(new HttpRequestLifetimeManager())
                .RegisterType<ITestRepository, TestRepository>(new HttpRequestLifetimeManager(),
                                                               new Interceptor<InterfaceInterceptor>(),
                                                               new InterceptionBehavior<PolicyInjectionBehavior>())
                .RegisterType<IAdminRepository, AdminRepository>(new HttpRequestLifetimeManager(),
                                                               new Interceptor<InterfaceInterceptor>(),
                                                               new InterceptionBehavior<PolicyInjectionBehavior>())
                .RegisterType<IUnitOfWork, UnitOfWork>(new HttpRequestLifetimeManager(),
                                                       new Interceptor<InterfaceInterceptor>(),
                                                       new InterceptionBehavior<PolicyInjectionBehavior>());

            presenters.ForEach(
                pr =>
                container.RegisterType(pr, new HttpRequestLifetimeManager(), new Interceptor<VirtualMethodInterceptor>(),
                                                            new InterceptionBehavior<PolicyInjectionBehavior>()));

            adminPresenters.ToList().ForEach(
                pr =>
                container.RegisterType(pr, new HttpRequestLifetimeManager(), new Interceptor<VirtualMethodInterceptor>(),
                        new InterceptionBehavior<PolicyInjectionBehavior>(),
                        new InjectionProperty("CacheManager"),
                        new InjectionProperty("SessionManager"),
                        new InjectionProperty("Navigator")));

            Global.Container = container;
        }
    }
}