using Microsoft.AspNetCore.Components;
using MudBlazor;
using MudBlazor.Components.Chart.Models;
using Shared.Models;
using Shared.Models.DTOs;
using System.Reflection;
using Web.Services;

namespace Web.Pages.Sensors
{
    public partial class DashboardPage : ComponentBase
    {
        public DateTime? InputModel { get; set; } = DateTime.UtcNow;
        public ModuleEspDto SelectedModule { get; set; }
        public List<ModuleEspDto> ModulesList{ get; set; }
        public bool IsBusy { get; set; } = false;
        public List<SensorData> Datas { get; set; } = [];        

        protected TimeSeriesChartSeries _chart1 = new();
        protected TimeSeriesChartSeries _chart2 = new();

        protected ChartOptions _options = new ChartOptions
        {
            YAxisLines = false,
            YAxisTicks = 5,
            MaxNumYAxisTicks = 10,
            XAxisLines = true,
            LineStrokeWidth = 1
        };

        protected List<TimeSeriesChartSeries> _series = new();

        [Inject]
        public ISensorsService SensorService { get; set; } = null!;

        [Inject]
        public IModulesService ModulesService { get; set; } =null!;

        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;

        protected override async Task OnInitializedAsync()
        {
            IsBusy = true;
            try
            {
                var result = await ModulesService.LoadModulesAsync();
                if (result.IsSuccess)
                {
                    ModulesList = result.Data ?? [];                    
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

        public async void OnModuleSelected(ModuleEspDto selectedModule)
        {
            SelectedModule = selectedModule;
            var result = await SensorService.LoadSensorsDataByDayAndModuleIdAsync(InputModel, SelectedModule.Id);
            if (result.IsSuccess) {
                Datas = result.Data ?? [];
                LoadChart();
                StateHasChanged();
            }
        }

        protected async void OnSelectDate()
        {
            Snackbar.Add(InputModel.ToString());
            IsBusy = true;
            Datas.Clear();
            try
            {
                var result = await SensorService.LoadSensorsDataByDayAndModuleIdAsync(InputModel, SelectedModule.Id);
                if (result.IsSuccess)
                {
                    Datas = result.Data ?? [];
                    LoadChart();
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

        public double averageHumidity {  get; set; }
        public double averageTemperature { get; set; }

        protected void LoadChart()
        {
            var humiditySeries = Datas
                    .Select(
                        s => new TimeSeriesChartSeries.TimeValue( 
                            s.CreatedAt, 
                            s.Humidity))
                    .OrderBy(
                        s => s.DateTime)
                    .ToList();

            var temperatureSeries = Datas
                    .Select(
                        s => new TimeSeriesChartSeries.TimeValue(
                            s.CreatedAt,
                            s.Temperature))
                    .OrderBy(
                        s => s.DateTime)
                    .ToList();

            averageHumidity = humiditySeries.Average(s => s.Value);
            averageTemperature = temperatureSeries.Average(s => s.Value);

            _series.Clear();

            _chart1 = new TimeSeriesChartSeries
            {
                Index = 0,
                Name = "Temperatura",
                Data = temperatureSeries,
                IsVisible = true,
                Type = TimeSeriesDiplayType.Line
            };

            _chart2 = new TimeSeriesChartSeries
            {
                Index = 1,
                Name = "Humidade",
                Data = humiditySeries,
                IsVisible = true,
                Type = TimeSeriesDiplayType.Line
            };

            _series.Add(_chart1);
            _series.Add(_chart2);            
        }
    }
}
