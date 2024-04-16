using Musical_Instrument;
using lab12_2;

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
            ht.AddData(mi, guitar1);
            ht.AddData(mi, guitar2);
            int index = ht.GetIndex(mi);
            Assert.Equal(mi, ht.Items[index].Key);
            Assert.Equal(guitar1, ht.Items[index].Value);
            Assert.Equal(mi, ht.Items[index + 1].Key);
            Assert.Equal(guitar2, ht.Items[index + 1].Value);
        }

        /// <summary>
        /// Проверка удаления
        /// </summary>
        [Fact]
        public void CheckDelete()
        {
            MyHashtable<MusicalInstrument, Guitar>? ht = new MyHashtable<MusicalInstrument, Guitar>(4);
            MusicalInstrument mi = new MusicalInstrument();
            mi.RandomInit();
            Guitar guitar1 = new Guitar();
            guitar1.RandomInit();
            ht.AddData(mi, guitar1);
            int index = ht.GetIndex(mi);
            ht.RemoveData(mi);
            Assert.Null(ht.Items[index]);
        }

        /// <summary>
        /// Проверка удаления элементов с одинаковым хешкодом
        /// </summary>
        [Fact]
        public void CheckDeleteCoupleItems()
        {
            MyHashtable<MusicalInstrument, Guitar>? ht = new MyHashtable<MusicalInstrument, Guitar>(4);
            MusicalInstrument mi = new MusicalInstrument();
            mi.RandomInit();
            Guitar guitar1 = new Guitar();
            guitar1.RandomInit();
            Guitar guitar2 = new Guitar();
            guitar2.RandomInit();
            Guitar guitar3 = new Guitar();
            guitar2.RandomInit();
            ht.AddData(mi, guitar1);
            ht.AddData(mi, guitar2);
            ht.AddData(mi, guitar3);
            int index = ht.GetIndex(mi);
            ht.RemoveData(mi);
            Assert.Null(ht.Items[index]);
            Assert.Equal(mi, ht.Items[index + 1].Key);
            Assert.Equal(guitar2, ht.Items[index + 1].Value);
            ht.RemoveData(mi);
            Assert.Null(ht.Items[index]);
            Assert.Null(ht.Items[index + 1]);
            index = ht.GetIndex(mi);
            ht.RemoveData(mi);
            Assert.Null(ht.Items[index]);
        }

        /// <summary>
        /// Проверка поиска 
        /// </summary>
        [Fact]
        public void CheckFindItems()
        {
            MyHashtable<MusicalInstrument, Guitar>? ht = new MyHashtable<MusicalInstrument, Guitar>(4);
            MusicalInstrument mi = new MusicalInstrument();
            mi.RandomInit();
            for (int i = 0;i < 3; i++)
            {
                Guitar guitar1 = new Guitar();
                guitar1.RandomInit();
                ht.AddData(mi, guitar1);
            }
            Assert.Equal(mi, ht.FindKeyByIDCode(mi.Id.Number));
        }
    }
}