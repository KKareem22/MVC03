using RouteProject.BLL.Services.ViewModels.PlanViewModels;

namespace RouteProject.BLL.Services.Interfaces
{
    public interface IPlanService
    {
        Task<IEnumerable<PlanViewModel>> GetAllAsync(CancellationToken ct = default);
        Task<PlanViewModel?> GetPlanDetailsAsync(int id, CancellationToken ct = default);
        Task<PlanToUpdateViewModel?> GetPlanToUpdateViewModel(int id, CancellationToken ct = default);
        Task<bool> UpdatePlanAsync(int id, PlanToUpdateViewModel model, CancellationToken ct = default);
    }
}
