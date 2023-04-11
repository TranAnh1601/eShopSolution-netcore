using eShopSolution.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Data.Entities
{
    public class Category
    {
        public int Id { set; get; }
        public int SortOrder { set; get; }
        public bool IsShowOnHome { set; get; }
        public int? ParentId { set; get; } //? cho phep null
        public Status Status { set; get; }

         public List<ProductInCategory> ProductInCategories { get; set; } // list lien ket  khoa ngoai

         public List<CategoryTranslation> CategoryTranslations { get; set; }
    }
}
