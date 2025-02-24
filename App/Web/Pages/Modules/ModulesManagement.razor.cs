using Microsoft.AspNetCore.Components;
using MudBlazor;
using Shared.Models;
using Shared.Models.DTOs;
using Web.Services;

namespace Web.Pages.Modules
{
    public partial class ModulesManagementPage : ComponentBase
    {
        public bool IsBusy { get; set; } = false;
        public List<ModuleEspDto> Modules { get; set; } = [];

        [Inject]
        public NavigationManager Navigation{ get; set; }        

        [Inject]
        public IModulesService Service { get; set; } = null!;

        [Inject]
        public IDialogService DialogService {  get; set; } = null!;

        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;

        protected override async Task OnInitializedAsync()
        {
            IsBusy = true;
            try
            {
                var result = await Service.LoadModulesAsync();
                if (result.IsSuccess)
                {
                    Modules = result.Data ?? [];                    
                }
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public Task OpenDialogAsync()
        {
            var options = new DialogOptions { CloseOnEscapeKey = true };

            return DialogService.ShowAsync<CreateModuleDialog>("Simple Dialog", options);
        }

        public void EditModule(ModuleEspDto module)
        {
            Navigation.NavigateTo($"/modules/{module.Id}");
        }

        public async void DeleteModule(ModuleEspDto module)
        {
            bool? result = await DialogService.ShowMessageBox(
            "Atenção",
            "Deseja realmente deletar esse módulo?",
            yesText: "Deletar", cancelText: "Cancelar");

            if (result is not null && result == true)
            {
                var requestResult = await Service.DeleteModuleAsync(module.Id);
                if (requestResult.Data)
                {
                    Modules.Remove(module);
                }
                else
                {
                    Snackbar.Add(requestResult.Message, Severity.Error);
                }
            }
            StateHasChanged();
        }
    }
}
