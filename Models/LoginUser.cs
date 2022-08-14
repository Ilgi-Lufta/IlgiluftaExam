#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace IlgiluftaExam.Models;
public class LoginUser
{
    // No other fields!
    [Required]
     [MinLength(3)]
    [MaxLength(15)]

    public string Username { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    [MinLength(8, ErrorMessage="Password must be 8 characters or longer!")]
    public string Password { get; set; } 
}