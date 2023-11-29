using Asp.Net_Core_Web_API.Interface;
using System.Collections.Generic;
using System.Text.Json;

namespace Asp.Net_Core_Web_API.Models
{
    public class Products: IProduct
    {
        public int ProductID { get; set; }
        public string? ProductName { get; set; }

        private string? ProductsJson = "none";

        List<Products> ProductsList =new List<Products>();
        public Products()
        {
            ProductsJson = System.IO.File.ReadAllText(@"C:\Development\.net\Asp.Net Core Web API\Asp.Net Core Web API\Models\Products.txt");
            ProductsList = JsonSerializer.Deserialize<List<Products>>(ProductsJson);
        }
        public string GetProductById(int id)
        {
            Products products = ProductsList.FirstOrDefault(x => x.ProductID == id);
            return JsonSerializer.Serialize(products);
        }
        public string GetProducts()
        {
           return ProductsJson;
        }
        public void SetProducts(List<Products> ProductsList)
        {
            ProductsJson = JsonSerializer.Serialize(ProductsList);
            System.IO.File.WriteAllText(@"C:\Development\.net\Asp.Net Core Web API\Asp.Net Core Web API\Models\Products.txt", ProductsJson);
        }

        public void UpdateProducts(string value)
        {
            string[] strings = value.Split(',');
            Products products = new Products();
            products.ProductID = Convert.ToInt32(strings[0]);
            products.ProductName = strings[1];
            ProductsList.Add(products);
            SetProducts(ProductsList);
        }

        public void DeleteProducts(int Id)
        {
            Products products = ProductsList.FirstOrDefault(x => x.ProductID == Id);
            ProductsList.Remove(products);
            SetProducts(ProductsList);
        }

        public void AddProducts(string value)
        {
            string[] strings = value.Split(',');
            Products products = new Products();
            products.ProductID = Convert.ToInt32(strings[0]);
            products.ProductName = strings[1];
            ProductsList.Add(products);
            SetProducts(ProductsList);
        }

        public void UpdateProducts(int Id, string value)
        {
            Products products = ProductsList.FirstOrDefault(x => x.ProductID == Id);
            products.ProductName = value;
            SetProducts(ProductsList);
        }
    }
}
