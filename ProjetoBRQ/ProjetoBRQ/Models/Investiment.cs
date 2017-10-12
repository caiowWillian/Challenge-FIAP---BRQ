using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjetoBRQ.Models
{
    [Table("INVESTIMENT")]
    public class Investiment
    {
        [Column("ID")]
        [Key]
        public int? Id { get; set; }

        [Display(Name = "Nome")]
        [Column("NAME")]
        [Required(ErrorMessage = "Nome é obrigatorio", AllowEmptyStrings = false)]
        [StringLength(50, ErrorMessage = "Nome pode ter até 50 caracteres")]
        public string Name { get; set; }
        
        [Display(Name = "Descrição")]
        [Column("DESCRIPTION")]
        [Required(ErrorMessage = "Descrição é obrigatorio", AllowEmptyStrings = false)]
        [StringLength(100, ErrorMessage = "Descrição pode ter até 100 caracteres")]
        public string Description { get; set; }

        [Display(Name = "Estoque")]
        [Column("STOCK")]
        [Range(0,999999,ErrorMessage = "Numero de estoque invalido")]
        [Required(ErrorMessage = "Estoque é obrigatorio", AllowEmptyStrings = false)]
        public int? Stock { get; set; }

        [Display(Name = "Valor")]
        [Column("VALUE")]
        [Range(1, 999999, ErrorMessage = "valor invalido")]
        [Required(ErrorMessage = "valor é obrigatorio", AllowEmptyStrings = false)]
        public double? Value { get; set; }

        [Display(Name = "Date de cadastro")]
        [Column("INPUT_DATE")]
        public DateTime InputDate { get; set; }

        [Display(Name = "Data de alteração")]
        [Column("UPDATE_DATE")]
        public DateTime? UpdateDate { get; set; }

        [Column("DELETED")]
        public bool Deleted { get; set; }

        public virtual InvestimentUser InvestimentUser{ get;set; }
    }
}