using KVSC.Common;
using KVSC.Data;
using KVSC.Data.Models;
using KVSC.Service.Base;

namespace KVSC.Service.Service
{
    public interface ICustomerService
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(int id);
        Task<IBusinessResult> Save(Customer customer);
        Task<IBusinessResult> Delete(int customerId);
    }

    public class CustomerService : ICustomerService
    {
        private UnitOfWork _unitOfWork;

        public CustomerService()
        {
            _unitOfWork ??= new UnitOfWork();
        }

        public async Task<IBusinessResult> Delete(int customerId)
        {
            try
            {
                var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(customerId);
                if (customer == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new Customer());
                }

                var result = await _unitOfWork.CustomerRepository.RemoveAsync(customer);
                if (result)
                {
                    return new BusinessResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG, customer);
                }
                else
                {
                    return new BusinessResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG, customer);
                }
            }
            catch (Exception e)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, e.ToString());
            }
        }

        public async Task<IBusinessResult> GetAll()
        {
            try
            {
                var result = await _unitOfWork.CustomerRepository.GetAllAsync();
                if (!result.Any())
                {
                    return new BusinessResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG);
                }
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, result);
            }
            catch (Exception e)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, e.ToString());
            }
        }

        public async Task<IBusinessResult> GetById(int id)
        {
            try
            {
                var result = await _unitOfWork.CustomerRepository.GetByIdAsync(id);
                if (result == null)
                {
                    return new BusinessResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG);
                }
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, result);
            }
            catch (Exception e)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, e.ToString());
            }
        }

        public async Task<IBusinessResult> Save(Customer customer)
        {
            try
            {
                int result = -1;
                if (customer == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new Customer());
                }

                if (customer.CustomerId <= 0)
                {
                    result = await _unitOfWork.CustomerRepository.CreateAsync(customer);
                    if (result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, customer);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
                    }
                }
                else
                {
                    result = await _unitOfWork.CustomerRepository.UpdateAsync(customer);
                    if (result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, customer);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
                    }
                }
            }
            catch (Exception e)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, e.ToString());
            }
        }

    }
}

