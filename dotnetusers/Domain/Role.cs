using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace dotnetusers.Domain;

[Table("roles")]
[Index("Nome", Name = "roles_nome_key", IsUnique = true)]
public partial class Role
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("nome")]
    [StringLength(50)]
    public string Nome { get; set; } = null!;

    [ForeignKey("Roleid")]
    [InverseProperty("Roles")]
    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
