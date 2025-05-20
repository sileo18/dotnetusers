// DTOs/CreateTrackDTO.cs
using Microsoft.AspNetCore.Http; // Para IFormFile
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace dotnetusers.DTO_s
{
    public class CreateTrackDTO
    {
        [Required(ErrorMessage = "O nome da track é obrigatório.")]
        [MinLength(2, ErrorMessage = "O nome da track deve ter pelo menos 2 caracteres.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "BPM é obrigatório.")]
        [Range(1, 999, ErrorMessage = "BPM deve ser um valor válido.")]
        public int Bpm { get; set; }

        [Required(ErrorMessage = "O tom (KeyId) é obrigatório.")]
        public int KeyId { get; set; }

        [Required(ErrorMessage = "Pelo menos um gênero é obrigatório.")]
        [MinLength(1, ErrorMessage = "Selecione pelo menos um gênero.")] // Garante que a lista não seja vazia
        public List<int> GenreId { get; set; } = new List<int>();

        [Required(ErrorMessage = "UserId é obrigatório.")]
        public int UserId { get; set; }

        // Para [FromForm], Image pode ser opcional se você tiver um placeholder ou lógica
        // Se for obrigatório, adicione [Required]
        [Required(ErrorMessage = "A imagem da track é obrigatória.")]
        public IFormFile Image { get; set; } = null!; // null! se for obrigatório e verificado no controller

        [Required(ErrorMessage = "O arquivo de áudio é obrigatório.")]
        public IFormFile Audio { get; set; } = null!;
    }
}