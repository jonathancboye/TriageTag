using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;
using SQLite.Net;
using Xamarin.Forms;

namespace Callbacks
{
    public class Stock
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        [Indexed]
        public string stock { get; set; }
        public double cost { get; set; }
    }


    public partial class CallbackPage : ContentPage
    {
        SQLiteConnection DBConnection;

        public CallbackPage() {
            InitializeComponent();
        }

        async private void createDatabase() {
            DBConnection = await DependencyService.Get<ISQLite>().GetConnection();
            label.Text = "Database connection made :)";
            DBConnection.CreateTable<Stock>();
        }

        public void addStock() {
            var newStock = new Stock {
                stock = "Alphabet Inc",
                cost = 781.10
            };

            DBConnection.Insert( newStock );
        }

        public void displayStocks() {
            List<Stock> stocks = DBConnection.Query<Stock>( "select * from Stock" );
            foreach(Stock stock in stocks ) {
                scrollingStack.Children.Add(
                    new Label {
                        Text = stock.id.ToString()
                    } );
                scrollingStack.Children.Add(
                    new Label {
                        Text = stock.stock
                    } );
                scrollingStack.Children.Add(
                    new Label {
                        Text = stock.cost.ToString()
                    } );
            }
        }

        async private void OnButtonClicked( object sender, EventArgs e ) {
            Task<bool> task = DisplayAlert("This is an alert", "Click these buttons",
                "yes", "no");
            bool result = await task.ConfigureAwait(true);
            label.Text = string.Format( "The result is {0}", result.ToString() );
            createDatabase();
        }

        private void OnInsertButtonClicked( object sender, EventArgs e ) {
            addStock();
            displayStocks();
        }
    }
}
