using System.Diagnostics;

namespace Asp.Net_Core_Web_API.Models
{
    public class RequestDTO
    {
        public string? ApplicationID { get; set; }
        public string? UserID { get; set; }
        public string? RequestId { get; set; }
        public List<ProductDTO>? productList { get; set; }
    }
}
