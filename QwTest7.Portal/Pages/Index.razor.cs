using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Http;
using Microsoft.JSInterop;
using QwTest7.Portal.Services;
using QwTest7.Portal.Services.Kmp;
using Radzen;
using Radzen.Blazor;

namespace QwTest7.Portal.Pages
{
    public partial class Index
    {
        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected TooltipService TooltipService { get; set; }

        [Inject]
        protected ContextMenuService ContextMenuService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        [Inject]
        protected SecurityService Security { get; set; }

        [Inject]
        protected GlobalService Gnav { get; set; }

        [Inject]
        protected IHttpContextAccessor httpContextAccessor { get; set; }


        protected override void OnInitialized()
        {
            Gnav.UserAgent = httpContextAccessor.HttpContext.Request.Headers["User-Agent"];
            Gnav.IPAddress = httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            Gnav.UserName = Security?.User?.Name ?? "anonymous";
        }
    }
}