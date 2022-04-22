using System.Collections.Generic;
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
            var newItem= new TodoListItem(newId, "New title", "New description");
            _items.Add(newId, newItem);
        }

        public void UpdateItem(int id, string title, string description)
        {
            if (_items.TryGetValue(id, out var item))
            {
                item.Title = title;
                item.Description = description;
            }
        }

        public TodoListItem GetItemById(int id)
        {
            if (_items.TryGetValue(id, out var item)) return item;
            return null;
        }

        public IEnumerable<TodoListItem> GetItems()
        {
            return _items.Values;
        }

        public void DeleteItemById(int id)
        {
            _items.Remove(id);
        }
    }
}
