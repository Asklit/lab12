using Microsoft.VisualBasic;
using Musical_Instrument;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab14
{
    public class firstPart
    {
        /// <summary>
        /// Получение всех групп с количесовом инструментов больше указанного
        /// </summary>
        public static IEnumerable<dynamic> LinqWhereRequest(SortedDictionary<string, List<MusicalInstrument>> collection, int count)
        {
            return from data in collection
                   where data.Value.Count > count
                   select new { Key = data.Key, Value = data.Value };
        }

        /// <summary>
        /// Получение всех групп с количесовом инструментов больше указанного
        /// </summary>
        public static IEnumerable<dynamic> ExtensionWhereRequest(SortedDictionary<string, List<MusicalInstrument>> collection, int count)
        {
            return collection.Where(pair => pair.Value.Count > count).Select(pair => new { Key = pair.Key, Value = pair.Value }); ;
        }

        /// <summary>
        /// Получение количества всех инструментов всех групп
        /// </summary>
        public static int LinqSumRequest(SortedDictionary<string, List<MusicalInstrument>> collection)
        {
            return (from data in collection
                    select data.Value.Count).Sum();
        }

        /// <summary>
        /// Получение количества всех инструментов всех групп
        /// </summary>
        public static int ExtensionSumRequest(SortedDictionary<string, List<MusicalInstrument>> collection)
        {
            return collection.SelectMany(pair => pair.Value).Count();
        }

        /// <summary>
        /// Получение максимального количества инструментов у групп
        /// </summary>
        public static int LinqMaxRequest(SortedDictionary<string, List<MusicalInstrument>> collection)
        {
            return (from data in collection
                    select data.Value.Count).Max();
        }

        /// <summary>
        /// Получение максимального количества инструментов у групп
        /// </summary>
        public static int ExtensionMaxRequest(SortedDictionary<string, List<MusicalInstrument>> collection)
        {
            return collection.Select(pair => pair.Value.Count).Max();
        }

        /// <summary>
        /// Получение минимального количества инструментов у групп
        /// </summary>
        public static int LinqMinRequest(SortedDictionary<string, List<MusicalInstrument>> collection)
        {
            return (from data in collection
                    select data.Value.Count).Min();
        }

        /// <summary>
        /// Получение минимального количества инструментов у групп
        /// </summary>
        public static int ExtensionMinRequest(SortedDictionary<string, List<MusicalInstrument>> collection)
        {
            return collection.Select(pair => pair.Value.Count).Min();
        }

        /// <summary>
        /// Получение среднего количества инструментов у групп
        /// </summary>
        public static double LinqAvgRequest(SortedDictionary<string, List<MusicalInstrument>> collection)
        {
            return (from data in collection
                    select data.Value.Count).Average();
        }

        /// <summary>
        /// Получение среднего количества инструментов у групп
        /// </summary>
        public static double ExtensionAvgRequest(SortedDictionary<string, List<MusicalInstrument>> collection)
        {
            return collection.Select(pair => pair.Value.Count).Average();
        }

        /// <summary>
        /// Группировка по названию интрументов
        /// </summary>
        public static IEnumerable<dynamic> LinqGroupByRequest(SortedDictionary<string, List<MusicalInstrument>> collection)
        {
            return from groups in collection
                   from instrument in groups.Value
                   group instrument by instrument.Name;
        }

        /// <summary>
        /// Группировка по названию интрументов
        /// </summary>
        public static IEnumerable<dynamic> ExtensionGroupByRequest(SortedDictionary<string, List<MusicalInstrument>> collection)
        {
            return collection.SelectMany(pair => pair.Value, (pair, instrument) => new { Key = pair.Key, Value = instrument })
                     .GroupBy(x => x.Value.Name, x => x.Value);
        }

        /// <summary>
        /// Объединение коллекций
        /// </summary>
        public static IEnumerable<dynamic> LinqJoinRequest(SortedDictionary<string,
            List<MusicalInstrument>> collection1, SortedDictionary<string, List<MusicalInstrument>> collection2)
        {
            return from data1 in collection1
                   join data2 in collection2 on data1.Key equals data2.Key
                   let combinedList = new List<MusicalInstrument>(data1.Value)
                   select new { Key = data1.Key, Value = combinedList.Concat(data2.Value).ToList() };
        }

        /// <summary>
        /// Объединение коллекций
        /// </summary>
        public static IEnumerable<dynamic> ExtensionJoinRequest(SortedDictionary<string, List<MusicalInstrument>> collection1, SortedDictionary<string, List<MusicalInstrument>> collection2)
        {
            return collection1.Join(collection2,
                                    data1 => data1.Key,
                                    data2 => data2.Key,
                                    (data1, data2) =>
                                    {
                                        var combinedList = new List<MusicalInstrument>(data1.Value);
                                        combinedList.AddRange(data2.Value);
                                        return new { Key = data1.Key, Value = combinedList };
                                    });
        }
    }
}
