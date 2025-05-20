using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace dotnetusers.Domain
{
    [Table("genres")]
    public class Genre
    {
        public Genre()
        {
            Tracks = new HashSet<Track>();
        }

        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("nome")]
        [StringLength(50)]
        public string Nome { get; set; }

        [JsonIgnore]
        public virtual ICollection<Track> Tracks { get; set; }
    }
}
