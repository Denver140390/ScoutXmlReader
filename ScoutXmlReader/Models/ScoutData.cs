using System;
using System.Collections.Generic;
using ScoutXmlReader.Services;

namespace ScoutXmlReader.Models
{
    public sealed class ScoutData : IEquatable<ScoutData>
    {
        public DateTime? BeginDate;
        public DateTime? EndDate;
        public String ServerState;
        public TimeSpan? ServerUpTime;

        public List<ScoutTerminal> Terminals;

        public static ScoutData FromXml(String xmlFilePath)
        {
            return XmlToScoutDataParser.Parse(xmlFilePath);
        }

        public Boolean Equals(ScoutData other)
        {
            if (other == null)
                return false;

//            if (!(BeginDate.HasValue && other.BeginDate.HasValue) && !(EndDate.HasValue && other.EndDate.HasValue))
//                return true;
//            if (BeginDate != other.BeginDate || EndDate != other.EndDate)
//                return false;
            if (!(BeginDate.Equals(other.BeginDate) && EndDate.Equals(other.EndDate) && ServerUpTime.Equals(other.ServerUpTime)))
                return false;

            if ((ServerState == null && other.ServerState != null) || (ServerState != null && other.ServerState == null))
                return false;

            if (!Equals(ServerState, other.ServerState))
                return false;
            
            if ((Terminals == null && other.Terminals != null) || (Terminals != null && other.Terminals == null))
                return false;
            if (Terminals.Count != other.Terminals.Count)
                return false;
            for (int terminalIndex = 0; terminalIndex < Terminals.Count - 1; terminalIndex++)
            {
                if (!Terminals[terminalIndex].Equals(other.Terminals[terminalIndex]))
                    return false;
            }

            return true;
        }
    }
}
