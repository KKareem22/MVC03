using RouteProject.BLL.Services.Interfaces;
using RouteProject.BLL.Services.ViewModels.PlanViewModels;
using RouteProject.DAL.Data.Models;
using RouteProject.DAL.Repositories.Interfaces;

namespace RouteProject.BLL.Services.Classes
{
    public class PlanService : IPlanService
    {
        private readonly IGenericRepository<Plan> planRepository;
        private readonly IGenericRepository<MemberShip> membershipRepository;

        public PlanService(IGenericRepository<Plan> planRepository,
            IGenericRepository<MemberShip> membershipRepository)
        {
            this.planRepository = planRepository;
            this.membershipRepository = membershipRepository;
        }

        public async Task<bool> ActivationAsync(int id, CancellationToken ct = default)
        {
            var plan=await planRepository.GetByIdAsync(id, ct);
            if(plan is null)
                return false;
            if(plan.IsActive&&await membershipRepository.AnyAsync(m=>m.PlanId==id,ct))
                return false;
            plan.IsActive = !plan.IsActive;
            plan.UpdateAt = DateTime.Now;
            var result = await planRepository.UpdateAsync(plan, ct);
            return result > 0;

        }

        public async Task<IEnumerable<PlanViewModel>> GetAllAsync(CancellationToken ct)
        {
            var plans = await planRepository.GetAllAsync(ct: ct);
            if (plans is null)
                return [];
            var planViewModels = plans.Select(p => new PlanViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Duration = p.DurationDays,
                IsActive = p.IsActive
            });
            return planViewModels;
        }

        public async Task<PlanViewModel?> GetPlanDetailsAsync(int id, CancellationToken ct = default)
        {
            var plan = await planRepository.GetByIdAsync(id, ct);
            if (plan is null)
                return null;
            var planViewModel = new PlanViewModel
            {
                Id = plan.Id,
                Name = plan.Name,
                Description = plan.Description,
                Price = plan.Price,
                Duration = plan.DurationDays,
                IsActive = plan.IsActive
            };
            return planViewModel;
        }

        public async Task<PlanToUpdateViewModel?> GetPlanToUpdateViewModel(int id, CancellationToken ct = default)
        {
            var plan = await planRepository.GetByIdAsync(id, ct);
            if (plan is null)
                return null;
            if (await HasActiveMembership(id,ct))
                return null;
            else
            {
                return new PlanToUpdateViewModel
                {
                    Name = plan.Name,
                    Description = plan.Description,
                    Price = plan.Price,
                    DurationDay = plan.DurationDays
                };
            }

        }

        public async Task<bool> UpdatePlanAsync(int id, PlanToUpdateViewModel model, CancellationToken ct = default)
        {
            var plan = await planRepository.GetByIdAsync(id, ct);
            if (plan is null)
                return false;
            if(await HasActiveMembership(id,ct))
                return false;
            plan.DurationDays = model.DurationDay;
            plan.Description = model.Description;
            plan.Price = model.Price;
            plan.UpdateAt = DateTime.Now;

            var Result = await planRepository.UpdateAsync(plan, ct);
            return Result > 0;

        }

        #region Helper Method
        private async Task<bool>HasActiveMembership(int id ,CancellationToken ct)
        {
            return await membershipRepository.AnyAsync(m => m.PlanId == id, ct);
        }
        #endregion
    }
}
