using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using downr;

namespace Microsoft.Extensions.Hosting
{
    public static class AzureStorageExtensions
    {
        public static void WithAzureStorage(
            this DownrServicesCollectionExtensionsConfigurator configurator)
        {
            configurator.Services.AddSingleton<IYamlIndexer, AzureStorageYamlIndexer>();
            configurator.Services.Configure<AzureStorageConfiguration>(configurator.Configuration.GetSection("downr.AzureStorage"));
        }

        public static void UseAzureStorage(
            this DownrContentProviderConfigurator configurator)
        {
            IYamlIndexer yamlIndexer = (IYamlIndexer)
                configurator.Builder.ApplicationServices.GetService(typeof(IYamlIndexer));

            IOptions<AzureStorageConfiguration> config = (IOptions<AzureStorageConfiguration>)
                configurator.Builder.ApplicationServices.GetService(typeof(IOptions<AzureStorageConfiguration>));
            
            yamlIndexer.IndexContentFiles();
        }
    }
}