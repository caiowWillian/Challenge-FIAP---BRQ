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
        public int? Id { get; set; }

        [Column("USERGUID")]
        [Display(Name = "Usuario")]
        public string UserGUID { get; set; }

        [Column("NUM")]
        [Display(Name = "Número de unidades")]
        public int? Num { get; set; }

        [NotMapped]
        public IList<Investiment> Investiment { get; set; }

        [Column("INPUT_DATE")]
        public DateTime InputDate { get; set; }

        [Column("CARD_NUMBER")]
        [Display(Name = "Nº Cartão")]
        public string CardNumber { get; set; }

        [Column("ID_INVESTIMENT")]
        [Display(Name = "Tipo de investimento")]
        public int? InvestimentId { get; set; }

        [Column("VALUE_INVESTIMENT")]
        public float ValueInvestiment { get; set; }

        [NotMapped]
        public double? TotalValue { get; set; }

        [Display(Name = "Investimentos")]
        [NotMapped]
        public string GetAllInvestimentNameWithValue
        {
            get
            {
                string investimento = "<table>";

                for(int i = 0; i < Investiment.Count(); i++)
                {
                    investimento += "<tr>";
                    investimento += "<th>";
                    investimento += Investiment[i].Name;
                    investimento += "</th>";
                    investimento += "<td>";
                    investimento += "Quantidade: " + Investiment[i].Num;
                    investimento += "</td>";
                    investimento += "<td>";
                    investimento += "Total: R$ " + Investiment[i].Total;
                    investimento += "</td>";
                    investimento += "</tr>";
                }

                investimento += "</table>";

                return investimento;
            }
        }

        [Display(Name = "Total investido")]
        [NotMapped]
        public double? GetTotalInvestiment { get
            {
                double? tot = 0;

                for (int i = 0; i < Investiment.Count(); i++)
                    tot += Investiment[i].Num * Investiment[i].Value;

                return tot;
            }
        }
    }
}