using System;
using System.Collections.Generic;
using System.Text;

namespace RedeDoVerde.Domain.Post
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public string ImagePost { get; set; }
        public Account.Account Account { get; set; }
        public List<Comment.Comment> Comments { get; set; }

    }
}
