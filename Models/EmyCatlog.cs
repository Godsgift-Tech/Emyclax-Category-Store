using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Emyclax_Category_Store.Models
{
    public class EmyCatlog
    {
        [Key]
        public Guid Id { get; set; } // serves as primary key

        [Required] //This is the required data annotation for the properties in category below.

        [DisplayName("Category Name")]  //data annotation to display Name

        [MaxLength(50)]

        [RegularExpression(@"^[^\d]*$", ErrorMessage = "Category name cannot contain numbers.")]
        public string Name { get; set; }

        [DisplayName("Quantity Available")] //data annotation to display DisplayOrder

        [Range(1, 1000, ErrorMessage = "Display Order must be between 1-1000")]// giving a range and custom error mesage
        public int Quantity { get; set; }

        

    }
}
