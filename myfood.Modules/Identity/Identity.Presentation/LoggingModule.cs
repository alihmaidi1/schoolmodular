using Autofac;
using Carter;
using Microsoft.Extensions.Logging;
using ILogger = Serilog.ILogger;

namespace Identity.Presentation;

public class LoggingModule: Module
{


    private readonly ILogger _logger;

    internal LoggingModule(ILogger logger)
    {
        _logger = logger;
    }
    
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterInstance(_logger)
            .As<ILogger>()
            .SingleInstance();
    }

}