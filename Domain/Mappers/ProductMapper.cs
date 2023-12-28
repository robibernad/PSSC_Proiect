using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Mappers
{
    public static class ProductMapper
    {
        public static Product MapToProduct(ProductDTO productDTO)
        {
            if (productDTO == null)
                return null;

            return new Product(
                productDTO.ProductId,
                productDTO.Name,
                productDTO.Description,
                productDTO.Quantity,
                productDTO.Price
            );
        }

        public static ProductDTO MapToProductDTO(Product product)
        {
            if (product == null)
                return null;

            return new ProductDTO
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                Quantity = product.Quantity,
                Price = product.Price
            };
        }
    }
}

