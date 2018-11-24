using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using ch.hsr.wpf.gadgeothek.admin.Annotations;
using ch.hsr.wpf.gadgeothek.admin.Model;
using ch.hsr.wpf.gadgeothek.domain;

namespace ch.hsr.wpf.gadgeothek.admin {
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private GadgeothekRepository _repository;
        public Collection<Gadget> Gadgets => _repository.Gadgets;
        public Gadget SelectedGadget => Selector.SelectedItem as Gadget;

        public MainWindow() {
            InitializeComponent();

            _repository = GadgeothekRepository.GetInstance();
            _repository.Open();

            DataContext = this;
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
            WtbInventoryNumber.Text = SelectedGadget.InventoryNumber;
            WtbManufacturer.Text = SelectedGadget.Manufacturer;
            WtbName.Text = SelectedGadget.Name;
        }
    }
}