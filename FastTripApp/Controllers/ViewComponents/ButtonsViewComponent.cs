using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastTripApp.Web.Controllers.ViewComponents
{
    public class ButtonsViewComponent : ViewComponent
    {

        public async Task<IViewComponentResult> InvokeAsync()
        {
            //MyContext context = new MyContext(db);
            //IEnumerable<tableRowClass> mc = await context.tableRows.ToListAsync();
            return View();
        }
    }
}
