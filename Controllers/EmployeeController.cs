using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestAPI.Data;
using TestAPI.Models;
namespace TestAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public EmployeeController(ApplicationDbContext db)
        {
            _db = db;
        }
        
        [HttpGet]
        //[Route("[GetAllEmployees]")]
        public IActionResult GetAllEmployees()
        {
            var data = _db.Employees.ToList();
            if(data.Count() == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(data);
            }
        }
        [HttpGet("{id}")]
        // [Route("[GetEmployeeById]")]
        public IActionResult GetEmployeeById(int id)
        {
            
            if(id == 0)
            {
                return NotFound();
            }
            else
            {
                var data = _db.Employees.Where(e => e.Id == id).SingleOrDefault();
                if(data == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(data);
                }
            }
        }
        [HttpPost]
        // [Route("[AddEmployee]")]
        public IActionResult AddEmployee([FromBody] Employee model)
        {
            
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
            //    _db.Employees.Add(model);
               var data = new Employee
               {
                FirstName=model.FirstName,
                LastName=model.LastName,
                DOJ=model.DOJ,
                Designation=model.Designation,
                Email=model.Email

               };
               _db.Employees.Add(data);
               _db.SaveChanges();
               return Ok(data);
            }
        }

        [HttpPut]
        //[Route("[UpdateEmployee]")]
        public IActionResult UpdateEmployee([FromBody] Employee model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
            
             var data = _db.Employees.Where(e => e.Id == model.Id).SingleOrDefault();
                if(data == null)
                {
                return BadRequest();
                }
                else
                {
                    data.FirstName=model.FirstName;
                    data.LastName=model.LastName;
                    data.DOJ=model.DOJ;
                    data.Designation=model.Designation;
                    data.Email=model.Email;
                    _db.Employees.Update(data);
                    _db.SaveChanges();
                     return Ok();
                }
               
            } 
        }
       
       [HttpDelete("{id}")]
       
       public IActionResult DeleteEmployee(int id)
       {
            if(id != 0)
            {
                var data = _db.Employees.Where(e => e.Id == id).SingleOrDefault();
                if(data == null)
                {
                    return BadRequest();
                }
                else
                {
                    _db.Employees.Remove(data);
                    _db.SaveChanges();
                }
            }
            else
            {
                return BadRequest();
            }
            return Ok();
       }
        
    }
}