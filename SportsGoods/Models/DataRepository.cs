using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SportsGoods.Models
{
    public class DataRepository : IRepository
    {    //kada je u Listu stored onda preko AddSingleton kreira se jedan objekat(objekti koji su isti) kad se resolvuje dependency u IRepository
        //private List<Product> data = new List<Product>(); // equals to public IEnumerable<Product> Products { get { return data; } }
        private readonly DataContext context;
        public DataRepository(DataContext ctx) => context = ctx; // DataContext object is passed via DataRepository constructor, koji ce biti provided during dependency injection at runtime
        public IEnumerable<Product> Products => context.Products.Include(p => p.Category).ToArray(); //to include related category object in the query made by product repository
        public Product GetProduct(long key) => context.Products.Include(p => p.Category).First(p => p.Id == key); //provide a single Product object using its primary key value
        public void AddProduct(Product product) // Add method accepts product objects through DbSet<Product> property
        {
            context.Products.Add(product);
            context.SaveChanges();
        } 
        public void UpdateProduct(Product product)
        {
            Product p = context.Products.Find(product.Id);//query database
            p.Name = product.Name; //and copy values from HTTP data
            //p.Category = product.Category;
            p.PurchasePrice = product.PurchasePrice;
            p.RetailPrice = product.RetailPrice;
            p.CategoryId = product.CategoryId;
            context.SaveChanges();
        }

        public void UpdateAll(Product[] products) //for updating only changed values
        {

            // context.Products.UpdateRange(products);

            Dictionary<long, Product> data = products.ToDictionary(p => p.Id);
            IEnumerable<Product> baseline =
            context.Products.Where(p => data.Keys.Contains(p.Id));

            foreach (Product databaseProduct in baseline)//enumerated every query object from the request objects
            {
                Product requestProduct = data[databaseProduct.Id]; //indexer
                databaseProduct.Name = requestProduct.Name;
                databaseProduct.Category = requestProduct.Category;
                databaseProduct.PurchasePrice = requestProduct.PurchasePrice;
                databaseProduct.RetailPrice = requestProduct.RetailPrice;
            }
            context.SaveChanges();
        }

        public void Delete(Product product)
        {
            context.Products.Remove(product);
            context.SaveChanges();
        }
    }
}
