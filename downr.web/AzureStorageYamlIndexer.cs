using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Options;

namespace downr
{

    public class AzureStorageConfiguration {
        public string ConnectionString { get; set; }
        public string Container { get; set; }
    }

    public class AzureStorageYamlIndexer : IYamlIndexer {
        private readonly ILogger<AzureStorageYamlIndexer> logger;
        private readonly AzureStorageConfiguration config; 
        public List<Post> Posts { get; set; } = new List<Post>();
        private readonly PostFileParser postFileParser;

        public AzureStorageYamlIndexer (ILogger<AzureStorageYamlIndexer> logger,
            IOptions<AzureStorageConfiguration> config,
            PostFileParser postFileParser) 
        {
            this.postFileParser = postFileParser;
            this.config = config.Value;
            this.logger = logger;
        }

        public async Task IndexContentFiles () 
        {
            BlobContainerClient container = 
                new BlobContainerClient (config.ConnectionString, config.Container);

            await container.CreateIfNotExistsAsync();

            await foreach (BlobItem blobItem in container.GetBlobsAsync())
            {
                if(blobItem.Name.EndsWith("index.md"))
                {
                    logger.LogInformation($"Indexing {blobItem.Name}");
                    var blobClient = new BlobClient(config.ConnectionString, config.Container, blobItem.Name);
                    var reader = new StreamReader(blobClient.Download().Value.Content);
                    var post = ReadPost(reader).Result;
                    Posts.Add(post);
                }
            }

            Posts = Posts.OrderByDescending(x => x.PublicationDate).ToList();
        }

        public Task<Post> ReadPost (StreamReader postFileReader) 
        {
            var post = postFileParser.CreatePostFromReader(postFileReader);
            logger.LogInformation($"Indexed post {post.Title}");
            return Task.FromResult<Post>(post);
        }
    }
}