using lab12._3;
using lab12._4;
using Musical_Instrument;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace lab13
{
    public delegate void CollectionHandler(object source, CollectionHandlerEventArgs args);

    public class MyObservableCollection<T> : MyCollection<T> where T : IInit, ICloneable, IComparable, new()
    {
        public string NameCollection;

        public MyObservableCollection(string name = "Бинарное дерево") : base() { NameCollection = name; }
        public MyObservableCollection(int len, string name = "Бинарное дерево") : base(len) { NameCollection = name; }

        public event CollectionHandler? CollectionCountChanged;
        public event CollectionHandler? CollectionReferenceChanged;

        public void RegisterCountChangedHandler(CollectionHandler handler) => CollectionCountChanged += handler;
        public void RegisterReferenceChangedHandler(CollectionHandler handler) => CollectionReferenceChanged += handler;

        private void OnCollectionCountChanged(object source, CollectionHandlerEventArgs args) => CollectionCountChanged?.Invoke(source, args);

        private void OnCollectionReferenceChanged(object source, CollectionHandlerEventArgs args) => CollectionReferenceChanged?.Invoke(source, args);

        public Point<T> this[T item]
        {
            get
            {
                return GetItem(item);
            }
            set
            {
                bool isValueInCollection = Contains(value.Data);
                if (!isValueInCollection)
                {
                    bool isItemDeleted = false;
                    RecursiveRemove(root, new Point<T>(item), ref isItemDeleted);
                    if (isItemDeleted)
                    {
                        OnCollectionReferenceChanged(this, new(NameCollection, "Элемент изменен в дереве", item));
                        AddPointToFindTree(value.Data, root);
                        count++;
                    }
                }
            }
        }

        public new void Add(T item)
        {
            base.Add(item);
            OnCollectionCountChanged(this, new(NameCollection, "Элемент успешно добавлен", item));
        }

        public new bool Remove(T item)
        {
            bool flag = base.Remove(item);
            if (flag)
                OnCollectionCountChanged(this, new(NameCollection, "Элемент успешно удален", item));
            return flag;
        }
    }
}
