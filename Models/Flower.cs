namespace App.Flowershop.Items.Models
{
    public class Flower : ModelBase
    {
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string Category { get; set; }

        public decimal Price { get; set; }
    }
}
