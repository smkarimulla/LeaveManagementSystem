using LeaveManagementSystem.Web.Services.Periods;

namespace LeaveManagementSystem.Web.Controllers;

[Authorize(Roles =Roles.Administrator)]
public class PeriodsController(IPeriodsService periodsService) : Controller
{    
    private readonly IPeriodsService _periodsService = periodsService;
    public static string NameExistsValidationMessage = " period exists in the database";

    // GET: Periods
    public async Task<IActionResult> Index()
    {       
        var viewData =  await _periodsService.GetAll();
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

        var period = await _periodsService.Get<PeriodReadOnlyVM>(id.Value);
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
    public async Task<IActionResult> Create(PeriodCreateVM periodCreateVM)
    {
        if (await _periodsService.CheckIfPeriodExists(periodCreateVM.Name))
        {
            ModelState.AddModelError(nameof(periodCreateVM.Name), NameExistsValidationMessage);
        }
        if (ModelState.IsValid)
        {
            await _periodsService.Create(periodCreateVM);
            return RedirectToAction(nameof(Index));
        }
        return View(periodCreateVM);
    }

    // GET: Periods/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        
        var viewPeriod = await _periodsService.Get<PeriodEditVM>(id.Value);

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
    public async Task<IActionResult> Edit(int id, PeriodEditVM periodEditVM)
    {
        if (id != periodEditVM.Id)
        {
            return NotFound();
        }

        if(await _periodsService.CheckIfPeriodExistsForEdit(periodEditVM))
        {
            ModelState.AddModelError(nameof(periodEditVM.Name), NameExistsValidationMessage);
        }

        if (ModelState.IsValid)
        {
            try
            {
               await _periodsService.Edit(periodEditVM);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_periodsService.PeriodExists(periodEditVM.Id))
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
        return View(periodEditVM);
    }

    // GET: Periods/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }


        var viewPeriod = await _periodsService.Get<PeriodReadOnlyVM>(id.Value);
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
