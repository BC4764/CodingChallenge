using ContactManager.Data;
using ContactManager.Hubs;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;
using ElmahCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ContractResolver = new DefaultContractResolver();
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    });

builder.Services.AddDbContext<ApplicationContext>(options =>
                options
                    .UseSqlServer(builder.Configuration.GetConnectionString("ContactDb"),
                        opts => opts.CommandTimeout(600)));

builder.Services.AddSignalR();

// Add ELMAH services and configuration
builder.Services.AddElmah(options =>
{
    options.ConnectionString = builder.Configuration.GetConnectionString("Elmah");
    options.Path = builder.Configuration["Elmah:Path"];
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
    dataContext.Database.Migrate();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseElmah();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHub<ContactHub>("/contacthub");

app.Run();
