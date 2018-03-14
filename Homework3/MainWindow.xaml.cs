using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Homework3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //To track the column selected for sorting
        private GridViewColumnHeader sortColumn = null;
        private ListSortDirection currentDirection;
        private string currentSortBy;

        public MainWindow()
        {
            InitializeComponent();

            //Populate the users list
            var users = new List<Models.User>
            {
                new Models.User { Name = "Dave", Password = "1DavePwd" },
                new Models.User { Name = "Steve", Password = "2StevePwd" },
                new Models.User { Name = "Lisa", Password = "3LisaPwd" }
            };

            //Set the data source for the usList ListView.ItemsSource
            uxList.ItemsSource = users;

            //Set the default sort order to the Name property of the User class (taken from uxList.ItemsSource)
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(uxList.ItemsSource);
            view.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));

        }

        private void uxListColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            //Get a reference to the column header that was clicked
            if (sender is GridViewColumnHeader column)
            {
                string sortBy = column.Tag.ToString(); //Tag is an object, so convert to string for use with the first parameter of SortDescription 

                if (sortColumn != null) //If a sortColumn has been selected...
                {
                    //In this particular case, we only have one SortDescription - so what is it?
                    currentDirection = uxList.Items.SortDescriptions[0].Direction;
                    currentSortBy = uxList.Items.SortDescriptions[0].PropertyName;

                    uxList.Items.SortDescriptions.Clear();  //Remove the existing SortDescriptions
                }

                ListSortDirection newDirection = ListSortDirection.Ascending;

                if (sortColumn == column && currentDirection == newDirection) newDirection = ListSortDirection.Descending;
                
                sortColumn = column;

                uxList.Items.SortDescriptions.Add(new SortDescription(sortBy, newDirection));
            }
        }
    }
}
