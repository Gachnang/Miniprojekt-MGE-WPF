using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ch.hsr.wpf.gadgeothek.domain;
using ch.hsr.wpf.gadgeothek.service;
using ch.hsr.wpf.gadgeothek.websocket;

namespace ch.hsr.wpf.gadgeothek.admin.Model
{
    public class GadgeothekRepository : IDisposable {
        private static GadgeothekRepository _instance;
        private static String DefaultServerUrl = "http://localhost:8080";
        private LibraryAdminService _libraryService;

        private WebSocketClient _databaseObserver;
        private Task _databaseObserverTask;

        private ObservableCollection<Loan> _loans;
        public ObservableCollection<Loan> Loans => _loans;

        private ObservableCollection<Gadget> _gadgets;
        public ObservableCollection<Gadget> Gadgets => _gadgets;

        private ObservableCollection<Reservation> _reservations;
        public ObservableCollection<Reservation> Reservations => _reservations;

        private ObservableCollection<Customer> _customers;
        public ObservableCollection<Customer> Customers => _customers;

        public static GadgeothekRepository GetInstance(string serverUrl = null) {
            serverUrl = String.IsNullOrEmpty(serverUrl) ? DefaultServerUrl : serverUrl;
            if (_instance == null || !_instance._libraryService.ServerUrl.Equals(serverUrl)) {
                _instance = new GadgeothekRepository(serverUrl);
            }

            return _instance;
        }

        protected GadgeothekRepository(string serverUrl) {
            _libraryService = new LibraryAdminService(serverUrl);
        }

        public bool IsOpen() => _databaseObserverTask != null;

        public void Open() {
            if (!IsOpen()) {
                try {
                    _loans = new ObservableCollection<Loan>(_libraryService.GetAllLoans());
                    _gadgets = new ObservableCollection<Gadget>(_libraryService.GetAllGadgets());
                    _reservations = new ObservableCollection<Reservation>(_libraryService.GetAllReservations());
                    _customers = new ObservableCollection<Customer>(_libraryService.GetAllCustomers());

                    _databaseObserver = new WebSocketClient(_libraryService.ServerUrl);
                    _databaseObserver.NotificationReceived += DatabaseObserver;

                    _databaseObserverTask?.Dispose();
                    _databaseObserverTask = _databaseObserver.ListenAsync();
                } catch (Exception e) {
                    throw new Exception("Failed to open GadgeothekRepository.", e);
                }
            }
        }

        public void Close() {
            this.Dispose();
        }

        private void DatabaseObserver(Object target, WebSocketClientNotificationEventArgs eventArgs) {
            Console.WriteLine("WebSocket::Notification: " + eventArgs.Notification.Target + " > " + eventArgs.Notification.Type);

            if (eventArgs.Notification.Target == typeof(Loan).Name.ToLower()) {
                Loan loan = eventArgs.Notification.DataAs<Loan>();

                switch (eventArgs.Notification.Type) {
                    case WebSocketClientNotificationTypeEnum.Add:
                        _loans.Add(loan);
                        break;
                    case WebSocketClientNotificationTypeEnum.Delete:
                        _loans.Remove(_loans.SingleOrDefault(l => l.Id.Equals(loan.Id)));
                        break;
                    case WebSocketClientNotificationTypeEnum.Update:
                        // PropertyChangedEvent will not work because of missing INotifyPropertyChanged, we remove and add item.
                        // It's not great, but will work.
                        _loans.Remove(_loans.SingleOrDefault(l => l.Id.Equals(loan.Id)));
                        _loans.Add(loan);
                        break;
                }
            } else if (eventArgs.Notification.Target == typeof(Gadget).Name.ToLower()) {
                Gadget gadget = eventArgs.Notification.DataAs<Gadget>();

                switch (eventArgs.Notification.Type)
                {
                    case WebSocketClientNotificationTypeEnum.Add:
                        _gadgets.Add(gadget);
                        break;
                    case WebSocketClientNotificationTypeEnum.Delete:
                        _gadgets.Remove(_gadgets.SingleOrDefault(g => g.InventoryNumber.Equals(gadget.InventoryNumber)));
                        break;
                    case WebSocketClientNotificationTypeEnum.Update:
                        // PropertyChangedEvent will not work because of missing INotifyPropertyChanged, we remove and add item.
                        // It's not great, but will work.
                        _gadgets.Remove(_gadgets.SingleOrDefault(g => g.InventoryNumber.Equals(gadget.InventoryNumber)));
                        _gadgets.Add(gadget);
                        break;
                }
            } else if (eventArgs.Notification.Target == typeof(Reservation).Name.ToLower()) {
                Reservation reservation = eventArgs.Notification.DataAs<Reservation>();

                switch (eventArgs.Notification.Type)
                {
                    case WebSocketClientNotificationTypeEnum.Add:
                        _reservations.Add(reservation);
                        break;
                    case WebSocketClientNotificationTypeEnum.Delete:
                        _reservations.Remove(_reservations.SingleOrDefault(r => r.Id.Equals(reservation.Id)));
                        break;
                    case WebSocketClientNotificationTypeEnum.Update:
                        // PropertyChangedEvent will not work because of missing INotifyPropertyChanged, we remove and add item.
                        // It's not great, but will work.
                        _reservations.Remove(_reservations.SingleOrDefault(r => r.Id.Equals(reservation.Id)));
                        _reservations.Add(reservation);
                        break;
                }
            }
            else if (eventArgs.Notification.Target == typeof(Reservation).Name.ToLower())
            {
                Reservation reservation = eventArgs.Notification.DataAs<Reservation>();

                switch (eventArgs.Notification.Type)
                {
                    case WebSocketClientNotificationTypeEnum.Add:
                        _reservations.Add(reservation);
                        break;
                    case WebSocketClientNotificationTypeEnum.Delete:
                        _reservations.Remove(_reservations.SingleOrDefault(r => r.Id.Equals(reservation.Id)));
                        break;
                    case WebSocketClientNotificationTypeEnum.Update:
                        // PropertyChangedEvent will not work because of missing INotifyPropertyChanged, we remove and add item.
                        // It's not great, but will work.
                        _reservations.Remove(_reservations.SingleOrDefault(r => r.Id.Equals(reservation.Id)));
                        _reservations.Add(reservation);
                        break;
                }
            }
            else if (eventArgs.Notification.Target == typeof(Customer).Name.ToLower())
            {
                Customer customer = eventArgs.Notification.DataAs<Customer>();

                switch (eventArgs.Notification.Type)
                {
                    case WebSocketClientNotificationTypeEnum.Add:
                        _customers.Add(customer);
                        break;
                    case WebSocketClientNotificationTypeEnum.Delete:
                        _customers.Remove(_customers.SingleOrDefault(c => c.Studentnumber.Equals(customer.Studentnumber)));
                        break;
                    case WebSocketClientNotificationTypeEnum.Update:
                        // PropertyChangedEvent will not work because of missing INotifyPropertyChanged, we remove and add item.
                        // It's not great, but will work.
                        _customers.Remove(_customers.SingleOrDefault(c => c.Studentnumber.Equals(customer.Studentnumber)));
                        _customers.Add(customer);
                        break;
                }
            } else {
                throw new EvaluateException("Could not figure out, which data changed..\nValue=" + eventArgs.Notification.Target);
            }
        }

        public bool SetGadget(Gadget gadget) {
            Gadget gadgetDb = _gadgets.FirstOrDefault(g => g.InventoryNumber.Equals(gadget.InventoryNumber));

            if (gadgetDb != null) {
                return _libraryService.UpdateGadget(gadget);
            }

            return gadgetDb == null && _libraryService.AddGadget(gadget);
        }

        public bool DeleteGadget(Gadget gadget) {
            Gadget gadgetDb = _gadgets.FirstOrDefault(g => g.InventoryNumber.Equals(gadget.InventoryNumber));

            if (gadgetDb != null && !gadgetDb.Equals(gadget))
            {
                return _libraryService.DeleteGadget(gadget);
            }

            return false;
        }

        public void Dispose()
        {
            _databaseObserverTask?.Dispose();
            _databaseObserverTask = null;

            _loans?.Clear();
            _customers?.Clear();
            _gadgets?.Clear();
            _reservations?.Clear();
        }
    }
}
