using lab14;
using Musical_Instrument;

namespace lab14test
{
    public class UnitTest1
    {
        [Fact]
        public void TestLinqWhereRequest()
        {
            SortedDictionary<string, List<MusicalInstrument>> collection = new();
            for (int i = 0; i < 5; i++)
            {
                List<MusicalInstrument> lst = new();
                for (int j = 0; j < 2; j++)
                {
                    MusicalInstrument m = new MusicalInstrument();
                    m.RandomInit();
                    lst.Add(m);
                }
                collection.Add($"Ãðóïïà{i}", lst);
                for (int j = 0; j < 3; j++)
                {
                    MusicalInstrument m = new MusicalInstrument();
                    m.RandomInit();
                    lst.Add(m);
                }
                collection.Add($"Ãðóïïà{i + 5}", lst);
            }

            var res = firstPart.LinqWhereRequest(collection, 3);
            foreach (var item in res)
            {
                Assert.Equal(5, item.Value.Count);
            }
        }

        [Fact]
        public void TestExtentionWhereRequest()
        {
            SortedDictionary<string, List<MusicalInstrument>> collection = new();
            for (int i = 0; i < 5; i++)
            {
                List<MusicalInstrument> lst = new();
                for (int j = 0; j < 2; j++)
                {
                    MusicalInstrument m = new MusicalInstrument();
                    m.RandomInit();
                    lst.Add(m);
                }
                collection.Add($"Ãðóïïà{i}", lst);
                for (int j = 0; j < 3; j++)
                {
                    MusicalInstrument m = new MusicalInstrument();
                    m.RandomInit();
                    lst.Add(m);
                }
                collection.Add($"Ãðóïïà{i + 5}", lst);
            }

            var res = firstPart.ExtensionWhereRequest(collection, 3);
            foreach (var item in res)
            {
                Assert.Equal(5, item.Value.Count);
            }
        }

        [Fact]
        public void TestLinqAgregateRequest()
        {
            SortedDictionary<string, List<MusicalInstrument>> collection = new();

            List<MusicalInstrument> lst1 = new();
            for (int i = 0; i < 5; i++)
            {
                MusicalInstrument mi = new MusicalInstrument();
                mi.RandomInit();
                lst1.Add(mi);
            }
                

            List<MusicalInstrument> lst2 = new();
            for (int i = 0; i < 3; i++)
            {
                MusicalInstrument mi = new MusicalInstrument();
                mi.RandomInit();
                lst2.Add(mi);
            }

            List<MusicalInstrument> lst3 = new();
            for (int i = 0; i < 4; i++)
            {
                MusicalInstrument mi = new MusicalInstrument();
                mi.RandomInit();
                lst3.Add(mi);
            }

            collection.Add("Ãðóïïà 1", lst1);
            collection.Add("Ãðóïïà 2", lst2);
            collection.Add("Ãðóïïà 3", lst3);

            var sum = firstPart.LinqSumRequest(collection);
            var max = firstPart.LinqMaxRequest(collection);
            var min = firstPart.LinqMinRequest(collection);
            var avg = firstPart.LinqAvgRequest(collection);

            Assert.Equal(12, sum);
            Assert.Equal(5, max);
            Assert.Equal(3, min);
            Assert.Equal(4, avg);
        }

        [Fact]
        public void TestExtentionAgregateRequest()
        {
            SortedDictionary<string, List<MusicalInstrument>> collection = new();

            List<MusicalInstrument> lst1 = new();
            for (int i = 0; i < 5; i++)
            {
                MusicalInstrument mi = new MusicalInstrument();
                mi.RandomInit();
                lst1.Add(mi);
            }


            List<MusicalInstrument> lst2 = new();
            for (int i = 0; i < 3; i++)
            {
                MusicalInstrument mi = new MusicalInstrument();
                mi.RandomInit();
                lst2.Add(mi);
            }

            List<MusicalInstrument> lst3 = new();
            for (int i = 0; i < 4; i++)
            {
                MusicalInstrument mi = new MusicalInstrument();
                mi.RandomInit();
                lst3.Add(mi);
            }

            collection.Add("Ãðóïïà 1", lst1);
            collection.Add("Ãðóïïà 2", lst2);
            collection.Add("Ãðóïïà 3", lst3);

            var sum = firstPart.ExtensionSumRequest(collection);
            var max = firstPart.ExtensionMaxRequest(collection);
            var min = firstPart.ExtensionMinRequest(collection);
            var avg = firstPart.ExtensionAvgRequest(collection);

            Assert.Equal(12, sum);
            Assert.Equal(5, max);
            Assert.Equal(3, min);
            Assert.Equal(4, avg);
        }

        [Fact]
        public void TestLinqGroupByRequest()
        {
            SortedDictionary<string, List<MusicalInstrument>> collection = new();

            List<MusicalInstrument> lst1 = new();
            for (int i = 0; i < 3; i++)
            {
                Piano mi = new Piano("Piano", 10, "", 10);
                lst1.Add(mi);
            }


            List<MusicalInstrument> lst2 = new();
            for (int i = 0; i < 4; i++)
            {
                ElectricGuitar mi = new ElectricGuitar("ElectricGuitar", 10, "", 10);
                lst2.Add(mi);
            }

            List<MusicalInstrument> lst3 = new();
            for (int i = 0; i < 5; i++)
            {
                Guitar mi = new Guitar("Guitar", 10, 10);
                lst3.Add(mi);
            }

            collection.Add("Ãðóïïà 1", lst1);
            collection.Add("Ãðóïïà 2", lst2);
            collection.Add("Ãðóïïà 3", lst3);

            var max = firstPart.LinqGroupByRequest(collection);
            int count = 3;
            foreach (var item in max)
            {
                Assert.Equal(count, item.Count());
                count++;
            }
        }

        [Fact]
        public void TestExtensionGroupByRequest()
        {
            SortedDictionary<string, List<MusicalInstrument>> collection = new();

            List<MusicalInstrument> lst1 = new();
            for (int i = 0; i < 3; i++)
            {
                Piano mi = new Piano("Piano", 10, "", 10);
                lst1.Add(mi);
            }


            List<MusicalInstrument> lst2 = new();
            for (int i = 0; i < 4; i++)
            {
                ElectricGuitar mi = new ElectricGuitar("ElectricGuitar", 10, "", 10);
                lst2.Add(mi);
            }

            List<MusicalInstrument> lst3 = new();
            for (int i = 0; i < 5; i++)
            {
                Guitar mi = new Guitar("Guitar", 10, 10);
                lst3.Add(mi);
            }

            collection.Add("Ãðóïïà 1", lst1);
            collection.Add("Ãðóïïà 2", lst2);
            collection.Add("Ãðóïïà 3", lst3);

            var max = firstPart.ExtensionGroupByRequest(collection);
            int count = 3;
            foreach (var item in max)
            {
                Assert.Equal(count, item.Count());
                count++;
            }
        }

        [Fact]
        public void TestLinqJoinRequest()
        {
            SortedDictionary<string, List<MusicalInstrument>> collection = new();
            for (int i = 0; i < 5; i++) 
            {
                List<MusicalInstrument> lst = new();
                for (int j = 0; j < 5; j++)
                {
                    MusicalInstrument m = new MusicalInstrument();
                    m.RandomInit();
                    lst.Add(m);
                }
                collection.Add($"Ãðóïïà{i}", lst);
            }

            SortedDictionary<string, List<MusicalInstrument>> collection2 = new();
            for (int i = 0; i < 5; i++)
            {
                List<MusicalInstrument> lst = new();
                for (int j = 0; j < 5; j++)
                {
                    MusicalInstrument m = new MusicalInstrument();
                    m.RandomInit();
                    lst.Add(m);
                }
                collection2.Add($"Ãðóïïà{i}", lst);
            }

            var res = firstPart.LinqJoinRequest(collection, collection2);
            foreach (var item in res)
            {
                Assert.Equal(10, item.Value.Count);
            }
        }

        [Fact]
        public void TestExtensionJoinRequest()
        {
            SortedDictionary<string, List<MusicalInstrument>> collection = new();
            for (int i = 0; i < 5; i++)
            {
                List<MusicalInstrument> lst = new();
                for (int j = 0; j < 5; j++)
                {
                    MusicalInstrument m = new MusicalInstrument();
                    m.RandomInit();
                    lst.Add(m);
                }
                collection.Add($"Ãðóïïà{i}", lst);
            }

            SortedDictionary<string, List<MusicalInstrument>> collection2 = new();
            for (int i = 0; i < 5; i++)
            {
                List<MusicalInstrument> lst = new();
                for (int j = 0; j < 5; j++)
                {
                    MusicalInstrument m = new MusicalInstrument();
                    m.RandomInit();
                    lst.Add(m);
                }
                collection2.Add($"Ãðóïïà{i}", lst);
            }

            var res = firstPart.ExtensionJoinRequest(collection, collection2);
            foreach (var item in res)
            {
                Assert.Equal(10, item.Value.Count);
            }
        }
    }
}