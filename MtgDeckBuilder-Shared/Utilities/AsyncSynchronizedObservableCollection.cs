using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Utilities;

namespace SeriusSoft.MtgDeckBuilder.Utilities
{
	public class AsyncSynchronizedObservableCollection<TOuter, TInner> : IList<TOuter>, ICollection, INotifyCollectionChanged, IWeakEventListener, INotifyPropertyChanged
	{
		private Func<TInner, TOuter> Projection { get; set; }
		private AsyncObservableCollection<TOuter> OuterCollection { get; set; }
		private SynchronizationContext _synchronizationContext = SynchronizationContext.Current;

		public AsyncSynchronizedObservableCollection(ObservableCollection<TInner> innerCollection, Func<TInner, TOuter> projection)
		{
			this.Projection = projection;
			this.OuterCollection = new AsyncObservableCollection<TOuter>();
			this.OuterCollection.CollectionChanged += OuterCollectionOnCollectionChanged;
			((INotifyPropertyChanged)this.OuterCollection).PropertyChanged += OuterCollectionOnPropertyChanged;
			ResetCollection(innerCollection);
			CollectionChangedEventManager.AddListener(innerCollection, this);
		}

		public bool ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
		{
			if (managerType != typeof(CollectionChangedEventManager)) 
				return false;

			var args = e as NotifyCollectionChangedEventArgs;
			if (args == null) 
				return false;

			lock (((ICollection)this.OuterCollection).SyncRoot)
			{
				switch (args.Action)
				{
					case NotifyCollectionChangedAction.Add:
						{
							Debug.Assert(args.NewItems.Count == 1);
							var newItem = this.Projection((TInner)args.NewItems[0]);
							this.OuterCollection.Insert(args.NewStartingIndex, newItem);
						}
						break;

					case NotifyCollectionChangedAction.Remove:
						Debug.Assert(args.OldItems.Count == 1);
						this.OuterCollection.RemoveAt(args.OldStartingIndex);
						break;

					case NotifyCollectionChangedAction.Replace:
						{
							Debug.Assert(args.OldItems.Count == 1);
							this.OuterCollection.RemoveAt(args.OldStartingIndex);

							Debug.Assert(args.NewItems.Count == 1);
							var newItem = this.Projection((TInner)args.NewItems[0]);
							this.OuterCollection.Insert(args.NewStartingIndex, newItem);
						}
						break;

					case NotifyCollectionChangedAction.Move:
						Debug.Assert(args.OldItems.Count == 1);
						this.OuterCollection.Move(args.OldStartingIndex, args.NewStartingIndex);
						break;

					case NotifyCollectionChangedAction.Reset:
						ResetCollection(sender as ObservableCollection<TInner>);
						break;
				}	//end switch(args.action)
			}	//end lock
			return true;
		}

		private void OuterCollectionOnPropertyChanged(object sender, PropertyChangedEventArgs args)
		{
			if (SynchronizationContext.Current == _synchronizationContext)
			{
				// Execute the CollectionChanged event on the current thread
				CallPropertyChanged(args);
			}
			else
			{
				// Raises the CollectionChanged event on the creator thread
				_synchronizationContext.Send(CallPropertyChanged, args);
			}
		}

		private void OuterCollectionOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
		{
			if (SynchronizationContext.Current == _synchronizationContext)
			{
				// Execute the CollectionChanged event on the current thread
				CallCollectionChanged(args);
			}
			else
			{
				// Raises the CollectionChanged event on the creator thread
				_synchronizationContext.Send(CallCollectionChanged, args);
			}
		}

		private void CallPropertyChanged(object param)
		{
			if (this.PropertyChanged == null)
				return;
			var args = param as PropertyChangedEventArgs;
			Application.Current.Dispatcher.Invoke((Action)delegate()
			{
				this.PropertyChanged(this, args);
			});
		}

		private void CallCollectionChanged(object param)
		{
			if (this.CollectionChanged == null)
				return;
			var args = param as NotifyCollectionChangedEventArgs;
			Application.Current.Dispatcher.Invoke((Action)delegate()
			{
				this.CollectionChanged(this, args);
			});
		}

		private void ResetCollection(Collection<TInner> innerCollection)
		{
			this.OuterCollection.Clear();
			lock (((ICollection)innerCollection).SyncRoot)
			{
				foreach (var item in innerCollection)
				{
					this.OuterCollection.Add(this.Projection(item));
				}
			}
		}

		public IEnumerator<TOuter> GetEnumerator()
		{
			return this.OuterCollection.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public event NotifyCollectionChangedEventHandler CollectionChanged;
		public event PropertyChangedEventHandler PropertyChanged;

		[Obsolete("This operation is not supported because the collection is read-only.", error: true)]
		void ICollection<TOuter>.Add(TOuter item)
		{
			throw new NotSupportedException("This operation is not supported because the collection is read-only.");
		}

		[Obsolete("This operation is not supported because the collection is read-only.", error: true)]
		void ICollection<TOuter>.Clear()
		{
			throw new NotSupportedException("This operation is not supported because the collection is read-only.");
		}

		public bool Contains(TOuter item)
		{
			return this.OuterCollection.Contains(item);
		}

		public void CopyTo(TOuter[] array, int arrayIndex)
		{
			this.OuterCollection.CopyTo(array, arrayIndex);
		}

		[Obsolete("This operation is not supported because the collection is read-only.", error: true)]
		bool ICollection<TOuter>.Remove(TOuter item)
		{
			throw new NotSupportedException("This operation is not supported because the collection is read-only.");
		}

		void ICollection.CopyTo(Array array, int index)
		{
			this.CopyTo((TOuter[])array, index);
		}

		public int Count { get { return this.OuterCollection.Count; } }
		public object SyncRoot { get { return ((ICollection)this.OuterCollection).SyncRoot; } }
		public bool IsSynchronized { get { return true; } }
		public bool IsReadOnly { get { return true; } }

		public int IndexOf(TOuter item)
		{
			return this.OuterCollection.IndexOf(item);
		}

		[Obsolete("This operation is not supported because the collection is read-only.", error: true)]
		void IList<TOuter>.Insert(int index, TOuter item)
		{
			throw new NotSupportedException("This operation is not supported because the collection is read-only.");
		}

		[Obsolete("This operation is not supported because the collection is read-only.", error: true)]
		void IList<TOuter>.RemoveAt(int index)
		{
			throw new NotSupportedException("This operation is not supported because the collection is read-only.");
		}

		public TOuter this[int index]
		{
			get { return this.OuterCollection[index]; }
		}

		TOuter IList<TOuter>.this[int index]
		{
			get { return this[index]; }
			set { throw new NotSupportedException("This operation is not supported because the collection is read-only."); }
		}
	}
}
