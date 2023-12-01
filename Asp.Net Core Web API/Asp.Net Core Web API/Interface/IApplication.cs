namespace Asp.Net_Core_Web_API.Interface
{
    public interface IApplication: IRequest
    {
        public string applicationId { get; set; }
    }
}
