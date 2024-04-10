using Musical_Instrument;
using lab12;
using Xunit.Sdk;

namespace ListTest
{
    public class UnitTest1
    {
        /// <summary>
        /// �������� ������������ ����� ��� �������� ������ ��� ������� ����������
        /// </summary>
        [Fact]
        public void CheckLenInCreateEmptyList()
        {
            MyList<MusicalInstrument> list = new MyList<MusicalInstrument>();
            Assert.Equal(0, list.count);
        }

        /// <summary>
        /// �������� ������������ ��������� ������� ��� �������� ������ ��� ������� ����������
        /// </summary>
        [Fact]
        public void CheckBeginPointInCreateList()
        {
            MyList<MusicalInstrument> list = new MyList<MusicalInstrument>();
            Assert.Null(list.begin);
        }

        /// <summary>
        /// �������� ������������ �������� ������� ��� �������� ������ ��� ������� ����������
        /// </summary>
        [Fact]
        public void CheckEndPointInCreateList()
        {
            MyList<MusicalInstrument> list = new MyList<MusicalInstrument>();
            Assert.Null(list.end);
        }

        /// <summary>
        /// �������� ��������� ��������� � ������ ��� �������� ������ � ������ 0
        /// </summary>
        [Fact]
        public void CheckCreateListWithZeroLen()
        {
            Assert.Throws<Exception>(() => new MyList<MusicalInstrument>(0));
        }

        /// <summary>
        /// �������� ������������ ����� ��� �������� ������ � �������� ������
        /// </summary>
        [Fact]
        public void CheckLenInCreateList()
        {
            MyList<MusicalInstrument> list = new MyList<MusicalInstrument>(5);
            Assert.Equal(5, list.count); 
        }

        /// <summary>
        /// �������� ������������ ������ ��������� ��� �������� ������ � �������� ������
        /// </summary>
        [Fact]
        public void CheckDataInCreateList()
        {
            MyList<MusicalInstrument> list = new MyList<MusicalInstrument>(5);
            Point<MusicalInstrument> start = list.begin;
            Point<MusicalInstrument> end = list.end;
            for (int i = 0; end.Prev != null; i++)
                end = end.Prev;
            for (int i = 0; start != null; i++)
            {
                Assert.Equal(start.Data, end.Data);
                start = start.Next;
                end = end.Next;
            }
        }

        /// <summary>
        /// �������� ��������� ��������� � ������ ��� �������� null � �������� ������
        /// </summary>
        [Fact]
        public void CheckNullExceptionInCreateListFromArr()
        {
            Assert.Throws<Exception>(() => new MyList<MusicalInstrument>((MusicalInstrument[])null));
        }

        /// <summary>
        /// �������� ��������� ��������� � ������ ��� �������� ������� ������
        /// </summary>
        [Fact]
        public void CheckZeroLenExceptionInCreateListFromArr()
        {
            MusicalInstrument[] arr = new MusicalInstrument[0];
            Assert.Throws<Exception>(() => new MyList<MusicalInstrument>(arr));
        }

        /// <summary>
        /// �������� ��������� ��������� � ������ ��� �������� null � �������� �������
        /// </summary>
        [Fact]
        public void CheckNullExceptionInCloneList()
        {
            Assert.Throws<Exception>(() => new MyList<MusicalInstrument>((MyList<MusicalInstrument>)null));
        }

        /// <summary>
        /// �������� ��������� ��������� � ������ ��� �������� ������� �������
        /// </summary>
        [Fact]
        public void CheckZeroLenExceptionInCloneList()
        {
            MyList<MusicalInstrument> list = new MyList<MusicalInstrument>();
            Assert.Throws<Exception>(() => new MyList<MusicalInstrument>(list));
        }

        /// <summary>
        /// �������� ������������ �������� ������ �� ����������� �������
        /// </summary>
        [Fact]
        public void CheckCorrectTransformFromArray()
        {
            MusicalInstrument[] arr = new MusicalInstrument[5];
            for (int i = 0; i < arr.Length; i++)
            {
                MusicalInstrument data = new MusicalInstrument();
                data.RandomInit();
                arr[i] = data;
            }

            MyList<MusicalInstrument> list = new MyList<MusicalInstrument>(arr);
            Point<MusicalInstrument> current = list.begin;
            for (int i = 0; current != null; i++)
            {
                Assert.Equal(current.Data.Id, arr[i].Id);
                Assert.Equal(current.Data.Name, arr[i].Name);
                current = current.Next;
            }
        }

        /// <summary>
        /// �������� ������������ ���������� ��������� � ������ ������� ������
        /// </summary>
        [Fact]
        public void AddItemToStartInEmptyList()
        {
            MyList<MusicalInstrument> list = new MyList<MusicalInstrument>();
            MusicalInstrument instrument = new MusicalInstrument();
            instrument.RandomInit();
            list.AddToBegin(instrument);
            Assert.Equal(instrument, list.begin.Data);
            Assert.Equal(instrument, list.end.Data);
        }

        /// <summary>
        /// �������� ������������ ���������� ��������� � ����� ������� ������
        /// </summary>
        [Fact]
        public void AddItemToEndInEmptyList()
        {
            MyList<MusicalInstrument> list = new MyList<MusicalInstrument>();
            MusicalInstrument instrument = new MusicalInstrument();
            instrument.RandomInit();
            list.AddToEnd(instrument);
            Assert.Equal(instrument, list.begin.Data);
            Assert.Equal(instrument, list.end.Data);
        }

        /// <summary>
        /// �������� ������������ ���������� ��������� � ������ �� ������� ������
        /// </summary>
        [Fact]
        public void AddItemToStartInList()
        {
            MyList<MusicalInstrument> list = new MyList<MusicalInstrument>(5);
            MyList<MusicalInstrument> cloneList = new MyList<MusicalInstrument>(list);
            MusicalInstrument instrument = new MusicalInstrument();
            instrument.RandomInit();

            list.AddToBegin(instrument);
            Point<MusicalInstrument>? current = list.begin.Next;
            Point<MusicalInstrument>? currentClone = cloneList.begin;

            Assert.Equal(instrument, list.begin.Data);
            for (int i = 0; current != null; i++)
            {
                Assert.Equal(current.Data, currentClone.Data);
                current = current.Next;
                currentClone = currentClone.Next;
            }
        }

        /// <summary>
        /// �������� ������������ ���������� ��������� � ����� �� ������� ������
        /// </summary>
        [Fact]
        public void AddItemToEndInList()
        {
            MyList<MusicalInstrument> list = new MyList<MusicalInstrument>(5);
            MyList<MusicalInstrument> cloneList = new MyList<MusicalInstrument>(list);
            MusicalInstrument instrument = new MusicalInstrument();
            instrument.RandomInit();

            list.AddToEnd(instrument);
            Point<MusicalInstrument>? current = list.begin;
            Point<MusicalInstrument>? currentClone = cloneList.begin;

            for (int i = 0; currentClone != null; i++)
            {
                Assert.Equal(current.Data, currentClone.Data);
                current = current.Next;
                currentClone = currentClone.Next;
            }
            Assert.Equal(instrument, current.Data);
        }

        /// <summary>
        /// ����� ��������������� ��������
        /// </summary>
        [Fact]
        public void SearchNonExistentElement()
        {
            MyList<MusicalInstrument> list = new MyList<MusicalInstrument>(5);
            MusicalInstrument instrument = new MusicalInstrument();
            instrument.RandomInit();
            Assert.Null(list.FindItem(instrument));
        }

        /// <summary>
        /// ����� ������������� �������� � ������ ������
        /// </summary>
        [Fact]
        public void SearchExistentElementInStart()
        {
            MyList<MusicalInstrument> list = new MyList<MusicalInstrument>(5);
            MusicalInstrument instrument = list.begin.Data;
            Assert.Equal(list.FindItem(instrument), list.begin);
        }

        /// <summary>
        /// ����� ������������� �������� � �������� ������
        /// </summary>
        [Fact]
        public void SearchExistentElementInMiddle()
        {
            MyList<MusicalInstrument> list = new MyList<MusicalInstrument>(5);
            Point<MusicalInstrument> instrument = list.begin.Next.Next;
            Assert.Equal(list.FindItem(instrument.Data), instrument);
        }

        /// <summary>
        /// ����� ������������� �������� � ����� ������
        /// </summary>
        [Fact]
        public void SearchExistentElementInEnd()
        {
            MyList<MusicalInstrument> list = new MyList<MusicalInstrument>(5);
            MusicalInstrument instrument = list.end.Data;
            Assert.Equal(list.FindItem(instrument), list.end);
        }

        /// <summary>
        /// �������� �������� �� ������� ������
        /// </summary>
        [Fact]
        public void DeleteItemFromEmptyList()
        {
            MyList<MusicalInstrument> list = new MyList<MusicalInstrument>();
            MusicalInstrument instrument = new MusicalInstrument();
            instrument.RandomInit();
            Assert.Throws<Exception>(() => list.RemoveItem(instrument));
        }

        /// <summary>
        /// �������� �������� �� ������ ����� 1
        /// </summary>
        [Fact]
        public void DeleteItemFromOneLenList()
        {
            MyList<MusicalInstrument> list = new MyList<MusicalInstrument>(1);
            list.RemoveItem(list.begin.Data);
            Assert.Equal(0, list.count);
            Assert.Null(list.begin);
            Assert.Null(list.end);
        }

        /// <summary>
        /// �������� ��������������� ��������
        /// </summary>
        [Fact]
        public void DeleteNullElemFromList()
        {
            MyList<MusicalInstrument> list = new MyList<MusicalInstrument>(5);
            MusicalInstrument instrument = new MusicalInstrument();
            instrument.RandomInit();
            Assert.False(list.RemoveItem(instrument));
        }

        /// <summary>
        /// �������� �������� ������� �������� ������
        /// </summary>
        [Fact]
        public void DeleteFirstItemFromList()
        {
            MyList<MusicalInstrument> list = new MyList<MusicalInstrument>(5);
            MyList<MusicalInstrument> cloneList = new MyList<MusicalInstrument>(list);
            MusicalInstrument instrument = new MusicalInstrument();
            instrument.RandomInit();

            list.AddToBegin(instrument);
            Assert.True(list.RemoveItem(list.begin.Data));

            Point<MusicalInstrument>? current = list.begin;
            Point<MusicalInstrument>? currentClone = cloneList.begin;

            for (int i = 0; current != null; i++)
            {
                Assert.Equal(current.Data, currentClone.Data);
                current = current.Next;
                currentClone = currentClone.Next;
            }
        }

        /// <summary>
        /// �������� �������� �������� �������� ������
        /// </summary>
        [Fact]
        public void DeleteMiddleItemFromList()
        {
            MyList<MusicalInstrument> list = new MyList<MusicalInstrument>(5);
            MyList<MusicalInstrument> cloneList = new MyList<MusicalInstrument>(list);

            MusicalInstrument instrument = new MusicalInstrument();
            instrument.RandomInit();

            MusicalInstrument instrument2 = new MusicalInstrument();
            instrument.RandomInit();

            list.AddToBegin(instrument);

            list.AddToBegin(instrument2);
            cloneList.AddToBegin(instrument2);

            Assert.True(list.RemoveItem(list.begin.Next.Data));

            Point<MusicalInstrument>? current = list.begin;
            Point<MusicalInstrument>? currentClone = cloneList.begin;

            for (int i = 0; current != null; i++)
            {
                Assert.Equal(current.Data, currentClone.Data);
                current = current.Next;
                currentClone = currentClone.Next;
            }
        }

        /// <summary>
        /// �������� �������� ���������� �������� ������
        /// </summary>
        [Fact]
        public void DeleteLastItemFromList()
        {
            MyList<MusicalInstrument> list = new MyList<MusicalInstrument>(5);
            MyList<MusicalInstrument> cloneList = new MyList<MusicalInstrument>(list);

            MusicalInstrument instrument = new MusicalInstrument();
            instrument.RandomInit();

            list.AddToEnd(instrument);
            Assert.True(list.RemoveItem(list.end.Data));

            Point<MusicalInstrument>? current = list.begin;
            Point<MusicalInstrument>? currentClone = cloneList.begin;

            for (int i = 0; current != null; i++)
            {
                Assert.Equal(current.Data, currentClone.Data);
                current = current.Next;
                currentClone = currentClone.Next;
            }
        }
    }
}