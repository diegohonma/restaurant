namespace Restaurant.Domain.Entities
{
    public class Entity<T> where T : class
    {
        public T Value { get; }

        public string Error { get; }

        public Entity(T value, string error)
        {
            Value = value;
            Error = error;
        }
    }
}
