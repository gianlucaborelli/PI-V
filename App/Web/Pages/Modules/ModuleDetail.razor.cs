using Microsoft.AspNetCore.Components;
using MudBlazor;
using MudBlazor.Components.Chart.Models;
using Shared.Models;
using Shared.Models.DTOs;
using System.Runtime.CompilerServices;
using Web.Services;

namespace Web.Pages.Modules
{
    public partial class ModuleDetailPage : ComponentBase
    {
        public bool IsBusy { get; set; } = false;
        public ModuleEspDto Module { get; set; }

        [Parameter]
        public int Id { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        [Inject]
        public IModulesService Service { get; set; } = null!;

        [Inject]
        public ISensorsService SensorService { get; set; } = null!;

        [Inject]
        public IDialogService DialogService { get; set; } = null!;

        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;

        public List<SensorData> SensorDatas { get; set; } = [];
        public DateTime? InputModel { get; set; } = DateTime.UtcNow;
        protected TimeSeriesChartSeries _humidityChart = new();
        protected TimeSeriesChartSeries _temperatureChart = new();

        protected ChartOptions _options = new ChartOptions
        {
            YAxisLines = false,
            YAxisTicks = 5,
            MaxNumYAxisTicks = 10,
            XAxisLines = true,
            LineStrokeWidth = 1
        };

        protected List<TimeSeriesChartSeries> _series = new();

        protected override async Task OnInitializedAsync()
        {
            IsBusy = true;
            try
            {
                var result = await Service.LoadModuleDetailAsync(Id);
                if (result.IsSuccess)
                {
                    Module = result.Data!;
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

        public async void AddNewSensor()
        {
            var parameters = new DialogParameters
            {
                { "ModuleId", Module.Id } 
            };

            var options = new DialogOptions { CloseOnEscapeKey = true };

            var dialog = DialogService.Show<AddNewSensor>("Adicionar novo Sensor", parameters, options);
            var result = await dialog.Result;

            if (!result.Canceled)
            {
                Snackbar.Add("Sensor Adicionado com Sucesso", Severity.Success);
                await LoadDataAsync();
            }
        }

        public async void EditModule()
        {
            var parameters = new DialogParameters
            {
                { "Module", Module }
            };

            var options = new DialogOptions { CloseOnEscapeKey = true };

            var dialog = DialogService.Show<UpdateModuleDialog>("Atualizar módulo", parameters, options);
            var result = await dialog.Result;

            if (!result.Canceled)
            {
                Snackbar.Add("Módulo atualizado com Sucesso", Severity.Success);
                await LoadDataAsync();
            }
        }

        public async void EditSensor(SensorDto sensor)
        {
            var parameters = new DialogParameters
            {
                { "Module", Module }
            };

            var options = new DialogOptions { CloseOnEscapeKey = true };

            var result = await DialogService.ShowAsync<UpdateModuleDialog>("Atualizar módulo", parameters, options);

            if (result.Result.IsCompleted)
            {
                Snackbar.Add("Módulo atualizado com Sucesso", Severity.Success);
                await LoadDataAsync();
            }
        }

        public async void DeleteModule(SensorDto module)
        {
            bool? result = await DialogService.ShowMessageBox(
            "Atenção",
            "Deseja realmente deletar esse sensor?",
            yesText: "Deletar", cancelText: "Cancelar");

            if (result is not null && result == true)
            {
                var requestResult = await Service.DeleteModuleAsync(module.Id);
                if (requestResult.Data)
                {
                    Snackbar.Add("Deletado com Sucesso", Severity.Success);
                }
                else
                {
                    Snackbar.Add(requestResult.Message, Severity.Error);
                }
            }
            StateHasChanged();
        }

        private int selectedRowNumber = -1;
        private SensorDto? selectedSensor;
        public MudDataGrid<SensorDto> mudTable;
        public string SelectedRowClassFunc(SensorDto element, int rowNumber)
        {
            if (selectedRowNumber == rowNumber)
            {
                selectedRowNumber = -1;
                selectedSensor = null;
                return string.Empty;
            }
            else if (mudTable.SelectedItem != null && mudTable.SelectedItem.Equals(element))
            {
                selectedRowNumber = rowNumber;
                selectedSensor = element;
                LoadSensorData();
                return "selected";
            }
            else
            {
                selectedSensor = null;
                return string.Empty;
            }            
        }

        private async Task LoadDataAsync()
        {
            var result = await Service.LoadModuleDetailAsync(Module.Id);

            Module = result.Data;
            StateHasChanged(); 
        }

        protected void LoadChart()
        {
            var humiditySeries = SensorDatas
                    .Select(
                        s => new TimeSeriesChartSeries.TimeValue(
                            s.CreatedAt,
                            s.Humidity))
                    .OrderBy(
                        s => s.DateTime)
                    .ToList();

            var temperatureSeries = SensorDatas
                    .Select(
                        s => new TimeSeriesChartSeries.TimeValue(
                            s.CreatedAt,
                            s.Temperature))
                    .OrderBy(
                        s => s.DateTime)
                    .ToList();

            _series.Clear();

            _humidityChart = new TimeSeriesChartSeries
            {
                Index = 0,
                Name = "Humidade",
                Data = humiditySeries,
                IsVisible = true,
                Type = TimeSeriesDiplayType.Line
                
            };

            _temperatureChart = new TimeSeriesChartSeries
            {
                Index = 1,
                Name = "Temperatura",
                Data = temperatureSeries,
                IsVisible = true,
                Type = TimeSeriesDiplayType.Line
            };

            _series.Add(_humidityChart);
            _series.Add(_temperatureChart);
            StateHasChanged();
        }

        private async void LoadSensorData()
        {
            var result = await SensorService.LoadSensorsDataAsync(InputModel, selectedSensor.Id);
            if (result.IsSuccess)
            {
                SensorDatas = result.Data ?? [];
                LoadChart();
            }
        }

        protected async void OnSelectDate()
        {
            IsBusy = true;
            SensorDatas.Clear();
            try
            {
                if (selectedSensor is not null)
                {
                    LoadSensorData();
                }
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
            finally
            {
                StateHasChanged();
                IsBusy = false;
            }
        }
    }
}
