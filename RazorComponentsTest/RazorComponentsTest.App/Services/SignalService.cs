using BlazorSignalR;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RazorComponentsTest.App.Services
{
    public interface ISignalService
    {
        Task Connect();
        IDisposable Register<T>(string name, Action<T> callback);
        Task Invoke<T>(string name, T payload);
        Task Invoke<S, T>(string name, S arg1, T arg2);

    }
    internal sealed class SignalService : ISignalService
    {

        private HubConnection _connection;

        public SignalService()
        {
                _connection = new HubConnectionBuilder().WithUrlBlazor("/signalhub", options: opt =>
            {
                opt.Transports = HttpTransportType.WebSockets | HttpTransportType.ServerSentEvents | HttpTransportType.LongPolling;
                opt.AccessTokenProvider = async () =>
                {
                    return "ishmeet";
                };
            }).Build();
            
            _connection.Closed += exception =>
            {
                _connection.StartAsync();
                return Task.CompletedTask;
            };
        }

        IDisposable ISignalService.Register<T>(string name, Action<T> handler)
        {
            return _connection.On(name, handler);
        }

        async Task ISignalService.Connect()
        {
            await _connection.StartAsync();
        }

        async Task ISignalService.Invoke<T>(string name, T payload)
        {
            await _connection.InvokeAsync(name, payload);
        }

        async Task ISignalService.Invoke<S, T>(string name, S arg1, T arg2)
        {
            await _connection.InvokeAsync(name, arg1, arg2);
        }
    }
}
