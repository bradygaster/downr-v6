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
    public IEnumerable<Post> Posts { get; private set; }

    public IndexModel(ILogger<IndexModel> logger,
            PostService postService,
            DownrOptions options)
    {
        _logger = logger;
        _postService = postService;
        _options = options;
    }

    private IEnumerable<Post> GetPostList(int page, string category = null)
    {
        var pageSize = _options.PageSize;
        var posts = _postService.GetPosts(page * pageSize, pageSize, category);
        var postCount = _postService.GetNumberOfPosts(category);
        return posts;
    }

    public void OnGet()
    {
        Posts = GetPostList(0);
    }
}
