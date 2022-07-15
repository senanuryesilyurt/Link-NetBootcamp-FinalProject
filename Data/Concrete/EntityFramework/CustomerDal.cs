using Core.Data.EntityFramework;
using Data.Abstract;
using Entities.Concrete;


namespace Data.Concrete.EntityFramework
{
    public class CustomerDal : GenericRepository<Customer>, ICustomerDal
    {
        public CustomerDal(PostgreSqlContext context) : base(context)
        {
        }

        
    }
}
