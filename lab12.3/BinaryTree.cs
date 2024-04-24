using Musical_Instrument;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace lab12._3
{
    public class MyTree<T> where T : IInit, IComparable, new()
    {
        /// <summary>
        /// Вершина дерева
        /// </summary>
        public Point<T> root = null;

        /// <summary>
        /// Количество элементов в дереве
        /// </summary>
        int count = 0;

        public int Count => count;

        /// <summary>
        /// Констурктор создания сбалансированного дерева заданной длины
        /// </summary>
        /// <param name="len">длина дерева</param>
        public MyTree(int len)
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
        /// Рекурсивная функция создания дерева
        /// </summary>
        /// <param name="len">длина дерева</param>
        /// <returns>Значение поддерева (ссылка на элемент Point)</returns>
        Point<T>? MakeTree(int len)
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
            Point<T>? current = null;
            bool isExist = false;
            while (point != null && !isExist)
            {
                current = point;
                if (point.Data.CompareTo(data) == 0)
                    isExist = true;
                else
                {
                    if (point.Data.CompareTo(data) > 0)
                        point = point.Left;
                    else
                        point = point.Right;
                }
            }
            if (isExist)
                return;
            Point<T> newPoint = new Point<T>(data);
            if (current.Data.CompareTo(data) > 0)
                current.Left = newPoint;
            else
                current.Right = newPoint;
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
        /// Удаление дерева из памяти
        /// </summary>
        public void DeleteTree()
        {
            DeleteRecursive(root);
        }
    }
}
