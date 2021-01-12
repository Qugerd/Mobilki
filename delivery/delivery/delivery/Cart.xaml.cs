using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace delivery
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Cart : ContentPage
    {

        public static Dictionary<string,Product>product = new Dictionary<string, Product>();
        
        public class Product
        {
            public string Name { get; set; }
            public string ImageUrl { get; set; }
            public string Count { get; set; }
        }
        public Cart()
        {
            InitializeComponent();

            goods_list.ItemsSource = product.Select((a) => { return a.Value; }).ToList();
        }

        void buttonDelete_clicked(object sender, EventArgs e)
        {
            var item = (Xamarin.Forms.Button)sender;
            var listitem = (from itm in product
                            where itm.Value.Name == item.CommandParameter.ToString()
                            select itm).ToList().First();


            product.Remove(listitem.Key);

            goods_list.ItemsSource = product.Select((a) => { return a.Value; }).ToList();
        }
    }
}