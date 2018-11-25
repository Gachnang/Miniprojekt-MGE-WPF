using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using ch.hsr.wpf.gadgeothek.admin.Model;
using ch.hsr.wpf.gadgeothek.Annotations;
using ch.hsr.wpf.gadgeothek.domain;

namespace ch.hsr.wpf.gadgeothek.admin {
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged {
        private GadgeothekRepository _repository;
        public Collection<Gadget> Gadgets => _repository.Gadgets;
        public Gadget SelectedGadget => Selector.SelectedItem as Gadget;

        public MainWindow() {
            InitializeComponent();

            _repository = GadgeothekRepository.GetInstance();
            _repository.Open();

            DataContext = _repository;
        }

        private void BtnSave_OnClick(object sender, RoutedEventArgs e) {
            _repository.SetGadget(new Gadget() {
                Name = WtbName.Text,
                Manufacturer = WtbManufacturer.Text,
                InventoryNumber = WtbInventoryNumber.Text
            });
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OnPropertyChanged("SelectedGadget");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}