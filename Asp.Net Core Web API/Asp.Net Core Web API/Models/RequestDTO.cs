using System.Diagnostics;

namespace Asp.Net_Core_Web_API.Models
{
    public class RequestDTO
    {
        public int ApplicationID { get; set; }
        public int UserID { get; set; }
        public int RequestId { get; set; }
        public List<ProductDTO>? productList { get; set; }
    }
}
