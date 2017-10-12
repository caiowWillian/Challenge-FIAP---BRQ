using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjetoBRQ.Models
{
    [Table("INVESTIMENT_USER")]
    public class InvestimentUser
    {
        [Column("ID")]
        [Key]
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Column("USER")]
        [Display(Name = "Usuario")]
        public string UserGUID { get; set; }

        [Column("NUMBER")]
        [Display(Name = "Número de unidades")]
        public int Num { get; set; }

        public virtual ICollection<Investiment> Investiment { get; set; }

        [Column("INPUT_DATE")]
        public DateTime InputDate { get; set; }

        [CreditCard]
        [Column("CARD_NUMBER")]
        public string CardNumber { get; set; }

        [NotMapped]
        public int InvestimentId { get; set; }
    }
}