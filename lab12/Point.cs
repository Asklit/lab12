using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace lab12
{
    public class Point<T> where T : ICloneable
    {
        /// <summary>
        /// Информация объекта
        /// </summary>
        public T? Data { get; set; }
        /// <summary>
        /// Следующий объект списка
        /// </summary>
        public Point<T>? Next { get; set; }
        /// <summary>
        /// Предыдущий объект списка
        /// </summary>
        public Point<T>? Prev { get; set; }

        /// <summary>
        /// Создание объекта с данными по умолчанию
        /// </summary>
        public Point()
        {
            this.Data = default(T);
            this.Next = null;
            this.Prev = null;
        }

        /// <summary>
        /// Создание объекта с данными указанными данными
        /// </summary>
        /// <param name="data"></param>
        public Point(T data)
        {
            this.Data = data;
            this.Next = null;
            this.Prev = null;
        }

        /// <summary>
        /// Информация о объекте в виде string
        /// </summary>
        public override string? ToString()
        {
            return Data == null ? "" : Data.ToString();
        }

        /// <summary>
        /// Клонирование информации объекта
        /// </summary>
        /// <returns>склонированная точка</returns>
        public Point<T> Clone()
        {
            T newData = (T)Data.Clone();
            Point<T> newPoint = new Point<T>(newData);
            return newPoint;
        }
    }
}
