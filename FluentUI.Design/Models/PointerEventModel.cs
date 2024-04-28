using System;
using System.Windows;
using System.Windows.Input;

namespace FluentUI.Design.Models
{
    public class PointerEventModel
    {
        public RoutedEventHandler EventHandler { get; set; }

        public MouseButtonEventHandler MouseButtonEvent { get; set; }

        public EventHandler<TouchEventArgs> TouchEvent { get; set; }

        public static PointerEventModel Instance(RoutedEventHandler handler)
        {
            return new PointerEventModel
            {
                EventHandler = handler,
                MouseButtonEvent = (object sender, MouseButtonEventArgs e) =>
                {
                    if (sender is UIElement element)
                    {
                        if (e.StylusDevice == null)
                        {
                            handler.Invoke(sender, e);
                        }
                    }
                },
                TouchEvent = handler.Invoke
            };
        }
    }
}
