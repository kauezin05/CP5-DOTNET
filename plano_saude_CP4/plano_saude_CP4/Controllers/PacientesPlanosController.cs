using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using plano_saude_CP4.Data;
using plano_saude_CP4.Models;

namespace plano_saude_CP4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacientesPlanosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PacientesPlanosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/PacientesPlanos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PacientePlano>>> GetPacientesPlanos()
        {
            return await _context.PacientesPlanos
                .Include(pp => pp.Paciente) 
                .Include(pp => pp.PlanoSaude) 
                .ToListAsync();
        }

        // GET: api/PacientesPlanos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PacientePlano>> GetPacientePlano(int id)
        {
            var pacienteplano = await _context.PacientesPlanos
                .Include(pp => pp.Paciente) 
                .Include(pp => pp.PlanoSaude) 
                .FirstOrDefaultAsync(pp => pp.Id == id);

            if (pacienteplano == null)
            {
                return NotFound();
            }

            return pacienteplano;
        }

        // PUT: api/PacientesPlanos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPacientePlano(int id, PacientePlano pacienteplano)
        {
            if (id != pacienteplano.Id)
            {
                return BadRequest("O ID do paciente-plano não corresponde.");
            }

            _context.Entry(pacienteplano).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PacientePlanoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }



   
        // POST: api/PacientesPlanos
        [HttpPost]
        public async Task<IActionResult> PostPacientePlano([FromBody] PacientePlano pacienteplano)
        {
            
            var planoExistente = await _context.PlanosSaude.FindAsync(pacienteplano.PlanoSaudeId);
            if (planoExistente == null)
            {
                return BadRequest(new { message = "Plano de saúde não encontrado." });
            }

            
            var pacienteExistente = await _context.Pacientes.FindAsync(pacienteplano.PacienteId);
            if (pacienteExistente == null)
            {
                return BadRequest(new { message = "Paciente não encontrado." });
            }

            
            pacienteplano.PlanoSaude = planoExistente;
            pacienteplano.Paciente = pacienteExistente;

            
            _context.PacientesPlanos.Add(pacienteplano);

            
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPacientePlano", new { id = pacienteplano.Id }, pacienteplano);
        }



        // DELETE: api/PacientesPlanos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePacientePlano(int id)
        {
            var pacienteplano = await _context.PacientesPlanos.FindAsync(id);
            if (pacienteplano == null)
            {
                return NotFound();
            }

            _context.PacientesPlanos.Remove(pacienteplano);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PacientePlanoExists(int id)
        {
            return _context.PacientesPlanos.Any(e => e.Id == id);
        }
    }
}
