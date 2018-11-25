using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ch.hsr.wpf.gadgeothek.admin.Model;
using ch.hsr.wpf.gadgeothek.Annotations;
using ch.hsr.wpf.gadgeothek.domain;

namespace ch.hsr.wpf.gadgeothek.admin.View.Window
{
    public class MainWindowModel : INotifyPropertyChanged {
        public GadgeothekRepository Repository { get; private set; }

        private Gadget _selectedGadget;
        public Gadget SelectedGadget {
            get => _selectedGadget;
            set {
                _selectedGadget = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainWindowModel() {
            Repository = GadgeothekRepository.GetInstance();
            Repository.PropertyChanged += (o, e) => { this.OnPropertyChanged("Repository");};
            Repository.Open();
        }

        public void Save() {
            Repository.SetGadget(SelectedGadget);
        }
    }
}
