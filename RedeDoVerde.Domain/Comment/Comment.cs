using System;
using System.Collections.Generic;
using System.Text;

namespace RedeDoVerde.Domain.Comment
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public Account.Account Account { get; set; }
        public Post.Post Post { get; set; }
    }
}