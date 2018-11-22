using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ch.hsr.wpf.gadgeothek.admin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ExampleViewModel m_ViewModel;
        public MainWindow()
        {
            InitializeComponent();
            m_ViewModel = new ExampleViewModel();
            DataContext = m_ViewModel;
        }
    }


    public class ExampleViewModel : INotifyPropertyChanged
    {
        private string m_Name = "Type Here";
        public ExampleViewModel()
        {

        }

        public string Name
        {
            get
            {
                return m_Name;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    throw new Exception("Name can not be empty.");
                }
                if (value.Length > 12)
                {
                    throw new Exception("name can not be longer than 12 characters");
                }
                if (m_Name != value)
                {
                    m_Name = value;
                    OnPropertyChanged("Name");
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }

        }
    }
}
