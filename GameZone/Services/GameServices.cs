namespace GameZone.Services
{
    public class GameServices : IGameServices
    {
        private readonly IWebHostEnvironment webhostEnv;
        private readonly ApplicationDbContext context;
        public string imagPath;


        public GameServices(IWebHostEnvironment _webhostEnv, ApplicationDbContext _context)
        {
            webhostEnv = _webhostEnv;
            context = _context;
            imagPath = $"{webhostEnv.WebRootPath}/Assests/Images/Game";

        }
        public IEnumerable<Game> GetAllGames()
        {
            return context.Games.Include(g => g.category).
                Include(d => d.Devices).ThenInclude(d => d.Device).
                AsNoTracking().
                ToList();
        }
        public Game? GetGameById(int id)
        {
            return context.Games.
                Include(c => c.category).
                Include(d => d.Devices).
                ThenInclude(d => d.Device).AsNoTracking().
                SingleOrDefault(g => g.Id == id);
        }
        public async Task create(CreateGameFormViewModel model)
        {
            var CoverName = await SaveFile(model.cover);
            //stream.Dispose();
            Game game = new()
            {
                Name = model.Name,
                Description = model.Description,
                categoryId = model.CategoryId,
                cover = CoverName,
                Devices = model.SelectedDevices.Select(d => new GamesDevice { DeviceId = d }).ToList()
            };
            context.Games.Add(game);
            context.SaveChanges();
        }

        public async Task<Game?> Edit(EditFormViewModel model)
        {
            var game = context.Games.Include(d=>d.Devices).FirstOrDefault(g=>g.Id==model.Id);
            if (game == null)
            {
                return null;
            }
            var oldcover=game.cover;
            game.Name = model.Name;
            game.Description = model.Description;
            game.categoryId = model.CategoryId;
            game.Devices = model.SelectedDevices.Select(d => new GamesDevice { DeviceId = d }).ToList();

            var hasCover = model.cover is not null;
            if (hasCover)
            {
                game.cover = await SaveFile(model.cover!);
            }
            var effectedRows = context.SaveChanges();
            if(effectedRows>0)
            {
                if (hasCover) {
                    var cover = Path.Combine(imagPath, oldcover);
                    File.Delete(cover);
                }
                return game;
            }
            else
            {
                var cover = Path.Combine(imagPath, game.cover);
                File.Delete(cover);
                return null;
            }


        } 


        //method to upload file
        private async Task<string> SaveFile(IFormFile cover)
        {
            var CoverName = $"{Guid.NewGuid()}{Path.GetExtension(cover.FileName)}";
            var path = Path.Combine(imagPath, CoverName);
            using var stream = File.Create(path);
            await cover.CopyToAsync(stream);
            return CoverName;
        }

        public bool Delete(int id)
        {
            var valueReturn = false;
            var game = context.Games.Find(id);
            if(game == null)
            {
                return false;
            }
            context.Games.Remove(game);
            var effectRows= context.SaveChanges();  
            if(effectRows > 0)
            {
                var coverDeleted=Path.Combine(imagPath,game.cover);
                File.Delete(coverDeleted);
                valueReturn = true;
            }

            return valueReturn;
        }
    }
}
