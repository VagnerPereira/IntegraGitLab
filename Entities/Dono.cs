using System.ComponentModel.DataAnnotations.Schema;


namespace AppDeCastracao.Entities
{
    [Table("tbDono")]
    public class Dono
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string Endereco { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public DateTime DataCadastro { get; set; } = DateTime.Now;

        // Relacionamento opcional com Gato (se houver)
        public List<Gato>? Gatos { get; set; }
    }
}