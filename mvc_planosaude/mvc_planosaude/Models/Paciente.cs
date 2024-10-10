using mvc_planosaude.Models;

namespace mvc_planosaude.Models
{
    public class Paciente
    {
        public int Id { get; set; }
        public string NomeCompleto { get; set; }
        public DateTime DataNascimento { get; set; }
        public string CPF { get; set; }
        public string Endereco { get; set; }
        public string Telefone { get; set; }

    }
}
