using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace RedeDoVerde.Domain.Post
{
    public class Post
    {
        public Guid Id { get; set; }
        
        [Required]
        public string Content { get; set; }
        public string ImagePost { get; set; }
        public Account.Account Account { get; set; }

        [JsonIgnore]
        public List<Comment.Comments> Comments { get; set; }

    }
}
