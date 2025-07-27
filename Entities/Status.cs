using System.ComponentModel.DataAnnotations.Schema;

namespace AppDeCastracao.Entities
{
    [Table("tbStatus")]
    public class Status
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;

        public ICollection<GatoStatus> GatoStatuses { get; set; } = new List<GatoStatus>();
    }
}