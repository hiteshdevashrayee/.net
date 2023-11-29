using Asp.Net_Core_Web_API.Interface;

namespace Asp.Net_Core_Web_API.Utility
{
    public class Message: IMessage  
    {
        public void WriteMessage(string message)
        {
            Console.WriteLine("Dependency Injection");
        }

        public void WriteMessage()
        {
            throw new NotImplementedException();
        }
    }
}
