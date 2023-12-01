namespace Asp.Net_Core_Web_API.Interface
{
    public interface IRequest
    {
        public Guid RequestId { get;}

        string GetId(string Id);
    }
}
