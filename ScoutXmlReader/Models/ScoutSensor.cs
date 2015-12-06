using System;

namespace ScoutXmlReader.Models
{
    public sealed class ScoutSensor : IEquatable<ScoutSensor>
    {
        public String Type;
        public String Value;

        public Boolean Equals(ScoutSensor other)
        {
            if (other == null)
                return false;

            if ((Type == null && other.Type != null) || (Type != null && other.Type == null))
                return false;
            if ((Value == null && other.Value != null) || (Value != null && other.Value == null))
                return false;

            return Type.Equals(other.Type) && Value.Equals(other.Value);
        }
    }
}