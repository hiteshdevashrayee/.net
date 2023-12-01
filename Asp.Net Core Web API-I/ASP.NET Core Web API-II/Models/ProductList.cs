using ASP.NET_Core_Web_API_II.Interface;
using System.Text.Json;

namespace ASP.NET_Core_Web_API_II.Models
{
    public class ProductList : IProductList
    {
        public delegate string CallBack(string a);
        public string GetProductId()
        {
            return Guid.NewGuid().ToString();
        }


        public static string DelegateMethod(string a) => "Called Delegate Method";

        CallBack _callBack = DelegateMethod;
    }
}
