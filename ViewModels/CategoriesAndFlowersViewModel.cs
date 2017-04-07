using App.Flowershop.Items.Models;
using System.Collections.Generic;

namespace App.Flowershop.Items.ViewModels
{
    public class CategoriesViewModel
    {
        public List<Category> Categories { get; set; }

        private List<Flower> flowerList;
        public List<Flower> FlowerList
        {
            get
            {
                return flowerList;
            }

            set
            {
                if (value == null)
                    flowerList = new List<Flower>();
                else
                    flowerList = value;

            }
        }

        public string SectionCartSummary { get; set; }

        public string SectionCartCheckout { get; set; }

        public CategoriesViewModel()
        {
            Categories = new List<Category>();
            FlowerList = new List<Flower>();
        }
    }
}
