using KVSC.Common;
using KVSC.Data.Models;
using KVSC.Data;
using KVSC.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KVSC.Service.Service
{
    public interface IDoctorScheduleService
    {

        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(int id);
        Task<IBusinessResult> DeleteById(int id);

        Task<IBusinessResult> Save(DoctorsSchedule doctorsSchedule);


    }

    public class DoctorScheduleService : IDoctorScheduleService
    {
        private readonly UnitOfWork _unitOfWork;

        public DoctorScheduleService()
        {
            _unitOfWork ??= new UnitOfWork();
        }

        public async Task<IBusinessResult> GetAll()
        {
            try
            {
                var doctorSchedule = await _unitOfWork.DoctorSheduleRepository.GetAllAsync();

                if (doctorSchedule == null || !doctorSchedule.Any())
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
                }

                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, doctorSchedule);
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<IBusinessResult> GetById(int id)
        {
            try
            {
                var doctorSchedule = await _unitOfWork.DoctorSheduleRepository.GetByIdAsync(id);


                if (doctorSchedule == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
                }

                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, doctorSchedule);
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<IBusinessResult> DeleteById(int id)
        {
            try
            {
                var doctorSchedule = await _unitOfWork.DoctorSheduleRepository.GetByIdAsync(id);
                if (doctorSchedule != null)
                {
                    bool result = await _unitOfWork.DoctorSheduleRepository.RemoveAsync(doctorSchedule);
                    if (result)
                    {
                        return new BusinessResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG);
                    }

                    return new BusinessResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG);
                }

                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }

        public async Task<IBusinessResult> Save(DoctorsSchedule doctorsSchedule)
        {
            try
            {
                int result = -1;
                if (doctorsSchedule != null && doctorsSchedule.ScheduleId <= 0)
                {
                    result = await _unitOfWork.DoctorSheduleRepository.CreateAsync(doctorsSchedule);
                    if (result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, doctorsSchedule);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
                    }
                }
                else
                {
                    result = await _unitOfWork.DoctorSheduleRepository.UpdateAsync(doctorsSchedule);
                    if (result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, doctorsSchedule);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
                    }

                }


            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }
    }
}
