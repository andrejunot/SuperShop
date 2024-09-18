using Microsoft.AspNetCore.Http;
using SuperShop.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace SuperShop.Models
{
    public class ProductViewModel : Product
    {
        [Display(Name ="Photo")]
        public IFormFile ImageFile { get; set; }
    }
}
