namespace BeforeTheScholarship.Common.Exceptions
{
    public class ProcessException : Exception
    {
        public ProcessException() { }

        public ProcessException(string message) : base(message) { }

        public ProcessException(string code, string message) : base(message) { }

        public ProcessException(string message, Exception inner) : base(message, inner) { }

        public ProcessException(Exception inner) : base (inner.Message, inner) { }

        public static void ThrowIf(Func<bool> condition, string message)
        {
            if (condition.Invoke())
                throw new ProcessException(message);
        }
    }
}
