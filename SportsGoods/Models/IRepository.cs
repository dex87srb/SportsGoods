using System.Collections.Generic;

namespace SportsGoods.Models
{

    public interface IRepository
    {

        IEnumerable<Product> Products { get; }
        Product GetProduct(long key);
        void AddProduct(Product product);
        void UpdateProduct(Product product);

        void UpdateAll(Product[] products);

        void Delete(Product product);
    }
}
    
