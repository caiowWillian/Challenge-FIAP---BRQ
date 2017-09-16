using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace ProjetoBRQ.Models
{
    [Table("NEWS")]
    public class News
    {
        [Column("ID"),Key]
        public int Id { get; set; }

        [Column("TITLE")]
        public string Title { get; set; }

        [Column("DESCRIPTION")]
        public string Description { get; set; }

        [Column("BODY")]
        public string Body { get; set; }

        [Column("DELETADO")]
        public int? Deletado { get; set; }

        //[Column("DATA_CADASTRO")]
        //public DateTime DataCadastro { get; set; }

        //[Column("DATA_ALTERACAO")]
        //public DateTime DataAlteracao { get; set; }

        //[Column("ID_IMD")]
        //[Column("ID_NOTICIA")]
        public virtual ICollection<ImgNews> ImgNews { get; set; }

        [NotMapped]
        public HttpPostedFileBase File { get; set; }
        //public virtual List<ImgNews> ImgNews { get; set; }
    }
}