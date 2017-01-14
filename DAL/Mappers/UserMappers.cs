using System.Collections.Generic;
using System.Linq;
using DAL.Interfaces.Entities;
using ORM;

namespace DAL.Mappers
{
    public static class UserMappers
    {
        public static DalUser ToDalUser(this User user)
        {
            if (user == null) return null;

            return new DalUser
            {
                Id = user.Id,
                Login = user.Login,
                Password = user.Password,
                Roles = user.Roles.Select(role => role.Name)
            };
        }

        public static User ToOrmUser(this DalUser dalUser)
        {
            if (dalUser == null) return null;

            return new User
            {
                Id = dalUser.Id,
                Login = dalUser.Login,
                Password = dalUser.Password
            };
        }

        public static IEnumerable<DalUser> ToDalUsers(this IQueryable<User> users)
        {
            return users.AsEnumerable().Select(user => user.ToDalUser()).ToList();
        }
    }
}