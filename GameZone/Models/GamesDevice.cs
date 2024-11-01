namespace GameZone.Models
{
    public class GamesDevice
    {
        public int GameId { get; set; }
        public Game Game{ get; set; }
        public int  DeviceId { get; set; }
        public Device Device { get; set; }
    }
}
