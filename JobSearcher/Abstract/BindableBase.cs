using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using JobSearcher.Properties;

namespace JobSearcher.Abstract
{
    /// <inheritdoc cref="INotifyPropertyChanged" />
    /// <summary>
    ///     Implementation of <see cref="T:System.ComponentModel.INotifyPropertyChanged" /> to simplify models.
    /// </summary>
    internal abstract class BindableBase : INotifyPropertyChanged
    {
        #region Fields

        private readonly Dictionary<string, object> _propertyBackingDictionary = new Dictionary<string, object>();

        #endregion

        #region Events and Delegates

        /// <inheritdoc />
        /// <summary>
        ///     Multicast event for property change notifications.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Public Methods

        public override string ToString()
        {
            var coll = TypeDescriptor.GetProperties(this);
            var builder = new StringBuilder();
            foreach (PropertyDescriptor pd in coll)
            {
                builder.Append($"{pd.Name} : {pd.GetValue(this)}");
            }

            return builder.ToString();
        }

        #endregion

        #region Protected Methods

        protected T Get<T>([CallerMemberName] string propertyName = null)
        {
            if (propertyName == null)
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            if (_propertyBackingDictionary.TryGetValue(propertyName, out var value))
            {
                return (T)value;
            }

            return default(T);
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool Set<T>(T newValue, [CallerMemberName] string propertyName = null)
        {
            if (propertyName == null)
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            if (EqualityComparer<T>.Default.Equals(newValue, Get<T>(propertyName)))
            {
                return false;
            }

            _propertyBackingDictionary[propertyName] = newValue;
            OnPropertyChanged(propertyName);
            return true;
        }

        #endregion
    }
}