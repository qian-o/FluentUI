using CommunityToolkit.Mvvm.Messaging.Messages;
using System;

namespace FluentUI.Demo.Models.Messages
{
    public class NavigationPageMessage : ValueChangedMessage<Type>
    {
        public NavigationPageMessage(Type value) : base(value)
        {
        }
    }
}
