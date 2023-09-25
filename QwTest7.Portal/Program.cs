using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;
using QwTest7.Data;
using QwTest7.Database.Authentification.Models;
using QwTest7.Database.Models;
using QwTest7.Portal.Services;
using QwTest7.Portal.Services.Kmp;
using Radzen;
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
    builder.Services.AddRadzenComponents();
    builder.Services.AddDbContext<QuvaContext>();

    //MD Kmp
    //so nicht - builder.Services.AddScoped<IGlobalService>(a => new GlobalService("QUVA"));
    builder.Services.AddScoped<KmpDbService>();
    builder.Services.AddScoped<IniDbService>();
    builder.Services.AddScoped<GlobalService>();
    builder.Services.AddScoped<ProtService>();

    //Blacki Quva
    builder.Services.AddScoped<QuvaDbService>();

    //Security
    builder.Services.AddHttpClient("QwTest7").AddHeaderPropagation(o => o.Headers.Add("Cookie"));
    builder.Services.AddHeaderPropagation(o => o.Headers.Add("Cookie"));
    builder.Services.AddAuthentication();
    builder.Services.AddAuthorization();
    builder.Services.AddScoped<SecurityService>();
    builder.Services.AddDbContext<ApplicationIdentityDbContext>();
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
    builder.Services.AddScoped<AuthenticationStateProvider, ApplicationAuthenticationStateProvider>();

    //Studio Qusy -> Kmp

    var app = builder.Build();

    //md Serilog:
    app.UseSerilogRequestLogging();
    Log.Information("### Programmstart");

    // Configure the HTTP request pipeline.
    app.Use(async (httpContext, next) =>
    {
        //Get username  
        var username = httpContext.User.Identity.IsAuthenticated ? httpContext.User.Identity.Name : "anonymous";
        LogContext.PushProperty("User", username);

        //Get remote IP address  
        var ip = httpContext.Connection.RemoteIpAddress.ToString();
        ip = !string.IsNullOrWhiteSpace(ip) ? ip : "unknown";
        LogContext.PushProperty("IP", ip);

        LogContext.PushProperty("Maschine", GlobalService.GetMachineNameFromIPAddress(ip));

        await next.Invoke();
    });
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseWebSockets();
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
    Log.Information("### Programmende");
    Log.CloseAndFlush();
}
