using System;

namespace Chatroom
{
    internal class ConnectionHandlerBase
    {
        public event EventHandler<string> MessageReceived;
    }
}