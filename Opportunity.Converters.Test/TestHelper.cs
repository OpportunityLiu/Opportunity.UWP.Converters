using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.UI.Core;
using System.Diagnostics;

namespace Opportunity.Converters.Test
{
    [DebuggerStepThrough]
    static class TestHelper
    {
        public static CoreDispatcher Dispatcher { get; } = CoreApplication.MainView.CoreWindow.Dispatcher;

        public static IAsyncAction RunAtUIThread(DispatchedHandler task)
        {
            return Dispatcher.RunAsync(CoreDispatcherPriority.Normal, task);
        }
    }
}
