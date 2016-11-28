using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace TodoWebApplication.Models
{
    public class TodoRepository : ITodoRepository
    {
        private static IEnumerable<TodoItem> _todoList = new List<TodoItem>();

        private static HttpClient client = new HttpClient();

        public TodoRepository()
        {
            ConnectToWebApi();
        }

        private void ConnectToWebApi()
        {
            client.BaseAddress = new Uri("http://todowebapi.azurewebsites.net/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IEnumerable<TodoItem>> GetAll()
        {
            HttpResponseMessage response = await client.GetAsync("api/todo");
            if(response.IsSuccessStatusCode)
            {
                _todoList = await response.Content.ReadAsAsync<IEnumerable<TodoItem>>();
            }
            return _todoList;
        }

        public async Task<Uri> Add(TodoItem item)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("api/todo", item);
            response.EnsureSuccessStatusCode();

            return response.Headers.Location;
        }

        public async Task<TodoItem> Find(string key)
        {
            TodoItem item = null;
            HttpResponseMessage response = await client.GetAsync($"api/todo/{key}");
            if (response.IsSuccessStatusCode)
            {
                item = await response.Content.ReadAsAsync<TodoItem>();
            }
            return item;
        }

        public async Task Remove(string key)
        {
            HttpResponseMessage response = await client.DeleteAsync($"api/todo/{key}");
        }

        public async Task<TodoItem> Update(TodoItem item)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync($"api/todo/{item.Key}", item);
            response.EnsureSuccessStatusCode();

            item = await response.Content.ReadAsAsync<TodoItem>();

            return item;
        }
    }
}
