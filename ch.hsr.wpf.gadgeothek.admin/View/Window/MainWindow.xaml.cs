using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using ch.hsr.wpf.gadgeothek.admin.Model;
using ch.hsr.wpf.gadgeothek.Annotations;
using ch.hsr.wpf.gadgeothek.domain;

namespace ch.hsr.wpf.gadgeothek.admin.View.Window
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window {
        public MainWindowModel Model;
        public MainWindow()
        {
            InitializeComponent();

            Model = new MainWindowModel();
            DataContext = Model;
        }

        private void BtnSave_OnClick(object sender, RoutedEventArgs e) {
            Model.Save();
        }
    }
}
