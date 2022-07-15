using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constans;
using Data.UnitOfWork;
using Core.Utilities.Results;
using Data.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Concrete
{
    public class CommercialActivityService : ICommercialActivityService
    {
        private readonly ICommercialActivityDal _commericalActivityDal;
        private readonly IUnitofWork _unitofWork;
        public CommercialActivityService(ICommercialActivityDal commercialActivityDal, IUnitofWork unitofWork)
        {
            _commericalActivityDal= commercialActivityDal;
            _unitofWork= unitofWork;
        }

        [SecuredOperation("admin,editor")]
        public IResult Add(CommercialActivity commercialActivity)
        {
            if(commercialActivity is not null)
            {
                _commericalActivityDal.Add(commercialActivity);
                _unitofWork.Commit();
                return new SuccessResult(Messages.ErrorCommercialActivityAdded);
            }
            return new SuccessResult(Messages.ErrorCommercialActivityAdded);
        }

        [SecuredOperation("admin")]
        public IResult Delete(CommercialActivity commercialActivity)
        {
            if (commercialActivity is not null)
            {
                _commericalActivityDal.Delete(commercialActivity);
                _unitofWork.Commit();
                return new SuccessResult(Messages.CommercialActivityDeleted);
            }
            return new SuccessResult(Messages.ErrorCommercialActivityDeleted);
        }

        [SecuredOperation("admin,editor")]
        public IDataResult<CommercialActivity> Get(int id)
        {
            return new SuccessDataResult<CommercialActivity>(_commericalActivityDal.Get(x=>x.Id==id));
        }

        [SecuredOperation("admin,editor")]
        public IDataResult<List<CommercialActivity>> GetAll()
        {
            return new SuccessDataResult<List<CommercialActivity>>(_commericalActivityDal.GetAll());
        }

        [SecuredOperation("admin,editor")]
        public IDataResult<List<CustomerCommercialActivityDto>> GetAll_With_CustomerCommercialActivityDto()
        {
            return new SuccessDataResult<List<CustomerCommercialActivityDto>>(_commericalActivityDal.GetAllWithCustomerCommercialActivityDto());
        }

        [SecuredOperation("admin,editor")]
        public IDataResult<List<CustomerCommercialActivityDto>> Get_With_CustomerCommercialActivityDto(int CustomerId)
        {
            return new SuccessDataResult<List<CustomerCommercialActivityDto>>(_commericalActivityDal.GetWithCustomerCommercialActivityDto(CustomerId));
        }

        [SecuredOperation("admin,editor")]
        public IResult Update(CommercialActivity commercialActivity)
        {
            if (commercialActivity is not null)
            {
                _commericalActivityDal.Update(commercialActivity);
                _unitofWork.Commit();
                return new SuccessResult(Messages.CommercialActivityUpdated);
            }
            return new SuccessResult(Messages.ErrorCommercialActivityUpdateted);
        }
    }
}
