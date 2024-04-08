using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Musical_Instrument;

namespace lab12
{
    internal class MyList<T> where T : IInit, ICloneable, new()
    {
        public Point<T>? begin = null;
        public Point<T>? end = null;

        public int count = 0;

        public Point<T> MakeRandomData()
        {
            T data = new T();
            data.RandomInit();
            return new Point<T>(data);
        }

        public T MakeRandomItem()
        {
            T data = new T();
            data.RandomInit();
            return data;
        }

        public void AddToBegin(T item)
        {
            T newData = (T)item.Clone();
            Point<T> newItem = new Point<T>(newData);
            count++;
            if ( begin != null ) 
            {
                begin.Prev = newItem;
                newItem.Next = begin;
                begin = newItem;
            }
            else
            {
                begin = newItem;
                end = begin;
            }
        }

        public void AddToEnd(T item)
        {
            T newData = (T)item.Clone();
            Point<T> newItem = new Point<T>(newData);
            count++;
            if (end != null)
            {
                end.Next = newItem;
                newItem.Prev = end;
                end = newItem;
            }
            else
            {
                begin = newItem;
                end = begin;
            }
        }

        public MyList() { }

        public MyList(int len) 
        {
            if (len <= 0) throw new Exception("длина меньше нуля");
            begin = MakeRandomData();
            end = begin;
            for (int i = 1; i < len; i++)
            {
                T newItem = MakeRandomItem();
                AddToBegin(newItem);
            }
            count = len;
        }

        public MyList(MyList<T> list)
        {
            if (list == null) throw new Exception("пустая коллекция");
            if (list.count == 0) throw new Exception("пустая коллекция");
            Point<T> current = list.begin;
            for (int i = 0; current != null; i++)
            {
                AddToEnd(current.Data);
                current = current.Next;
            }
        }

        public MyList(T[] collection)
        {
            if (collection == null) throw new Exception("пустая коллекция");
            if (collection.Length == 0) throw new Exception("пустая коллекция");
            T newData = (T)collection.Clone();
            begin = new Point<T>(newData);
            end = begin;
            for (int i = 0; i < collection.Length; i++)
            {
                AddToEnd(collection[i]);
            }
        }

        public void PrintList()
        {
            Console.WriteLine();
            if (count == 0)
                Console.WriteLine("Список пуст");
            Point<T>? current = begin;
            for (int i = 1; current != null; i++)
            {
                Console.WriteLine($"{i}. {current}");
                current = current.Next;
            }
        }

        public Point<T>? FindItem(T item)
        {
            Point<T>? current = begin;
            while (current != null)
            {
                if (current.Data == null) throw new Exception("элемент не найден");
                if (current.Data.Equals(item)) return current;
                current = current.Next;
            }
            return null;
        }

        public bool RemoveItem(T item)
        {
            if (begin == null) throw new Exception("список пустой");
            Point<T>? pos = FindItem(item);
            if (pos == null) return false;
            count--;
            if (begin == end)
            {
                begin = end = null;
                return true;
            }
            if (pos.Prev == null)
            {
                begin = begin?.Next;
                begin.Prev = null;
                return true;
            }
            if (pos.Next == null)
            {
                end = end?.Prev;
                end.Next = null;
                return true;
            }
            Point<T>? next = pos.Next;
            Point<T>? prev = pos.Prev;
            pos.Next.Prev = prev;
            pos.Prev.Next = next;
            return true;
        }
    }
}
