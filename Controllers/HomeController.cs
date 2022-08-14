using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;

using IlgiluftaExam.Models;

namespace IlgiluftaExam.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private MyContext _context;

    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        int? IntVariable = HttpContext.Session.GetInt32("UserId");

        ViewBag.id = IntVariable;
        if(IntVariable==null){
            return RedirectToAction("Register");
        }
        ViewBag.AllHobbies =_context.Hobbys.Include(e=> e.MyEnthusiasts).ToList();
        ViewBag.userlog = _context.Users.FirstOrDefault(u=>u.UserId == IntVariable);
      


        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Register()
    {
          int? IntVariable = HttpContext.Session.GetInt32("UserId");


        if(IntVariable!=null){
            return RedirectToAction("Index");
        }
            return View();
        
        
    }

    


  
    
     [HttpPost("Register")]
    public IActionResult Register(User user)
    {
        if (ModelState.IsValid)
        {
            // If a User exists with provided email
            if (_context.Users.Any(u => u.Username == user.Username))
            {
                // Manually add a ModelState error to the Email field, with provided
                // error message
                ModelState.AddModelError("Username", "Email already in use!");

                // You may consider returning to the View at this point
                return View("Register");
            }
            // Initializing a PasswordHasher object, providing our User class as its type
            PasswordHasher<User> Hasher = new PasswordHasher<User>();
            user.Password = Hasher.HashPassword(user, user.Password);
            //Save your user object to the database

            _context.Users.Add(user);
            _context.SaveChanges();
            User Userdb = _context.Users.FirstOrDefault(u => u.Username == user.Username);

            HttpContext.Session.SetInt32("UserId", Userdb.UserId);
            int IntVariable = (int)HttpContext.Session.GetInt32("UserId");

            return RedirectToAction("Index");

        }
        else
        {
            return View("Register");
        }

    }

    [HttpPost]
    public IActionResult LogIn(LoginUser user)
    {
        if (ModelState.IsValid)
        {
            // If initial ModelState is valid, query for a user with provided email
            var userInDb = _context.Users.FirstOrDefault(u => u.Username == user.Username);
            // If no user exists with provided email
            if (userInDb == null)
            {
                // Add an error to ModelState and return to View!
                ModelState.AddModelError("Username", "Invalid Email/Password");
                return View("Register");
            }

            // Initialize hasher object
            var hasher = new PasswordHasher<LoginUser>();

            // verify provided password against hash stored in db
            var result = hasher.VerifyHashedPassword(user, userInDb.Password, user.Password);


            // result can be compared to 0 for failure
            if (result == 0)
            {
                ModelState.AddModelError("Password", "Invalid Password");
                // handle failure (this should be similar to how "existing email" is handled)
                return View("Register");
            }

            HttpContext.Session.SetInt32("UserId", userInDb.UserId);

            

            return RedirectToAction("Index");
        }
        return View("Register");
    }

    // public IActionResult LogOut()
    // {
    //     HttpContext.Session.Clear();

    //     return RedirectToAction("Register");
    // }

     public IActionResult AddHobby()
    {
        
        return View();
    }

     [HttpPost]
    public IActionResult CreateHobby(Hobby marrngaadd)
    {
        if (ModelState.IsValid)
        {
            if (_context.Hobbys.Any(u => u.Name == marrngaadd.Name))
            {
                // Manually add a ModelState error to the Email field, with provided
                // error message
                ModelState.AddModelError("Name", "Name already in use!");

                // You may consider returning to the View at this point
                return View("AddHobby");
            }


                int IntVariable = (int)HttpContext.Session.GetInt32("UserId");
                marrngaadd.UserId = IntVariable;
                _context.Add(marrngaadd);
                _context.SaveChanges(); 
                return RedirectToAction("Index");
            }


        
        return View("AddHobby");
    }

     [HttpGet("SHowHobby/{id}")]
    public IActionResult SHowHobby(int id)
    {
        ViewBag.hobbyId = id;
        ViewBag.myHobby = _context.Hobbys.Include(e=>e.MyEnthusiasts).ThenInclude(e=>e.Person).FirstOrDefault(e => e.HobbyId == id);
        // ViewBag.guests=_context.Invites.Include(e=>e.Person).Where(e=> e.WeddingId ==id).ToList();
        return View();
    }

    
[HttpPost]
     public IActionResult BecomeEnthusiast(int id,string proeficiency)
    {

         int Variable1 = (int)HttpContext.Session.GetInt32("UserId");
        List<Enthusiast> allEnthusiast = _context.Enthusiasts.ToList();
        Enthusiast dbEnthusiast = _context.Enthusiasts.FirstOrDefault(p => p.HobbyId == id && p.UserId == Variable1);
         if (allEnthusiast.Contains(dbEnthusiast))
        {
            return RedirectToAction("SHowHobby",new {id = id});

        }
        else
        {
            Enthusiast MYEnthusiast = new Enthusiast()
            {
                HobbyId = id,
                UserId = Variable1,
                Proficiency = proeficiency
            };
            _context.Add(MYEnthusiast);
            _context.SaveChanges();
            
           return RedirectToAction("SHowHobby",new {id = id});
        }
        
    }

     public IActionResult EditHobby(int id)
    {
        
        ViewBag.hobbyId=id;
        Hobby Editing = _context.Hobbys.FirstOrDefault(p=>p.HobbyId == id);
        
        return View(Editing);
    }

  [HttpPost]
    public IActionResult EditedHobby(Hobby marrngaadd,int id)
    {
        if (ModelState.IsValid)
        {
            if (_context.Hobbys.Any(u => u.Name == marrngaadd.Name))
            {
                // Manually add a ModelState error to the Email field, with provided
                // error message
                Hobby Editing1 = _context.Hobbys.FirstOrDefault(p => p.HobbyId==id);
                ModelState.AddModelError("Name", "Name already in use!");

                // You may consider returning to the View at this point
                return RedirectToAction("EditHobby",new { id = id });
            }else{

                Hobby editing = _context.Hobbys.FirstOrDefault(p => p.HobbyId == id); // marrengaadd.DishId nuk mund te zevendesohet me id sepse nxjerr problem

           editing.Name = marrngaadd.Name;
          
           editing .Discription = marrngaadd.Discription;
           
           editing.UpdatedAt = DateTime.Now;
            _context.SaveChanges();
            return RedirectToAction("SHowHobby",new { id = id });
           

            }


        }
        else
        {
             Hobby editing = _context.Hobbys.FirstOrDefault(p => p.HobbyId == id);
            // Oh no!  We need to return a ViewResponse to preserve the ModelState, and the errors it now contains!
            return RedirectToAction("EditHobby",new { id = id });
            //   return RedirectToAction("Edit",new { id = id}); // return view nuk punon kur ben submit 2 here nje fushe te gabuar

        }


        
       
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
