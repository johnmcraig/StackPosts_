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
    public class ReplyService : RepositoryService<ReplyModel>, IReplyService
    {
        private readonly HttpClient _client;
        private readonly ILogger<RepositoryService<ReplyModel>> _logger;

        public ReplyService(HttpClient client, ILogger<RepositoryService<ReplyModel>> logger) : base(client, logger)
        {
            _client = client;
            _logger = logger;
        }
    }
}
