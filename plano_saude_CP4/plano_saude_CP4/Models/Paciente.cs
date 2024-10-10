using plano_saude_CP4.Models;

namespace plano_saude_CP4.Models
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
