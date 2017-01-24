namespace App.Models
{
    public class ModelResult<T>
    {
        public ResultStatus Status { get;}
        public T Value { get;}

        public ModelResult(ResultStatus status, T value)
        {
            Status = status;
            Value = value;
        }

        public ModelResult(ResultStatus status)
        {
            Status = status;
        }
    }
}