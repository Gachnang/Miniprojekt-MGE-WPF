using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ch.hsr.wpf.gadgeothek.Annotations;

namespace ch.hsr.wpf.gadgeothek.domain
{
    public class Loan : INotifyPropertyChanged {
        private string _id;
        public string Id {
            get => _id;
            set {
                _id = value;
                OnPropertyChanged();
            }
        }

        private string _customerId;
        public string CustomerId {
            get => _customerId;
            set {
                _customerId = value;
                OnPropertyChanged();
            }
        }

        private Customer _customer;
        public Customer Customer {
            get => _customer;
            set {
                _customer = value;
                OnPropertyChanged();
            }
        }

        private string _gadgetId;
        public string GadgetId {
            get => _gadgetId;
            set {
                _gadgetId = value;
                OnPropertyChanged();
            }
        }

        private Gadget _gadget;
        public Gadget Gadget {
            get => _gadget;
            set {
                _gadget = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _pickupDate;
        public DateTime? PickupDate {
            get => _pickupDate;
            set {
                _pickupDate = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _returnDate;
        public DateTime? ReturnDate {
            get => _returnDate;
            set {
                _returnDate = value;
                OnPropertyChanged();
            }
        }

        public DateTime? OverDueDate => PickupDate?.AddDays(DaysToReturn);

        public bool WasReturned => ReturnDate.HasValue;

        private static readonly int DaysToReturn = 7;

        // parameterless constructor is needed for automatic json conversion
        public Loan() {}

        public Loan(String id, Gadget gadget, Customer customer, DateTime? pickupDate, DateTime? returnDate) {
            Id = id;
            GadgetId = gadget.InventoryNumber;
            Gadget = gadget;
            CustomerId = customer.Studentnumber;
            Customer = customer;
            PickupDate = pickupDate;
            ReturnDate = returnDate;
        }

        public bool IsLent => ReturnDate == null;

        public bool IsOverdue => IsLent && OverDueDate < DateTime.Now;


        public override string ToString() {
            return $"Loan {Id}: {Gadget} from {PickupDate:yyyy-MM-dd} to {ReturnDate:yyyy-MM-dd}";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
