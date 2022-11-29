﻿using la_mia_pizzeria_static.Data;
using la_mia_pizzeria_static.Data.Repository;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;

namespace la_mia_pizzeria_static.Controllers
{
    [Route("[controller]/[action]/{id?}",Order = 0)] //serve per dare un ordine in caso di controller con nome ed action uguale
    public class PizzaController : Controller
    {
        IPizzaRepository pizzaRepository;
        ICategoryRepository categoryRepository;
        IIngredientRepository ingredientRepository;
        public PizzaController(IPizzaRepository _pizzaRepository, ICategoryRepository _categoryRepository, IIngredientRepository _ingredientRepository) : base()
        {
            pizzaRepository = _pizzaRepository;
            categoryRepository = _categoryRepository;
            ingredientRepository = _ingredientRepository;
        }
        public IActionResult Index()
        {
            List<Pizza> pizze = pizzaRepository.All();
            return View(pizze);
        }

        public IActionResult Detail(int id)
        {
            Pizza pizza = pizzaRepository.GetById(id);
            if (pizza == null)
                return View("NotFound","La pizza cercata non è stata trovata");
            return View(pizza);
        }

        // versione select multiple ingredients seza uso del repository
        //public IActionResult Create()
        //{
        //    FormPizza formPizza = new FormPizza();
        //    formPizza.Pizza = new Pizza();
        //    formPizza.Categories = db.Categories.ToList();
        //    formPizza.Ingredients = new List<SelectListItem>();
        //    List<Ingredient> ingredients = db.Ingredients.ToList();
        //    foreach(Ingredient ingredient in ingredients)
        //    {
        //        SelectListItem item = new SelectListItem(ingredient.Name,ingredient.Id.ToString());
        //        formPizza.Ingredients.Add(item);
        //    }
        //    return View(formPizza);
        //}
        public IActionResult Create()
        {
            FormPizza formPizza = new FormPizza();
            formPizza.Pizza = new Pizza();
            formPizza.Categories = categoryRepository.All();
            formPizza.AreChecked = new List<int>();
            formPizza.Ingredients = ingredientRepository.All();
            return View(formPizza);
        }

        // versione select multiple ingredients seza uso del repository
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create(FormPizza formPizza)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        if (ModelState["Pizza.Price"].Errors.Count > 0)
        //        {
        //            ModelState["Pizza.Price"].Errors.Clear();
        //            ModelState["Pizza.Price"].Errors.Add("Il prezzo deve essere compreso tra 1 e 30");
        //        }
        //        formPizza.Categories = db.Categories.ToList();
        //        formPizza.Ingredients = new List<SelectListItem>();
        //        List<Ingredient> ingredients = db.Ingredients.ToList();
        //        foreach (Ingredient ingredient in ingredients)
        //        {
        //            SelectListItem item = new SelectListItem(ingredient.Name, ingredient.Id.ToString());
        //            formPizza.Ingredients.Add(item);
        //        }
        //        return View(formPizza);
        //    }

        //    formPizza.Pizza.Ingredients = new List<Ingredient>();
        //    foreach (int ingredientId in formPizza.SelectedIngredients)
        //    {
        //        Ingredient ingredient = db.Ingredients.Where(i => i.Id == ingredientId).FirstOrDefault();
        //        formPizza.Pizza.Ingredients.Add(ingredient);
        //    }

        //    db.Pizze.Add(formPizza.Pizza);
        //    db.SaveChanges();

        //    return RedirectToAction("Index");
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(FormPizza formPizza)
        {
            if (!ModelState.IsValid)
            {
                if (ModelState["Pizza.Price"].Errors.Count > 0)
                {
                    ModelState["Pizza.Price"].Errors.Clear();
                    ModelState["Pizza.Price"].Errors.Add("Il prezzo deve essere compreso tra 1 e 30");
                }
                formPizza.Categories = categoryRepository.All();
                formPizza.Ingredients = ingredientRepository.All();
                return View(formPizza);
            }

            List<Ingredient> ingredients = ingredientRepository.GetList(formPizza.AreChecked);
            Category category = categoryRepository.GetById(formPizza.Pizza.CategoryId);
            pizzaRepository.Create(formPizza.Pizza, ingredients, category);

            return RedirectToAction("Index");
        }

        // versione select multiple ingredients seza uso del repository
        //public IActionResult Update(int id)
        //{
        //    FormPizza formPizza = new FormPizza();
        //    formPizza.Pizza = db.Pizze.Where(p => p.Id == id).Include("Ingredients").FirstOrDefault();
        //    if (formPizza.Pizza == null)
        //        return View("NotFound", "La pizza cercata non è stata trovata");
        //    formPizza.Categories = db.Categories.ToList();
        //    formPizza.Ingredients = new List<SelectListItem>();
        //    List<Ingredient> ingredients = db.Ingredients.ToList();
        //    foreach (Ingredient ingredient in ingredients)
        //    {
        //        SelectListItem item = new SelectListItem(ingredient.Name, ingredient.Id.ToString(), formPizza.Pizza.Ingredients.Any( i => i.Id == ingredient.Id));
        //        formPizza.Ingredients.Add(item);
        //    }
        //    return View(formPizza);
        //}
        public IActionResult Update(int id)
        {
            FormPizza formPizza = new FormPizza();
            formPizza.Pizza = pizzaRepository.GetById(id);
            if (formPizza.Pizza == null)
                return View("NotFound", "La pizza cercata non è stata trovata");
            formPizza.Categories = categoryRepository.All();
            formPizza.AreChecked = new List<int>();
            formPizza.Ingredients = ingredientRepository.All();
            foreach (Ingredient ingredient in formPizza.Ingredients)
            {
                if(formPizza.Pizza.Ingredients.Any(i => i.Id == ingredient.Id))
                    formPizza.AreChecked.Add(ingredient.Id);
            }
            return View(formPizza);
        }

        // versione select multiple ingredients seza uso del repository
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Update(int id, FormPizza formPizza)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        if (ModelState["Pizza.Price"].Errors.Count > 0)
        //        {
        //            ModelState["Pizza.Price"].Errors.Clear();
        //            ModelState["Pizza.Price"].Errors.Add("Il prezzo deve essere compreso tra 1 e 30");
        //        }
        //        formPizza.Pizza.Id = id;
        //        formPizza.Categories = db.Categories.ToList();
        //        formPizza.Ingredients = new List<SelectListItem>();
        //        List<Ingredient> ingredients = db.Ingredients.ToList();
        //        foreach (Ingredient ingredient in ingredients)
        //        {
        //            SelectListItem item = new SelectListItem(ingredient.Name, ingredient.Id.ToString());
        //            formPizza.Ingredients.Add(item);
        //        }
        //        return View(formPizza);
        //    }

        //    Pizza pizza = db.Pizze.Where( p => p.Id == id).Include("Ingredients").FirstOrDefault();
        //    pizza.Name = formPizza.Pizza.Name;
        //    pizza.Description = formPizza.Pizza.Description;
        //    pizza.Price = formPizza.Pizza.Price;
        //    pizza.CategoryId = formPizza.Pizza.CategoryId;
        //    pizza.Ingredients.Clear();
        //    if (formPizza.SelectedIngredients == null)
        //        formPizza.SelectedIngredients = new List<int>();
        //    foreach (int ingredientId in formPizza.SelectedIngredients)
        //    {
        //        Ingredient ingredient = db.Ingredients.Where(i => i.Id == ingredientId).FirstOrDefault();
        //        pizza.Ingredients.Add(ingredient);
        //    }

        //    db.SaveChanges();

        //    return RedirectToAction("Detail", new { id = pizza.Id });
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, FormPizza formPizza)
        {
            if (!ModelState.IsValid)
            {
                if (ModelState["Pizza.Price"].Errors.Count > 0)
                {
                    ModelState["Pizza.Price"].Errors.Clear();
                    ModelState["Pizza.Price"].Errors.Add("Il prezzo deve essere compreso tra 1 e 30");
                }
                formPizza.Pizza.Id = id;
                formPizza.Categories = categoryRepository.All();
                formPizza.Ingredients = ingredientRepository.All();
                return View(formPizza);
            }

            Pizza pizza = pizzaRepository.GetById(id);
            List<Ingredient> ingredients = ingredientRepository.GetList(formPizza.AreChecked);
            Category category = categoryRepository.GetById(formPizza.Pizza.CategoryId);
            pizzaRepository.Update(pizza, formPizza.Pizza, ingredients, category);

            return RedirectToAction("Detail", new { id = pizza.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            Pizza pizza = pizzaRepository.GetById(id);

            if (pizza == null)
                return View("NotFound", "La pizza cercata non è stata trovata");

            pizzaRepository.Delete(pizza);

            return RedirectToAction("Index");
        }
    }
}
