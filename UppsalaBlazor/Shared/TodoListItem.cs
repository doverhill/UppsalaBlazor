using System.Collections.Generic;
using System.Linq;

namespace UppsalaBlazor.Shared
{
    public class TodoListItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Hours { get; set; }

        public double PercentOfTotal { get; set; }

        public TodoListItem()
        {
        }

        public TodoListItem(int id, string title, string description, int hours)
        {
            Id = id;
            Title = title;
            Description = description;
            Hours = hours;
        }

        public static List<TodoListItem> Calculate(List<TodoListItem> items)
        {
            var totalHours = items.Sum(item => item.Hours);
            return items.Select(item => new TodoListItem(item.Id, item.Title, item.Description, item.Hours) { PercentOfTotal = 100.0 * item.Hours / totalHours }).ToList();
        }
    }
}
