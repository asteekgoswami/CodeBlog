using CodeBlog.Data;
using CodeBlog.Repositories.Implementation;
using CodeBlog.Repositories.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<CodeBlogDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("CodeBlogConnectionString"))); 

//if want to use another db  for the identity

/*builder.Services.AddDbContext<AuthDbContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("CodeBlogAuthDbConnectionString")));*/


//using identity
builder.Services.AddIdentity<IdentityUser,IdentityRole>().
    AddEntityFrameworkStores<CodeBlogDbContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});
builder.Services.AddScoped<ITagInterface,TagRepository>();
builder.Services.AddScoped<IBlogPostInterface,BlogPostRepository>();
builder.Services.AddScoped<IImageInterface,CloudinaryImageRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
