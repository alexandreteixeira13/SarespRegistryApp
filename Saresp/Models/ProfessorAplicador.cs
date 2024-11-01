using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace Saresp.Models
{
    public class ProfessorAplicador
    {
        [Display(Name = "Id")]
        public int Id{ get; set; }


        [Required(ErrorMessage = "O campo nome é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo CPF é obrigatório")]
        public decimal CPF { get; set; }

        [Required(ErrorMessage = "o campo RG é obrigatório")]
        public string RG { get; set; }

        [Required(ErrorMessage = "O telefone é obrigatório")]
        public decimal telefone { get; set; }

        [Display(Name = "Data de nascimento")]
        [Required(ErrorMessage = "A data de nascimento é obrigatória")]
        [DataType(DataType.DateTime)]
        public DateTime DataNascimento { get; set; }

        
    }
}
