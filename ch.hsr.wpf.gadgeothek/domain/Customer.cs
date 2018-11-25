using System.ComponentModel;
using System.Runtime.CompilerServices;
using ch.hsr.wpf.gadgeothek.Annotations;

namespace ch.hsr.wpf.gadgeothek.domain
{
    public class Customer : INotifyPropertyChanged {
        private string _name;
        public string Name {
            get => _name;
            set {
                _name = value;
                OnPropertyChanged();
            }
        }

        private string _password;
        public string Password {
            get => _password;
            set {
                _password = value;
                OnPropertyChanged();
            }
        }

        private string _email;
        public string Email {
            get => _email;
            set {
                _email = value;
                OnPropertyChanged();
            }
        }

        private string _studentnumber;
        public string Studentnumber {
            get => _studentnumber;
            set {
                _studentnumber = value;
                OnPropertyChanged();
            }
        } // Number with lowercase n to conform to the back end (server.js)

        // parameterless constructor is needed for automatic json conversion
        public Customer(){}

        public Customer(string name, string password, string email, string studentNumber) {
            _name = name;
            _password = password;
            _email = email;
            _studentnumber = studentNumber;
        }

        public override int GetHashCode() {
            return Studentnumber?.GetHashCode() ?? 31;
        }

        public override bool Equals(object obj) {
            if (this == obj)
                return true;
            if (obj == null)
                return false;
            var other = obj as Customer;
            if (other == null)
                return false;
            if (Studentnumber == null)
                return other.Studentnumber == null;
            return Studentnumber == other.Studentnumber;
        }


        public override string ToString() {
            return $"{Name}<{Email}> [{Studentnumber}]";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}