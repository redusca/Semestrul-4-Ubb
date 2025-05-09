namespace networking.jsonProtocol.ServerException

{
    [Serializable]
    internal class ServerStopException : Exception
    {
        public ServerStopException()
        {
        }

        public ServerStopException(string? message) : base(message)
        {
        }

        public ServerStopException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}