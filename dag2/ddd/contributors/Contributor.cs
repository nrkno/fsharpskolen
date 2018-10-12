using System;
using JetBrains.Annotations;

namespace PS.Catalog.Metadata
{
    [Serializable]
    public class Contributor : IEquatable<Contributor>
    {
        public Contributor(
            string givenName, 
            string familyName, 
            string name, 
            string role)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (name.Length == 0) throw new ArgumentException("The string cannot be empty.", nameof(name));

            if (role == null) throw new ArgumentNullException(nameof(role));
            if (role.Length == 0) throw new ArgumentException("The string cannot be empty.", nameof(role));

            Name = name;
            Role = role;
            GivenName = givenName;
            FamilyName = familyName;
        }
        
        [NotNull]
        public string Name { get; }

        [NotNull]
        public string Role { get; }

        [CanBeNull]
        public string GivenName { get; }

        [CanBeNull]
        public string FamilyName { get; }

        public bool Equals(Contributor other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name) && string.Equals(Role, other.Role) && string.Equals(GivenName, other.GivenName) && string.Equals(FamilyName, other.FamilyName);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Contributor) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Name.GetHashCode();
                hashCode = (hashCode * 397) ^ Role.GetHashCode();
                hashCode = (hashCode * 397) ^ (GivenName != null ? GivenName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (FamilyName != null ? FamilyName.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}
