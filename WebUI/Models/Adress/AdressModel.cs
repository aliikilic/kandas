using Entities.Models;

namespace WebUI.Models.Adress
{
    public class AdressModel
    {
        public List<GetCitiesDto> Cities { get; set; }
        public List<GetDistrictsDto> Districts { get; set; }
        public List<GetNeighborhoodsDto> Neighborhoods { get; set; }
    }
}
