

using KVSC.Common;
using KVSC.Data;
using KVSC.Data.Models;
using KVSC.Service.Base;

namespace KVSC.Service.Service;

public interface IServiceRequestService
{
    Task<IBusinessResult> GetAll();
    Task<IBusinessResult> GetById(int id);
    Task<IBusinessResult> Save(ServiceRequest serviceRequest);
    Task<IBusinessResult> Delete(int serviceRequestID);
}
public class ServiceRequestService : IServiceRequestService
{
    private UnitOfWork _unitOfWork;

    public ServiceRequestService()
    {
        _unitOfWork ??= new UnitOfWork();
    }

    public async Task<IBusinessResult> Delete(int serviceRequestID)
    {
        try
        {
            var serviceRequest = await _unitOfWork.ServiceRequestRepository.GetByIdAsync(serviceRequestID);
            if (serviceRequest == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new ServiceRequest());
            }
            var result = await _unitOfWork.ServiceRequestRepository.RemoveAsync(serviceRequest);
            if (result)
            {
                return new BusinessResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG, serviceRequest);
            }
            else
            {
                return new BusinessResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG);
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
            var result = await _unitOfWork.ServiceRequestRepository.GetAllAsync();
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
            var result = await _unitOfWork.ServiceRequestRepository.GetByIdAsync(id);
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

    public async Task<IBusinessResult> Save(ServiceRequest serviceRequest)
    {
        try
        {
            int result = -1;
            if (serviceRequest == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new ServiceRequest());
            }
            
            if (serviceRequest.RequestId <= 0)
            {
                result = await _unitOfWork.ServiceRequestRepository.CreateAsync(serviceRequest);
                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, serviceRequest);
                }
                else
                {
                    return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
                }
            }
            else
            {
                result = await _unitOfWork.ServiceRequestRepository.UpdateAsync(serviceRequest);
                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, serviceRequest);
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
