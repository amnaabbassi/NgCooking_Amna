using NgCooking.DAL.Entities;
using System;
using System.Collections.Generic;

namespace ngCooking.Models
{
    public class IngredientCategorie
    {
        public List<Ingredient> Ingredient { get; set; }

        public int IdIngredient { get; set; }
        public string Name { get; set; }
        public Nullable<bool> IsAvailable { get; set; }
        public string picture { get; set; }
        public Nullable<int> calories { get; set; }
      
        public List<Categorie> Categorie { get; set; }
        public int IdCategorie { get; set; }
        public string nameToDisplay { get; set; }

    }
}