using Business.Abstract;
using Data.UnitOfWork;
using Core.Entities.Concrete;
using Data.Abstract;

namespace Business.Concrete
{
    public class UserService:IUserService
    {
        private IUserDal _userDal;
        private IUnitofWork _unitofWork;

        public UserService(IUserDal userDal, IUnitofWork unitofWork)
        {
            _userDal = userDal;
            _unitofWork = unitofWork;
        }

        public void Add(User user)
        {
            _userDal.Add(user);
            _unitofWork.Commit();
        }

        public void Delete(User user)
        {
            _userDal.Delete(user);
            _unitofWork.Commit();
        }

        public User GetByMail(string email)
        {
            return _userDal.Get(u=>u.Email == email);
        }
        public List<Role> GetRoles(User user)
        {
            return _userDal.GetRoles(user);
        }

        public void Update(User user)
        {
            _userDal.Update(user);
            _unitofWork.Commit();
        }
       
    }
}
