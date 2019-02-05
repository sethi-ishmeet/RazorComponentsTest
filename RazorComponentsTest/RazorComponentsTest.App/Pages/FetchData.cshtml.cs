using Microsoft.AspNetCore.Components;
using RazorComponentsTest.App.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RazorComponentsTest.App.Pages
{
    public class FetchDataComponent : ComponentBase, IDisposable
    {
        internal List<WeatherForecast> forecasts;
        [Inject] WeatherForecastService ForecastService { get; set; }

        private IDisposable _sendHandle;
        private IDisposable _demoObjectHandle;

        protected override async Task OnInitAsync()
        {
            _sendHandle = SignalService.Register<string>("Send", addItem);
            await SignalService.Connect();

            forecasts = await ForecastService.GetForecastAsync(DateTime.Now);
        }

        void IDisposable.Dispose()
        {
            _sendHandle.Dispose();
            _demoObjectHandle.Dispose();
        }

        public void addItem(string msg)
        {
            forecasts.Add(new WeatherForecast
            {
                Date = DateTime.UtcNow,
                Summary = msg,
                TemperatureC = 76,
                TemperatureF = 23
            });

        }
        [Inject] protected ISignalService SignalService { get; set; }
    }
}
