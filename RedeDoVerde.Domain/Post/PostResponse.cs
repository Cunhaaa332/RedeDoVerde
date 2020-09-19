using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace RedeDoVerde.Domain.Post
{
    public class PostResponse
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public string ImagePost { get; set; }
        public Account.Account Account { get; set; }

        public List<Comment.Comments> Comments { get; set; }

    }
}
