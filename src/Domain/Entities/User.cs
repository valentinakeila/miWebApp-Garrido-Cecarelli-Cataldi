using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Entities;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column(TypeName = "nvarchar(50)")]
    public string FirstName { get; set; }

    [Column(TypeName = "nvarchar(50)")]
    public string LastName { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string Email { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string Password { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(20)")]
    public UserRole Role { get; set; }

    public User()
    {
    }

    public User(string firstName, string lastName, string email, string password, UserRole role)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
        Role = role;
    }
}
