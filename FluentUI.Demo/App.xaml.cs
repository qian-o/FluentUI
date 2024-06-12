using System.Windows;
using FluentUI.Demo.ViewModels;
using FluentUI.Demo.Views;
using FluentUI.Design;
using Microsoft.Extensions.DependencyInjection;

namespace FluentUI.Demo
{
    public partial class App : Application
    {
        public static new MainWindow MainWindow { get; } = new MainWindow();

        protected override void OnStartup(StartupEventArgs e)
        {
            //Core.Initialize(services =>
            //{
            //    services.AddSingleton<ShellPage>();
            //    services.AddSingleton<ShellViewModel>();

            //    services.AddSingleton<Page1>();
            //    services.AddSingleton<Page1ViewModel>();

            //    services.AddSingleton<Page2>();
            //    services.AddSingleton<Page2ViewModel>();

            //    services.AddSingleton<SettingPage>();
            //    services.AddSingleton<SettingViewModel>();
            //});

            //MainWindow.FrameContent.PageContent = Core.GetService<ShellPage>();
            //MainWindow.Show();
        }
    }
}
