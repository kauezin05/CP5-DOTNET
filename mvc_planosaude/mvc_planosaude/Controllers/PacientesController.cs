using Microsoft.AspNetCore.Mvc;
using mvc_planosaude.Models;
using System.Diagnostics;

namespace mvc_planosaude.Controllers
{
    public class PacientesController : Controller
    {
        private readonly ILogger<PacientesController> _logger;

        private readonly IHttpClientFactory _clientFactory;
        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}
        public PacientesController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _clientFactory.CreateClient("APIClient");
            List<Paciente> pacientes = new List<Paciente>(); 

            HttpResponseMessage response = await client.GetAsync("api/pacientes");

            if (response.IsSuccessStatusCode)
            {
                pacientes = await response.Content.ReadFromJsonAsync<List<Paciente>>() ?? new List<Paciente>();
            }

            return View(pacientes); 
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

        // GET: Paciente/Create
        public IActionResult Create()
        {
            return View(new Paciente());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                var client = _clientFactory.CreateClient("APIClient");
                HttpResponseMessage response = await client.PostAsJsonAsync("api/pacientes", paciente);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index)); 
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Não foi possível criar o paciente.");
                }
            }

            return View(paciente); 
        }

        // GET: Paciente/Update/5
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var client = _clientFactory.CreateClient("APIClient");
            var response = await client.GetAsync($"api/pacientes/{id}");

            if (response.IsSuccessStatusCode)
            {
                var paciente = await response.Content.ReadFromJsonAsync<Paciente>();
                if (paciente != null)
                {
                    return View(paciente);
                }
            }

            TempData["ErrorMessage"] = "Paciente não encontrado.";
            return RedirectToAction(nameof(Index));
        }

        // POST: Paciente/Update/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, Paciente paciente)
        {
            if (id != paciente.Id) 
            {
                return BadRequest(); 
            }

            if (ModelState.IsValid)
            {
                var client = _clientFactory.CreateClient("APIClient");
                HttpResponseMessage response = await client.PutAsJsonAsync($"api/pacientes/{id}", paciente);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Não foi possível atualizar o paciente.");
                }
            }

            return View(paciente);
        }


        // DELETE: Paciente/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var client = _clientFactory.CreateClient("APIClient");

            HttpResponseMessage response = await client.DeleteAsync($"api/pacientes/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                // Tratamento de erro
                TempData["ErrorMessage"] = "Não foi possível excluir o paciente.";
                return RedirectToAction(nameof(Index));
            }
        }



    }
}
