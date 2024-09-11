using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIBatch1.Data;
using WebAPIBatch1.Models;

namespace WebAPIBatch1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext db;
        public ProductsController(ApplicationDbContext db)
        {
            this.db = db;
        }
        [HttpGet]
        public IActionResult GetAllProductsDetails()
        {
           var data= db.products.ToList();
            return Ok(data);
        }
        [HttpPost]
        public IActionResult AddNewProducts(Product p)
        {
            db.products.Add(p);
            db.SaveChanges();
            return Ok("Product Added Successfully");
        }
        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var data=db.products.Find(id);
            return Ok(data);
        }
        [HttpGet("FindByName/{name}")]
        public IActionResult GetProductByName(string  name)
        {
            var data = db.products.Where(z=>z.Pname.Contains(name)).ToList();
            return Ok(data);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var data = db.products.Find(id);
            if (data == null)
            {
                return BadRequest();
            }
            else
            {
                db.products.Remove(data);
                db.SaveChanges();
                return Ok("Delete Success");
            }
        }
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id,Product p)
        {
            if(id !=p.Id)
            {
                return BadRequest();
            }
            else
            {
                db.Entry(p).State=EntityState.Modified;     //it will Modify the details which is given by dot net core
                db.SaveChanges();
                return Ok("Product Updated Successfully");
            }
        }
    }
}
