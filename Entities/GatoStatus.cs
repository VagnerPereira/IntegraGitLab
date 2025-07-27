using AppDeCastracao.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppDeCastracao.Entities
{
    [Table("tbGatoStatus")]
    public class GatoStatus
    {

        public int GatoId { get; set; }
        [ForeignKey("GatoId")]
        public Gato? Gato { get; set; }
        public int StatusId { get; set; }
        [ForeignKey("StatusId")]
        public Status? Status { get; set; }

        public DateTime DataAplicacao { get; set; } = DateTime.Now;
    }
}