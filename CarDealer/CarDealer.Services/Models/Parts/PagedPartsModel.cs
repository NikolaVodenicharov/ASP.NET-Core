namespace CarDealer.Services.Models.Parts
{
    using System.Collections.Generic;

    public class PagedPartsModel
    {
        public int PreviousPage =>
            this.CurrentPage == 1 ? 1 : this.CurrentPage - 1;

        public int CurrentPage { get; set; }

        public int NextPage =>
            this.CurrentPage == this.TotalPages ? this.CurrentPage : this.CurrentPage + 1;

        public int TotalPages { get; set; }

        public IEnumerable<PartModel> Parts { get; set; }
    }
}
