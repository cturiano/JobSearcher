using System;
using JobSearcher.Abstract;

namespace JobSearcher.Models
{
    internal class Credentials : BindableBase, IEquatable<Credentials>
    {
        #region Constructors

        public Credentials(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        public Credentials()
        {
            UserName = string.Empty;
            Password = string.Empty;
        }

        #endregion

        #region Properties

        public string Password
        {
            get => Get<string>();
            set => Set(value);
        }

        public string UserName
        {
            get => Get<string>();
            set => Set(value);
        }

        #endregion

        #region Public Methods

        public bool Equals(Credentials other)
        {
            return other != null && ReferenceEquals(this, other) || Password.Equals(other.Password) && UserName.Equals(other.UserName);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Credentials other))
            {
                return false;
            }

            return ReferenceEquals(this, obj) || Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = (int)2166136261;
                hash += (hash * 16777619) ^ (Password?.GetHashCode() ?? 0);
                hash += (hash * 16777619) ^ (UserName?.GetHashCode() ?? 0);
                return hash;
            }
        }

        #endregion
    }
}