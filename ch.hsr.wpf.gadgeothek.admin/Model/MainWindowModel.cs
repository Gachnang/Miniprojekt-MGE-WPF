using System.ComponentModel;
using System.Runtime.CompilerServices;
using ch.hsr.wpf.gadgeothek.Annotations;
using ch.hsr.wpf.gadgeothek.domain;

namespace ch.hsr.wpf.gadgeothek.admin.Model
{
    public class MainWindowModel : INotifyPropertyChanged {
        public GadgeothekRepository Repository { get; private set; }

        public GadgetsTabModel GadgetsTabModel { get; private set; }

        public MainWindowModel() {

            GadgetsTabModel = new GadgetsTabModel(this);

            Repository = GadgeothekRepository.GetInstance();
            Repository.PropertyChanged += (o, e) => { this.OnPropertyChanged("Repository");};
            Repository.Open();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
