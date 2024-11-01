using System.Diagnostics.Metrics;

namespace GameZone.Services
{
    public class CategoryServices: ICategoryServices
    {
        private readonly ApplicationDbContext context;

        public CategoryServices(ApplicationDbContext _context)
        {
            context = _context;
        }
        public IEnumerable<SelectListItem> CategoryList()
        {
            return context.Categories.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }).OrderBy(c => c.Text).ToList();
        }
    }
}
