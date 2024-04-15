using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab12._2
{
    public class HashItem<TKey>
    {
        public int HashCode { get; set; }
        public TKey Key { get; set; }

        public HashItem()
        {
            this.HashCode = -1;
            this.Key = default;
        }

        /// <summary>
        /// Создание объекта с переданным ключем
        /// </summary>
        public HashItem(TKey key)
        {
            this.HashCode = key.GetHashCode();
            this.Key = key;
        }
    }
}
