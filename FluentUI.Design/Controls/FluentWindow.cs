using FluentUI.Design.Enums;
using FluentUI.Design.Tools;
using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace FluentUI.Design.Controls
{
    public class FluentWindow : Window
    {
        private readonly FluentThemeResource _themeResource;

        #region 依赖属性
        public static readonly DependencyProperty RequestedThemeProperty = DependencyProperty.Register(nameof(RequestedTheme), typeof(ElementTheme), typeof(FluentWindow), new PropertyMetadata(ElementTheme.Light, OnRequestedThemeChanged));

        public ElementTheme RequestedTheme
        {
            get => (ElementTheme)GetValue(RequestedThemeProperty);
            set => SetValue(RequestedThemeProperty, value);
        }
        #endregion

        static FluentWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FluentWindow), new FrameworkPropertyMetadata(typeof(FluentWindow)));

            if (Environment.OSVersion.Version.Build > 17763)
            {
                SystemEvents.UserPreferenceChanged += SystemEvents_UserPreferenceChanged;
            }
        }

        public FluentWindow()
        {
            _themeResource = new FluentThemeResource();

            Resources.MergedDictionaries.Add(_themeResource);
            RefreshTheme();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            UniformGrid uniformGrid = GetTemplateChild("AeroCaptionButtons") as UniformGrid;
            foreach (object item in uniformGrid.Children)
            {
                if (item is Button button)
                {
                    button.Click += AeroCaption_Click;
                }
            }
        }

        private void AeroCaption_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            if (button.Tag.ToString() == "Minimized")
            {
                WindowState = WindowState.Minimized;
            }
            else if (button.Tag.ToString() == "WindowState")
            {
                WindowState = WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
            }
            else
            {
                Close();
            }
        }

        private static void OnRequestedThemeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FluentWindow window)
            {
                window._themeResource.Theme = window.RequestedTheme;
            }
        }

        private static void SystemEvents_UserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
        {
            Application.Current.Dispatcher.InvokeAsync(() =>
            {
                foreach (var item in Application.Current.Windows)
                {
                    if (item is FluentWindow window)
                    {
                        window.RefreshTheme();
                    }
                }
            });
        }

        /// <summary>
        /// 获取注册表信息，刷新主题
        /// </summary>
        private void RefreshTheme()
        {
            if (Environment.OSVersion.Version.Build > 17763)
            {
                RegistryKey registryKey = Registry.CurrentUser;
                RegistryKey personalize = registryKey.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize");
                RequestedTheme = Convert.ToBoolean(personalize.GetValue("AppsUseLightTheme")) ? ElementTheme.Light : ElementTheme.Dark;
            }
        }
    }
}
