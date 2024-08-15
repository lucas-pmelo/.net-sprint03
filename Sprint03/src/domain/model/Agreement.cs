using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sprint03.domain.model;

[Table("TB_AGREEMENT_03")]
public class Agreement
{
    [Key]
    public string Id { get; set; }
    [Required, MaxLength(100)]
    public string Name { get; set; }
    [Required]
    public double Value { get; set; }
    public string ServiceType { get; set; }
    public string Coverage { get; set; }
    
    public Agreement()
    {
        Id = Guid.NewGuid().ToString();
    }
}