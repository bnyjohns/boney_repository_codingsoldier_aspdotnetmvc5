namespace CodingSoldier
{
    public class LogConfig
    {
        public static void ConfigureLogs()
        {
            log4net.Config.XmlConfigurator.Configure();
        }
    }
}