using System.Buffers.Text;

namespace StackPosts.Client.Static
{
    public class Endpoints
    {
        private const string BaseUrl = "https://localhost:5000";
        public static readonly string PostsEndpoint = $"{BaseUrl}/api/posts";
    }
}