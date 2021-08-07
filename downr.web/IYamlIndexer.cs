namespace downr
{
    public interface IYamlIndexer
    {
        List<Post> Posts { get; set; }
        Task IndexContentFiles();
        Task<Post> ReadPost(StreamReader postFileReader);
    }
}