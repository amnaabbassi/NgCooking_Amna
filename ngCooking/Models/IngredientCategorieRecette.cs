using NgCooking.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ngCooking.Models
{
    public class IngredientCategorieRecette
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

        public List<Recette> Recette { get; set; }
        public int Id_Recette { get; set; }
        public string name { get; set; }
        public Nullable<int> CreateId { get; set; }
        public Nullable<int> Calories { get; set; }
        public string preparation { get; set; }
       
        public List<RecetteIngredient> RecetteIngredient { get; set; }
        [ForeignKey("idRecette")]
        public Nullable<int> idRecette { get; set; }
        [ForeignKey("IdIngredient")]
        public Nullable<int> Id_Ingredient { get; set; }
        public int IdRecetteIngredient { get; set; }

        public List<Comment> Comment { get; set; }
        public int IdComment { get; set; }
        public string title { get; set; }
        public string comment1 { get; set; }
        public Nullable<int> mark { get; set; }

        public List<Communaute> Communaute { get; set; }
        public int idcommunaute { get; set; }
        public string firstname { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public Nullable<int> niveau { get; set; }
        public string city { get; set; }
        public Nullable<int> birth { get; set; }
        public string bio { get; set; }
    }
}