using Musical_Instrument;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace lab12._3
{
    public class MyTree<T> where T : IInit, IComparable, ICloneable, new()
    {
        /// <summary>
        /// Вершина дерева
        /// </summary>
        public Point<T> root = null;

        /// <summary>
        /// Количество элементов в дереве
        /// </summary>
        protected int count = 0;

        public int Count { get => count; set { count = value; } }

        /// <summary>
        /// Констурктор создания сбалансированного дерева заданной длины
        /// </summary>
        /// <param name="len">длина дерева</param>
        public MyTree(int len = 10)
        {
            count = len;
            root = MakeTree(len);
        }

        /// <summary>
        /// Констурктор создания дерева заданной длины с заданной вершиной
        /// </summary>
        /// <param name="len">длина дерева</param>
        public MyTree(int len, Point<T> newRoot)
        {
            count = len;
            root = newRoot;
        }

        /// <summary>
        /// Констурктор клонирования дерева
        /// </summary>
        public MyTree(MyTree<T> tree)
        {
            root = new Point<T>(tree.root);
            count = tree.count;
            root = CloneTree(root, tree.root);
        }

        /// <summary>
        /// Клонирование дерева
        /// </summary>
        Point<T> CloneTree(Point<T> current, Point<T> treeCurrent)
        {   
            if (treeCurrent != null)
            {
                current = new Point<T>(treeCurrent);
                current.Left = CloneTree(current.Left, treeCurrent.Left);
                current.Right = CloneTree(current.Right, treeCurrent.Right);
            }
            return current;
        }

        /// <summary>
        /// Рекурсивная функция создания дерева
        /// </summary>
        /// <param name="len">длина дерева</param>
        /// <returns>Значение поддерева (ссылка на элемент Point)</returns>
        public Point<T>? MakeTree(int len)
        {
            T data = new T();
            data.RandomInit();
            Point<T> newItem = new Point<T>(data);
            if (len == 0) return null;

            int left = len / 2;
            int right = len - left - 1;
            newItem.Left = MakeTree(left);
            newItem.Right = MakeTree(right);
            return newItem;
        }

        [ExcludeFromCodeCoverage]
        /// <summary>
        /// Вызов рекурсивной печати дерева
        /// </summary>
        public void Print()
        {
            PrintRecursiveMethod(root, 0);
        }

        [ExcludeFromCodeCoverage]
        /// <summary>
        /// Рекурсивная печать дерева
        /// </summary>
        /// <param name="point">текущий элемент</param>
        /// <param name="spaces">сдвиг</param>
        void PrintRecursiveMethod(Point<T>? point, int spaces = 5)
        {
            if (point != null)
            {
                PrintRecursiveMethod(point.Left, spaces + 5);
                for (int i = 0; i < spaces; i++)
                    Console.Write(" ");
                Console.WriteLine(point.Data);
                PrintRecursiveMethod(point.Right, spaces + 5);
            }
        }

        /// <summary>
        /// Добавление элемента в дерево поиска
        /// </summary>
        /// <param name="data">значение элемента</param>
        /// <param name="point">текущая точка</param>
        public void AddPointToFindTree(T data, Point<T>? point)
        {
            if (point == null)
            {
                root = new Point<T>(data);
            }
            else
            {
                Point<T>? current = null;
                bool isExist = false;
                Point<T> newPoint = new Point<T>(data);
                while (point != null && !isExist)
                {
                    current = point;
                    if (point.CompareTo(newPoint) == 0)
                        isExist = true;
                    else
                    {
                        if (point.CompareTo(newPoint) > 0)
                            point = point.Left;
                        else
                            point = point.Right;
                    }
                }
                if (isExist)
                    return;
                if (current.CompareTo(newPoint) > 0)
                    current.Left = newPoint;
                else
                    current.Right = newPoint;
            }
        }
        
        /// <summary>
        /// Конвертация ИСД в массив данных
        /// </summary>
        /// <param name="point">текущая точка</param>
        /// <param name="array">массив</param>
        /// <param name="index">текущий индекс массива для заполнения</param>
        public void ConvertTreeToArray(Point<T>? point, T[] array, ref int index)
        {
            if (point != null)
            {
                ConvertTreeToArray(point.Left, array, ref index);
                array[index] = point.Data;
                index++;
                ConvertTreeToArray(point.Right, array, ref index);
            }
        }

        /// <summary>
        /// Конвертация в дерево поиска
        /// </summary>
        /// <returns>Дерево поиска</returns>
        public MyTree<T> ConvertToFindTree()
        {
            T[] array = new T[count];
            int index = 0;
            ConvertTreeToArray(root, array, ref index);

            Point<T> newRoot = new Point<T>(array[0]);
            for (int i = 1; i < array.Length; i++)
                AddPointToFindTree(array[i], newRoot);
            return new MyTree<T>(count, newRoot);
        }

        /// <summary>
        /// Рекурсия поиска максимального элемента
        /// </summary>
        /// <param name="point">текущая точка</param>
        /// <param name="foundPoint">найденный максимальный элемент</param>
        void SeachMaxItem(Point<T> point, ref Point<T> foundPoint)
        {
            if (point != null)
            {
                SeachMaxItem(point.Left, ref foundPoint);
                if (point.CompareTo(foundPoint) > 0)
                    foundPoint = point;
                SeachMaxItem(point.Right, ref foundPoint);
            }
        }

        /// <summary>
        /// Запуск рекурсии поиска максимального элемента
        /// </summary>
        /// <returns>Возвращает максимальный элемент</returns>
        public T GetMaxValue()
        {
            Point<T>  foundPoint = new Point<T>();
            foundPoint.Data = new T();
            SeachMaxItem(root, ref foundPoint);
            return foundPoint.Data;
        }

        /// <summary>
        /// Рекурсивное удаление ссылок между вершинами дерева
        /// </summary>
        /// <param name="point">текущая точка</param>
        Point<T>? DeleteRecursive(Point<T>? point)
        {
            if (point != null)
            {
                point.Left = DeleteRecursive(point.Left);
                point.Data = default;
                point.Right = DeleteRecursive(point.Right);
            }
            return null;
        }

        /// <summary>
        /// Рекусивное удаление 
        /// </summary>
        protected Point<T> RecursiveRemove(Point<T> current, Point<T> item, ref bool flag)
        {
            if (current != null)
            {
                if (current.CompareTo(item) > 0)
                    current.Left = RecursiveRemove(current.Left, item, ref flag);
                if (current.CompareTo(item) < 0)
                    current.Right = RecursiveRemove(current.Right, item, ref flag);
                if (current.CompareTo(item) == 0)
                {
                    count--;
                    Point<T>? point = null;
                    if (current.Left == null || current.Right == null)
                    {
                        if (current.Left == null)
                            point = current.Right;
                        if (current.Right == null)
                            point = current.Left;
                        if (point == null)
                        {
                            current = null;
                        }
                        else
                        {
                            current = point;
                        }
                    }
                    else
                    {
                        Point<T> foundPoint = new Point<T>();
                        foundPoint.Data = new T();
                        SeachMaxItem(current.Left, ref foundPoint);
                        current.Data = foundPoint.Data;
                        bool newFlag = false;
                        current.Left = RecursiveRemove(current.Left, foundPoint, ref newFlag);
                    }
                    flag = true;
                }
            }
            if (count == 0)
                root = null;
            return current;
        }

        /// <summary>
        /// Удаление дерева из памяти
        /// </summary>
        public void DeleteTree()
        {
            DeleteRecursive(root);
            root = null;
            count = 0;
        }

        /// <summary>
        /// Поиск элемента в дереве поиска
        /// </summary>
        protected void RecursiveContainsItemInFindTree(Point<T> current, Point<T> item, ref bool flag)
        {
            if (!flag && current != null)
            {
                if (current.CompareTo(item) == 0)
                {
                    flag = true;
                }
                else if (current.CompareTo(item) > 0)
                {
                    RecursiveContainsItemInFindTree(current.Left, item, ref flag);
                }
                else
                {
                    RecursiveContainsItemInFindTree(current.Right, item, ref flag);
                }
            }
        }

        /// <summary>
        /// Вернуть вершину T
        /// </summary>
        public Point<T> GetItem(T item)
        {
            Point<T> foundPoint = null;

            Point<T> itemForSearch = new Point<T>(item);
            SeachItem(root, itemForSearch, ref foundPoint);
            return foundPoint;
        }

        /// <summary>
        /// Поиск точки с T
        /// </summary>
        void SeachItem(Point<T> current, Point<T> itemForSearch, ref Point<T> foundPoint)
        {
            if (foundPoint == null && current != null)
            {
                if (current.CompareTo(itemForSearch) == 0)
                {
                    foundPoint = current;
                }
                else if (current.CompareTo(itemForSearch) > 0)
                {
                    SeachItem(current.Right, itemForSearch, ref foundPoint);
                }
                else
                {
                    SeachItem(current.Left, itemForSearch, ref foundPoint);
                }
            }
        }
    }
}
