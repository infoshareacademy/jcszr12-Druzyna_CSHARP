using ProjectClock.BusinessLogic.Dtos.WorkingTime.WorkingTimeDtos;
using ProjectClock.Database.Entities;

namespace ProjectClock.BusinessLogic.Services.WorkingTimeServices
{
    public interface IWorkingTimeServices
    {
        Task<bool> Create(StartStopWorkingTimeDto dto);
        Task<WorkingTime>? GetById(int id);
        Task<List<WorkingTime>> GetAll();
        Task Update(UpdateWorkingTimeDto dto);
        Task<bool> Delete(int id);
        bool WorkingTimeExist(StartStopWorkingTimeDto dto);
        Task<IEnumerable<NotFinisedWorkingTimeDto>> GetNotFinisedWorkingTimes();
        Task<bool> StopWork(StartStopWorkingTimeDto dto);
    }

}

