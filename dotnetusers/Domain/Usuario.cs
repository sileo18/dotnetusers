using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace dotnetusers.Domain;

[Table("usuarios")]
[Index("Email", Name = "usuarios_email_key", IsUnique = true)]
public partial class Usuario
{
    public Usuario()
    {
        Roles = new HashSet<Role>();
        UserTracks = new HashSet<Track>();
    }

    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("nome")]
    [StringLength(100)]
    public string Nome { get; set; } = null!;

    [Column("email")]
    [StringLength(100)]
    public string Email { get; set; } = null!;

    [Column("passwordhashed")]
    [StringLength(255)]
    public string Passwordhashed { get; set; } = null!;

    [Column("active")]
    public bool Active { get; set; }

    [Column("createdat")]
    public DateTime Createdat { get; set; }

    [ForeignKey("Usuarioid")]
    [InverseProperty("Usuarios")]
    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();

    [InverseProperty(nameof(Track.Usuario))]
    public virtual ICollection<Track> UserTracks { get; set; } = new List<Track>();

}
