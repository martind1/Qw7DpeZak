namespace DpeZak.Services.Kmp.Exceptions
{
    /// <summary>
    /// Basis Exception für unsere Anwendung
    /// </summary>
    [Serializable]
    public class KmpException : Exception
    {
        public KmpException() { }
        public KmpException(string message) : base(message) { }
        public KmpException(string message, Exception inner) : base(message, inner) { }
    }

}
