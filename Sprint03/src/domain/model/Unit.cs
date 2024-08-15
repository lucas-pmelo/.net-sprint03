using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sprint03.domain.model;

[Table("TB_UNIT_03")]
public class Unit
{
    [Key]
    public string Id { get; set; }
    [Required, MaxLength(100)]
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    [Required, MaxLength(50)]
    public string Type { get; set; }
    [Required, MaxLength(10)]
    public string Cep { get; set; }
    
    public Unit()
    {
        Id = Guid.NewGuid().ToString();
    }
}