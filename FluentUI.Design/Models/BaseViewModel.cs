using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Controls;

namespace FluentUI.Design.Models
{
    public partial class BaseViewModel<TPage> : ObservableRecipient where TPage : Page
    {
        public TPage Page { get; }
    }
}
