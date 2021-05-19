using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServer.Model
{
    public class PostCategory
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Abstract { get; set; }
        public string Content { get; set; }

        [Display(Name = "Created Date")]
        [DataType(DataType.Date)]
        public DateTime CreateDate { get; set; }

        [Display(Name = "Update Date")]
        public DateTime UpdateDate { get; set; }

        [Display(Name = "Product Ready?")]
        public bool IsproductionReady { get; set; }
        public string Slug { get; set; }

        [Display(Name = "Upload Image")]
        public Byte[] ImageData { get; set; }
        public string ContentType { get; set; }

        [Display(Name = "View")]
        public int ViewCount { get; set; }

        //[Display(Name = "Comment")]
        //public int CountComment { get; set; }

        [Display(Name = "Category")]
        public int BlogCategoryId { get; set; }
        public virtual BlogCategory BlogCategory { get; set; }

        public virtual ICollection<PostComment> PostComments { get; set; } = new HashSet<PostComment>();
        public virtual ICollection<Tag> Tags { get; set; } = new HashSet<Tag>();
    }
}
