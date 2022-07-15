using Core.Data.EntityFramework;
using Core.Entities.Concrete;
using Data.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Data.Concrete.EntityFramework
{
    public class UserDal : GenericRepository<User>, IUserDal
    {
        private PostgreSqlContext _context;
        public UserDal(PostgreSqlContext context) : base(context)
        {
            _context = context;
        }

        public List<Role> GetRoles(User user)
        {
           
                var result = from role in _context.Roles
                            join userRole in _context.UserRoles
                            on role.Id equals userRole.RoleId
                             where userRole.UserId == user.Id
                             select new Role { Id = role.Id, Name = role.Name };
                return result.ToList();
           
            
            
        }

    }
}
