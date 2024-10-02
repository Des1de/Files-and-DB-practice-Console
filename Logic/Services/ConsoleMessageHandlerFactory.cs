using Interfaces;

namespace Services
{
    public class ConsoleMessageHandlerFactory : IMessageHandlerFactory
    {
        public IMessageHandler CreateMessageHandler()
        {
            return new ConsoleMessageHandler(); 
        }
    }
}