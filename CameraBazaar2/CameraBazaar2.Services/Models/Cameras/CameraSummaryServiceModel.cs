using CameraBazaar2.Data.Enums;

namespace CameraBazaar2.Services.Models.Cameras
{
    public class CameraSummaryServiceModel
    {
        public int Id { get; set; }

        public CameraMake Make { get; set; }

        public string Model { get; set; }

        public decimal Price { get; set; }
        
        public bool IsInStock { get; set; }

        public string ImageUrl { get; set; }
    }
}
