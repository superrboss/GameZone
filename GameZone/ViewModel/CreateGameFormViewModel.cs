using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZone.ViewModel
{
    public class CreateGameFormViewModel
    {

        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;
        [Display(Name = "Description")]
        [MaxLength(2500)]
        public string Description { get; set; } = string.Empty;
        public IFormFile cover { get; set; } = default!;

        [Display(Name = "Category")]

        public int CategoryId { get; set; }
        //only To Send
        public IEnumerable<SelectListItem> Categories { get; set; } = Enumerable.Empty<SelectListItem>();
        //only To Send

        public IEnumerable<SelectListItem> Devices { get; set; } = Enumerable.Empty<SelectListItem>();
        [Display(Name = "Devices Support")]
        //only To Get Id in list 

        public List<int> SelectedDevices { get; set; } = new List<int>();
        // when get this list we will convert it to Enumerable to added in DB 

    }
}
