using Asp.Net_Core_Web_API.Models;

namespace Asp.Net_Core_Web_API.Interface
{
    public interface IProduct
    {
        string GetProducts();
        string GetProductById(int Id);
        void SetProducts(List<Products> ProductsList);        
        void AddProducts(string value);
        void UpdateProducts(int Id, string value);
        void DeleteProducts(int Id);
    }
}
