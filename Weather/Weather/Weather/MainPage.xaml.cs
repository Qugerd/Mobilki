using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using Xamarin.Essentials;
using System.Threading;

namespace Weather
{
    public partial class MainPage : ContentPage
    {

        public string Location { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public string cityName = "Moscow";

        
        public MainPage()
        {
            InitializeComponent();
            GetCurrentLocation();
            GetWeatherInfo(cityName);            
        }

        private void GetWeatherInfo(string cityName)
        {
            string url = $"http://api.openweathermap.org/data/2.5/weather?q={cityName}&units=metric&appid=6e3c7091a8b557bb4861808f01c09db6";

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            string response;

            using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                response = streamReader.ReadToEnd();
            }

            WeatherJson weatherJason = JsonConvert.DeserializeObject<WeatherJson>(response);

            LabelTemp.Text = weatherJason.Main.temp.ToString("0") + "°C";
            LabelCity.Text = weatherJason.Name;
            LabelYasno.Text = weatherJason.weather[0].description;
            LabelFeelsLike.Text = "Ощущается как: " + weatherJason.Main.feels_like.ToString("0");
            labelPressureValue.Text = weatherJason.Main.pressure.ToString() + "гПа";
            LabelHumidityValue.Text = weatherJason.Main.humidity.ToString() + "%";
            LabelWindValue.Text = weatherJason.wind.speed.ToString("0") + "м/с";
            LabelCloudsValue.Text = weatherJason.clouds.all.ToString() + "%";
            LabelSunriseValue.Text = UnixTimeToDateTime(weatherJason.sys.sunrise).ToString("t", System.Globalization.CultureInfo.CreateSpecificCulture("sv-FI"));
            LabelSunsetValue.Text = UnixTimeToDateTime(weatherJason.sys.sunset).ToString("t", System.Globalization.CultureInfo.CreateSpecificCulture("sv-FI"));
        }
        
        CancellationTokenSource cts;

        async Task GetCurrentLocation()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                cts = new CancellationTokenSource();
                var location = await Geolocation.GetLocationAsync(request, cts.Token);

                if (location != null)
                {
                    string url = $"http://api.openweathermap.org/data/2.5/weather?lat={location.Latitude}&lon={location.Longitude}&units=metric&appid=6e3c7091a8b557bb4861808f01c09db6";
                    HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                    HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                    string response;

                    using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                    {
                        response = streamReader.ReadToEnd();
                    }
                    WeatherJson kek = JsonConvert.DeserializeObject<WeatherJson>(response);

                    LabelTemp.Text = kek.Main.temp.ToString("0") + "°C";
                    LabelCity.Text = kek.Name;
                    LabelYasno.Text = kek.weather[0].description;
                    LabelFeelsLike.Text = "Ощущается как: " + kek.Main.feels_like.ToString("0");
                    labelPressureValue.Text = kek.Main.pressure.ToString() + "гПа";
                    LabelHumidityValue.Text = kek.Main.humidity.ToString() + "%";
                    LabelWindValue.Text = kek.wind.speed.ToString("0") + "м/с";
                    LabelCloudsValue.Text = kek.clouds.all.ToString() + "%";
                    LabelSunriseValue.Text = UnixTimeToDateTime(kek.sys.sunrise).ToString("t", System.Globalization.CultureInfo.CreateSpecificCulture("sv-FI"));
                    LabelSunsetValue.Text = UnixTimeToDateTime(kek.sys.sunset).ToString("t", System.Globalization.CultureInfo.CreateSpecificCulture("sv-FI"));

                    await DisplayAlert("Ваше местоположение", $"Геолокация определила что ваш город {kek.Name}?", "Понял") ;
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                }                
            }            
            catch 
            {
                await DisplayAlert("Ошибочка", "Не удалось разгодать ваше место положение", "Понял - принял");
            }
        }

        private async void ButtonCheck_Clicked(object sender, EventArgs e)
        {
            try
            {
                string cityName = Editor.Text;
                GetWeatherInfo(cityName);
            }
            catch 
            {
                await DisplayAlert("Что - то пошло не так ...", "Введите правильное название города", "Повторить");
            }
            
        }

        private DateTime UnixTimeToDateTime(double UnixTime)
        {
            DateTime origin = new DateTime(1970, 1, 1, 6, 0, 0);
            return origin.AddSeconds(UnixTime);
        }

        private async void OnEditorCompleted(object sender, EventArgs e)
        {
            try
            {
                string text = Editor.Text;
                string cityName = text;
                GetWeatherInfo(cityName);
            }
            catch 
            {
                await DisplayAlert("Что - то пошло не так ...", "Введите правильное название города", "Повторить");
            }
        }
    }
}
