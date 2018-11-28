using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ch.hsr.wpf.gadgeothek.admin.Model;
using ch.hsr.wpf.gadgeothek.domain;

namespace ch.hsr.wpf.gadgeothek.admin.View.Window
{
    /// <summary>
    /// Interaction logic for GadgetsTab.xaml
    /// </summary>
    public partial class GadgetsTab : UserControl
    {
        public GadgetsTabModel Model => this.DataContext as GadgetsTabModel;

        public GadgetsTab()
        {
            InitializeComponent();
        }

        private void BtnSave_OnClick(object sender, RoutedEventArgs e)
        {
            Model.Save();
        }
    }
}
