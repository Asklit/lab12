using lab12._4;
using Musical_Instrument;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace lab14
{
    public class secondPart
    {
        public static int LinqWhereRequest(MyCollection<MusicalInstrument> collection, string name)
        {
            return (from elem in collection
                   where elem.Name == name
                   select elem).Count();
        }

        public static int ExtensionWhereRequest(MyCollection<MusicalInstrument> collection, string name)
        {
            return (collection.Where(elem => elem.Name == name)).Count();
        }

        public static int LinqSumRequest(MyCollection<MusicalInstrument> collection)
        {
            return (from elem in collection
                    select elem.Id.Number).Sum();
        }

        public static int ExtensionSumRequest(MyCollection<MusicalInstrument> collection)
        {
            return collection.Sum(elem => elem.Id.Number);
        }

        public static int LinqMaxRequest(MyCollection<MusicalInstrument> collection)
        {
            return (from elem in collection
                    select elem.Id.Number).Max();
        }

        public static int ExtensionMaxRequest(MyCollection<MusicalInstrument> collection)
        {
            return collection.Max(elem => elem.Id.Number);
        }

        public static int LinqMinRequest(MyCollection<MusicalInstrument> collection)
        {
            return (from elem in collection
                    select elem.Id.Number).Min();
        } 

        public static int ExtensionMinRequest(MyCollection<MusicalInstrument> collection)
        {
            return collection.Min(elem => elem.Id.Number);
        }

        public static double LinqAvgRequest(MyCollection<MusicalInstrument> collection)
        {
            return (from elem in collection
                    select elem.Id.Number).Average();
        }

        public static double ExtensionAvgRequest(MyCollection<MusicalInstrument> collection)
        {
            return collection.Average(elem => elem.Id.Number);
        }

        public static IEnumerable<IGrouping<string, MusicalInstrument>> LinqGroupByRequest(MyCollection<MusicalInstrument> collection)
        {
            return from elem in collection
                   group elem by elem.Name;
        }

        public static IEnumerable<IGrouping<string, MusicalInstrument>> ExtensionGroupByRequest(MyCollection<MusicalInstrument> collection)
        {
            return collection.GroupBy(elem => elem.Name);
        }
    }
}
