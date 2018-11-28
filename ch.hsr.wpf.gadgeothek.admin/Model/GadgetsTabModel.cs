using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ch.hsr.wpf.gadgeothek.Annotations;
using ch.hsr.wpf.gadgeothek.domain;

namespace ch.hsr.wpf.gadgeothek.admin.Model
{
    public class GadgetsTabModel : INotifyPropertyChanged {
        private readonly MainWindowModel _mainWindowModel;
        public GadgeothekRepository Repository => _mainWindowModel.Repository;
        public Collection<Gadget> Gadgets => Repository.Gadgets;

        private Gadget _selectedGadget;
        public Gadget SelectedGadget
        {
            get => _selectedGadget;
            set
            {
                _selectedGadget = value;
                OnPropertyChanged();
            }
        }

        public GadgetsTabModel() {
            if ((bool)DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject)).DefaultValue)
            {
                _mainWindowModel = new MainWindowModel();
            }
            else
            {
                throw new ArgumentException(
                    "MainWindowModel is needed to inject repository.\n" +
                    "(It is allowed to be null to show data in designTime)");
            }
        }
        public GadgetsTabModel(MainWindowModel mainWindowModel)
        {
            _mainWindowModel = mainWindowModel;
        }

        public void Save()
        {
            Repository.SetGadget(SelectedGadget);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
