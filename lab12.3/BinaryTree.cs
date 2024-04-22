using Musical_Instrument;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace lab12._3
{
    internal class Tree<T> where T : IInit, IComparable, new()
    {
        /// <summary>
        /// Вершина дерева
        /// </summary>
        Point<T> root = null;

        /// <summary>
        /// Количество элементов в дереве
        /// </summary>
        int count = 0;

        public int Count => count;

        /// <summary>
        /// Констурктор создания сбалансированного дерева заданной длины
        /// </summary>
        /// <param name="len">длина дерева</param>
        public Tree(int len) 
        {
            count = len;
            root = MakeTree(len);
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

        public void Show()
        {
               
        }

        void ShowRecursiveMethod(Point<T>? point, int spaces = 5)
        {
            if (point != null)
            {
                ShowRecursiveMethod(point.Left, spaces + 5);
                for (int i = 0; i < spaces; i++)
                    Console.WriteLine(" ");
                Console.WriteLine(point.Data);
                ShowRecursiveMethod(point.Right, spaces + 5);
            }
        }
    }
}
