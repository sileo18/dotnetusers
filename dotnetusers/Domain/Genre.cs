using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [InverseProperty(nameof(Track.Genre))]
        public virtual ICollection<Track> Tracks { get; set; }
    }
}
