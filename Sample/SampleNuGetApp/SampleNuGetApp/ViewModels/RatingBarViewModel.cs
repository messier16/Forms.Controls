using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.ViewModels
{
    public class RatingBarViewModel : ViewModelBase
    {

        private int _beerRating;
        public int BeerRating
        {
            get { return _beerRating; }
            set { _beerRating = value; OnPropertyChanged(); }
        }

        private int _rating;
        public int Rating
        {
            get { return _rating; }
            set { _rating = value; OnPropertyChanged(); }
        }
    }
}
