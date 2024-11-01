using GameZone.Models;
using GameZone.Services;
namespace GameZone.Controllers
{
    public class GamesController : Controller
    {
        private readonly ICategoryServices CategoryServices;
        private readonly IDevicesServices DevicesServices;
        private readonly IGameServices GameServices;

        public GamesController(ICategoryServices _categoryServices, IDevicesServices _devicesServices, IGameServices _GameServices)
        {
            DevicesServices = _devicesServices;
            CategoryServices = _categoryServices;
            GameServices = _GameServices;
        }

        public IActionResult Index()
        {

            return View(GameServices.GetAllGames());
        }
        public IActionResult Details(int id)
        {
            var game = GameServices.GetGameById(id);
            if (game == null)
                return NotFound();

            return View(game);
        }
        //Get Edit



        public IActionResult Create()
        {
            CreateGameFormViewModel SendModel = new()
            {
                Categories = CategoryServices.CategoryList(),
                Devices = DevicesServices.DevicesList(),
            };
            return View(SendModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGameFormViewModel Model)
        {
            Model.Categories = CategoryServices.CategoryList();
            Model.Devices = DevicesServices.DevicesList();

            if (!ModelState.IsValid)
            {
                return View(Model);
            }
            await GameServices.create(Model);
            return RedirectToAction("Index");
        }






        //*******************
        public IActionResult Edit(int id)
        {

            var game = GameServices.GetGameById(id);
            if (game == null)
                return NotFound();
            EditFormViewModel modelsend = new()
            {
                Id = id,
                Name = game.Name,
                Description = game.Description,
                SelectedDevices = game.Devices.Select(d => d.DeviceId).ToList(),
                Categories = CategoryServices.CategoryList(),
                CategoryId = game.categoryId,
                Devices = DevicesServices.DevicesList(),
                CurrentCover = game.cover,


            };

            return View(modelsend);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = CategoryServices.CategoryList();
                model.Devices = DevicesServices.DevicesList();
                return View(model);
            }
            var game = await GameServices.Edit(model);
            if (game == null) { return BadRequest(); }


            return RedirectToAction("Index");
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {

            var isDeleted = GameServices.Delete(id);

            return isDeleted ? Ok() : BadRequest();
        }
    }
}
