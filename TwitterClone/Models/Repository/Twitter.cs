using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterClone.DataContext;
using TwitterClone.Models;

namespace TwitterClone.Models.Repository
{
    public class Twitter
    {
        private readonly TwitterCloneDBContext _context;

        public Twitter(TwitterCloneDBContext context)
        {
            _context = context;
        }

        public void Logout(Person session)
        {
            _context.Update(session);
            _context.SaveChanges();
        }

        public List<Tweet> TweetList()
        {
            return _context.Tweet.ToList();
        }
    }
}
