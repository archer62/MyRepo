using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace eventful
{
    class MainViewModel : INotifyPropertyChanged
    {
        //private Response _response;
        private ObservableCollection<Event> _events;
        private Event _selectedEvent;

        public ObservableCollection<Event> Events
        {
            get { return _events; }
            set
            {
                _events = value;
                OnPropertyChanged("Events");
            }
        }

        public Event SelectdEevent
        {
            get
            {
                return _selectedEvent;
            }
            set
            {
                _selectedEvent = value;
                OnPropertyChanged("SelectedEvent");
            }
        }

        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        const string key = "bL25bH5cQnqLjNH5";

        static string MakeQuery(string location = "", DateTime? start = null, DateTime? end = null, string keywords = "")
        {
            return string.Format("http://api.eventful.com/json/events/search?app_key={0}&location={1}&date={2}00-{3}00&keywords={4}", key, location, String.Format("{0:yyyyMMdd}", start), String.Format("{0:yyyyMMdd}", end), keywords);
        }

        public void WebRequest( CancellationToken token, string location = "", DateTime? start = null, DateTime? end = null, string keywords = "")
        {
            var webClient = new System.Net.WebClient();
            string result = webClient.DownloadString(MakeQuery(location, start, end, keywords));
            var r = JsonConvert.DeserializeObject<Response>(result);

            Events = new ObservableCollection<Event>(r.Events.Event);

            for (int i = 2; i <= r.PageCount; i++)
            {
                string res = webClient.DownloadString(string.Format(MakeQuery(location, start, end, keywords) + "&page_number={0}", i));
                var r1 = JsonConvert.DeserializeObject<Response>(res);
                List<Event> nl = r.Events.Event.Concat(r1.Events.Event).ToList<Event>();
                r.Events.Event = nl;
                foreach (var eve in r1.Events.Event)
                {
                    App.Current.Dispatcher.Invoke((Action)delegate
                    {
                        Events.Add(eve);
                    });
                    token.ThrowIfCancellationRequested();
                }
            }
            
        }

        public static CancellationTokenSource source = new CancellationTokenSource();
        static CancellationToken token = source.Token;

        public async Task<ObservableCollection<Event>> GetResponse(string location = "", DateTime? start = null, DateTime? end = null, string keywords = "")
        {
            _events = new ObservableCollection<Event>();
            var t = Task.Run(() => WebRequest(token, location, start, end, keywords), token);
            await t;

            return _events;
        }


        public void Cancel()
        {
            source.Cancel();
        }
    }
}
