using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServer.Model
{
    public class BlogCategory
    {
        public int Id { get; set; }
        [Required]
        [StringLength(35, ErrorMessage = "The {0} must be at least {2} and at max {1} character long.", MinimumLength = 2)]
        public string Name { get; set; }
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} character long.", MinimumLength = 2)]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreateDate { get; set; }


        public DateTime UpdateDate { get; set; }

        public virtual ICollection<PostCategory> PostCategories { get; set; } = new HashSet<PostCategory>();
    }
}
