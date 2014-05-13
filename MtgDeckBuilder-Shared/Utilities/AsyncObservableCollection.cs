using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Utilities
{
	/// <summary>
	/// http://www.thomaslevesque.com/2009/04/17/wpf-binding-to-an-asynchronous-collection/
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class AsyncObservableCollection<T> : ObservableCollection<T>, IDisposable
	{
		private SynchronizationContext _synchronizationContext = SynchronizationContext.Current;

		/// <summary>
		/// This is a read-write locker. This will enable the UI thread to continue to watch this (even as changes are occurring) while changes are being made.
		/// </summary>
		private ReaderWriterLockSlim Locker = new ReaderWriterLockSlim();
		public AsyncObservableCollection()
		{
		}

		public AsyncObservableCollection(IEnumerable<T> list)	: base(list)
		{
		}

		protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
		{
			if (SynchronizationContext.Current == _synchronizationContext)
			{
				// Execute the CollectionChanged event on the current thread
				RaiseCollectionChanged(e);
			}
			else
			{
				// Raises the CollectionChanged event on the creator thread
				_synchronizationContext.Send(RaiseCollectionChanged, e);
			}
		}

		private void RaiseCollectionChanged(object param)
		{
			// We are in the creator thread, call the base implementation directly
			base.OnCollectionChanged((NotifyCollectionChangedEventArgs)param);
		}

		protected override void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			if (SynchronizationContext.Current == _synchronizationContext)
			{
				// Execute the PropertyChanged event on the current thread
				RaisePropertyChanged(e);
			}
			else
			{
				// Raises the PropertyChanged event on the creator thread
				_synchronizationContext.Send(RaisePropertyChanged, e);
			}
		}

		private void RaisePropertyChanged(object param)
		{
			// We are in the creator thread, call the base implementation directly
			base.OnPropertyChanged((PropertyChangedEventArgs)param);
		}

		#region Thread Safety

		protected override void ClearItems()
		{
			this.Locker.EnterWriteLock();
			try
			{
				base.ClearItems();
			}
			finally
			{
				this.Locker.ExitWriteLock();
			}
		}

		protected override void InsertItem(int index, T item)
		{
			this.Locker.EnterWriteLock();
			try
			{
				base.InsertItem(index, item);
			}
			finally
			{
				this.Locker.ExitWriteLock();
			}
		}

		protected override void MoveItem(int oldIndex, int newIndex)
		{
			this.Locker.EnterWriteLock();
			try
			{
				base.MoveItem(oldIndex, newIndex);
			}
			finally
			{
				this.Locker.ExitWriteLock();
			}
		}

		protected override void RemoveItem(int index)
		{
			this.Locker.EnterWriteLock();
			try
			{
				base.RemoveItem(index);
			}
			finally
			{
				this.Locker.ExitWriteLock();
			}
		}

		protected override void SetItem(int index, T item)
		{
			this.Locker.EnterWriteLock();
			try
			{
				base.SetItem(index, item);
			}
			finally
			{
				this.Locker.ExitWriteLock();
			}
		}

		public new T this[int index]
		{
			get
			{
				this.Locker.EnterReadLock();
				try
				{
					return base[index];
				}
				finally
				{
					this.Locker.ExitReadLock();
				}
			}
			set
			{
				this.Locker.EnterWriteLock();
				try
				{
					var newValue = value;
					base[index] = newValue;
				}
				finally
				{
					this.Locker.ExitWriteLock();
				}
			}
		}

		#endregion  Thread Safety
		public void Dispose()
		{
			if (Locker != null)
			{
				this.Locker.Dispose();
				this.Locker = null;
			}
		}
	}
}
