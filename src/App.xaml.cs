using System.Windows;
using zywrite.Core;

namespace zywrite
{
    public partial class App : Application
    {
        private static Mutex? _mutex;

        protected override void OnStartup(StartupEventArgs e)
        {
            AppStartUp();
            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            AppShutdown();
            base.OnExit(e);
        }

        private static void CreateMutex()
        {
            _mutex = new Mutex(true, "zywrite", out var createdNew);

            if (!createdNew)
            {
                _mutex.Dispose();
                Current.Shutdown();
                return;
            }
        }

        private static void DisposeMutex()
        {
            _mutex?.ReleaseMutex();
            _mutex?.Dispose();
        }

        private static void AppStartUp()
        {
            CreateMutex();
        }

        private static void AppShutdown()
        {
            DisposeMutex();
        }
    }
}
