using mvc_planosaude.Models;

namespace mvc_planosaude.Models
{
    public class PacientePlano
    {
        public int Id { get; set; } 
        public int PacienteId { get; set; } 
        public int PlanoSaudeId { get; set; } 

        public virtual Paciente Paciente { get; set; } 
        public virtual PlanoSaude PlanoSaude { get; set; } 
    }
}
