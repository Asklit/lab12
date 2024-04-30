using lab12._3;
using Musical_Instrument;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Metrics;

namespace TreeTest
{
    public class UnitTest1
    {
        /// <summary>
        /// Проверка корректности создания дерева
        /// </summary>
        [Fact]
        public void TestCreateTree()
        {
            MyTree<MusicalInstrument> tree = new MyTree<MusicalInstrument>(5);
            Assert.Equal(5, tree.Count);
        }

        /// <summary>
        /// Вспомогательная рекурсивная функция проверки корректности заполнения дерева
        /// </summary>
        void RecursivePassTree(Point<MusicalInstrument> current, ref Point<MusicalInstrument> last)
        {
            if (current != null)
            {
                RecursivePassTree(current.Left, ref last);
                Assert.Equal(1, current.Data.CompareTo(last.Data));
                RecursivePassTree(current.Right, ref last);
            }
        }

        /// <summary>
        /// Проверка корректности заполения дерева
        /// </summary>
        [Fact]
        public void TestConvertToFindTree()
        {
            MyTree<MusicalInstrument> tree = new MyTree<MusicalInstrument>(5);
            MyTree<MusicalInstrument> newTree = tree.ConvertToFindTree();
            MusicalInstrument data = new MusicalInstrument();
            Point<MusicalInstrument> last = new Point<MusicalInstrument>(data);
            RecursivePassTree(newTree.root, ref last);
        }

        /// <summary>
        /// Добавление уже существующего элемента в дерево поиска
        /// </summary>
        [Fact]
        public void TestAddExistItem()
        {
            MyTree<MusicalInstrument> tree = new MyTree<MusicalInstrument>(5);
            MyTree<MusicalInstrument> newTree = tree.ConvertToFindTree();
            newTree.AddPointToFindTree(newTree.root.Data, newTree.root);
            Assert.Equal(5, tree.Count);
        }

        /// <summary>
        /// Проверка поиска максимального элемента в дереве
        /// </summary>
        [Fact]
        public void TestGetMaxItem()
        {
            MyTree<MusicalInstrument> tree = new MyTree<MusicalInstrument>(5);
            MusicalInstrument[] array = new MusicalInstrument[tree.Count];
            int index = 0;
            tree.ConvertTreeToArray(tree.root, array, ref index);
            MusicalInstrument maxValue = new MusicalInstrument();
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].CompareTo(maxValue) > 0)
                    maxValue = array[i];
            }
            Assert.Equal(tree.GetMaxValue(), maxValue);
        }

        /// <summary>
        /// Проверка удаление дерева из памяти
        /// </summary>
        [Fact]
        public void TestDelete()
        {
            MyTree<MusicalInstrument> tree = new MyTree<MusicalInstrument>(1, new Point<MusicalInstrument>(new MusicalInstrument("test", 10)));
            MusicalInstrument data1 = new MusicalInstrument("test", 9);
            tree.AddPointToFindTree(data1, tree.root);
            MusicalInstrument data2 = new MusicalInstrument("test", 11);
            tree.AddPointToFindTree(data2, tree.root);
            Point<MusicalInstrument> pointRoot = tree.root;
            Point<MusicalInstrument> pointLeft = tree.root.Left;
            Point<MusicalInstrument> pointRight = tree.root.Right;

            Assert.Equal(pointRoot.Data, new MusicalInstrument("test", 10));
            Assert.Equal(pointRoot.Left.Data, data1);
            Assert.Equal(pointRoot.Right.Data, data2);

            tree.DeleteTree();

            Assert.Null(pointRoot.Data);
            Assert.Null(pointRoot.Left);
            Assert.Null(pointRoot.Right);

            Assert.Null(pointLeft.Data);
            Assert.Null(pointLeft.Left);
            Assert.Null(pointLeft.Right);

            Assert.Null(pointRight.Data);
            Assert.Null(pointRight.Left);
            Assert.Null(pointRight.Right);
        }

        /// <summary>
        /// Тестирование преобразование в строку
        /// </summary>
        [Fact]
        public void TestToStringPoint()
        {
            MusicalInstrument data = new MusicalInstrument();
            data.RandomInit();
            Point<MusicalInstrument> point = new Point<MusicalInstrument>(data);
            Assert.Equal(point.ToString(), data.ToString());
        }

        /// <summary>
        /// Тестирование итерфейса IComparable
        /// </summary>
        [Fact]
        public void TestCompareTo()
        {
            Point<MusicalInstrument>[] arr = new Point<MusicalInstrument>[10];
            for (int i = 0; i < 10; i++)
            {
                MusicalInstrument data = new MusicalInstrument();
                data.RandomInit();
                Point<MusicalInstrument> point = new Point<MusicalInstrument>(data);
                arr[i] = point;
            }
            Array.Sort(arr);
            for (int i = 0;i < arr.Length - 1;i++)
            {
                Assert.Equal(-1, arr[i].CompareTo(arr[i + 1]));
            }
        }

        [Fact]
        public void TestExceptionCompareTo() 
        {
            MusicalInstrument data = new MusicalInstrument();
            data.RandomInit();
            Point<MusicalInstrument> point = new Point<MusicalInstrument>(data);
            Guitar data2 = new Guitar();
            data2.RandomInit();
            Point<Guitar> point2 = new Point<Guitar>(data2);
            Assert.Throws<Exception>(() => point.CompareTo(point2));
        }
    }
}