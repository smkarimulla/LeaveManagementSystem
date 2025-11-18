using LeaveManagementSystem.Application.Services.Periods;
using LeaveManagementSystem.Common.Static;

namespace LeaveManagementSystem.Web.Controllers;

[Authorize(Roles = Roles.Administrator)]
public class PeriodsController(IPeriodsService periodsService) : Controller
{
    private readonly IPeriodsService _periodsService = periodsService;
    public static string NameExistsValidationMessage = " period exists in the database";

    // GET: Periods
    public async Task<IActionResult> Index()
    {
        var viewData = await _periodsService.GetAll();
        if (viewData == null)
        {
            return null;
        }
        return View(viewData);
    }

    // GET: Periods/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var period = await _periodsService.Get<PeriodVM>(id.Value);
        return View(period);
    }

    // GET: Periods/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Periods/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    //public async Task<IActionResult> Create([Bind("Name,StartDate,EndDate,Id")] Period period)
    public async Task<IActionResult> Create(PeriodVM periodVM)
    {
        if (await _periodsService.CheckIfPeriodExists(periodVM.Name))
        {
            ModelState.AddModelError(nameof(periodVM.Name), NameExistsValidationMessage);
        }
        if (ModelState.IsValid)
        {
            await _periodsService.Create(periodVM);
            return RedirectToAction(nameof(Index));
        }
        return View(periodVM);
    }

    // GET: Periods/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var viewPeriod = await _periodsService.Get<PeriodVM>(id.Value);

        if (viewPeriod == null)
        {
            return NotFound();
        }
        return View(viewPeriod);
    }

    // POST: Periods/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, PeriodVM periodVM)
    {
        if (id != periodVM.Id)
        {
            return NotFound();
        }

        if (await _periodsService.CheckIfPeriodExistsForEdit(periodVM))
        {
            ModelState.AddModelError(nameof(periodVM.Name), NameExistsValidationMessage);
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _periodsService.Edit(periodVM);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_periodsService.PeriodExists(periodVM.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(periodVM);
    }

    // GET: Periods/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }


        var viewPeriod = await _periodsService.Get<PeriodVM>(id.Value);
        if (viewPeriod == null)
        {
            return NotFound();
        }

        return View(viewPeriod);
    }

    // POST: Periods/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _periodsService.Delete(id);
        return RedirectToAction(nameof(Index));
    }


}
