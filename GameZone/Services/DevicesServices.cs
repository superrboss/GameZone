
using System.Diagnostics.Metrics;

namespace GameZone.Services
{
    public class DevicesServices : IDevicesServices
    {
        private readonly ApplicationDbContext context;

        public DevicesServices(ApplicationDbContext _context)
        {
            context = _context;
        }
        public IEnumerable<SelectListItem> DevicesList()
        {
            return context.Devices.Select(d => new SelectListItem
            { Value = d.Id.ToString(), Text = d.Name })
                .AsNoTracking()
                .OrderBy(o => o.Text).ToList();
        }
    }
}
