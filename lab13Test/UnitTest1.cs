using Musical_Instrument;
using lab12;
using lab13;
using System.Collections.ObjectModel;
using lab12._3;

namespace lab13Test
{
    public class UnitTest1
    {
        [Fact]
        public void TestAddItem()
        {
            MyObservableCollection<MusicalInstrument> firstObsCollection = new(0, "������ ���������");
            Journal journal = new Journal();
            firstObsCollection.RegisterCountChangedHandler(journal.Add);
            MusicalInstrument mi = new("������", 10);
            firstObsCollection.Add(mi);
            Assert.Equal(1, journal.JournalList.Count);
            Assert.Equal(journal.JournalList[0].ToString(), "� ��������� \"������ ���������\" ��������� ������� \"������� ������� ��������\" � ������� \"id: 10. ������\".");
        }

        [Fact]
        public void TestDeleteItem() 
        {
            MyObservableCollection<MusicalInstrument> firstObsCollection = new(10, "������ ���������");
            Journal journal = new Journal();
            firstObsCollection.RegisterCountChangedHandler(journal.Add);
            MusicalInstrument mi = firstObsCollection.root.Data;
            firstObsCollection.Remove(mi);
            Assert.Equal(1, journal.JournalList.Count);
            Assert.Equal(journal.JournalList[0].ToString(), $"� ��������� \"������ ���������\" ��������� ������� \"������� ������� ������\" � ������� \"{mi.Id}. {mi.Name}\".");
        }

        [Fact]
        public void TestChangeReference()
        {
            MyObservableCollection<MusicalInstrument> firstObsCollection = new(10, "������ ���������");
            Journal journal = new Journal();
            firstObsCollection.RegisterReferenceChangedHandler(journal.Add);
            MusicalInstrument mi = new("Name", 777);
            MusicalInstrument temp = new(firstObsCollection.root.Data.Name, firstObsCollection.root.Data.Id.Number);
            firstObsCollection[temp] = new Point<MusicalInstrument>(mi);
            Assert.Equal(1, journal.JournalList.Count);
            Assert.Equal(journal.JournalList[0].ToString(), $"� ��������� \"������ ���������\" ��������� ������� \"������� ������� � ������\" � ������� \"{temp.Id}. {temp.Name}\".");
        }

        [Fact]
        public void TestGetIndexer()
        {
            MyObservableCollection<MusicalInstrument> firstObsCollection = new(10, "������ ���������");
            Assert.Equal(firstObsCollection[new(firstObsCollection.root.Data.Name, firstObsCollection.root.Data.Id.Number)].Data, new MusicalInstrument(firstObsCollection.root.Data.Name, firstObsCollection.root.Data.Id.Number));
        }

        [Fact]
        public void TestSetIndexer() 
        {
            MyObservableCollection<MusicalInstrument> firstObsCollection = new(10, "������ ���������");
            MusicalInstrument mi = new("Name", 777);
            MusicalInstrument temp = new(firstObsCollection.root.Data.Name, firstObsCollection.root.Data.Id.Number);
            firstObsCollection[temp] = new Point<MusicalInstrument>(mi);
            Assert.False(firstObsCollection.Contains(temp));
            Assert.True(firstObsCollection.Contains(mi));
        }

        [Fact]
        public void TestConstructor() 
        {
            MyObservableCollection<MusicalInstrument> firstObsCollection = new();
            Assert.Equal(10, firstObsCollection.Count);
        }
    }
}