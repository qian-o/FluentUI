using System;
using FluentUI.Design.Controls;

namespace FluentUI.Demo
{
    public partial class MainWindow : FluentWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            Console.WriteLine("Hello, FluentUI!");

            // Set the title of the window
            Title = "FluentUI Demo";
        }
    }
}
