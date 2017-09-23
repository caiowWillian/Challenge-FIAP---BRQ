using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoBRQ.Models
{
    [Table("CONTATOS")]
    public class Contatos
    {
        [Key]
        [Column("ID")]
        public int? Id { get; set; }

        [StringLength(200,ErrorMessage = "Nome deve ter até 200 caracteres")]
        [Required(ErrorMessage = "Nome é obrigatorio")]
        [Column("NOME")]
        public string Nome { get; set; }

        [StringLength(50, ErrorMessage = "Email deve ter até 50 caracteres")]
        [Required(ErrorMessage = "Email é obrigatorio")]
        [Column("EMAIL")]
        public string Email { get; set; }

        [StringLength(15, ErrorMessage = "Telefone deve ter até 15 caracteres")]
        [Required(ErrorMessage = "Telefone é obrigatorio")]
        [Column("TELEFONE")]
        public string Telefone { get; set; }

        [Column("DELETADO")]
        public bool Deletado { get; set; }

        [Display(Name = "Data de Cadastro")]
        [Column("DATA_CADASTRO")]
        public DateTime DataCadastro { get; set; }

        [Display(Name = "Ultima alteração")]
        [Column("DATA_ALTERACAO")]
        public DateTime? DataAlteracao { get; set; }
    }
}