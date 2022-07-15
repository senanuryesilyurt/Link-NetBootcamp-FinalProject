using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface ICommercialActivityService
    {
        IDataResult<List<CommercialActivity>> GetAll();
        IDataResult<CommercialActivity> Get(int id);
        IDataResult<List<CustomerCommercialActivityDto>> Get_With_CustomerCommercialActivityDto(int CustomerId);
        IDataResult<List<CustomerCommercialActivityDto>> GetAll_With_CustomerCommercialActivityDto();
        IResult Add(CommercialActivity commercialActivity);
        IResult Delete(CommercialActivity commercialActivity);
        IResult Update(CommercialActivity commercialActivity);
    }
}
