using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Controls;

namespace FluentUI.Demo.Models
{
    public class PageModel : ObservableObject
    {
        private bool isSelect;

        public string Icon { get; set; }

        public Page Page { get; set; }

        public bool IsSelect
        {
            get => isSelect;
            set
            {
                isSelect = value;

                OnPropertyChanged(nameof(IsSelect));
            }
        }
    }
}
