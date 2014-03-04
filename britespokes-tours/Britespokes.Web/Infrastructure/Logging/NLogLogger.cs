using NLog;

namespace Britespokes.Web.Infrastructure.Logging
{
  public class NLogLogger : ILogger
  {
    private readonly Logger _logger;

    public NLogLogger()
    {
      _logger = LogManager.GetCurrentClassLogger();
    }

    public void Trace(string format, params object[] args)
    {
      _logger.Trace(format, args);
    }

    public void Debug(string format, params object[] args)
    {
      _logger.Debug(format, args);
    }

    public void Info(string format, params object[] args)
    {
      _logger.Info(format, args);
    }

    public void Warn(string format, params object[] args)
    {
      _logger.Warn(format, args);
    }

    public void Error(string format, params object[] args)
    {
      _logger.Error(format, args);
    }
  }
}