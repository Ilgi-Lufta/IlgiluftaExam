#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IlgiluftaExam.Models;
public class Enthusiast
{
    [Key]
    public int EnthusiastID { get; set; }

    public string Proficiency { get; set; }
    public int UserId { get; set; }
    public int HobbyId { get; set; }

    public User? Person { get; set; }

    public Hobby? MyHooby { get; set; }

   
}