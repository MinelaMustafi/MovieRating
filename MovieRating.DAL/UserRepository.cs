using MovieRating.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieRating.DAL
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(MovieRatingContext context) : base(context) { }

        public override async Task Insert(User user)
        {
            user.Password = user.Username.HashWith(user.Password);
            dbSet.Add(user);
        }

        public override async Task Update(User user, int id)
        {
            User oldUser = await Get(id);
            if (oldUser != null)
            {
                user.Password = oldUser.Password;
                _ctx.Entry(oldUser).CurrentValues.SetValues(user);
            }
        }


        public async Task SetPassword(int userId, string password)
        {
            User user = await Get(userId);
            if (user != null)
            {
                user.Password = user.Username.HashWith(password);
                _ctx.Entry(user).CurrentValues.SetValues(user);
            }
        }
    }
}
