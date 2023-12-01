using Asp.Net_Core_Web_API.Interface;
using Asp.Net_Core_Web_API.Models;
using Microsoft.AspNetCore.Authentication;

namespace Asp.Net_Core_Web_API.Utility
{
    public class Application : IApplication
    {
        public string? applicationId { get; set; }
        
        public Guid RequestId => new Guid();

        public string GetId(string Id)
        {
            if (string.IsNullOrEmpty(Id))
            {
                Guid guid = Guid.NewGuid();
                applicationId = guid.ToString();
            }
            
            return applicationId;            
        }
    }
}
