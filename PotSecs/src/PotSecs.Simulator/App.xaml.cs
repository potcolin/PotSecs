using System.Windows;
using PotSecs.Simulator.ViewModels;
using PotSecs.Simulator.Views;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Mvvm;

namespace PotSecs.Simulator
{
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<Services.IMessageService, Services.MessageService>();
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();
            ViewModelLocationProvider.Register<MainWindow, MainWindowViewModel>();
        }
    }
}
