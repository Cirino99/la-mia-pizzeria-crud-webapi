﻿using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace la_mia_pizzeria_static.Data
{
    public class FormPizza
    {
        public Pizza Pizza { get; set; }
        public List<Category>? Categories { get; set; }
        // versione select multiple ingredients
        //public List<SelectListItem>? Ingredients { get; set; }
        //public List<int>? SelectedIngredients { get; set; }
        public List<Ingredient>? Ingredients { get; set; }
        public List<int>? AreChecked { get; set; }
    }
}
