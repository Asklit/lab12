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
        public void TestRemoveRoot()
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
        /// Проверка Remove
        /// </summary>
        [Fact]
        public void TestRemoveRootLeftItem()
        {
            MyCollection<MusicalInstrument> collection = new MyCollection<MusicalInstrument>(100);
            MyCollection<MusicalInstrument> findTree = collection.ConvertToFindTree();
            Point<MusicalInstrument> point;
            while (collection.root.Left == null)
            {
                collection = new MyCollection<MusicalInstrument>(100);
            }
            Assert.True(findTree.Remove(findTree.root.Left.Data));
        }

        /// <summary>
        /// Проверка Remove
        /// </summary>
        [Fact]
        public void TestRemoveExtremeItem()
        {
            MyCollection<MusicalInstrument> collection = new MyCollection<MusicalInstrument>(100);
            MyCollection<MusicalInstrument> findTree = collection.ConvertToFindTree();
            Point<MusicalInstrument> point;
            while (collection.root.Right == null)
            {
                collection = new MyCollection<MusicalInstrument>(100);
            }
            collection.root.Right.Right = null;
            collection.root.Right.Left = null;
            Assert.True(findTree.Remove(findTree.root.Right.Data));
        }

        /// <summary>
        /// Проверка Remove
        /// </summary>
        [Fact]
        public void TestRemoveOneNullNextItem()
        {
            MyCollection<MusicalInstrument> collection = new MyCollection<MusicalInstrument>(100);
            MyCollection<MusicalInstrument> findTree = collection.ConvertToFindTree();
            Point<MusicalInstrument> point;
            while (collection.root.Right == null)
            {
                collection = new MyCollection<MusicalInstrument>(100);
            }
            collection.root.Right.Left = null;
            MusicalInstrument mi = new MusicalInstrument();
            mi.RandomInit();
            collection.root.Right.Right = new Point<MusicalInstrument>(mi);
            Assert.True(findTree.Remove(findTree.root.Right.Data));
        }

        /// <summary>
        /// Проверка Remove 1 элемента из коллекции
        /// </summary>
        [Fact]
        public void TestRemoveFromOneLenCollection()
        {
            MyCollection<MusicalInstrument> collection = new MyCollection<MusicalInstrument>(1);
            collection = collection.ConvertToFindTree();
            Assert.True(collection.Remove(collection.root.Data));
        }

        /// <summary>
        /// Тестирование конструктора без параметров
        /// </summary>
        [Fact]
        public void TestConstructor()
        {
            MyCollection<MusicalInstrument> collection = new MyCollection<MusicalInstrument>();
            collection = collection.ConvertToFindTree();
            Assert.Equal(10, collection.Count);
        }

        /// <summary>
        /// Проверка корректного получения сообщения о ошибке при вызове CopyTo
        /// </summary>
        [Fact]
        public void TestCopyToException() 
        {
            MyCollection<MusicalInstrument> collection = new MyCollection<MusicalInstrument>();
            collection = collection.ConvertToFindTree();
            MusicalInstrument[] array = new MusicalInstrument[5];
            Assert.Throws<Exception>(() => collection.CopyTo(array));
        }

        /// <summary>
        /// Проверка создания клона коллекции
        /// </summary>
        [Fact]
        public void TestConstructorCopy()
        {
            MyCollection<MusicalInstrument> collection = new MyCollection<MusicalInstrument>();
            collection = collection.ConvertToFindTree();
            MyCollection<MusicalInstrument> collection2 = new MyCollection<MusicalInstrument>(collection);
            collection.root.Data.RandomInit();
            Assert.NotEqual(collection.root.Data, collection2.root.Data);
            Assert.Equal(collection.root.Left.Data, collection2.root.Left.Data);
            Assert.Equal(collection.root.Right.Data, collection2.root.Right.Data);
        }

        /// <summary>
        /// Проверка Contains
        /// </summary>
        [Fact]
        public void TestContainsRightItem()
        {
            MyCollection<MusicalInstrument> collection = new MyCollection<MusicalInstrument>(100);
            MyCollection<MusicalInstrument> findTree = collection.ConvertToFindTree();
            while (findTree.root.Right == null)
            {
                findTree = new MyCollection<MusicalInstrument>(100);
            }
            Assert.True(findTree.Contains(findTree.root.Right.Data));
        }

        /// <summary>
        /// Проверка Contains
        /// </summary>
        [Fact]
        public void TestContainsLeftItem()
        {
            MyCollection<MusicalInstrument> collection = new MyCollection<MusicalInstrument>(100);
            MyCollection<MusicalInstrument> findTree = collection.ConvertToFindTree();
            while (findTree.root.Left == null)
            {
                findTree = new MyCollection<MusicalInstrument>(100);
            }
            Assert.True(findTree.Contains(findTree.root.Left.Data));
        }
    }
}