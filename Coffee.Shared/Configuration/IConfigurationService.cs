namespace Coffee.Shared.Configuration
{
    public interface IConfigurationService
    {
        T GetValue<T>();

        string DatabaseConnectionString { get; }
    }
}