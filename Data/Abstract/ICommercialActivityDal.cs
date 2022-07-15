using Core.Data.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;


namespace Data.Abstract
{
    public interface ICommercialActivityDal:IGenericRepository<CommercialActivity>
    {
        List<CustomerCommercialActivityDto> GetWithCustomerCommercialActivityDto(int CustomerId);
        List<CustomerCommercialActivityDto> GetAllWithCustomerCommercialActivityDto();

    }
}
