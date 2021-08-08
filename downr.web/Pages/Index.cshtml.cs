using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace downr.web.Pages;
public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly DownrOptions _options;
    private readonly PostService _postService;

    [BindProperty(SupportsGet = true)]
    public string Slug { get; set; }

    [BindProperty(SupportsGet = true, Name = "pg")]
    public int Page { get; set; } = 0;

    public List<Post> Posts { get; set; } = new List<Post>();

    public IndexModel(ILogger<IndexModel> logger,
                      PostService postService,
                      DownrOptions options)
    {
        _logger = logger;
        _postService = postService;
        _options = options;
    }

    private IEnumerable<Post> GetPostList(string category = null)
    {
        var pageSize = _options.PageSize;
        var posts = _postService.GetPosts(Page * (pageSize + 1), pageSize, category);
        var postCount = _postService.GetNumberOfPosts(category);
        return posts;
    }

    public void OnGet()
    {
        if(string.IsNullOrEmpty(Slug))
        {
            Posts.Clear();
            Posts.AddRange(GetPostList());
        }
        else
        {
            Posts.Clear();
            Posts.Add(_postService.GetPostBySlug(Slug));
        }
    }
}
