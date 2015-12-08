using System;
using System.Collections.ObjectModel;

namespace ScoutXmlReader.ViewModels
{
    public sealed class TerminalViewModel : ViewModelBase
    {
        private String _protocol;
        private String _serialId;
        private String _simNumber;
        private String _connectionTime;

        public String Protocol
        {
            get { return _protocol; }
            set
            {
                _protocol = value;
                OnPropertyChanged();
            }
        }

        public String SerialId
        {
            get { return _serialId; }
            set
            {
                _serialId = value;
                OnPropertyChanged();
            }
        }

        public String SimNumber
        {
            get { return _simNumber; }
            set
            {
                _simNumber = value;
                OnPropertyChanged();
            }
        }

        public String ConnectionTime
        {
            get { return _connectionTime; }
            set
            {
                _connectionTime = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<String> SensorValues { get; } = new ObservableCollection<String>();
    }
}