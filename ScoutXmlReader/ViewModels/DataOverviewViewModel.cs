using System;
using System.Collections.Generic;
using ScoutXmlReader.Models;

namespace ScoutXmlReader.ViewModels
{
    public sealed class DataOverviewViewModel : ViewModelBase
    {
        private String _beginDate;
        private String _endDate;
        private String _serverState;
        private String _serverUpTime;

        public String BeginDate
        {
            get { return _beginDate; }
            set
            {
                _beginDate = value;
                OnPropertyChanged();
            }
        }

        public String EndDate
        {
            get { return _endDate; }
            set
            {
                _endDate = value;
                OnPropertyChanged();
            }
        }

        public String ServerState
        {
            get { return _serverState; }
            set
            {
                _serverState = value;
                OnPropertyChanged();
            }
        }

        public String ServerUpTime
        {
            get { return _serverUpTime; }
            set
            {
                _serverUpTime = value;
                OnPropertyChanged();
            }
        }

        public void LoadData(List<ScoutData> data)
        {
            if (data.Count == 0) return;
            ScoutData lastData = data[data.Count - 1];
            BeginDate = lastData.BeginDate.ToString();
            EndDate = lastData.EndDate.ToString();
            ServerState = lastData.ServerState;
            ServerUpTime = lastData.ServerUpTime.ToString();
        }
    }
}
