namespace Restaurant.Application.Responses
{
    public class GetProductsResponse
    {
        public int Id { get; }

        public string Description { get; }

        public int CookTime { get; }

        public GetProductsResponse(int id, string description, int cookTime)
        {
            Id = id;
            Description = description;
            CookTime = cookTime;
        }
    }
}
