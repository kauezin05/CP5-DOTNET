using Microsoft.AspNetCore.Mvc;
using mvc_planosaude.Models;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Json;

namespace mvc_planosaude.Controllers
{
    public class PacientesPLanosController : Controller
    {
        private readonly ILogger<PacientesPLanosController> _logger;
        private readonly IHttpClientFactory _clientFactory;
       

        public PacientesPLanosController(IHttpClientFactory clientFactory, ILogger<PacientesPLanosController> logger)
        {
            _clientFactory = clientFactory;
            _logger = logger;
        }

        // GET: PacientesPlanos
        public async Task<IActionResult> Index()
        {
            var client = _clientFactory.CreateClient("APIClient");
            List<PacientePlano> pacientesPlanos = new List<PacientePlano>();

            HttpResponseMessage response = await client.GetAsync("api/pacientesplanos");

            if (response.IsSuccessStatusCode)
            {
                pacientesPlanos = await response.Content.ReadFromJsonAsync<List<PacientePlano>>() ?? new List<PacientePlano>();
            }

            return View(pacientesPlanos);
        }

        // GET: PacientesPlanos/Create
        public async Task<IActionResult> Create()
        {
            var client = _clientFactory.CreateClient("APIClient");

            
            var responsePacientes = await client.GetAsync("api/pacientes");
            var pacientes = new List<Paciente>();
            if (responsePacientes.IsSuccessStatusCode)
            {
                pacientes = await responsePacientes.Content.ReadFromJsonAsync<List<Paciente>>() ?? new List<Paciente>();
            }

           
            var responsePlanos = await client.GetAsync("api/planossaude");
            var planos = new List<PlanoSaude>();
            if (responsePlanos.IsSuccessStatusCode)
            {
                planos = await responsePlanos.Content.ReadFromJsonAsync<List<PlanoSaude>>() ?? new List<PlanoSaude>();
            }

           
            ViewData["Title"] = "Novo PacientePlano";

            
            ViewBag.Pacientes = pacientes;
            ViewBag.Planos = planos;

            return View(new PacientePlano());
        }

        [HttpPost]
        public async Task<IActionResult> Create(int pacienteId, int planoSaudeId)
        {
            
            var client = _clientFactory.CreateClient("APIClient");
            var associacaoResponse = await client.GetAsync($"api/pacientesplanos/{pacienteId}/{planoSaudeId}");

            
            var jsonContent = await associacaoResponse.Content.ReadAsStringAsync(); 
            Console.WriteLine(jsonContent); 

            if (associacaoResponse.IsSuccessStatusCode)
            {
                var associacaoExistente = await associacaoResponse.Content.ReadFromJsonAsync<PacientePlano>(new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true 
                });

                if (associacaoExistente != null)
                {
                    ViewBag.Message = "Esta associação já existe.";
                }
            }
            else
            {
                
                var novaAssociacao = new PacientePlano
                {
                    PacienteId = pacienteId,
                    PlanoSaudeId = planoSaudeId
                };

                var postResponse = await client.PostAsJsonAsync("api/pacientesplanos", novaAssociacao);

                if (postResponse.IsSuccessStatusCode)
                {
                    ViewBag.Message = "Paciente associado ao Plano de Saúde com sucesso.";
                }
                else
                {
                    ViewBag.Message = "Erro ao criar a associação.";
                }
            }

            return View();
        }







        // GET: PacientesPlanos/Update/5
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var client = _clientFactory.CreateClient("APIClient");
            var response = await client.GetAsync($"api/pacientesplanos/{id}");

            if (response.IsSuccessStatusCode)
            {
                var pacientePlano = await response.Content.ReadFromJsonAsync<PacientePlano>();
                if (pacientePlano != null)
                {
                    var responsePacientes = await client.GetAsync("api/pacientes");
                    ViewBag.Pacientes = await responsePacientes.Content.ReadFromJsonAsync<List<Paciente>>() ?? new List<Paciente>();

                    var responsePlanos = await client.GetAsync("api/planossaude");
                    ViewBag.Planos = await responsePlanos.Content.ReadFromJsonAsync<List<PlanoSaude>>() ?? new List<PlanoSaude>();

                    return View(pacientePlano);
                }
            }

            TempData["ErrorMessage"] = "Associação não encontrada.";
            return RedirectToAction(nameof(Index));
        }


        // POST: PacientesPLanos/Update/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, PacientePlano pacientePlano)
        {
            if (id != pacientePlano.Id) 
            {
                return BadRequest(); 
            }

            if (ModelState.IsValid)
            {
                var client = _clientFactory.CreateClient("APIClient");
                HttpResponseMessage response = await client.PutAsJsonAsync($"api/pacientesplanos/{id}", pacientePlano);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, $"Não foi possível atualizar a associação. Erro: {errorContent}");
                }
            }

            return View(pacientePlano);
        }

        // DELETE: PacientesPLanos/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var client = _clientFactory.CreateClient("APIClient");
            HttpResponseMessage response = await client.DeleteAsync($"api/pacientesplanos/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                
                TempData["ErrorMessage"] = "Não foi possível excluir a associação.";
                return RedirectToAction(nameof(Index));
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
