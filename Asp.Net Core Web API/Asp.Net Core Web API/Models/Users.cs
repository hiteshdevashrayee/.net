using Asp.Net_Core_Web_API.Interface;

namespace Asp.Net_Core_Web_API.Models
{
    public class Users : IUsers
    {
        public Guid UserId => Guid.NewGuid();
    }
}
