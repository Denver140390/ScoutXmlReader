using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using ScoutXmlReader.Models;

namespace ScoutXmlReader.ViewModels
{

    public sealed class DataViewModel : ViewModelBase
    {
        private readonly String EMPTY_CELL = "-";
        private readonly List<ScoutData> _data = new List<ScoutData>();

        public ObservableCollection<TerminalViewModel> Terminals { get; } = new ObservableCollection<TerminalViewModel>();
        public ObservableCollection<String> SensorTypes { get; } = new ObservableCollection<String>();

        public async void LoadData(List<ScoutData> data)
        {
            //Compare with previous entries
            var dataToRemove = (from scoutData in _data from scoutDataNew in data where scoutDataNew.Equals(scoutData) select scoutDataNew).ToList();
            foreach (var d in dataToRemove)
            {
                data.Remove(d);
            }
            _data.AddRange(data);

            //Give DataGrid time to update
            //await Task.Delay(10);

            foreach (var scoutData in data)
            {
                //TODO Check terminals for equality
                //TODO Determine sensor value types (int, double or string)
                foreach (var scoutTerminal in scoutData.Terminals)
                {
                    var terminal = new TerminalViewModel
                    {
                        Protocol = scoutTerminal.Protocol ?? EMPTY_CELL,
                        SerialId = scoutTerminal.SerialId ?? EMPTY_CELL,
                        SimNumber = scoutTerminal.SimNumber ?? EMPTY_CELL,
                        ConnectionTime = scoutTerminal.ConnectionTime?.ToString() ?? EMPTY_CELL
                    };
                    
                    foreach (var scoutSensor in scoutTerminal.Sensors)
                    {
                        //Determine new sensor types
                        if (scoutSensor.Type != null && !SensorTypes.Contains(scoutSensor.Type))
                        {
                            SensorTypes.Add(scoutSensor.Type);

                            //If new sensor type detected - fill corresponding column with "-" for all previous terminals
                            foreach (var t in Terminals)
                            {
                                //temp.Add(scoutSensor.Type);
                                t.SensorValues.Add(EMPTY_CELL);
                            }
                        }
                    }

                    //Determine sensor types with values, fill others with "-"
                    var alreadyAddedTypes = new List<string>();
                    foreach (var sensorType in SensorTypes)
                    {
                        foreach (var sensor in scoutTerminal.Sensors)
                        {
                            if (alreadyAddedTypes.Contains(sensorType) || alreadyAddedTypes.Contains(sensor.Type)) continue;

                            if (sensorType == sensor.Type)
                            {
                                alreadyAddedTypes.Add(sensorType);
                                terminal.SensorValues.Add(sensor.Value);
                            }
                            else
                            {
                                alreadyAddedTypes.Add(sensorType);
                                terminal.SensorValues.Add(EMPTY_CELL);
                            }
                        }
                    }

                    //Fill empty cells with "-"
                    for (Int32 typeIndex = SensorTypes.Count - terminal.SensorValues.Count; typeIndex > 0; typeIndex--)
                    {
                        terminal.SensorValues.Add(EMPTY_CELL);
                    }
                    //if (terminal.SensorValues.Count != 0)
                    Terminals.Add(terminal);
                }
            }
        }

        public void Clear()
        {
            _data.Clear();
            Terminals.Clear();
            SensorTypes.Clear();
        }
    }
}