using Microsoft.Extensions.Logging;
using StackPosts_.Client.Contracts;
using StackPosts_.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace StackPosts_.Client.Services
{
    public class PostService : RepositoryService<PostModel>, IPostService
    {
        private readonly HttpClient _client;
        private readonly ILogger<RepositoryService<PostModel>> _logger;

        public PostService(HttpClient client, ILogger<RepositoryService<PostModel>> logger) : base(client, logger)
        {
            _client = client;
            _logger = logger;
        }
    }
}
