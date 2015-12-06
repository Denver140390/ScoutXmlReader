using System;

namespace ScoutXmlReader.Models
{
    public sealed class ScoutServer : IEquatable<ScoutServer>
    {
        public Boolean? State;
        public TimeSpan? UpTime;


        public Boolean Equals(ScoutServer other)
        {
            if (other == null)
                return false;
            
            return State.Equals(other.State) && UpTime.Equals(other.UpTime);
        }
    }
}