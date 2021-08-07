﻿namespace downr;

public class DownrOptions
{
    public static string ConfigurationSectionName = "downr";

    /// <summary>
    /// The title for the blog (used in feed)
    /// </summary>
    /// <value></value>
    public string Title { get; set; }

    /// <summary>
    /// The external root of the blog (e.g. http://example.com/blog)
    /// </summary>
    /// <value></value>
    public string RootUrl { get; set; }

    /// <summary>
    /// PageSize, i.e. the number of items to show per page in a paged list
    /// </summary>
    /// <value></value>
    public int PageSize { get; set; }

    /// <summary>
    /// the name of the main author of the site
    /// </summary>
    /// <value></value>
    public string Author { get; set; }

    /// <summary>
    /// The text that is displayed at the bottom of the site. Useful for disclaimers. 
    /// </summary>
    public string FooterText { get; set; }

    /// <summary>
    /// the text displayed on the index page if the index page isn't edited from the default content
    /// </summary>
    /// <value></value>
    public string IndexPageText { get; set; }

    /// <summary>
    /// During post-file parsing, the image path is repaired, as during editing all image 
    /// paths should simply be marked as "media/{path-to}.png" (or whatever other file format). 
    /// This configuration setting allows you to create your own value. 
    /// 
    /// This is useful in scenarios where you want to serve images not from your local file 
    /// system, but from a CDN. 
    /// 
    /// The default value without being configured is "/posts/{0}/media/".
    /// </summary>
    /// <value></value>
    public string ImagePathFormat { get; set; } = "/posts/{0}/media/";

    /// <summary>
    /// This value is optional. If it is provided in your configuration, the Google tracking 
    /// JavaScript code will be injected into your pages. 
    /// </summary>
    /// <value></value>
    public string GoogleTrackingCode { get; set; }
}
