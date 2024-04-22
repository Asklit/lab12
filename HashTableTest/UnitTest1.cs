using Musical_Instrument;
using lab12_2;
using lab12._2;
using Xunit;
using Microsoft.VisualStudio.TestPlatform.Common.Interfaces;

namespace HashTableTest
{
    public class UnitTest1
    {
        /// <summary>
        /// Проверка длины при создании хештаблицы
        /// </summary>
        [Fact]
        public void CheckLen()
        {
            MyHashtable<MusicalInstrument, Guitar>? ht = new MyHashtable<MusicalInstrument, Guitar>(4);
            for (int i = 0; i < 4; i++) 
            {
                MusicalInstrument mi = new MusicalInstrument();
                mi.RandomInit();
                Guitar guitar = new Guitar();
                guitar.RandomInit();
                ht.AddData(mi, guitar);
            }
            Assert.Equal(4, ht.Count);
        }

        /// <summary>
        /// Проверка коллизии
        /// </summary>
        [Fact]
        public void CheckAdd()
        {
            MyHashtable<MusicalInstrument, Guitar>? ht = new MyHashtable<MusicalInstrument, Guitar>(4);
            MusicalInstrument mi = new MusicalInstrument();
            mi.RandomInit();
            Guitar guitar1 = new Guitar();
            guitar1.RandomInit();
            Guitar guitar2 = new Guitar();
            guitar2.RandomInit();
            Assert.True(ht.AddData(mi, guitar1));
            Assert.True(ht.AddData(mi, guitar2));
            int index = ht.GetIndex(mi);
            Assert.Equal(mi, ht.Items[index].Key);
            Assert.Equal(guitar1, ht.Items[index].Value);
            if (index == ht.Capacity - 1)
                index = -1;
            Assert.Equal(mi, ht.Items[index + 1].Key);
            Assert.Equal(guitar2, ht.Items[index + 1].Value);
        }

        /// <summary>
        /// Проверка добаления абсолютно идентичного элемента
        /// </summary>
        [Fact]
        public void CheckAddExistingItem()
        {
            MyHashtable<MusicalInstrument, Guitar>? ht = new MyHashtable<MusicalInstrument, Guitar>(4);
            MusicalInstrument mi = new MusicalInstrument();
            mi.RandomInit();
            Guitar guitar = new Guitar();
            guitar.RandomInit();
            Assert.True(ht.AddData(mi, guitar));
            Assert.False(ht.AddData(mi, guitar));
        }

        /// <summary>
        /// Поиск первого элемента из хештаблицы
        /// </summary>
        [Fact]
        public void CheckFindFirstElem()
        {
            MyHashtable<MusicalInstrument, Guitar>? ht = new MyHashtable<MusicalInstrument, Guitar>(8);
            for (int i = 0; i < 7; i++)
            {
                var mi = new MusicalInstrument();
                mi.RandomInit();
                var guitar = new Guitar();
                guitar.RandomInit();
                ht.AddData(mi, guitar);
            }
            int index = 0;
            while (ht.Items[index] == null)
                index++;
            Assert.Equal(ht.FindKeyByData(ht.Items[index].Key.Id.Number, ht.Items[index].Key.Name).Key, ht.Items[index].Key);
            Assert.Equal(ht.FindKeyByData(ht.Items[index].Key.Id.Number, ht.Items[index].Key.Name).Value, ht.Items[index].Value);
        }

        /// <summary>
        /// Поиск последнего элемента из хештаблицы
        /// </summary>
        [Fact]
        public void CheckFindEndElem()
        {
            MyHashtable<MusicalInstrument, Guitar>? ht = new MyHashtable<MusicalInstrument, Guitar>(8);
            for (int i = 0; i < 7; i++)
            {
                var mi = new MusicalInstrument();
                mi.RandomInit();
                var guitar = new Guitar();
                guitar.RandomInit();
                ht.AddData(mi, guitar);
            }
            int index = ht.Capacity-1;
            while (ht.Items[index] == null)
                index--;
            Assert.Equal(ht.FindKeyByData(ht.Items[index].Key.Id.Number, ht.Items[index].Key.Name).Key, ht.Items[index].Key);
            Assert.Equal(ht.FindKeyByData(ht.Items[index].Key.Id.Number, ht.Items[index].Key.Name).Value, ht.Items[index].Value);
        }

        /// <summary>
        /// Поиск элемента сдвинутого с конца в начало таблицы из за коллизии
        /// </summary>
        [Fact]
        public void CheckFindCollitionItemOnBegin()
        {
            MyHashtable<MusicalInstrument, Guitar>? ht = new MyHashtable<MusicalInstrument, Guitar>(8);
            
            for (int i = 0; i < 5; i++)
            {
                var mi = new MusicalInstrument();
                mi.RandomInit();
                var guitar = new Guitar();
                guitar.RandomInit();
                ht.AddData(mi, guitar);
            }
            while (ht.Items[ht.Capacity-1] == null)
            {
                var mi = new MusicalInstrument();
                mi.RandomInit();
                var guitar = new Guitar();
                guitar.RandomInit();
                ht.AddData(mi, guitar);
            }
            var instr = new MusicalInstrument();
            instr.RandomInit();
            var newguitar = new Guitar();
            newguitar.RandomInit();
            while (ht.Items[ht.Capacity-1].GetHashCode() % ht.Capacity != instr.GetHashCode()%ht.Capacity)
                instr.RandomInit();
            ht.AddData(instr, newguitar);
            Item<MusicalInstrument, Guitar> item = ht.FindKeyByData(instr.Id.Number, instr.Name);
            Assert.Equal(item.Key, instr);
            Assert.Equal(item.Value, newguitar);
        }

        /// <summary>
        /// Проверка удаления из пустой таблицы
        /// </summary>
        [Fact]
        public void CheckDeleteFromZeroHashTable()
        {
            MyHashtable<MusicalInstrument, Guitar>? ht = new MyHashtable<MusicalInstrument, Guitar>(4);
            MusicalInstrument mi = new MusicalInstrument();
            mi.RandomInit();
            Guitar guitar = new Guitar();
            guitar.RandomInit();
            ht.AddData(mi, guitar);
            int index = ht.GetIndex(mi);
            ht.RemoveData(new Item<MusicalInstrument, Guitar>(mi, guitar));
            Assert.Null(ht.Items[index]);
        }

        /// <summary>
        /// Проверка удаления первого элемента с одинаковым индексом с учетом коллизии
        /// </summary>
        [Fact]
        public void CheckDeleteFirstCollisionItem()
        {
            MyHashtable<MusicalInstrument, Guitar>? ht = new MyHashtable<MusicalInstrument, Guitar>(2);
            for (int i = 0; i < 4; i++)
            {
                var insturument = new MusicalInstrument();
                insturument.RandomInit();
                var guitar = new Guitar();
                guitar.RandomInit();
                ht.AddData(insturument, guitar);
            }

            MusicalInstrument mi1 = new MusicalInstrument();
            mi1.RandomInit();
            MusicalInstrument mi2 = new MusicalInstrument();
            mi2.RandomInit();
            Guitar guitar1 = new Guitar();
            guitar1.RandomInit();
            Guitar guitar2 = new Guitar();
            guitar2.RandomInit();
            ht.AddData(mi1, guitar1);
            ht.AddData(mi2, guitar2);

            Assert.True(ht.RemoveData(new Item<MusicalInstrument, Guitar>(mi1, guitar1)));
            Assert.Null(ht.FindKeyByData(mi1.Id.Number, mi1.Name));
            Assert.Equal(ht.FindKeyByData(mi2.Id.Number, mi2.Name).Key, mi2);
            Assert.Equal(ht.FindKeyByData(mi2.Id.Number, mi2.Name).Value, guitar2);
        }

        /// <summary>
        /// Проверка удаления второго элемента с одинаковым индексом с учетом коллизии
        /// </summary>
        [Fact]
        public void CheckDeleteSecondCollisionItem()
        {
            MyHashtable<MusicalInstrument, Guitar>? ht = new MyHashtable<MusicalInstrument, Guitar>(2);
            for (int i = 0; i < 4; i++)
            {
                var insturument = new MusicalInstrument();
                insturument.RandomInit();
                var guitar = new Guitar();
                guitar.RandomInit();
                ht.AddData(insturument, guitar);
            }

            MusicalInstrument mi1 = new MusicalInstrument();
            mi1.RandomInit();
            MusicalInstrument mi2 = new MusicalInstrument();
            mi2.RandomInit();
            Guitar guitar1 = new Guitar();
            guitar1.RandomInit();
            Guitar guitar2 = new Guitar();
            guitar2.RandomInit();
            ht.AddData(mi1, guitar1);
            ht.AddData(mi2, guitar2);

            ht.RemoveData(new Item<MusicalInstrument, Guitar>(mi2, guitar2));
            Assert.Null(ht.FindKeyByData(mi2.Id.Number, mi2.Name));
            Assert.Equal(ht.FindKeyByData(mi1.Id.Number, mi1.Name).Key, mi1);
            Assert.Equal(ht.FindKeyByData(mi1.Id.Number, mi1.Name).Value, guitar1);
        }

        /// <summary>
        /// Удаление элемента сдвинутого с конца в начало таблицы из за коллизии
        /// </summary>
        [Fact]
        public void CheckDeleteCollitionItemOnBegin()
        {
            MyHashtable<MusicalInstrument, Guitar>? ht = new MyHashtable<MusicalInstrument, Guitar>(8);

            for (int i = 0; i < 5; i++)
            {
                var mi = new MusicalInstrument();
                mi.RandomInit();
                var guitar = new Guitar();
                guitar.RandomInit();
                ht.AddData(mi, guitar);
            }
            while (ht.Items[ht.Capacity - 1] == null)
            {
                var mi = new MusicalInstrument();
                mi.RandomInit();
                var guitar = new Guitar();
                guitar.RandomInit();
                ht.AddData(mi, guitar);
            }
            var instr = new MusicalInstrument();
            instr.RandomInit();
            var newguitar = new Guitar();
            newguitar.RandomInit();
            while (ht.Items[ht.Capacity - 1].GetHashCode() % ht.Capacity != instr.GetHashCode() % ht.Capacity)
                instr.RandomInit();
            ht.AddData(instr, newguitar);
            ht.RemoveData(new Item<MusicalInstrument, Guitar>(instr, newguitar));
            Assert.Null(ht.FindKeyByData(instr.Id.Number, instr.Name));
        }

        /// <summary>
        /// Удаление несуществующего элемента
        /// </summary>
        [Fact]
        public void CheckDeleteNonExistingItems()
        {
            MyHashtable<MusicalInstrument, Guitar>? ht = new MyHashtable<MusicalInstrument, Guitar>(8);

            for (int i = 0; i < 5; i++)
            {
                var mi = new MusicalInstrument();
                mi.RandomInit();
                var guitar = new Guitar();
                guitar.RandomInit();
                ht.AddData(mi, guitar);
            }
            var instr = new MusicalInstrument();
            instr.RandomInit();
            var newguitar = new Guitar();
            newguitar.RandomInit();
            Assert.False(ht.RemoveData(new Item<MusicalInstrument, Guitar>(instr, newguitar)));
        }
    }
}