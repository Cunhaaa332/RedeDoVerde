using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace RedeDoVerde.Domain.Account
{
    public class Account
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DtBirthday { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }

        public List<Post.Post> Posts { get; set; }

        [JsonIgnore]
        public List<Comment.Comments> Comments{ get; set; }
    }
}
