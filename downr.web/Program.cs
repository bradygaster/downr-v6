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

app.Run();
