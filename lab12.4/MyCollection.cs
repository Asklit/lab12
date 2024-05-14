using lab12._3;
using Musical_Instrument;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace lab12._4
{
    public class MyCollection<T> : MyTree<T>, IEnumerable<T>, ICollection<T> where T: IInit, ICloneable, IComparable, new()
    {
        public MyCollection() : base() { }
        public MyCollection(int len) : base(len) { }
        /// public MyCollection(MyCollection<T> collection) : base(collection) { }
        public MyCollection(int len, Point<T> root) : base(len, root) { }

        public bool IsReadOnly => throw new NotImplementedException();

        /// <summary>
        /// Функция добавление элементов в дерево поиска
        /// </summary>
        /// <param name="item"></param>
        public void Add(T item)
        {
            count++;
            AddPointToFindTree(item, root);
        }

        /// <summary>
        /// Функция очищения памяти
        /// </summary>
        public void Clear()
        {
            DeleteTree();
        }

        /// <summary>
        /// Проверка наличия элемента в дереве
        /// </summary>
        /// <returns>True/False</returns>
        public bool Contains(T item)
        {
            foreach (var elem in this)
                if (elem.Equals(item)) return true;
            return false;
        }

        /// <summary>
        /// Конвертация в дерево поиска
        /// </summary>
        /// <returns>Дерево поиска</returns>
        public new MyCollection<T> ConvertToFindTree()
        {
            T[] array = new T[Count];
            int index = 0;
            ConvertTreeToArray(root, array, ref index);

            Point<T> newRoot = new Point<T>(array[0]);
            for (int i = 1; i < array.Length; i++)
                AddPointToFindTree(array[i], newRoot);
            return new MyCollection<T>(Count, newRoot);
        }

        /// <summary>
        /// Преобразование информации в массив
        /// </summary>
        public void CopyTo(T[] array, int arrayIndex)
        {
            int index = 0;
            foreach (T item in this)
            {
                array[index] = item;
                index++;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return GetData(root).GetEnumerator();
        }

        /// <summary>
        /// Функция удаления элемента
        /// </summary>
        /// <returns>True/False - удален элемент или нет</returns>
        public bool Remove(T item)
        {
            bool flag = false;
            Point<T> point = new Point<T>(item);
            RecursiveRemove(root, point, ref flag);
            return flag;
        }

        /// <summary>
        /// Релизация IEnumerable
        /// </summary>
        private IEnumerable<T> GetData(Point<T> point)
        {
            if (point != null)
            {
                foreach (var item in GetData(point.Left))
                {
                    yield return item;
                }
                yield return point.Data;
                foreach (var item in GetData(point.Right))
                { 
                    yield return item; 
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
