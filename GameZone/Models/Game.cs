
namespace GameZone.Models
{
    public class Game:BaseEntity
    {
        [MaxLength(2500)]
        public string Description { get; set; }
        [MaxLength(500)]
        public string cover { get; set; }


        public int categoryId { get; set; }
        public Category category { get; set; }

        public ICollection<GamesDevice> Devices { get; set; } = new List<GamesDevice>();



    }
}
