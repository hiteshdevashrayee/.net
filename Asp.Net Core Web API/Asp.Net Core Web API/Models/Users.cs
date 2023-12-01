using Asp.Net_Core_Web_API.Interface;
using System;

namespace Asp.Net_Core_Web_API.Models
{
    public class Users : IUsers
    {
        public string? userId { get; set; }
        
        public Guid RequestId => new Guid();

        public string GetId(string Id)
        {
            if (string.IsNullOrEmpty(Id))
            {
                Guid guid = Guid.NewGuid();
                userId = guid.ToString();
            }            
            return userId;
        }
    }
}
