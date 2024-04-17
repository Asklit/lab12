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
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace lab12
{
    public class MyList<T> where T : IInit, ICloneable, new()
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
            T data = new T();
            data.RandomInit();
            begin = new Point<T>(data);
            end = begin;
            for (int i = 1; i < len; i++)
            {
                data = new T();
                data.RandomInit();
                AddToBegin(data);
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
            begin = new Point<T>((T)collection[0].Clone());
            end = begin;
            for (int i = 1; i < collection.Length; i++)
            {
                AddToEnd((T)collection[i].Clone());
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
        /// Добавление в начало списка
        /// </summary>
        /// <param name="item">Данные для добавления</param>
        public void AddToBegin(T item)
        {
            T newData = (T)item.Clone();
            Point<T> newItem = new Point<T>(newData);
            count++;
            if (begin != null)
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

        public void AddOddItems()
        {
            Point<T> current = begin;
            Point<T> randomItem;
            T data;
            while (current != null)
            {
                data = new T();
                data.RandomInit();
                randomItem = new Point<T>(data);
                if (current == begin)
                {
                    AddToBegin(randomItem.Data);
                    current = current.Next;
                }
                else
                {
                    count++;
                    randomItem.Prev = current.Prev;
                    randomItem.Next = current;
                    current.Prev.Next = randomItem;
                    current.Prev = randomItem;
                    current = current.Next;
                }
            }
            if (count != 0)
            {
                data = new T();
                data.RandomInit();
                randomItem = new Point<T>(data);
                randomItem.Prev = end;
                randomItem.Prev.Next = randomItem;
                end = randomItem;
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

        public bool RemoveItemById(int id)
        {
            Point<T>? current = begin;
            if (current.Data is not MusicalInstrument)
            {
                throw new Exception("Тип данных не имеет поля id");
            }
            while (current != null)
            {
                if ((current.Data as MusicalInstrument).Id.Number == id) break;
                current = current.Next;
            }
            if (current == null)
                return false;
            else
            {
                while (current != null)
                {
                    RemoveItem(current.Data);
                    current = current.Next;
                }
            }
            return true;
        }
    }
}
