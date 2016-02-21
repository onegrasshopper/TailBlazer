using System;

namespace TailBlazer.Domain.Formatting
{
    public struct HueKey : IEquatable<HueKey>
    {
        public CaseInsensitiveString Swatch { get;  }
        public CaseInsensitiveString Name { get;  }

        public HueKey(string swatch, string name)
        {
            Swatch = (CaseInsensitiveString)swatch;
            Name = (CaseInsensitiveString)name;
        }

        #region Equality

        public bool Equals(HueKey other)
        {
            return string.Equals(Swatch, other.Swatch) && string.Equals(Name, other.Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is HueKey && Equals((HueKey) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Swatch != null ? Swatch.GetHashCode() : 0)*397) ^ (Name != null ? Name.GetHashCode() : 0);
            }
        }

        public static bool operator ==(HueKey left, HueKey right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(HueKey left, HueKey right)
        {
            return !left.Equals(right);
        }

        #endregion

        public override string ToString()
        {
            return $"{Swatch} ({Name})";
        }
    }
}