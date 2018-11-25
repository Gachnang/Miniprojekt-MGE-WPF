using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ch.hsr.wpf.gadgeothek.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ch.hsr.wpf.gadgeothek.domain
{
    public class Gadget : INotifyPropertyChanged {
        private string _inventoryNumber;
        public string InventoryNumber {
            get => _inventoryNumber;
            set {
                _inventoryNumber = value;
                OnPropertyChanged();
            }
        }

        private Condition _condition;
        public Condition Condition {
            get => _condition;
            set {
                _condition = value;
                OnPropertyChanged();
            }
        }

        private double _price;
        public double Price {
            get => _price;
            set {
                _price = value;
                OnPropertyChanged();
            }
        }

        private string _manufacturer;
        public string Manufacturer {
            get => _manufacturer;
            set {
                _manufacturer = value;
                OnPropertyChanged();
            }
        }

        private string _name;

        public string Name {
            get => _name;
            set {
                _name = value;
                OnPropertyChanged();
            }
        }


        // parameterless constructor is needed for automatic json conversion
        public Gadget() {}

        public Gadget(string name) {
            Name = name;
            var bits = Guid.NewGuid().ToByteArray().Take(8).ToArray();
            var nr = BitConverter.ToUInt64(bits, 0);
            InventoryNumber = nr.ToString();
            Condition = Condition.New;
        }

        public override int GetHashCode() {
            return InventoryNumber?.GetHashCode() ?? 31;
        }

        public override bool Equals(object obj) {
            if (this == obj)
                return true;
            if (obj == null)
                return false;
            var other = obj as Gadget;
            if (other == null)
                return false;
            if (InventoryNumber == null)
                return other.InventoryNumber == null;
            return InventoryNumber == other.InventoryNumber;
        }

        public override string ToString() {
            return FullDescription();
        }

        public string ShortDescription() {
            return $"{Name} [{InventoryNumber}]";
        }

        public string FullDescription() {
            return $"{Name} [{InventoryNumber}] by {Manufacturer} - Condition: {Condition.ToString().ToUpper()}";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


    
}
