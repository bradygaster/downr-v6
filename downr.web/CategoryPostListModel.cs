namespace downr
{
    public class CategoryPostListModel
    {
        public string CategoryName { get; set; }
        public List<Post> Posts { get; set; } = new List<Post>();
    }
}