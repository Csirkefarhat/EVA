using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Model;

namespace Beadandó.ViewModels
{
    public partial class ObservableAsteroid : ViewModelBase

    {
        private Asteroid _asteroid;


        public int X
        {
            get => _asteroid.X;
            set
            {
                _asteroid.X = value;
                OnPropertyChanged(nameof(X));
            }
        }

        public int Y
        {
            get => _asteroid.Y;
            set
            {
                _asteroid.Y = value;
                OnPropertyChanged(nameof(Y));
            }
        }

        public ObservableAsteroid(Asteroid a) => _asteroid = a;
    }
}
