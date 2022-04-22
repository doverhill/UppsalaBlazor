namespace UppsalaBlazor.Shared
{
    public class TodoListItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public TodoListItem()
        {
        }

        public TodoListItem(int id, string title, string description)
        {
            Id = id;
            Title = title;
            Description = description;
        }
    }
}
