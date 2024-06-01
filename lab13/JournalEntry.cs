using System;
namespace lab13
{
	public class JournalEntry
	{

		public string CollectionName { get; set; }
        public string Action { get; set; }
        public string Item { get; set; }

        public JournalEntry(string name, string action, string item)
		{
			CollectionName = name;
			Action = action;
			Item = item;
		}

        public override string ToString()
        {
            return $"В коллекции \"{CollectionName}\" произошло событие \"{Action}\" у объекта \"{Item}\".";
        }
    }
}

