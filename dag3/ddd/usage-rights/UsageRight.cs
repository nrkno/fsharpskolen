using System;
using NodaTime;

namespace PS.Catalog.Playback
{
    [Serializable]
    public class UsageRight : IEquatable<UsageRight>
    {
        private readonly bool _rightsExpire;

        public UsageRight(
            Instant availableFromUtc, 
            Instant availableToUtc, 
            bool isGeoBlocked, 
            AccessibilityType accessibilityType)
        {
            AvailableToUtc = availableToUtc;
            AvailableFromUtc = availableFromUtc;
            IsGeoBlocked = isGeoBlocked;
            _rightsExpire = true;
            AccessibilityType = accessibilityType;
        }

        // Blokket for visning utenfor Norge
        public bool IsGeoBlocked { get; }

        // Why is this nullable? It always receives a value.
        public Instant? AvailableFromUtc { get; }

        // Why is this nullable? It always receives a value.
        public Instant? AvailableToUtc { get; }

        public AccessibilityType AccessibilityType { get; }

        public static UsageRight DefaultOnDemand => 
            new UsageRight(Instant.MinValue, Instant.MinValue, true, AccessibilityType.None);

        public bool HasRightsNowInNorway(Instant timenow)
        {
            return HasRightsNow(timenow);
        }

        public bool HasRightsNowAbroad(Instant timenow)
        {
            return !IsGeoBlocked && HasRightsNow(timenow);
        }

        // trenger vi denne?
        public bool HasRightsNow(Instant currentTime, AccessibilityType accessibilityType = AccessibilityType.None)
        {
            if (_rightsExpire)
            {
                return currentTime < AvailableToUtc.Value && currentTime > AvailableFromUtc.Value && accessibilityType == AccessibilityType;
            }

            return currentTime > AvailableFromUtc.Value && accessibilityType == AccessibilityType;
        }

        public bool Equals(UsageRight other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _rightsExpire == other._rightsExpire 
                && IsGeoBlocked == other.IsGeoBlocked 
                && AvailableFromUtc.Equals(other.AvailableFromUtc) 
                && AvailableToUtc.Equals(other.AvailableToUtc) 
                && AccessibilityType == other.AccessibilityType;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((UsageRight) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = _rightsExpire.GetHashCode();
                hashCode = (hashCode * 397) ^ IsGeoBlocked.GetHashCode();
                hashCode = (hashCode * 397) ^ AvailableFromUtc.GetHashCode();
                hashCode = (hashCode * 397) ^ AvailableToUtc.GetHashCode();
                hashCode = (hashCode * 397) ^ (int) AccessibilityType;
                return hashCode;
            }
        }
    }
}