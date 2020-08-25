using System.Buffers.Text;

namespace StackPosts_.Client.Static
{
    public class Endpoints
    {
        private const string BaseUrl = "https://localhost:5001";
        public static readonly string PostsEndpoint = $"{BaseUrl}/api/posts";
    }
}