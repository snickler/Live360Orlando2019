using MovieList.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MovieList.ViewModel
{
    public class MovieViewModel : INotifyPropertyChanged
    {
        private static readonly Lazy<MovieViewModel>
          _instance =new  Lazy<MovieViewModel>(() => new MovieViewModel());

        public static MovieViewModel Instance => _instance.Value;

        private ObservableCollection<Movie> _movies;
        public ObservableCollection<Movie> Movies
        {
            get => _movies;
            set => NotifyPropertyChanged(ref _movies, value);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged<T>(ref T obj, T value, [CallerMemberName] string name = null)
        {
            obj = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
