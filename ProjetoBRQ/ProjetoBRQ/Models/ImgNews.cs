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
    [Table("IMG_NEWS")]
    public class ImgNews
    {
        [Column("ID"),Key]
        public int Id { get; set; }

        [Column("MIME_TYPE")]
        public string MimeType { get; set; }

        [Column("FILE_NAME")]
        public string FileName { get; set; }

        [Column("FILE_CONTENT")]
        public byte[] FileContent { get; set; }

        [ScriptIgnore(ApplyToOverrides = true)]
        [JsonIgnore]
        public virtual News News { get; set; }

        [ForeignKey("News"),Column("ID_NOTICIA")]
        public int IdNews { get; set; }

        [Column("DELETADO")]
        public bool Deletado { get; set; }

        [Column("DATA_CADASTRO")]
        public DateTime DataCadastro { get; set; } = DateTime.Now;

        [Column("DATA_ALTERACAO")]
        public DateTime? DataAlteracao { get; set; }

        [Column("FILE_LENGHT")]
        public int FileLenght { get; set; }

        public string Base64Image()
        {
            if (FileContent != null && MimeType != null)
                return "data:"+MimeType+";base64,"+Convert.ToBase64String(FileContent);
            return "";
        }
    }
}