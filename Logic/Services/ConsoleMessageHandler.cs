using Interfaces;

namespace Services
{
    public class ConsoleMessageHandler : IMessageHandler
    {
        public void SendMessage(string message)
        {
            System.Console.WriteLine(message);
        }
    }
}