using plano_saude_CP4.Models;

public class PacientePlano
{
    public int Id { get; set; }

    public int PacienteId { get; set; }
    public virtual Paciente Paciente { get; set; }

    public int PlanoSaudeId { get; set; }
    public virtual PlanoSaude PlanoSaude { get; set; }
}
