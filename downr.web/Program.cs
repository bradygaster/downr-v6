using System.Xml;
using System;
using downr;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

// add downr's services
builder.AddDownr().WithWebServerFileSystemStorage();

var app = builder.Build();

// start using downr
app.UseDownr().UseWebServerFileSystemStorage();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapGet("/rss", (DownrOptions options, PostService postService) =>
{
    var posts = postService.GetPosts(10);
    StringWriter parent = new StringWriter();
    using (XmlWriter writer = XmlWriter.Create(parent, new XmlWriterSettings
    {
        OmitXmlDeclaration = true
    }))
    {
        writer.WriteStartElement("rss");
        writer.WriteAttributeString("version", "2.0");

        // write out 
        writer.WriteStartElement("channel");

        var rootUri = new Uri(options.RootUrl);

        // write out -level elements
        writer.WriteElementString("title", options.Title);
        writer.WriteElementString("link", rootUri.ToString());
        writer.WriteElementString("ttl", "60");

        if (posts != null)
        {
            foreach (var article in posts)
            {
                writer.WriteStartElement("item");

                writer.WriteElementString("title", article.Title);
                ///writer.WriteElementString("link", new Uri(rootUri, relativeUrl).ToString());
                writer.WriteElementString("description", article.Description);

                writer.WriteEndElement();
            }
        }

        // write out 
        writer.WriteEndElement();

        // write out 
        writer.WriteEndElement();
    }

    return parent.ToString();
})
.WithName("rss")
.Produces(200, contentType: "application/xml");

app.Run();
