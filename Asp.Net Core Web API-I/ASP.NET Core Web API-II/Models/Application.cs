using ASP.NET_Core_Web_API_II.Interface;

namespace ASP.NET_Core_Web_API_II.Models
{
    public class Application : IApplication
    {
        public string GetApplicationId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
