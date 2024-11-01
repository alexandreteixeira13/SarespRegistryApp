using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace Saresp.Models
{
    public class Aluno
    {
        [Required(ErrorMessage = "O campo ID é obrigatório")]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo nome é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo email é obrigatório")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "O Email não é valido")]
        public string Email { get; set; }

        [Required(ErrorMessage ="O telefone é obrigatório")]
        public decimal Telefone { get; set; }

        [Required(ErrorMessage = "O telefone é obrigatório")]
        public int Serie { get; set; }

        [Required(ErrorMessage = "A dada de nascimento é obrigatória")]
        public string Turma { get; set; }

        [Display(Name = "Data de nascimento")]
        [Required(ErrorMessage = "A data de nascimento é obrigatória")]
        [DataType(DataType.DateTime)]
        public DateTime DataNascimento { get; set; }
    }
}
