//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NgCooking.DAL.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Communaute
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Communaute()
        {
            this.Recette = new HashSet<Recette>();
        }
        public int afficher { get; set; }
        public int Taille { get; set; }
    
        public int idcommunaute { get; set; }
        public string firstname { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public Nullable<int> niveau { get; set; }
        public string picture { get; set; }
        public string city { get; set; }
        public Nullable<int> birth { get; set; }
        public string bio { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Recette> Recette { get; set; }
    }
}
