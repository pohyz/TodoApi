using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoWebApplication.Models
{
    public interface ITodoRepository
    {
        Task<Uri> Add(TodoItem item);
        Task<IEnumerable<TodoItem>> GetAll();
        Task<TodoItem> Find(string key);
        Task Remove(string key);
        Task<TodoItem> Update(TodoItem item);
    }
}
