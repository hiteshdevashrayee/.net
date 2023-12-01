using Asp.Net_Core_Web_API.Interface;
using System.Collections.Generic;
using System.Text.Json;

namespace Asp.Net_Core_Web_API.Models
{
    public class Products : IProduct
    {
        public int ProductID { get; set; }
        public string? ProductName { get; set; }
        private string? ProductsJson { get; set; }

        public Guid RequestId => new Guid();

        List<ProductDTO>? ProductsList = new();

        //public delegate string GetCustomersDelegate(string productID);

        public Products()
        {
            LoadProducts();
        }

        public void LoadProducts()
        {

            ProductsJson = System.IO.File.ReadAllText(@"C:\Development\.net\Asp.Net Core Web API\Asp.Net Core Web API\Models\Products.txt");
            if (string.IsNullOrEmpty(ProductsJson))
            {
                ProductsList = new List<ProductDTO>();
            }
            else
                ProductsList = JsonSerializer.Deserialize<List<ProductDTO>>(ProductsJson);
        }
        public string GetProductById(int id)
        {
            ProductDTO? products = ProductsList.FirstOrDefault(x => x.ProductID == id);
            return JsonSerializer.Serialize(products);
        }
        public string GetProducts()
        {
            RequestDTO requestDTO = new RequestDTO();
            requestDTO.productList = ProductsList;
            requestDTO.ApplicationID = 1;
            requestDTO.UserID = 1;
            ProductsJson = JsonSerializer.Serialize(requestDTO);
            return ProductsJson;
        }
        public void SetProducts(List<ProductDTO> ProductsList)
        {
            ProductsJson = JsonSerializer.Serialize(ProductsList);
            System.IO.File.WriteAllText(@"C:\Development\.net\Asp.Net Core Web API\Asp.Net Core Web API\Models\Products.txt", ProductsJson);
        }

        public void UpdateProducts(string value)
        {
            string[] strings = value.Split(',');
            ProductDTO products = new ProductDTO();
            products.ProductID = Convert.ToInt32(strings[0]);
            products.ProductName = strings[1];
            ProductsList.Add(products);
            SetProducts(ProductsList);
        }

        public void DeleteProducts(int Id)
        {
            ProductDTO? products = ProductsList.FirstOrDefault(x => x.ProductID == Id);
            ProductsList.Remove(products);
            SetProducts(ProductsList);
        }

        public void AddProducts(string value)
        {
            ProductDTO products = new ProductDTO();
            products = new ProductDTO();
            products.ProductID = ProductsList.Count();
            products.ProductName = value;
            ProductsList.Add(products);
            SetProducts(ProductsList);
        }

        public void UpdateProducts(int Id, string value)
        {
            ProductDTO products = ProductsList.FirstOrDefault(x => x.ProductID == Id);
            products.ProductName = value;
            SetProducts(ProductsList);
        }
    }
}
