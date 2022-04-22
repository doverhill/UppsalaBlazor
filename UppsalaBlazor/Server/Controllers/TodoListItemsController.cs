using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using UppsalaBlazor.Server.Helpers;
using UppsalaBlazor.Shared;

namespace UppsalaBlazor.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoListItemsController : Controller
    {
        private Database _db;

        public TodoListItemsController(Database db)
        {
            _db = db;
        }

        [HttpGet]
        [Route("getAll")]
        public IEnumerable<TodoListItem> GetAll()
        {
            return _db.GetItems();
        }

        [HttpGet]
        [Route("add")]
        public IEnumerable<TodoListItem> Add()
        {
            _db.CreateNew();
            return _db.GetItems();
        }

        [HttpPost]
        [Route("update")]
        public IEnumerable<TodoListItem> Update([FromBody] TodoListItem item)
        {
            _db.UpdateItem(item.Id, item.Title, item.Description, item.Hours);
            return _db.GetItems();
        }

        [HttpGet]
        [Route("delete/{id}")]
        public IEnumerable<TodoListItem> Delete(int id)
        {
            _db.DeleteItemById(id);
            return _db.GetItems();
        }
    }
}
