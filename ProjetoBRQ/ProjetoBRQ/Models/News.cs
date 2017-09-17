using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace ProjetoBRQ.Models
{
    [Table("NEWS")]
    public class News
    {
        [Key]
        [Column("ID")]
        public int? Id { get; set; }

        [Display(Name = "Titulo")]
        [Column("TITLE")]
        [StringLength(50,ErrorMessage = "Titulo deve ter até 50 caracteres")]
        [Required(ErrorMessage = "Título é obrigatorio", AllowEmptyStrings = false)]
        public string Title { get; set; }

        [Display(Name = "Descrição")]
        [Column("DESCRIPTION")]
        [StringLength(100,ErrorMessage = "Titulo deve ter até 100 caracteres")]
        [Required(ErrorMessage = "Descrição é obrigatoria", AllowEmptyStrings = false)]
        public string Description { get; set; }

        [Display(Name = "Corpo da noticia")]
        [Column("BODY")]
        [StringLength(1000,ErrorMessage = "Corpo da noticia deve ter até 10000 caracteres")]
        [Required(ErrorMessage = "Corpo da noticia é obrigatoria", AllowEmptyStrings = false)]
        public string Body { get; set; }

        [Column("DELETADO")]
        public int? Deletado { get; set; }

        [Column("DATA_CADASTRO")]
        public DateTime DataCadastro { get; set; } = DateTime.Now;

        [Column("DATA_ALTERACAO")]
        public DateTime? DataAlteracao { get; set; }

        public virtual ICollection<ImgNews> ImgNews { get; set; }

        [NotMapped]
        public HttpPostedFileBase File { get; set; }
    }
}