namespace Restaurant.Application.Responses
{
    public class GetProductsResponse
    {
        public int Id { get; }

        public string Description { get; }

        public string CookTime { get; }

        public GetProductsResponse(int id, string description, string cookTime)
        {
            Id = id;
            Description = description;
            CookTime = cookTime;
        }
    }
}
