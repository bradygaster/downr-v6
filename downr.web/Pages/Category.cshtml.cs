using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace downr.web.Pages;

public class CategoryModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly DownrOptions _options;
    private readonly PostService _postService;

    [BindProperty(SupportsGet = true)]
    public string Category { get; set; }

    public List<Post> Posts { get; set; } = new List<Post>();

    public CategoryModel(ILogger<IndexModel> logger,
                      PostService postService,
                      DownrOptions options)
    {
        _logger = logger;
        _postService = postService;
        _options = options;
    }

    public void OnGet()
    {
        if (!string.IsNullOrEmpty(Category))
        {
            Posts.Clear();
            Posts.AddRange(_postService.GetPosts().Where(_ => _.Categories.Contains(Category)).OrderByDescending(_ => _.PublicationDate));
        }
    }
}
