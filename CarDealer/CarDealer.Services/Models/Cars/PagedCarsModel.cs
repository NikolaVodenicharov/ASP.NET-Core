namespace CarDealer.Services.Models.Cars
{
    using System.Collections.Generic;

    public class PagedCarsModel
    {
        public int PreviousPage =>
            (this.CurrentPage - 1) > 0 ? (this.CurrentPage - 1) : 1;

        public int CurrentPage { get; set; }

        public int NextPage =>
            (this.CurrentPage + 1) < this.TotalPage ? (this.CurrentPage + 1) : this.CurrentPage;

        public int TotalPage { get; set; }

        public IEnumerable<CarModel> Cars { get; set; }
    }
}
