using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics.CodeAnalysis;
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
        /// <summary>
        /// Начало списка
        /// </summary>
        public Point<T>? begin = null;

        /// <summary>
        /// Конец списка
        /// </summary>
        public Point<T>? end = null;

        /// <summary>
        /// Длина списка
        /// </summary>
        public int count = 0;

        /// <summary>
        /// Заполнение элемента спика рандомными данными
        /// </summary>
        /// <returns>Элемент списка</returns>
        [ExcludeFromCodeCoverage]
        public Point<T> MakeRandomData()
        {
            T data = new T();
            data.RandomInit();
            return new Point<T>(data);
        }

        /// <summary>
        /// Заполнение рандомных данных
        /// </summary>
        /// <returns>Рандомные данные точки элемента списка</returns>
        [ExcludeFromCodeCoverage]
        public T MakeRandomItem()
        {
            T data = new T();
            data.RandomInit();
            return data;
        }

        /// <summary>
        /// Добавление в начало списка
        /// </summary>
        /// <param name="item">Данные для добавления</param>
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

        /// <summary>
        /// Добавление в конец спика
        /// </summary>
        /// <param name="item">Данные для добавления</param>
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

        /// <summary>
        /// Создание пустого списка
        /// </summary>
        public MyList() { }

        /// <summary>
        /// Создание списка заданной длины с рандомными значениями
        /// </summary>
        /// <param name="len">Длина списка</param>
        /// <exception cref="Exception">список пуст</exception>
        public MyList(int len) 
        {
            if (len <= 0) throw new Exception("список пуст");
            begin = MakeRandomData();
            end = begin;
            for (int i = 1; i < len; i++)
            {
                T newItem = MakeRandomItem();
                AddToBegin(newItem);
            }
            count = len;
        }

        /// <summary>
        /// Создание клона списка
        /// </summary>
        /// <param name="list">список</param>
        /// <exception cref="Exception">список пуст</exception>
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

        /// <summary>
        /// Создание списка из массива Т
        /// </summary>
        /// <param name="collection">массив</param>
        /// <exception cref="Exception">исключение при передаче пустой коллекции</exception>
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

        /// <summary>
        /// Вывод информации элементов списка
        /// </summary>
        [ExcludeFromCodeCoverage]
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

        /// <summary>
        /// Поиск элемента списка
        /// </summary>
        /// <param name="item">информация хранящаяся в этом элементе</param>
        /// <returns>Найденный элемент или null</returns>
        /// <exception cref="Exception">Элемент не найден</exception>
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

        /// <summary>
        /// Удаление элемента
        /// </summary>
        /// <param name="item">информация хранящаяся в этом элементе</param>
        /// <returns>true/false - информация удалена или нет</returns>
        /// <exception cref="Exception">пустой список</exception>
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
