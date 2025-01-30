using Aszteroidák.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aszteroidak_WPF.ViewModel
{
    public class ObservableAsteroid : ViewModelBase
    {
        private Asteroid _asteroid;


        public int X
        {
            get => _asteroid.X;
            set
            {
                _asteroid.X = value;
                OnPropertyChanged();
            }
        }

        public int Y
        {
            get => _asteroid.Y;
            set
            {
                _asteroid.Y = value;
                OnPropertyChanged();
            }
        }

        public ObservableAsteroid(Asteroid a) { _asteroid = a; }
    }
}
