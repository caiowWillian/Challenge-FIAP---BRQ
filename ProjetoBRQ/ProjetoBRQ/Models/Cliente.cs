using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjetoBRQ.Models
{
    [Table("CLIENTES_BRQ")]
    public class Cliente
    {
        [Key]
        [Column("ID")]
        public int? Id { get; set; }

        [Display(Name = "Cpf")]
        [Column("CPF")]
        [Required(ErrorMessage = "CPF é Obrigatorio")]
        public string Cpf { get; set; }

        [Display(Name = "Nome")]
        [Column("NOME")]
        [StringLength(100,ErrorMessage = "Limite de 100 caractetes")]
        [Required(ErrorMessage = "É necessario digitar o nome do cliente")]
        public string Nome { get; set; }

        [Display(Name = "E-mail")]
        [Column("EMAIL")]
        [DataType(DataType.EmailAddress,ErrorMessage = "Formato Invalido!")]
        [StringLength(100, ErrorMessage = "Limite de 100 caractetes")]
        [Required(ErrorMessage = "É necessario Digitar o email do cliente")]
        public string Email { get; set; }

        [Display(Name = "Telefone")]
        [Column("TELEFONE")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Formato Invalido!")]
        [StringLength(20, ErrorMessage = "Limite de 20 caractetes")]
        [Required(ErrorMessage = "É necessario digitar o telefone")]
        public string Telefone { get; set; }

        [Display(Name = "Endereço")]
        [Column("ENDERECO")]
        [StringLength(200, ErrorMessage = "Limite de 200 caractetes")]
        [Required(ErrorMessage = "É necessário digitar o endereço")]
        public string Endereco { get; set; }

        [Display(Name = "Complemento")]
        [Column("COMPLEMENTO")]
        [StringLength(100, ErrorMessage = "Limite de 100 caractetes")]
        public string Complemento { get; set; }

        [Column("DELETADO")]
        public bool Deletado { get; set; }

        [Column("DATA_CADASTRO")]
        public DateTime DataCadastro { get; set; }

        [Column("DATA_ALTERACAO")]
        public DateTime? DataAlteracao { get; set; }
    }
}