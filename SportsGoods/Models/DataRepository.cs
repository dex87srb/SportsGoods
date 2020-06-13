﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SportsGoods.Models
{
    public class DataRepository : IRepository
    {    
        private readonly DataContext context;
        public DataRepository(DataContext ctx) => context = ctx; 
        public IEnumerable<Product> Products => context.Products.Include(p => p.Category).ToArray(); 
        public Product GetProduct(long key) => context.Products.Include(p => p.Category).First(p => p.Id == key); 
        public void AddProduct(Product product) 
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

        public void UpdateAll(Product[] products) 
        {

            Dictionary<long, Product> data = products.ToDictionary(p => p.Id);
            IEnumerable<Product> baseline =
            context.Products.Where(p => data.Keys.Contains(p.Id));

            foreach (Product databaseProduct in baseline)//enumerated every query object from the request objects
            {
                Product requestProduct = data[databaseProduct.Id]; 
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
