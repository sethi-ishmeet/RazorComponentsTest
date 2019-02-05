using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR;
using RazorComponentsTest.App.Services;

namespace RazorComponentsTest.App.Pages
{
    public class CounterComponent : ComponentBase
    {
        [Inject] protected ISignalService SignalService { get; set; }
        internal string _toEverybody { get; set; }

        internal async Task Broadcast()
        {
            await SignalService.Invoke("Send", _toEverybody);
        }

        internal async Task SendToOthers()
        {
            await SignalService.Invoke("SendToOthers", _toEverybody);
        }

        internal int currentCount = 0;

        internal void IncrementCount()
        {
            currentCount++;
        }
    }
}
