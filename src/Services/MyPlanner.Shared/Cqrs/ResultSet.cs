namespace MyPlanner.Shared.Cqrs
{
    public class ResultSet
    {
        public bool IsError { get; set; } = false;
        public bool IsSuccess { get; private set; } = false;
        public string Message { get; private set; } = string.Empty;
        public object? Data { get; private set; } = default!;

        private ResultSet() { }

        private ResultSet(bool isError, bool isSuccess, string message, object? data)
        {
            IsError = isError;
            IsSuccess = isSuccess;
            Message = message;
            Data = data;
        }

        public static ResultSet Success(string message, object data)
        {
            return new ResultSet(false, true, message, data);
        }

        public static ResultSet Error(string message, object data)
        {
            return new ResultSet(true, false, message, data);
        }

        public static ResultSet Error(string message)
        {
            return new ResultSet(true, false, message, null);
        }

        public static ResultSet Success(string message)
        {
            return new ResultSet(false, true, message, null);
        }

        public static ResultSet Success()
        {
            return new ResultSet(false, true, string.Empty, null);
        }

        public static ResultSet Error()
        {
            return new ResultSet(true, false, string.Empty, null);
        }

        public static ResultSet Error(object data)
        {
            return new ResultSet(true, false, string.Empty, data);
        }

        public static ResultSet Success(object data)
        {
            return new ResultSet(false, true, string.Empty, data);
        }
    }
}
