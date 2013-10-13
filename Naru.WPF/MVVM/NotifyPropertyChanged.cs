﻿using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace Naru.WPF.MVVM
{
    /// <summary>
    /// Base class for items that support property notification.
    /// Taken from PRISM.
    /// </summary>
    /// 
    /// <remarks>
    /// This class provides basic support for implementing the <see cref="T:System.ComponentModel.INotifyPropertyChanged"/> interface and for
    ///             marshalling execution to the UI thread.
    /// 
    /// </remarks>
    public abstract class NotifyPropertyChanged : INotifyPropertyChanged
    {
        /// <summary>
        /// Raised when a property on this object has a new value.
        /// 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises this object's PropertyChanged event.
        /// 
        /// </summary>
        /// <param name="propertyName">The property that has a new value.</param>
        protected virtual void RaisePropertyChanged(string propertyName)
        {
            var changedEventHandler = PropertyChanged;
            if (changedEventHandler == null)
            {
                return;
            }

            changedEventHandler(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Raises this object's PropertyChanged event for each of the properties.
        /// </summary>
        /// <param name="propertyNames">The properties that have a new value.</param>
        protected void RaisePropertyChanged(params string[] propertyNames)
        {
            if (propertyNames == null)
            {
                throw new ArgumentNullException("propertyNames");
            }

            foreach (var propertyName in propertyNames)
            {
                RaisePropertyChanged(propertyName);
            }
        }

        /// <summary>
        /// Raises this object's PropertyChanged event.
        /// 
        /// </summary>
        /// <typeparam name="T">The type of the property that has a new value</typeparam>
        /// <param name="propertyExpression">A Lambda expression representing the property that has a new value.</param>
        protected void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            RaisePropertyChanged(PropertyExtensions.ExtractPropertyName(propertyExpression));
        }
    }
}