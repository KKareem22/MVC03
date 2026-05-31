using Microsoft.AspNetCore.Mvc;
using RouteProject.BLL.Services.Interfaces;
using RouteProject.BLL.Services.ViewModels.PlanViewModels;

namespace RouteProject.PL.Controllers
{
    public class PlansController : Controller
    {
        private readonly IPlanService planService;


        public PlansController(IPlanService planService)
        {
            this.planService = planService;
        }
        #region GetAllPlans(Index)
        //Get  BaseURL/Plans/Index
        //Index -- Show All Plans
        public async Task<IActionResult> Index(CancellationToken ct)
        {
            var plans = await planService.GetAllAsync(ct);
            return View(plans);
        }
        #endregion
        #region Details
        //Get BaseURL/Plans/Details/{id}
        //Details __ Show one member's details
        public async Task<IActionResult> Details(int id, CancellationToken ct)
        {
            var plan = await planService.GetPlanDetailsAsync(id, ct);
            if (plan == null)
            {
                TempData["ErrorMessage"] = "Plan not found.";
                return RedirectToAction(nameof(Index));
            }

            return View(plan);

        }
        #endregion
        #region Edit
        //Get BaseURL/Plans/Edit/{id}
        //Edit __ Show form pre-filled with the member's current data for editing
        [HttpGet]
        public async Task<IActionResult> Edit(int id, CancellationToken ct)
        {
            var planToUpdate = await planService.GetPlanToUpdateViewModel(id, ct);
            if (planToUpdate is null)
            {
                TempData["ErrorMessage"] = "Plan not found.";
                return RedirectToAction(nameof(Index));
            }
            return View(planToUpdate);
        }

        //Post BaseURL/Plans/Edit/{Plan}
        //Edit  -- Save edits to the plan's data
        [HttpPost]
        public async Task<IActionResult> Edit(int id, PlanToUpdateViewModel model, CancellationToken ct)
        {
            if (!ModelState.IsValid)
                return View(model);
            var result = await planService.UpdatePlanAsync(id, model, ct);
            if (result)
            {
                TempData["SuccessMessage"] = "Plan updated successfully.";
                return RedirectToAction(nameof(Index));

            }
            else
            {
                TempData["ErrorMessage"] = "Failed to update the plan.";
                return View(model);
            }
        }
        #endregion
        
    }
}
