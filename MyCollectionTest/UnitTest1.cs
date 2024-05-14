using lab12._3;
using lab12._4;
using Musical_Instrument;
using System;
using System.ComponentModel.Design.Serialization;
using System.Drawing;

namespace MyCollectionTest
{
    public class UnitTest1
    {
        /// <summary>
        /// Тест проверяющий интерфейс IEnumerator
        /// </summary>
        [Fact]
        public void TestIEnumerator()
        {
            MyCollection<MusicalInstrument> collection = new MyCollection<MusicalInstrument>(10);
            MyCollection<MusicalInstrument> findTree = collection.ConvertToFindTree();
            MusicalInstrument[] array = findTree.ToArray();
            int index = 0;
            foreach (MusicalInstrument item in findTree)
            {
                Assert.Equal(item, array[index]);
                index++;
            }
        }

        /// <summary>
        /// Проверка добавления элемента в дерево
        /// </summary>
        [Fact]
        public void TestAddItem() 
        {
            MyCollection<MusicalInstrument> collection = new MyCollection<MusicalInstrument>(10);
            MyCollection<MusicalInstrument> findTree = collection.ConvertToFindTree();
            
            MusicalInstrument item = new MusicalInstrument();
            item.RandomInit();
            item.Id.Number = 99999999;
            findTree.Add(item);
            MusicalInstrument[] array = findTree.ToArray();

            int index = 0;
            foreach (MusicalInstrument elem in findTree)
            {
                Assert.Equal(elem, array[index]);
                index++;
            }
        }

        /// <summary>
        /// Проверка удаления дерева из памяти
        /// </summary>
        [Fact]
        public void TestDeleteTree()
        {
            MyCollection<MusicalInstrument> collection = new MyCollection<MusicalInstrument>(10);
            MyCollection<MusicalInstrument> findTree = collection.ConvertToFindTree();

            Point<MusicalInstrument> item = findTree.root;

            findTree.Clear();
            Assert.Null(findTree.root);
            Assert.Null(item.Data);
        }

        /// <summary>
        /// Проверка contains
        /// </summary>
        [Fact]
        public void TestContains()
        {
            MyCollection<MusicalInstrument> collection = new MyCollection<MusicalInstrument>(10);
            MyCollection<MusicalInstrument> findTree = collection.ConvertToFindTree();

            MusicalInstrument mi = new MusicalInstrument();
            mi.RandomInit();

            Assert.Contains(findTree.root.Data, findTree);
            Assert.DoesNotContain(mi, findTree);
        }

        /// <summary>
        /// Проверка Remove
        /// </summary>
        [Fact]
        public void TestRemove()
        {
            MyCollection<MusicalInstrument> collection = new MyCollection<MusicalInstrument>(100);
            MyCollection<MusicalInstrument> findTree = collection.ConvertToFindTree();
            Point<MusicalInstrument> point;
            if (findTree.root.Left != null)
            {
                point = findTree.root.Left;
                while (point.Right != null)
                    point = point.Right;
            }
            else
            {
                point = findTree.root.Right;
            }
            Assert.True(findTree.Remove(findTree.root.Data));
            Assert.Equal(point, findTree.root);
        }

        /// <summary>
        /// Тестирование конструктора без параметров
        /// </summary>
        [Fact]
        public void TestConstructor()
        {
            MyCollection<MusicalInstrument> collection = new MyCollection<MusicalInstrument>();
            Assert.Equal(10, collection.Count);
        }
    }
}