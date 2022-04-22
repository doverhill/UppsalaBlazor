using System.Collections.Generic;
using System.Linq;
using UppsalaBlazor.Shared;

namespace UppsalaBlazor.Server.Helpers
{
    public class Database
    {
        private int _nextId = 0;
        private SortedDictionary<int, TodoListItem> _items = new();

        public void CreateNew()
        {
            var newId = _nextId++;
            var newItem= new TodoListItem(newId, "New title", "New description", 1);
            _items.Add(newId, newItem);
        }

        public void UpdateItem(int id, string title, string description, int hours)
        {
            if (_items.TryGetValue(id, out var item))
            {
                item.Title = title;
                item.Description = description;
                item.Hours = hours;
            }
        }

        public TodoListItem GetItemById(int id)
        {
            if (_items.TryGetValue(id, out var item)) return item;
            return null;
        }

        public List<TodoListItem> GetItems()
        {
            return TodoListItem.Calculate(_items.Values.ToList());
        }

        public void DeleteItemById(int id)
        {
            _items.Remove(id);
        }
    }
}
