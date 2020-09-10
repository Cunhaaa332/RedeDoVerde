using RedeDoVerde.Repository.Context;
using RedeDoVerde.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace RedeDoVerde.Repository.Post
{
    public class PostRepository
    {
        private readonly RedeDoVerdeContext _context;

        public PostRepository(RedeDoVerdeContext context)
        {
            _context = context;
        }

        //public async Task CreatePost(Domain.Post.Post post)
        //{
        //    _context.Add(post);
        //    //await _context.SaveChanges();
        //}
    }
}
