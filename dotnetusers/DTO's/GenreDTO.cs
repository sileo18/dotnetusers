using System.ComponentModel.DataAnnotations;

namespace dotnetusers.DTO_s
{
    public class GenreDTO
    {
    }

    public class CreateGenreDTO
    {
        [Required]
        [StringLength(10), MinLength(2)]
        public string Nome { get; set; } = string.Empty;
    }
}
