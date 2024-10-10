using Microsoft.AspNetCore.Mvc;
using mvc_planosaude.Models;
using System.Diagnostics;

namespace mvc_planosaude.Controllers
{
    public class PlanosSaudeController : Controller
    {
        private readonly ILogger<PlanosSaudeController> _logger;

        private readonly IHttpClientFactory _clientFactory;
        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}
        public PlanosSaudeController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        // GET: PlanosSaude
        public async Task<IActionResult> Index()
        {
            var client = _clientFactory.CreateClient("APIClient");
            List<PlanoSaude> planos = new List<PlanoSaude>(); 

            HttpResponseMessage response = await client.GetAsync("api/PlanosSaude");

            
            planos = await response.Content.ReadFromJsonAsync<List<PlanoSaude>>() ?? new List<PlanoSaude>();
            

            return View(planos);
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // GET: PlanoSaude/Create
        public IActionResult Create()
        {
            return View(new PlanoSaude());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PlanoSaude planosaude)
        {
            if (ModelState.IsValid)
            {
                var client = _clientFactory.CreateClient("APIClient");
                HttpResponseMessage response = await client.PostAsJsonAsync("api/PlanosSaude", planosaude);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index)); 
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Não foi possível criar o plano.");
                }
            }

            return View(planosaude); 
        }

        // GET: PlanoSaude/Update/5
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var client = _clientFactory.CreateClient("APIClient");
            var response = await client.GetAsync($"api/PlanosSaude/{id}");

            if (response.IsSuccessStatusCode)
            {
                var planosaude = await response.Content.ReadFromJsonAsync<PlanoSaude>();
                if (planosaude != null)
                {
                    return View(planosaude);
                }
            }

            TempData["ErrorMessage"] = "Plano não encontrado.";
            return RedirectToAction(nameof(Index));
        }

        // POST: PlanoSaude/Update/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, PlanoSaude planosaude)
        {
            if (id != planosaude.Id) 
            {
                return BadRequest(); 
            }

            if (ModelState.IsValid)
            {
                var client = _clientFactory.CreateClient("APIClient");
                HttpResponseMessage response = await client.PutAsJsonAsync($"api/PlanosSaude/{id}", planosaude);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Não foi possível atualizar o plano.");
                }
            }

            return View(planosaude);
        }


        // DELETE: PlanoSaude/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var client = _clientFactory.CreateClient("APIClient");

            HttpResponseMessage response = await client.DeleteAsync($"api/PlanosSaude/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
              
                TempData["ErrorMessage"] = "Não foi possível excluir o plano.";
                return RedirectToAction(nameof(Index));
            }
        }



    }
}
