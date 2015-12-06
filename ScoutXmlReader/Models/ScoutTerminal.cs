using System;
using System.Collections.Generic;

namespace ScoutXmlReader.Models
{
    public sealed class ScoutTerminal : IEquatable<ScoutTerminal>
    {
        public String Protocol;
        public String SerialId;
        public String SimNumber;
        public DateTime? ConnectionTime;

        public List<ScoutSensor> Sensors;

        public Boolean Equals(ScoutTerminal other)
        {
            if (other == null)
                return false;
            
            if (((Protocol == null && other.Protocol != null) || (Protocol != null && other.Protocol == null)) ||
               ((SerialId == null && other.SerialId != null) || (SerialId != null && other.SerialId == null)) ||
               ((SimNumber == null && other.SimNumber != null) || (SimNumber != null && other.SimNumber == null)))
                return false;

            if (!(Equals(Protocol, other.Protocol) && Equals(SerialId, other.SerialId) && Equals(SimNumber, other.SimNumber)))
                return false;

//            if (!(ConnectionTime.HasValue && other.ConnectionTime.HasValue))
//                return true;
//            if (ConnectionTime != other.ConnectionTime)
//                return false;
            if (!(ConnectionTime.Equals(other.ConnectionTime)))
                return false;
            
            if ((Sensors == null && other.Sensors != null) || (Sensors != null && other.Sensors == null))
                return false;
            for (int sensorIndex = 0; sensorIndex < Sensors.Count - 1; sensorIndex++)
            {
                if (!(Sensors[sensorIndex].Equals(other.Sensors[sensorIndex])))
                    return false;
            }

            return true;
        }
    }
}