using Core.Data.EntityFramework;
using Entities.Concrete;

namespace Data.Abstract
{
    public interface ICustomerDal:IGenericRepository<Customer>
    {
    }
}
