namespace MyPlanner.Shared.Extensions
{
    /// <summary>
    /// Provides extension methods for LINQ select operations.
    /// </summary>
    public static class LinqSelectExtensions
    {
        /// <summary>
        /// Projects each element of a sequence into a new form and handles any exceptions that occur during the projection.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <typeparam name="TResult">The type of the value returned by the selector.</typeparam>
        /// <param name="enumerable">The source sequence.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> that contains the results of the projection.</returns>
        public static IEnumerable<SelectTryResult<TSource, TResult>> SelectTry<TSource, TResult>(this IEnumerable<TSource> enumerable, Func<TSource, TResult> selector)
        {
            foreach (TSource element in enumerable)
            {
                SelectTryResult<TSource, TResult> returnedValue;
                try
                {
                    returnedValue = new SelectTryResult<TSource, TResult>(element, selector(element), null);
                }
                catch (Exception ex)
                {
                    returnedValue = new SelectTryResult<TSource, TResult>(element, default!, ex);
                }
                yield return returnedValue;
            }
        }

        /// <summary>
        /// Handles any exceptions that occurred during the projection and returns the result or a default value.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <typeparam name="TResult">The type of the value returned by the selector.</typeparam>
        /// <param name="enumerable">The source sequence.</param>
        /// <param name="exceptionHandler">A function to handle the caught exception and return a result.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> that contains the results of the projection or the exception handler result.</returns>
        public static IEnumerable<TResult> OnCaughtException<TSource, TResult>(this IEnumerable<SelectTryResult<TSource, TResult>> enumerable, Func<Exception, TResult> exceptionHandler)
        {
            return enumerable.Select(x => x.CaughtException == null ? x.Result : exceptionHandler(x.CaughtException));
        }

        /// <summary>
        /// Handles any exceptions that occurred during the projection and returns the result or a result based on the source element and the caught exception.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <typeparam name="TResult">The type of the value returned by the selector.</typeparam>
        /// <param name="enumerable">The source sequence.</param>
        /// <param name="exceptionHandler">A function to handle the caught exception and return a result based on the source element and the exception.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> that contains the results of the projection or the exception handler result.</returns>
        public static IEnumerable<TResult> OnCaughtException<TSource, TResult>(this IEnumerable<SelectTryResult<TSource, TResult>> enumerable, Func<TSource, Exception, TResult> exceptionHandler)
        {
            return enumerable.Select(x => x.CaughtException == null ? x.Result : exceptionHandler(x.Source, x.CaughtException));
        }

        /// <summary>
        /// Represents the result of a select operation that may throw an exception.
        /// </summary>
        /// <typeparam name="TSource">The type of the source element.</typeparam>
        /// <typeparam name="TResult">The type of the result element.</typeparam>
        public class SelectTryResult<TSource, TResult>
        {
            internal SelectTryResult(TSource source, TResult result, Exception? exception)
            {
                Source = source;
                Result = result;
                CaughtException = exception;
            }

            /// <summary>
            /// Gets the source element.
            /// </summary>
            public TSource Source { get; private set; }

            /// <summary>
            /// Gets the result of the select operation.
            /// </summary>
            public TResult Result { get; private set; }

            /// <summary>
            /// Gets the exception caught during the select operation, if any.
            /// </summary>
            public Exception? CaughtException { get; private set; }
        }
    }
}
