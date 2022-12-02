using FluentUI.Design.Models;
using System.Collections.Generic;
using System.Windows;

namespace FluentUI.Design.Tools
{
    public static class PointerEvent
    {
        private static readonly List<PointerEventModel> pointerUpEvents = new();
        private static readonly List<PointerEventModel> pointerDownEvents = new();

        #region PointerUp
        public static readonly RoutedEvent PointerUpEvent = EventManager.RegisterRoutedEvent("PointerUp", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(UIElement));

        public static void AddPointerUpHandler(this UIElement element, RoutedEventHandler handler)
        {
            lock (pointerUpEvents)
            {
                PointerEventModel pointer = PointerEventModel.Instance(handler);
                element.MouseLeftButtonUp += pointer.MouseButtonEvent;
                element.TouchUp += pointer.TouchEvent;
                pointerUpEvents.Add(pointer);
            }
        }
        public static void RemovePointerUpHandler(this UIElement element, RoutedEventHandler handler)
        {
            lock (pointerUpEvents)
            {
                if (pointerUpEvents.Find(item => item.EventHandler.Equals(handler)) is PointerEventModel pointer)
                {
                    element.MouseLeftButtonUp -= pointer.MouseButtonEvent;
                    element.TouchUp -= pointer.TouchEvent;
                    pointerUpEvents.Remove(pointer);
                }
            }
        }
        #endregion

        #region PointerDown
        public static readonly RoutedEvent PointerDownEvent = EventManager.RegisterRoutedEvent("PointerDown", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(UIElement));

        public static void AddPointerDownHandler(this UIElement element, RoutedEventHandler handler)
        {
            lock (pointerDownEvents)
            {
                PointerEventModel pointer = PointerEventModel.Instance(handler);
                element.MouseLeftButtonDown += pointer.MouseButtonEvent;
                element.TouchDown += pointer.TouchEvent;
                pointerDownEvents.Add(pointer);
            }
        }
        public static void RemovePointerDownHandler(this UIElement element, RoutedEventHandler handler)
        {
            lock (pointerDownEvents)
            {
                if (pointerDownEvents.Find(item => item.EventHandler.Equals(handler)) is PointerEventModel pointer)
                {
                    element.MouseLeftButtonDown -= pointer.MouseButtonEvent;
                    element.TouchDown -= pointer.TouchEvent;
                    pointerDownEvents.Remove(pointer);
                }
            }
        }
        #endregion
    }
}
