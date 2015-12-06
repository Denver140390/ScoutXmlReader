using System;
using System.Collections.Generic;
using ScoutXmlReader.Services;

namespace ScoutXmlReader.Models
{
    public sealed class ScoutData : IEquatable<ScoutData>
    {
        public DateTime? BeginDate;
        public DateTime? EndDate;

        public List<ScoutServer> Servers;

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
            if (!(BeginDate.Equals(other.BeginDate) && EndDate.Equals(other.EndDate)))
                return false;
            
            if ((Servers == null && other.Servers != null) || (Servers != null && other.Servers == null))
                return false;
            for (int serverIndex = 0; serverIndex < Servers.Count - 1; serverIndex++)
            {
                if (!Servers[serverIndex].Equals(other.Servers[serverIndex]))
                    return false;
            }
            
            if ((Terminals == null && other.Terminals != null) || (Terminals != null && other.Terminals == null))
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
