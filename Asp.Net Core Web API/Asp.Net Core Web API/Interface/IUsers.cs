namespace Asp.Net_Core_Web_API.Interface
{
    public interface IUsers: IRequest
    {
        public string? userId { get; set; }
    }
}
