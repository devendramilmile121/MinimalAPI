using System.ComponentModel.DataAnnotations;

namespace Minimal.User.Models.Entity;
public class User
{
    [Key]
    public Guid Id { get; set; }
    [MaxLength(250)]
    public string FirstName { get; set; }
    [MaxLength(250)]
    public string LastName { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    public string Password { get; set; }
    [MaxLength(1000)]
    public string Address { get; set; }
}

