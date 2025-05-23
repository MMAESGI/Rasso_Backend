namespace Common.Database
{
    internal class InvalidDatabaseConnectionException : Exception
    {
        internal InvalidDatabaseConnectionException(string message) : base(message) { }
    }
    
}
