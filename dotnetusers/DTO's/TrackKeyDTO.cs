using System.ComponentModel.DataAnnotations;

namespace dotnetusers.DTO_s
{
    public class TrackKeyDTO
    {
    }

    public class CreateTrackKeyDTO
    {
        [Required]
        [StringLength(10), MinLength(2)]
        public string Nome { get; set; } = string.Empty;       
        
    }
}
