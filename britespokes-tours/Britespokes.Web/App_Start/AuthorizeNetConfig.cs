using System.Configuration;

namespace Britespokes.Web.App_Start
{
  public static class AuthorizeNetConfig
  {
    private static readonly string AuthorizeNetApiLogin;
    private static readonly string AuthorizeNetTransactionKey;
    private static readonly bool AuthorizeNetTestMode;
    private static readonly string AuthorizeNetMd5Hash;

    static AuthorizeNetConfig()
    {
      AuthorizeNetApiLogin = ConfigurationManager.AppSettings["authorizeNetApiLogin"];
      AuthorizeNetTransactionKey = ConfigurationManager.AppSettings["authorizeNetTransactionKey"];
      AuthorizeNetMd5Hash = ConfigurationManager.AppSettings["authorizeNetMd5Hash"];
      AuthorizeNetTestMode = ConfigurationManager.AppSettings["authorizeNetTestMode"] == "true";
    }

    public static string ApiLogin
    {
      get { return AuthorizeNetApiLogin; }
    }

    public static string TransactionKey
    {
      get { return AuthorizeNetTransactionKey; }
    }

    public static bool TestMode
    {
      get { return AuthorizeNetTestMode; }
    }

    public static string Md5HashValue
    {
      get { return AuthorizeNetMd5Hash; }
    }
  }
}