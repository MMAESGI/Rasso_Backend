namespace RassoApi.Exceptions
{
    /// <summary>
    /// Exception lorsque la configation est incorrecte
    /// </summary>
    internal class InvalidConfigurationException : Exception
    {
        internal InvalidConfigurationException(string message) : base(message) { }
        
    }
}
