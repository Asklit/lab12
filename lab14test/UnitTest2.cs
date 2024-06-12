using lab12._4;
using lab14;
using Musical_Instrument;

namespace lab14test
{
    public class UnitTest2
    {
        [Fact]
        public void TestLinqWhereRequest()
        {
            MyCollection<MusicalInstrument> collection = new MyCollection<MusicalInstrument>(0);
            MusicalInstrument mi1 = new MusicalInstrument("Guitar", 10);
            MusicalInstrument mi2 = new MusicalInstrument("Piano", 5);
            MusicalInstrument mi3 = new MusicalInstrument("ElectricGuitar", 15);
            MusicalInstrument mi4 = new MusicalInstrument("Piano", 20);

            collection.AddPointToFindTree(mi1, collection.root);
            collection.AddPointToFindTree(mi2, collection.root);
            collection.AddPointToFindTree(mi3, collection.root);
            collection.AddPointToFindTree(mi4, collection.root);

            var res = secondPart.LinqWhereRequest(collection, "Piano");
            Assert.Equal(2, res);
        }

        [Fact]
        public void TestExtensionWhereRequest()
        {
            MyCollection<MusicalInstrument> collection = new MyCollection<MusicalInstrument>(0);
            MusicalInstrument mi1 = new MusicalInstrument("Guitar", 10);
            MusicalInstrument mi2 = new MusicalInstrument("Piano", 5);
            MusicalInstrument mi3 = new MusicalInstrument("ElectricGuitar", 15);
            MusicalInstrument mi4 = new MusicalInstrument("Piano", 20);

            collection.AddPointToFindTree(mi1, collection.root);
            collection.AddPointToFindTree(mi2, collection.root);
            collection.AddPointToFindTree(mi3, collection.root);
            collection.AddPointToFindTree(mi4, collection.root);

            var res = secondPart.ExtensionWhereRequest(collection, "Piano");
            Assert.Equal(2, res);
        }

        [Fact]
        public void TestLinqAgregationRequest()
        {
            MyCollection<MusicalInstrument> collection = new MyCollection<MusicalInstrument>(0);
            MusicalInstrument mi1 = new MusicalInstrument("Guitar", 7);
            MusicalInstrument mi2 = new MusicalInstrument("Piano", 5);
            MusicalInstrument mi3 = new MusicalInstrument("ElectricGuitar", 15);
            MusicalInstrument mi4 = new MusicalInstrument("Piano", 20);
            MusicalInstrument mi5 = new MusicalInstrument("Piano", 13);

            collection.AddPointToFindTree(mi1, collection.root);
            collection.AddPointToFindTree(mi2, collection.root);
            collection.AddPointToFindTree(mi3, collection.root);
            collection.AddPointToFindTree(mi4, collection.root);
            collection.AddPointToFindTree(mi5, collection.root);

            var sum = secondPart.LinqSumRequest(collection);
            var max = secondPart.LinqMaxRequest(collection);
            var min = secondPart.LinqMinRequest(collection);
            var avg = secondPart.LinqAvgRequest(collection);

            Assert.Equal(60, sum);
            Assert.Equal(20, max);
            Assert.Equal(5, min);
            Assert.Equal(12, avg);
        }

        [Fact]
        public void TestExtensionAgregationRequest()
        {
            MyCollection<MusicalInstrument> collection = new MyCollection<MusicalInstrument>(0);
            MusicalInstrument mi1 = new MusicalInstrument("Guitar", 7);
            MusicalInstrument mi2 = new MusicalInstrument("Piano", 5);
            MusicalInstrument mi3 = new MusicalInstrument("ElectricGuitar", 15);
            MusicalInstrument mi4 = new MusicalInstrument("Piano", 20);
            MusicalInstrument mi5 = new MusicalInstrument("Piano", 13);

            collection.AddPointToFindTree(mi1, collection.root);
            collection.AddPointToFindTree(mi2, collection.root);
            collection.AddPointToFindTree(mi3, collection.root);
            collection.AddPointToFindTree(mi4, collection.root);
            collection.AddPointToFindTree(mi5, collection.root);

            var sum = secondPart.ExtensionSumRequest(collection);
            var max = secondPart.ExtensionMaxRequest(collection);
            var min = secondPart.ExtensionMinRequest(collection);
            var avg = secondPart.ExtensionAvgRequest(collection);

            Assert.Equal(60, sum);
            Assert.Equal(20, max);
            Assert.Equal(5, min);
            Assert.Equal(12, avg);
        }

        [Fact]
        public void TestLinqRequest()
        {
            MyCollection<MusicalInstrument> collection = new(0);

            for (int i = 0; i < 3; i++)
            {
                Piano mi = new Piano("Pianino", 10, "", i);
                collection.Add(mi);
            }


            for (int i = 0; i < 4; i++)
            {
                ElectricGuitar mi = new ElectricGuitar("ElectricGuitar", 5, "", i + 5);
                collection.Add(mi);
            }

            for (int i = 0; i < 5; i++)
            {
                Guitar mi = new Guitar("Guitar", 10, i + 10);
                collection.Add(mi);
            }

            var max = secondPart.LinqGroupByRequest(collection);
            int count = 3;
            foreach (var item in max)
            {
                Assert.Equal(count, item.Count());
                count++;
            }
        }

        [Fact]
        public void TestExtensionRequest()
        {
            MyCollection<MusicalInstrument> collection = new(0);

            for (int i = 0; i < 3; i++)
            {
                Piano mi = new Piano("Pianino", 10, "", i);
                collection.Add(mi);
            }


            for (int i = 0; i < 4; i++)
            {
                ElectricGuitar mi = new ElectricGuitar("ElectricGuitar", 5, "", i+5);
                collection.Add(mi);
            }

            for (int i = 0; i < 5; i++)
            {
                Guitar mi = new Guitar("Guitar", 10, i + 10);
                collection.Add(mi);
            }

            var max = secondPart.ExtensionGroupByRequest(collection);
            int count = 3;
            foreach (var item in max)
            {
                Assert.Equal(count, item.Count());
                count++;
            }
        }
    }
}