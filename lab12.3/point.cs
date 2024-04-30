using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab12._3
{
    public class Point<T> : IComparable where T : IComparable
    {
        /// <summary>
        /// Информация объекта
        /// </summary>
        public T? Data { get; set; }

        /// <summary>
        /// Левое поддерево
        /// </summary>
        public Point<T>? Left { get; set; }

        /// <summary>
        /// Правое поддерево
        /// </summary>
        public Point<T>? Right { get; set; }

        /// <summary>
        /// Создание объекта с данными по умолчанию
        /// </summary>
        public Point()
        {
            this.Data = default(T);
            this.Left = null;
            this.Right = null;
        }

        /// <summary>
        /// Создание объекта с данными указанными данными
        /// </summary>
        /// <param name="data"></param>
        public Point(T data)
        {
            this.Data = data;
            this.Left = null;
            this.Right = null;
        }

        /// <summary>
        /// Информация о объекте в виде string
        /// </summary>
        public override string? ToString()
        {
            return Data == null ? "" : Data.ToString();
        }

        public int CompareTo(object other)
        {
            if (other is Point<T>)
                return Data.CompareTo(((Point<T>)other).Data);
            else throw new Exception("Некорректный тип данных");
        }
    }
}
