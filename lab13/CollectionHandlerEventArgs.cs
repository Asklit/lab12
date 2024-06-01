using System;
namespace lab13
{
    public class CollectionHandlerEventArgs : EventArgs
    {
        public string Name { get; set; }
        public string Action { get; set; }
        public object Item { get; set; }

        public CollectionHandlerEventArgs(string name, string action, object item)
        {
            Action = action;
            Item = item;
            Name = name;
        }
    }
}

