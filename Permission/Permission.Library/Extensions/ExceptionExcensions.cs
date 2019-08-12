namespace Permission.Library.Extensions
{
    using System;

    public static class ExceptionExcensions
    {
        public static string GetInfo(this Exception ex)
        {
            return string.Concat(ex.Message, ex.StackTrace);
        }
    }
}
