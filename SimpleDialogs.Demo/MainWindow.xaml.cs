using MahApps.Metro.Controls;
using System.Windows;

namespace SimpleDialogs.Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public static MainWindow Instance { get; private set; }

        public MainWindow()
        {
            Instance = this;

            InitializeComponent();
        }
    }
}
