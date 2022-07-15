using Core.Data.EntityFramework;
using Data.Abstract;
using Entities.Concrete;
using Entities.DTOs;


namespace Data.Concrete.EntityFramework
{
    public class CommercialActivityDal : GenericRepository<CommercialActivity>, ICommercialActivityDal
    {
        private PostgreSqlContext _context;
        public CommercialActivityDal(PostgreSqlContext context) : base(context)
        {
            _context = context;
        }

        public List<CustomerCommercialActivityDto> GetAllWithCustomerCommercialActivityDto()
        {
            var result = from c in _context.Customers
                            join a in _context.CommercialActivities
                            on c.Id equals a.CustomerId
                            select new CustomerCommercialActivityDto
                            {
                                FirstName = c.FirstName,
                                LastName = c.LastName,
                                Email = c.Email,
                                PhoneNumber = c.PhoneNumber,
                                Job = a.Job,
                                Price = a.Price,
                                Date = a.Date
                            };
            return result.ToList();
            
        }

        public List<CustomerCommercialActivityDto> GetWithCustomerCommercialActivityDto(int CustomerId)
        {
            var result = from c in _context.Customers
                            join a in _context.CommercialActivities
                            on c.Id equals a.CustomerId
                            where c.Id == CustomerId
                            select new CustomerCommercialActivityDto
                            {
                                FirstName = c.FirstName,
                                LastName = c.LastName,
                                Email = c.Email,
                                PhoneNumber = c.PhoneNumber,
                                Job = a.Job,
                                Price = a.Price,
                                Date = a.Date
                            };
            return result.ToList();

        }
    }
}
