using AutoMapper;

namespace Britespokes.Web.App_Start.AutoMapper
{
  public class AutoMapperConfiguration
  {
    public static void Configure()
    {
      Mapper.Initialize(cfg =>
        {
          cfg.AddProfile<ViewModelProfile>();
          cfg.AddProfile<EditModelProfile>();
          cfg.AddProfile<ApiModelProfile>();
        });
    }
  }
}