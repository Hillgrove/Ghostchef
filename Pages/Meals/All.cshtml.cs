using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Ghostchef.Models;
using Ghostchef.Services;

namespace Ghostchef.Pages.Meals
{
    public class AllModel : PageModel
    {
        private IMealDataService _productDataService;
        
        public List<Meal> Data { get; private set; }

        public AllModel(IMealDataService productDataService)
        {
            _productDataService = productDataService;
        }

        public void OnGet()
        {
            Data = _productDataService.GetAll();
        }
    }
}
