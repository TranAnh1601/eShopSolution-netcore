using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Data.Entities
{
    public class ProductImage
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        //url
        public string ImagePath { get; set; }

        public string Caption { get; set; }

        //tuy chon anh mac dinh
        public bool IsDefault { get; set; }

        public DateTime DateCreated { get; set; }

        //thu tu
        public int SortOrder { get; set; }

        public long FileSize { get; set; }

        // lien ket product
        public Product Product { get; set; }
    }
}
