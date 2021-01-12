using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace delivery
{
    public partial class MainPage : ContentPage
    {
        public List<Water> Drinks { get; set; }

        public MainPage()
        {
            InitializeComponent();

            Drinks = new List<Water>();
            Drinks.Add(new Water
            {
                Name = "Bonaqua",
                Location = "Описание",
                ImageUrl = "wa.jpg"
            });

            Drinks.Add(new Water
            {
                Name = "Cocola",
                Location = "Описание",
                ImageUrl = "wa.jpg"
            });

            Drinks.Add(new Water
            {
                Name = "Pipsi",
                Location = "Описание",
                ImageUrl = "wa.jpg"
            });

            Drinks.Add(new Water
            {
                Name = "Monster",
                Location = "Энергетический напиток 0,5",
                ImageUrl = "monster.jpg"
            });

            Drinks.Add(new Water
            {
                Name = "Kvas",
                Location = "Описание",
                ImageUrl = "wa.jpg"
            });

            Drinks.Add(new Water
            {
                Name = "Monostirskaya",
                Location = "Описание Описание",
                ImageUrl = "wa.jpg"
            });

            Drinks.Add(new Water
            {
                Name = "Slavda",
                Location = "Описание",
                ImageUrl = "wa.jpg"
            });

            BindingContext = this;
        }

        public class Water
        {
            public string Name { get; set; }
            public string Location { get; set; }
            public string ImageUrl { get; set; }
        }

        void buttonOrder_clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            button.IsEnabled = false;

            var cart = new Cart();
            Navigation.PushAsync(cart);
            cart.Disappearing += (send, ev) => button.IsEnabled = true;
        }

        async void buttonAdd_clicked(object sender, EventArgs e)
        {
            var stepper = sender as Stepper;
            var button = sender as Button;
            try
            {
                string result = await DisplayPromptAsync("Выберите кол-во", "", initialValue: "1", keyboard: Keyboard.Numeric);
                if (int.Parse(result) > 0)
                {
                    Cart.product[button.CommandParameter.ToString()] = new Cart.Product()
                    {
                        Name = button.CommandParameter.ToString(),
                        ImageUrl = Drinks.Find(x => x.Name.Contains(button.CommandParameter.ToString())).ImageUrl,
                        Count = int.Parse(result).ToString(),
                    };
                }
            }
            catch (Exception) { }            
        }
    }
}