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

namespace lab13
{
    delegate void Handler();

    internal class MyObservableCollection<T> : MyCollection<T> where T : IInit, ICloneable, IComparable, new()
    {
        public MyObservableCollection() : base() { }
        public MyObservableCollection(int len) : base(len) { }
        public MyObservableCollection(MyObservableCollection<T> collection) : base(collection) { }
        public MyObservableCollection(int len, Point<T> root) : base(len, root) { }

        public int Length 
        {
            get => count;
        }

        public T this[int index]
        {
            get
            {
                if (index < count)
                {
                    int ind = 0;
                    foreach (var item in this)
                    {
                        if (ind == index)
                        {
                            return item;
                        }
                        ind++;
                    }
                }
                return default(T);
            }
            set
            {
                if (index < count)
                {
                    int ind = 0;
                    bool flag = false;
                    foreach (var item in this)
                    {
                        if (ind == index)
                        {
                            RecursiveRemove(root, new Point<T>(item), ref flag);
                            break;
                        }
                        ind++;
                    }
                    if (flag)
                    {
                        Add(value);
                    }
                }
                else
                {
                    throw new Exception("Len error");
                }
            }
        }


    }
}
