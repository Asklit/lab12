using System;
using System.Diagnostics.CodeAnalysis;
namespace lab13
{
    public class Journal
    {
        public List<JournalEntry> JournalList;

        public Journal()
        {
            JournalList = new List<JournalEntry>();
        }

        public void Add(object source, CollectionHandlerEventArgs args)
        {
            JournalList.Add(new(args.Name, args.Action, args.Item.ToString()));
        }

        [ExcludeFromCodeCoverage]
        public void PrintJournal()
        {
            if (JournalList.Count == 0)
                Console.WriteLine("Журнал пустой");
            else
            {
                foreach (var item in JournalList)
                {
                    Console.WriteLine(item);
                }
            }
        }
    }
}

