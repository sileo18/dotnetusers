using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnetusers.Domain
{
    [Table("track_keys")]
    public class TrackKeys
    {
        public TrackKeys()
        {
            TracksInThisKey = new HashSet<Track>();
        }

        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("nome")]
        [StringLength(10)]
        public string Nome{ get; set; } = null!;

        [InverseProperty(nameof(Track.Key))]

        public virtual ICollection<Track> TracksInThisKey { get; set; }
    }
}
