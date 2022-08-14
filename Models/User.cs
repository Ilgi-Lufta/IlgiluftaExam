#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IlgiluftaExam.Models;
public class User
{
    
    [Key]
    public int UserId { get; set; } 
    [Required]
    [MinLength(2)]
    [Display(Name = "First Name")]
    public string FirstName { get; set; }
    [Required]
    [MinLength(2)]
    [Display(Name = "Last Name")]
    public string LastName { get; set; } 
    
    [Required]
    [MinLength(3)]
    [MaxLength(15)]

    public string Username { get; set; }

    [DataType(DataType.Password)]
    [Required]
    [MinLength(8, ErrorMessage="Password must be 8 characters or longer!")]
    public string Password { get; set; } 
  
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

      public List<Hobby> MyHobbies { get; set; } = new List<Hobby>(); 

       public List<Enthusiast> Enthusiastof  { get; set; } = new List<Enthusiast>();

   
    [NotMapped]
    [Compare("Password")]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm PW")]
    public string Confirm { get; set; } 
}