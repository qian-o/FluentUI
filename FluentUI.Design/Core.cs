using FluentUI.Design.Controls;
using FluentUI.Design.Enums;
using FluentUI.Design.Tools;
using Microsoft.Win32;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

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

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        FluentThemeResource.Theme = value;
                        foreach (object item in Application.Current.Windows)
                        {
                            if (item is FluentWindow window)
                            {
                                window.RequestedTheme = RequestedTheme;
                            }
                        }
                    });
                }
            }
        }

        public static bool IsInitialize { get; set; }

        public static void Initialize()
        {
            if (!IsInitialize)
            {
                FluentThemeResource = Application.Current.Resources.MergedDictionaries.FirstOrDefault(item => item.GetType() == typeof(FluentThemeResource)) as FluentThemeResource;

                if (Environment.OSVersion.Version.Build > 17763)
                {
                    SystemEvents.UserPreferenceChanged += SystemEvents_UserPreferenceChanged;
                }
            }
        }

        private static void SystemEvents_UserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
        {
            Task.Run(() =>
            {
                RegistryKey registryKey = Registry.CurrentUser;
                RegistryKey personalize = registryKey.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize");
                RequestedTheme = Convert.ToBoolean(personalize.GetValue("AppsUseLightTheme")) ? ElementTheme.Light : ElementTheme.Dark;
            });
        }
    }
}
