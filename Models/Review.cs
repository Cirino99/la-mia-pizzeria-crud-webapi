using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace la_mia_pizzeria_static.Models
{
    public class Review
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Il nome è obbligatorio")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Il testo del messaggio è obbligatorio")]
        [Column(TypeName = "text")]
        public string Text { get; set; }
        public int PizzaId { get; set; }
        public Pizza? Pizza { get; set; }
    }
}
