using DpeZak.Services.Security;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen;
using Radzen.Blazor;

namespace DpeZak.Portal.Shared
{
    public partial class MainLayout
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

        private bool sidebarExpanded = true;

        [Inject]
        protected SecurityService Security { get; set; }

        public void SidebarToggleClick()
        {
            sidebarExpanded = !sidebarExpanded;
        }

        protected void ProfileMenuClick(RadzenProfileMenuItem args)
        {
            if (args.Value == "Logout")
            {
                Security.Logout();
            }
        }
        #region Test
        public void OnParentClicked(MenuItemEventArgs args)
        {
            //console.Log($"{args.Text} clicked from parent");
        }

        public void OnChildClicked(MenuItemEventArgs args)
        {
            //console.Log($"{args.Text} from child clicked");
        }
        #endregion

    }
}
