using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using FluentUI.Design.Enums;
using FluentUI.Design.Models;
using FluentUI.Design.Tools;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Win32;

namespace FluentUI.Design
{
    public static class Core
    {
        private static ElementTheme requestedTheme;

        internal static FluentThemeResource FluentThemeResource { get; set; }

        public static ElementTheme RequestedTheme
        {
            get => requestedTheme;
            set
            {
                if (requestedTheme != value)
                {
                    requestedTheme = value;

                    RefreshTheme();
                }
            }
        }

        public static bool IsInitialize { get; private set; }

        public static IHost Host { get; private set; }

        public static void Initialize(Action<IServiceCollection> configureDelegate)
        {
            if (!IsInitialize)
            {
                FluentThemeResource = Application.Current.Resources.MergedDictionaries.FirstOrDefault(item => item.GetType() == typeof(FluentThemeResource)) as FluentThemeResource;

                SystemEvents_UserPreferenceChanged(null, null);
                if (Environment.OSVersion.Version.Build > 17763)
                {
                    SystemEvents.UserPreferenceChanged += SystemEvents_UserPreferenceChanged;
                }

                Host = Microsoft.Extensions.Hosting.Host.
                    CreateDefaultBuilder().
                    ConfigureServices(configureDelegate).
                    Build();

                IsInitialize = true;
            }
        }

        public static T GetService<T>() where T : class
        {
            if (Host.Services.GetService(typeof(T)) is not T service)
            {
                throw new ArgumentException($"{typeof(T)} needs to be registered in ConfigureServices within App.xaml.cs.");
            }

            return service;
        }

        public static void BindingViewModel<TPage, T>(TPage page, T viewModel) where TPage : Page where T : BaseViewModel<TPage>
        {
            page.DataContext = viewModel;

            FieldInfo[] fieldInfos = typeof(BaseViewModel<TPage>).GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
            foreach (FieldInfo item in fieldInfos)
            {
                if (item.Name == $"<{nameof(BaseViewModel<TPage>.Page)}>k__BackingField")
                {
                    item.SetValue(viewModel, page);
                    break;
                }
            }
        }

        private static void SystemEvents_UserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
        {
            RegistryKey registryKey = Registry.CurrentUser;
            RegistryKey personalize = registryKey.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize");
            RequestedTheme = Convert.ToBoolean(personalize.GetValue("AppsUseLightTheme")) ? ElementTheme.Light : ElementTheme.Dark;
        }

        private static void RefreshTheme()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                FluentThemeResource.Theme = RequestedTheme;
            });
        }
    }
}
