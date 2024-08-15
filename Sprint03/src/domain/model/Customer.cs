using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sprint03.domain.model;

[Table("TB_CUSTOMER_03")]
public class Customer
{
    [Key]
    public string Id { get; init; }
    [Required, MaxLength(100)]
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    [Required]
    public DateTime BirthDate { get; set; }
    [Required, MaxLength(11)]
    public string Document { get; set; }
    [Required, MaxLength(10)]
    public string Cep { get; set; }
    public int AgreementId { get; set; }
    
    public Customer()
    {
        Id = Guid.NewGuid().ToString();
    }
}