using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constans;
using Data.UnitOfWork;
using Core.Utilities.Results;
using Data.Abstract;
using Entities.Concrete;


namespace Business.Concrete
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerDal _customerDal;
        private readonly IUnitofWork _unitofWork;
        public CustomerService(ICustomerDal customerDal, IUnitofWork unitofWork)
        {
            _customerDal = customerDal;
            _unitofWork = unitofWork;
        }

        [SecuredOperation("admin,editor")]
        public IResult Add(Customer customer)
        {
            if(customer is not null)
            {
                _customerDal.Add(customer);
                _unitofWork.Commit();
                return new SuccessResult(Messages.CustomerAdded);
            }
            return new ErrorResult(Messages.ErrorCustomerAdded);
        }
        [SecuredOperation("admin")]
        public IResult Delete(Customer customer)
        {
            if(customer is not null)
            {
                _customerDal.Delete(customer);
                _unitofWork.Commit();
                return new SuccessResult(Messages.CustomerDeleted);
            }
            return new ErrorResult(Messages.ErrorCustomerDeleted);
        }

        [SecuredOperation("admin,editor")]
        public IDataResult<Customer> Get(int id)
        {
           return new SuccessDataResult<Customer>(_customerDal.Get(x => x.Id == id));
        }

        [SecuredOperation("admin,editor")]
        public IDataResult<List<Customer>> GetAll()
        {
            return new SuccessDataResult<List<Customer>>(_customerDal.GetAll());
        }
        [SecuredOperation("admin,editor")]
        public IResult Update(Customer customer)
        {
            if(customer is not null)
            {
                _customerDal.Update(customer);
                _unitofWork.Commit();
                return new SuccessResult(Messages.CustomerUpdated);
            }
            return new SuccessResult(Messages.ErrorUpdatedCustomer);
        }
    }
}
