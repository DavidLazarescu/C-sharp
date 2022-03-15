namespace backend_learning.Extensions
{
    public static class ConfigurationExtensions
    {
        public static WebApplication AdditionalConfiguration(this WebApplication app, IServiceProvider services)
        {
            var loggerFactory = services.GetService<ILoggerFactory>();
            loggerFactory.AddFile(Directory.GetCurrentDirectory() + "/Data/Logs/");

            return app;
        }
    }
}