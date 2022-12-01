using FluentUI.Design.Enums;
using System.Windows;
using System.Windows.Data;

namespace FluentUI.Design.Tools
{
    public class FluentThemeTrigger : DataTrigger
    {
        #region 属性
        private bool isWindow;
        private ElementTheme theme;

        /// <summary>
        /// 是否为窗体
        /// </summary>
        public bool IsWindow
        {
            get => isWindow;
            set
            {
                isWindow = value;
                GenerateProperty();
            }
        }

        /// <summary>
        /// 主题
        /// </summary>
        public ElementTheme Theme
        {
            get => theme;
            set
            {
                theme = value;
                GenerateProperty();
            }
        }
        #endregion

        public FluentThemeTrigger()
        {
            GenerateProperty();
        }

        private void GenerateProperty()
        {
            Binding = new Binding()
            {
                Source = Core.FluentThemeResource,
                Path = new PropertyPath(nameof(FluentThemeResource.Theme))
            };
            Value = Theme;
        }
    }
}
