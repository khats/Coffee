namespace Coffee.Shared.Configuration
{
    public class ConfigurationService : IConfigurationService
    {
        public ConfigurationService(string connectionString)
        {
            DatabaseConnectionString = connectionString;
        }

        public T GetValue<T>()
        {
            throw new System.NotImplementedException();
        }

        public string DatabaseConnectionString { get; private set; }
    }
}