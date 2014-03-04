using System.Configuration;

namespace Britespokes.Web.App_Start
{
  public static class EnvironmentConfig
  {
    private static readonly string DeployEnv;
    private static readonly string ConnStr;
    private static readonly string OrderNotificationEmail;
    private static readonly string GiftOrderNotificationEmail;
    private static readonly string StudentInquiryNotificationEmail;

    static EnvironmentConfig()
    {
      DeployEnv = ConfigurationManager.AppSettings [ "environment" ];
      ConnStr = ConfigurationManager.ConnectionStrings [ "EFDbContext" ].ConnectionString;
      OrderNotificationEmail = ConfigurationManager.AppSettings["orderNotificationEmailAddress"];
      GiftOrderNotificationEmail = ConfigurationManager.AppSettings["giftorderNotificationEmailAddress"];
      StudentInquiryNotificationEmail = ConfigurationManager.AppSettings [ "studentInquiryNotificationEmailAddress" ];
    }

    public static string DeployEnvironment { get { return DeployEnv; } }

    public static string ConnectionString { get { return ConnStr; } }

    public static string OrderNotificationEmailAddress { get { return OrderNotificationEmail; } }
    public static string GiftOrderNotificationEmailAddress { get { return GiftOrderNotificationEmail; } }

    public static string StudentInquiryNotificationEmailAddress { get { return StudentInquiryNotificationEmail; } }

    public static bool IsDevelopment { get { return DeployEnv == "development"; } }

    public static bool IsProduction { get { return DeployEnv == "production"; } }
  }
}