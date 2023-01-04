using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Radzen;
using Microsoft.EntityFrameworkCore;
using QwTest7.Data;
using Microsoft.AspNetCore.Identity;
using QwTest7.Models;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.ModelBuilder;
using Microsoft.AspNetCore.Components.Authorization;
using Serilog;
using Serilog.Context;

//md Serilog noch ohne Builder und ohne appsettings.json:
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .Enrich.FromLogContext()
    .CreateBootstrapLogger();
Log.Information("Programmstart");
try
{
    var builder = WebApplication.CreateBuilder(args);

    //md Serilog anhand appsettings.json:
    builder.Host.UseSerilog((ctx, lc) => lc
            .Enrich.FromLogContext()
            .ReadFrom.Configuration(ctx.Configuration));
    builder.Services.AddLogging();
    builder.Logging.AddSerilog();

    // Add services to the container.
    builder.Services.AddRazorPages();
    builder.Services.AddServerSideBlazor()
        .AddCircuitOptions(option => { option.DetailedErrors = true; }); //Problem IIS 21.10.22
    builder.Services.AddScoped<DialogService>();
    builder.Services.AddScoped<NotificationService>();
    builder.Services.AddScoped<TooltipService>();
    builder.Services.AddScoped<ContextMenuService>();
    //08.08.22 keine Telemetrie builder.Services.AddApplicationInsightsTelemetry();

    //Studio Quva
    builder.Services.AddScoped<QwTest7.QuvaService>();
    builder.Services.AddDbContext<QwTest7.Data.QuvaContext>();
    //(options => { options.UseOracle(builder.Configuration.GetConnectionString("QuvaConnection"), b => b.UseOracleSQLCompatibility("11")); });

    //Security
    builder.Services.AddHttpClient("QwTest7").AddHeaderPropagation(o => o.Headers.Add("Cookie"));
    builder.Services.AddHeaderPropagation(o => o.Headers.Add("Cookie"));
    builder.Services.AddAuthentication();
    builder.Services.AddAuthorization();
    builder.Services.AddScoped<QwTest7.SecurityService>();
    builder.Services.AddDbContext<ApplicationIdentityDbContext>(options =>
    {
        options.UseOracle(builder.Configuration.GetConnectionString("QuvaConnection"), b => b.UseOracleSQLCompatibility("11"));
    });
    builder.Services.AddIdentity<ApplicationUser, ApplicationRole>().AddEntityFrameworkStores<ApplicationIdentityDbContext>().AddDefaultTokenProviders();
    builder.Services.AddControllers().AddOData(o =>
    {
        var oDataBuilder = new ODataConventionModelBuilder();
        oDataBuilder.EntitySet<ApplicationUser>("ApplicationUsers");
        var usersType = oDataBuilder.StructuralTypes.First(x => x.ClrType == typeof(ApplicationUser));
        usersType.AddProperty(typeof(ApplicationUser).GetProperty(nameof(ApplicationUser.Password)));
        usersType.AddProperty(typeof(ApplicationUser).GetProperty(nameof(ApplicationUser.ConfirmPassword)));
        oDataBuilder.EntitySet<ApplicationRole>("ApplicationRoles");
        o.AddRouteComponents("odata/Identity", oDataBuilder.GetEdmModel()).Count().Filter().OrderBy().Expand().Select().SetMaxTop(null).TimeZone = TimeZoneInfo.Utc;
    });
    builder.Services.AddScoped<AuthenticationStateProvider, QwTest7.ApplicationAuthenticationStateProvider>();

    //Studio Qusy
    builder.Services.AddScoped<QwTest7.QusyService>();
    builder.Services.AddDbContext<QwTest7.Data.QusyContext>(options =>
    {
        options.UseOracle(builder.Configuration.GetConnectionString("QusyConnection"), b => b.UseOracleSQLCompatibility("11"));
    });
    builder.Services.AddDbContext<QwTest7.Data.QusyContext>(options =>
    {
        options.UseOracle(builder.Configuration.GetConnectionString("QusyConnection"));
    });


    var app = builder.Build();

    //md Serilog:
    app.UseSerilogRequestLogging();

    // Configure the HTTP request pipeline.
    app.Use(async (httpContext, next) =>
    {
        //Get username  
        var username = httpContext.User.Identity.IsAuthenticated ? httpContext.User.Identity.Name : "anonymous";
        LogContext.PushProperty("User", username);

        //Get remote IP address  
        var ip = httpContext.Connection.RemoteIpAddress.ToString();
        LogContext.PushProperty("IP", !String.IsNullOrWhiteSpace(ip) ? ip : "unknown");

        await next.Invoke();
    });
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseHeaderPropagation();
    app.UseRouting();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.MapBlazorHub();
    app.MapFallbackToPage("/_Host");
    app.Services.CreateScope().ServiceProvider.GetRequiredService<ApplicationIdentityDbContext>().Database.Migrate();
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Schwerer Fehler");
}
finally
{
    Log.Information("Programmende");
    Log.CloseAndFlush();
}
