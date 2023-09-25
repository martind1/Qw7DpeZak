using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using QwTest7.Database.Authentification.Models;
using QwTest7.Portal.Services;
using Radzen;

namespace QwTest7.Portal.Pages.Security;

public partial class AddApplicationRole
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

    protected ApplicationRole role;
    protected string error;
    protected bool errorVisible;

    [Inject]
    protected SecurityService Security { get; set; }

    protected override async Task OnInitializedAsync()
    {
        role = new ApplicationRole();
    }

    protected async Task FormSubmit(ApplicationRole role)
    {
        try
        {
            await Security.CreateRole(role);

            DialogService.Close(null);
        }
        catch (Exception ex)
        {
            errorVisible = true;
            error = ex.Message;
        }
    }

    protected async Task CancelClick()
    {
        DialogService.Close(null);
    }
}