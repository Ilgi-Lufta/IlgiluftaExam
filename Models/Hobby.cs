#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IlgiluftaExam.Models;
public class Hobby
{
    
    [Key]
    public int HobbyId { get; set; } 
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Discription{ get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;


     public List<Enthusiast> MyEnthusiasts  { get; set; } = new List<Enthusiast>();

    [Required]
    public int UserId { get; set; }

     public User? Creator { get; set; }

    
}