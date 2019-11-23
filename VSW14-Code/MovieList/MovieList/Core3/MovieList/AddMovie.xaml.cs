using Microsoft.Toolkit.Wpf.UI.XamlHost;
using MovieList.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MovieList
{
    /// <summary>
    /// Interaction logic for AddMovie.xaml
    /// </summary>
    public partial class AddMovie : Window
    {
        public AddMovie()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MovieViewModel.Instance.Movies.Add(new Models.Movie
            {
                Title = txtMovieTitle.Text,
                Genre = cboGenre.Text,
                Date = (dtDate.Child as Windows.UI.Xaml.Controls.DatePicker).SelectedDate.Value.Date,
                //     Rating = int.Parse(cboRating.Text)
                Rating = (int)(double)cboRating.Child.GetType().GetProperty("Value").GetValue(cboRating.Child)
            });

            this.Close();
        }
    }
}
