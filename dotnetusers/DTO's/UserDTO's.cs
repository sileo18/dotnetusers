using dotnetusers.Domain;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace dotnetusers.DTO_s
{
    public class CreateUserDTO
    {
        [Required(ErrorMessage = "O nome é obrigatório!")]
        public string Nome { get; set; } = null!;

        [EmailAddress(ErrorMessage = "O email deve ser válido!")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "A senha é obrigatória!")]
        public string Senha { get; set; } = null!;
        public bool Active { get; set; }

        [Required(ErrorMessage = "A role é obrigatória!")]
        public string Role { get; set; } = null!;
    }

    public class ReturnUserDTO
    {
        public string Nome { get; set; } = null!;
        public string Email { get; set; } = null!;
        public List<string> Role { get; set; } = null!;
        public int Id { get; set; }
    }

    public class LoginDTO
    {
        [Required(ErrorMessage = "O email é obrigatório!")]
        [EmailAddress(ErrorMessage = "O email deve ser válido!")]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "A senha é obrigatória!")]
        public string Senha { get; set; } = null!;
    }
}