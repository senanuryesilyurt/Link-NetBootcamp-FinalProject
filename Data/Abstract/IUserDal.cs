using Core.Data.EntityFramework;
using Core.Entities.Concrete;

namespace Data.Abstract
{
    public interface IUserDal:IGenericRepository<User>
    {
        List<Role> GetRoles(User user);
    }
}
