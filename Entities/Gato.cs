using System.ComponentModel.DataAnnotations.Schema;

namespace AppDeCastracao.Entities
{
[Table("tbGato")]
    public class Gato
    {
        public int Id { get; set; }
        public string EtiquetaIdentificacao { get; set; } = null!;
        public ICollection<GatoStatus> GatoStatuses { get; set; } = new List<GatoStatus>();
        public string Nome { get; set; } = null!;
        public int? IdadeAnos { get; set; }
        public int? IdadeMeses { get; set; }
        public string? Sexo { get; set; }
        public string? Cor { get; set; }
        public string? Localizacao { get; set; }
        public string? Observacao { get; set; }
        public string? FotoUrl { get; set; }
        public bool PossuiDono { get; set; }
        public int? DonoId { get; set; }
        public Dono? Dono { get; set; }
    }
}