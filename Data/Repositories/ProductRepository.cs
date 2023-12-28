using System;
using Data;
using Data.Models;

namespace Data.Repositories
{
    public class ProductRepository
    {
        private readonly ProiectPsscDb dbContext;

        public ProductRepository(ProiectPsscDb dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ProductDTO> TryGetProduct(string searchedProduct, int quantity)
        {
            var product = await dbContext.Products.FindAsync(searchedProduct);
            if (product != null && product.Quantity >= quantity)
            {
                return product;
            }

            return null;
        }

        public async Task TryRemoveStoc(string productId, int quantity)
        {
            var product = await dbContext.Products.FindAsync(productId);
            if (product != null)
            {
                product.Quantity = product.Quantity - quantity;
                dbContext.SaveChanges();
            }
        }
    }
}

