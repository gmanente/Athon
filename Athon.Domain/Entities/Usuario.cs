using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Athon.Domain.Entities
{
    public class Usuario
    {
        [Key]
        [Required(ErrorMessage = "")]
        public Guid IdUsuario { get; set; }
        
        public string Nome { get; set; }        
        
        public DateTime DataCadastro { get; set; }
        
        public string NomeLogin { get; set; }
        
        public string Email { get; set; }
        
        public string Cpf { get; set; }
        
        public DateTime DataNascimento { get; set; }
        
        public string Telefone { get; set; }
        
        public string Celular { get; set; }
        
        public bool Ativo { get; set; }
    }
}
