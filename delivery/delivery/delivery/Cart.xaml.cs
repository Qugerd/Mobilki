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

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Стоп", "Вы уверены?", "Да", "Нет"))
            {
                if (product.Count() == 0)
                {
                    await DisplayAlert("Stop", "Корзина пустая", "Ok");
                }

                else
                {
                    await DisplayAlert("Ура", "Вы оформили заказ!", "Ok");
                    await Navigation.PopAsync();
                    product.Clear();
                }
            }
        }

        async void buttonMinus_clicked(object sender, EventArgs e)
        {            
            var button = sender as Button;
            int k = int.Parse(product[button.CommandParameter.ToString()].Count);

            if (k == 1)
            {
                if (await DisplayAlert("Стоп", "Вы хотите убрать этот продукт из корзины?", "Да", "Нет"))
                {
                    var item = (Xamarin.Forms.Button)sender;
                    var listitem = (from itm in product
                                    where itm.Value.Name == item.CommandParameter.ToString()
                                    select itm).ToList().First();


                    product.Remove(listitem.Key);
                }                
            }
            else
            {
                product[button.CommandParameter.ToString()].Count = (--k).ToString();
            }
            goods_list.ItemsSource = product.Select((a) => { return a.Value; }).ToList();            
        }

        void buttonPlus_clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            int k = int.Parse(product[button.CommandParameter.ToString()].Count);
            product[button.CommandParameter.ToString()].Count = (++k).ToString();
            goods_list.ItemsSource = product.Select((a) => { return a.Value; }).ToList();
        }
    }
}