using RouteProject.BLL.Services.ViewModels.TrainerViewModels;

namespace RouteProject.BLL.Services.Interfaces
{
    public interface ITrainerService
    {
        Task<IEnumerable<TrainerViewModel>> GetAllTrainersAsync(CancellationToken ct);
        Task<bool> CreateTrainerAsync(CreateTrainerViewModel trainer, CancellationToken ct = default);
        Task<TrainerViewModel?> GetTrainerDetailsAsync(int id, CancellationToken ct = default);
        Task<TrainerToUpdateViewModel?> GetTrainerToUpdateViewModel(int id, CancellationToken ct = default);
        Task<bool> UpdateTrainerAsync(int id, TrainerToUpdateViewModel model, CancellationToken ct = default);
        Task<bool> RemoveTrainerAsync(int id, CancellationToken ct);
    }
}
