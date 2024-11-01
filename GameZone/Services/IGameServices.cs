namespace GameZone.Services
{
    public interface IGameServices
    {
        IEnumerable<Game> GetAllGames();
        Game? GetGameById(int id);
        Task create(CreateGameFormViewModel model);
        Task<Game?> Edit (EditFormViewModel model);

        bool Delete(int id) ;
    }
}
