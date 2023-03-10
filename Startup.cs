using Baaijte.Optimizely.ImageSharp.Web;
using EPiServer.Cms.Shell;
using EPiServer.Cms.UI.AspNetIdentity;
using EPiServer.Scheduler;
using EPiServer.ServiceLocation;
using EPiServer.Web.Routing;
using EpiSvelte.Extensions;
using Microsoft.AspNetCore.Mvc.Razor;

namespace EpiSvelte;

public class Startup
{
    private readonly IWebHostEnvironment _webHostingEnvironment;

    public Startup(IWebHostEnvironment webHostingEnvironment)
    {
        _webHostingEnvironment = webHostingEnvironment;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        if (_webHostingEnvironment.IsDevelopment())
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(_webHostingEnvironment.ContentRootPath, "App_Data"));

            services.Configure<SchedulerOptions>(options => options.Enabled = false);
        }

        services.AddRazorPages().WithRazorPagesRoot("/Features/Episerver").AddRazorOptions(options =>
        {
            options.ViewLocationExpanders.Add(new ViewLocationExpander());
        });

        /*services.Configure<RazorViewEngineOptions>(options =>
        {
            options.PageViewLocationFormats.Add("/Views/Shared/Partials/{0}" + RazorViewEngine.ViewExtension);
        });*/

        services.AddBaaijteOptimizelyImageSharp();

        services
            .AddCmsAspNetIdentity<ApplicationUser>()
            .AddCms()
            .AddAdminUserRegistration()
            .AddEmbeddedLocalization<Startup>();

        services.AddContentDeliveryApi();

    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseBaaijteOptimizelyImageSharp();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapContent();
        });
    }
}
