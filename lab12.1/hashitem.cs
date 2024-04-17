using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab12._2
{
    public class Item<TKey, TValue>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }

        /// <summary>
        /// Создание объекта с переданным ключем и значением
        /// </summary>
        public Item(TKey key, TValue value)
        {
            this.Key = key;
            this.Value = value;
        }

        public override int GetHashCode()
        {
            return Key.GetHashCode();
        }
    }
}
