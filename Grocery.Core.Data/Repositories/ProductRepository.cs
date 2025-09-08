using Grocery.Core.Interfaces.Repositories;
using Grocery.Core.Models;

namespace Grocery.Core.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly List<Product> products;
        public ProductRepository()
        {
            products = [
                new Product(1, "Melk", 300),
                new Product(2, "Kaas", 100),
                new Product(3, "Brood", 400),
                new Product(4, "Cornflakes", 0)];
        }
        public List<Product> GetAll() => products;

        public Product? Get(int id) => products.FirstOrDefault(p => p.Id == id);

        public Product Add(Product item)
        {
            if (item == null)
                throw new ArgumentException("Ongeldig product", nameof(item));
            if (item.Name.ToLower() == null || item.Name.ToLower() == "")
                throw new ArgumentException("Ongeldig product", nameof(item));
            if (products.Any(p => p.Name.ToLower() == item.Name.ToLower()))
                throw new ArgumentException("Product bestaat al", nameof(item));

            item.Id = products.Max(p => p.Id) + 1;
            products.Add(item);
            return item;
        }

        public Product Delete(Product item)
        {
            if (item == null)
                throw new ArgumentException("Ongeldig product", nameof(item));
            if (products.All(p => p.Id != item.Id))
                throw new ArgumentException("Product bestaat niet", nameof(item));

            Product removeProduct = products.FirstOrDefault(p => p.Id == item.Id) ?? throw new InvalidOperationException();
            products.Remove(removeProduct);
            return item;
        }

        public Product? Update(Product item)
        {
            Product? product = products.FirstOrDefault(p => p.Id == item.Id);
            if (product == null) return null;
            product.Id = item.Id;
            return product;
        }
    }
}
