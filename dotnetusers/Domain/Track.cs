using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace dotnetusers.Domain
{
    [Table("tracks")]
    public class Track
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("nome")]
        public string Nome { get; set; } = null!;

        [Column("bpm")]
        public int Bpm { get; set; }

        [Column("image_url")]
        public string ImageUrl { get; set; } = null!;

        [Column("audio_url")]
        public string AudioUrl { get; set; } = null!;

        [Column("created_at", TypeName = "timestamp without time zone")]
        public DateTime? CreatedAt { get; set; }

        [Required]
        [Column("usuario_id")]
        public int UsuarioId { get; set; }

        [ForeignKey("UsuarioId")]
        [InverseProperty("UserTracks")]
        [JsonIgnore]
        public virtual Usuario Usuario { get; set; } = null!;        

        [Column("genre_id")]
        public int? GenreId { get; set; }

        [ForeignKey("GenreId")]
        [InverseProperty("Tracks")]
        [JsonIgnore]
        public virtual Genre Genre { get; set; } = null!;

        [Column("track_key_id")]
        public int? KeyId { get; set; }

        [ForeignKey("KeyId")]
        [InverseProperty("TracksInThisKey")]
        [JsonIgnore]
        public virtual TrackKeys Key { get; set; } = null!;




    }
}
