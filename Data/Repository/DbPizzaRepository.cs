using la_mia_pizzeria_static.Models;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_static.Data.Repository
{
    public class DbPizzaRepository : IPizzaRepository
    {
        PizzeriaDbContext db;
        public DbPizzaRepository(PizzeriaDbContext _db)
        {
            db = _db;
        }
        public List<Pizza> All()
        {
            return db.Pizze.ToList();
        } 
        public Pizza GetById(int id)
        {
            return db.Pizze.Where(p => p.Id == id).Include("Category").Include("Ingredients").FirstOrDefault();
        }
        public List<Pizza> GetPizze(string name, int categoryId)
        {
            if (name == null && categoryId == 0)
                return All();
            if (categoryId == 0)
                return db.Pizze.Where(p => p.Name.Contains(name)).ToList();
            if (name == null)
                return db.Pizze.Where(p => p.CategoryId == categoryId).ToList();
            return db.Pizze.Where(p => p.Name.Contains(name)).Where( p => p.CategoryId == categoryId).ToList();
        }
        public void Create(Pizza pizza, List<Ingredient> ingredients, Category category)
        {
            pizza.Category = category;
            pizza.Ingredients = ingredients;

            db.Pizze.Add(pizza);
            db.SaveChanges();
        }
        public void Update(Pizza pizza, Pizza formData, List<Ingredient> ingredients, Category category)
        {
            pizza.Name = formData.Name;
            pizza.Description = formData.Description;
            pizza.Price = formData.Price;
            pizza.CategoryId = formData.CategoryId;
            pizza.Category = category;
            pizza.Ingredients = ingredients;

            db.SaveChanges();
        }
        public void Delete(Pizza pizza)
        {
            db.Pizze.Remove(pizza);
            db.SaveChanges();
        }
    }
}
