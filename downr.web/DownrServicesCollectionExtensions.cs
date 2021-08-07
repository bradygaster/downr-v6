using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using downr;
using Microsoft.AspNetCore.ResponseCompression;

namespace Microsoft.Extensions.Hosting
{
    public static class DownrServicesCollectionExtensions
    {
        public static DownrServicesCollectionExtensionsConfigurator AddDownr(this WebApplicationBuilder builder)
        {
            var options = new DownrOptions();
            builder.Configuration.GetSection("downr").Bind(options);
            builder.Services.AddSingleton(options);
            builder.Services.AddSingleton<PostFileParser>();
            builder.Services.AddSingleton<PostService>();
            builder.Services.AddResponseCompression(options =>
            {
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[]
                {
                    "image/svg+xml",
                    "application/font-woff2"
                });
            });

            return new DownrServicesCollectionExtensionsConfigurator(builder);
        }
    }

    public class DownrServicesCollectionExtensionsConfigurator
    {
        internal DownrServicesCollectionExtensionsConfigurator(WebApplicationBuilder builder)
        {
            Services = builder.Services;
            Configuration = builder.Configuration;
        }

        public IServiceCollection Services { get; }
        public IConfiguration Configuration { get; }
    }
}