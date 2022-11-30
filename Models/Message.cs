using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace la_mia_pizzeria_static.Models
{
    public class Message
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "L'email è obbligatoria")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Il nome è obbligatorio")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Il titolo del messaggio è obbligatorio")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Il testo del messaggio è obbligatorio")]
        [Column(TypeName = "text")]
        public string Text { get; set; }
    }
}
