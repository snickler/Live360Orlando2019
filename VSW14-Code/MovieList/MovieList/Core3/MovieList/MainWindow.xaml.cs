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
using System.Windows.Navigation;
using System.Windows.Shapes;
using UWP;

namespace MovieList
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
       
            MovieViewModel.Instance.Movies = new System.Collections.ObjectModel.ObservableCollection<Models.Movie>();
        }

        private void Add_Movie()
        {
            var addMovie = new AddMovie();
            addMovie.Show();
        }

        private void btnAddMovie_ChildChanged(object sender, EventArgs e)
        {
            WindowsXamlHost windowsXamlHost = (WindowsXamlHost)sender;
            UWP.ButtonControl ctl =
       windowsXamlHost.Child as UWP.ButtonControl;
            if (ctl != null)
            {

                ctl.ButtonCommand = new SimpleCommand((obj) => Add_Movie(),(obj)=> true);
            
            }
        }

        
    }
}
