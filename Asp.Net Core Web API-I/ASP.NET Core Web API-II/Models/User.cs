using ASP.NET_Core_Web_API_II.Interface;

namespace ASP.NET_Core_Web_API_II.Models
{
    public class User : IUser
    {
        
        public string GetUserId()
        {
            return Guid.NewGuid().ToString();
        }

        
    }
}
