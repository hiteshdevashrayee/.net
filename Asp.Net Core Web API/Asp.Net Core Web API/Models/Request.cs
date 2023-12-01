using Asp.Net_Core_Web_API.Interface;
using System;

namespace Asp.Net_Core_Web_API.Models
{
    public class Request : IApplication, IUsers
    {
        Guid guid;
        public string applicationId { get; set; }
        public string userId { get; set; }

        public string RequestId { get; set; }

        Guid IRequest.RequestId => new Guid();

        //Guid IRequest.RequestId => throw new NotImplementedException();

        public Request() : this(Guid.NewGuid())
        {

        }
        public Request(Guid guid)
        {
            this.RequestId = guid.ToString();
        }
        //public Guid RequestId => new Guid();

        //public string? userId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string GetId(string Id)
        {
            if (string.IsNullOrEmpty(Id))
            {
                Guid guid = Guid.NewGuid();
                RequestId = guid.ToString();
            }
            return RequestId;
        }
    }

    public class RequestService
    {
        public IRequest request { get; }
        public IApplication application { get; }
        public IUsers users { get; }

        public RequestService(IRequest request, IApplication application, IUsers users)
        {
            this.request = request;
            this.application = application;
            this.users = users;
        }
    }
}
