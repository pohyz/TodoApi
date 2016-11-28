using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using TodoWebApplication.Models;

namespace TodoWebApplication.Controllers
{
    public class TodoController : Controller
    {
        private readonly ITodoRepository _todoRepo;
        public TodoController(ITodoRepository todoRepo)
        {
            _todoRepo = todoRepo;
        }
        //GET: /Todo/
        public async Task<IActionResult> Index()
        {
            return View(await _todoRepo.GetAll());
        }

        //GET: /Todo/Details/{Key}
        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return NotFound();
            }

            var todoItem = await _todoRepo.Find(id);
            if(todoItem == null)
            {
                return NotFound();
            }

            return View(todoItem);
        }

        //GET: /Todo/Create
        public IActionResult Create()
        {
            return View();
        }

        //POST: /Todo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Key,Name,IsComplete")] TodoItem todoItem)
        {
            if(ModelState.IsValid)
            {
                await _todoRepo.Add(todoItem);
                return RedirectToAction("Index");
            }

            return View(todoItem);
        }

        //GET: /Todo/Edit/{Key}
        public async Task<IActionResult> Edit(string id)
        {
            if(string.IsNullOrWhiteSpace(id))
            {
                return NotFound();
            }

            var todoItem = await _todoRepo.Find(id);
            if(todoItem == null)
            {
                return NotFound();
            }
            return View(todoItem);
        }

        //POST: /Todo/Edit/{Key}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Key,Name,IsComplete")] TodoItem todoItem)
        {
            if(id != todoItem.Key)
            {
                return NotFound();
            }

            if(ModelState.IsValid)
            {
                try
                {
                    await _todoRepo.Update(todoItem);
                }
                catch(HttpRequestException)
                {
                    throw;
                }
                return RedirectToAction("Index");
            }
            return View(todoItem);
        }

        // GET: /Todo/Delete/{Key}
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return NotFound();
            }

            var todoItem = await _todoRepo.Find(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            return View(todoItem);
        }

        // POST: /Todo/Delete/{Key}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var todoItem = await _todoRepo.Find(id);
            await _todoRepo.Remove(id);
            return RedirectToAction("Index");
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }
    }
}
