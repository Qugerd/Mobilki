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
        WeatherJson weatherJason;
        WeatherJasonOneCall weatherJsonOneCall;
        CancellationTokenSource cts;

        public MainPage()
        {
            InitializeComponent();
            InternetInfo();
        }

        private async void InternetInfo()
        {
            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
            {
                GetCurrentLocation();
                GetWeatherInfo($"http://api.openweathermap.org/data/2.5/weather?q={cityName}&units=metric&lang=ru&appid=6e3c7091a8b557bb4861808f01c09db6");
            }
            else
            {
                await DisplayAlert("Ошибка", "Нет доступа к интенету", "Понял");
            }
        }

        private string GetWeatherInfo(string url)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            string response;

            using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                response = streamReader.ReadToEnd();
            }          

            return response;
        }

        private void Kek()
        {
            //current inf
            LabelTemp.Text = weatherJason.Main.temp.ToString("0") + "°C";
            LabelCity.Text = weatherJason.Name;
            LabelYasno.Text = weatherJsonOneCall.daily[0].weather[0].description; 
            LabelFeelsLike.Text = "Ощущается как: " + weatherJason.Main.feels_like.ToString("0");
            labelPressureValue.Text = weatherJason.Main.pressure.ToString() + "гПа";
            LabelHumidityValue.Text = weatherJason.Main.humidity.ToString() + "%";
            LabelWindValue.Text = weatherJason.wind.speed.ToString("0") + "м/с";
            LabelCloudsValue.Text = weatherJason.clouds.all.ToString() + "%";
            ImagePath.Source = $"https://openweathermap.org/img/wn/{weatherJsonOneCall.daily[0].weather[0].icon}@2x.png";
            LabelSunriseValue.Text = UnixTimeToDateTime(weatherJason.sys.sunrise).ToString("t", System.Globalization.CultureInfo.CreateSpecificCulture("sv-FI"));
            LabelSunsetValue.Text = UnixTimeToDateTime(weatherJason.sys.sunset).ToString("t", System.Globalization.CultureInfo.CreateSpecificCulture("sv-FI"));          
            labelDayNightValue.Text = $"{weatherJsonOneCall.daily[0].temp.day.ToString("0")}/{weatherJsonOneCall.daily[0].temp.eve.ToString("0")}";            
         
            int i = 0;
            labelDescription0.Text = weatherJsonOneCall.daily[i].weather[0].description;
            labelDayNight0.Text = $"{weatherJsonOneCall.daily[i].temp.day.ToString("0")}/{weatherJsonOneCall.daily[i].temp.eve.ToString("0")}°C";
            image0.Source = $"https://openweathermap.org/img/wn/{weatherJsonOneCall.daily[i].weather[0].icon}@2x.png";
            //
            labelDay_1.Text = UnixTimeToDateTime(weatherJsonOneCall.daily[++i].dt).ToString("ddd", System.Globalization.CultureInfo.CreateSpecificCulture("ru-RU"));
            labelDescription_1.Text = weatherJsonOneCall.daily[i].weather[0].description;
            image_1.Source = $"https://openweathermap.org/img/wn/{weatherJsonOneCall.daily[i].weather[0].icon}@2x.png";
            labelDayNight_1.Text = $"{weatherJsonOneCall.daily[i].temp.day.ToString("0")}/{weatherJsonOneCall.daily[i].temp.eve.ToString("0")}°C";
            //
            labelDay_2.Text = UnixTimeToDateTime(weatherJsonOneCall.daily[++i].dt).ToString("ddd", System.Globalization.CultureInfo.CreateSpecificCulture("ru-RU"));
            labelDescription_2.Text = weatherJsonOneCall.daily[i].weather[0].description;
            image_2.Source = $"https://openweathermap.org/img/wn/{weatherJsonOneCall.daily[i].weather[0].icon}@2x.png";
            labelDayNight_2.Text = $"{weatherJsonOneCall.daily[i].temp.day.ToString("0")}/{weatherJsonOneCall.daily[i].temp.eve.ToString("0")}°C";
            //
            labelDay_3.Text = UnixTimeToDateTime(weatherJsonOneCall.daily[++i].dt).ToString("ddd", System.Globalization.CultureInfo.CreateSpecificCulture("ru-RU"));
            labelDescription_3.Text = weatherJsonOneCall.daily[i].weather[0].description;
            image_3.Source = $"https://openweathermap.org/img/wn/{weatherJsonOneCall.daily[i].weather[0].icon}@2x.png";
            labelDayNight_3.Text = $"{weatherJsonOneCall.daily[i].temp.day.ToString("0")}/{weatherJsonOneCall.daily[i].temp.eve.ToString("0")}°C";
             //
            labelDay_4.Text = UnixTimeToDateTime(weatherJsonOneCall.daily[++i].dt).ToString("ddd", System.Globalization.CultureInfo.CreateSpecificCulture("ru-RU"));
            labelDescription_4.Text = weatherJsonOneCall.daily[i].weather[0].description;
            image_4.Source = $"https://openweathermap.org/img/wn/{weatherJsonOneCall.daily[i].weather[0].icon}@2x.png";
            labelDayNight_4.Text = $"{weatherJsonOneCall.daily[i].temp.day.ToString("0")}/{weatherJsonOneCall.daily[i].temp.eve.ToString("0")}°C";
             //
            labelDay_5.Text = UnixTimeToDateTime(weatherJsonOneCall.daily[++i].dt).ToString("ddd", System.Globalization.CultureInfo.CreateSpecificCulture("ru-RU"));
            labelDescription_5.Text = weatherJsonOneCall.daily[i].weather[0].description;
            image_5.Source = $"https://openweathermap.org/img/wn/{weatherJsonOneCall.daily[i].weather[0].icon}@2x.png";
            labelDayNight_5.Text = $"{weatherJsonOneCall.daily[i].temp.day.ToString("0")}/{weatherJsonOneCall.daily[i].temp.eve.ToString("0")}°C";
             //
            labelDay_6.Text = UnixTimeToDateTime(weatherJsonOneCall.daily[++i].dt).ToString("ddd", System.Globalization.CultureInfo.CreateSpecificCulture("ru-RU"));
            labelDescription_6.Text = weatherJsonOneCall.daily[i].weather[0].description;
            image_6.Source = $"https://openweathermap.org/img/wn/{weatherJsonOneCall.daily[i].weather[0].icon}@2x.png";
            labelDayNight_6.Text = $"{weatherJsonOneCall.daily[i].temp.day.ToString("0")}/{weatherJsonOneCall.daily[i].temp.eve.ToString("0")}°C";

            int j = 0;
            label__0.Text = UnixTimeToDateTime(weatherJsonOneCall.hourly[j].dt).ToString("t", System.Globalization.CultureInfo.CreateSpecificCulture("sv-FI"));
            labelTemp__0.Text = weatherJsonOneCall.hourly[j].temp.ToString("0");
            image__0.Source = $"https://openweathermap.org/img/wn/{weatherJsonOneCall.hourly[j].weather[0].icon}@2x.png";

            j = j + 3;
            label__1.Text = UnixTimeToDateTime(weatherJsonOneCall.hourly[j].dt).ToString("t", System.Globalization.CultureInfo.CreateSpecificCulture("sv-FI"));
            labelTemp__1.Text = weatherJsonOneCall.hourly[j].temp.ToString("0");
            image__1.Source = $"https://openweathermap.org/img/wn/{weatherJsonOneCall.hourly[j].weather[0].icon}@2x.png";

            j = j + 3;
            label__2.Text = UnixTimeToDateTime(weatherJsonOneCall.hourly[j].dt).ToString("t", System.Globalization.CultureInfo.CreateSpecificCulture("sv-FI"));
            labelTemp__2.Text = weatherJsonOneCall.hourly[j].temp.ToString("0");
            image__2.Source = $"https://openweathermap.org/img/wn/{weatherJsonOneCall.hourly[j].weather[0].icon}@2x.png";

            j = j + 3;
            label__3.Text = UnixTimeToDateTime(weatherJsonOneCall.hourly[j].dt).ToString("t", System.Globalization.CultureInfo.CreateSpecificCulture("sv-FI"));
            labelTemp__3.Text = weatherJsonOneCall.hourly[j].temp.ToString("0");
            image__3.Source = $"https://openweathermap.org/img/wn/{weatherJsonOneCall.hourly[j].weather[0].icon}@2x.png";

            j = j + 3;
            label__4.Text = UnixTimeToDateTime(weatherJsonOneCall.hourly[j].dt).ToString("t", System.Globalization.CultureInfo.CreateSpecificCulture("sv-FI"));
            labelTemp__4.Text = weatherJsonOneCall.hourly[j].temp.ToString("0");
            image__4.Source = $"https://openweathermap.org/img/wn/{weatherJsonOneCall.hourly[j].weather[0].icon}@2x.png";

            j = j + 3;
            label__5.Text = UnixTimeToDateTime(weatherJsonOneCall.hourly[j].dt).ToString("t", System.Globalization.CultureInfo.CreateSpecificCulture("sv-FI"));
            labelTemp__5.Text = weatherJsonOneCall.hourly[j].temp.ToString("0");
            image__5.Source = $"https://openweathermap.org/img/wn/{weatherJsonOneCall.hourly[j].weather[0].icon}@2x.png";

            j = j + 3;
            label__6.Text = UnixTimeToDateTime(weatherJsonOneCall.hourly[j].dt).ToString("t", System.Globalization.CultureInfo.CreateSpecificCulture("sv-FI"));
            labelTemp__6.Text = weatherJsonOneCall.hourly[j].temp.ToString("0");
            image__6.Source = $"https://openweathermap.org/img/wn/{weatherJsonOneCall.hourly[j].weather[0].icon}@2x.png";

            j = j + 3;
            label__7.Text = UnixTimeToDateTime(weatherJsonOneCall.hourly[j].dt).ToString("t", System.Globalization.CultureInfo.CreateSpecificCulture("sv-FI"));
            labelTemp__7.Text = weatherJsonOneCall.hourly[j].temp.ToString("0");
            image__7.Source = $"https://openweathermap.org/img/wn/{weatherJsonOneCall.hourly[j].weather[0].icon}@2x.png";

            j = j + 3;
            label__8.Text = UnixTimeToDateTime(weatherJsonOneCall.hourly[j].dt).ToString("t", System.Globalization.CultureInfo.CreateSpecificCulture("sv-FI"));
            labelTemp__8.Text = weatherJsonOneCall.hourly[j].temp.ToString("0");
            image__8.Source = $"https://openweathermap.org/img/wn/{weatherJsonOneCall.hourly[j].weather[0].icon}@2x.png";
        }
        async Task GetCurrentLocation()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                cts = new CancellationTokenSource();
                var location = await Geolocation.GetLocationAsync(request, cts.Token);

                if (location != null)
                {
                    weatherJason = JsonConvert.DeserializeObject<WeatherJson>(GetWeatherInfo($"http://api.openweathermap.org/data/2.5/weather?lat={location.Latitude}&lon={location.Longitude}&units=metric&lang=ru&appid=6e3c7091a8b557bb4861808f01c09db6"));
                    weatherJsonOneCall = JsonConvert.DeserializeObject<WeatherJasonOneCall>(GetWeatherInfo($"http://api.openweathermap.org/data/2.5/onecall?lat={location.Latitude}&lon={location.Longitude}&units=metric&lang=ru&appid=6e3c7091a8b557bb4861808f01c09db6"));
                    Kek();
                    await DisplayAlert("Ваше местоположение", $"Геолокация определила что ваш город {weatherJason.Name}?", "Понял");
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
                weatherJason = JsonConvert.DeserializeObject<WeatherJson>(GetWeatherInfo($"http://api.openweathermap.org/data/2.5/weather?q={cityName}&lang=ru&units=metric&appid=6e3c7091a8b557bb4861808f01c09db6"));
                weatherJsonOneCall = JsonConvert.DeserializeObject<WeatherJasonOneCall>(GetWeatherInfo($"http://api.openweathermap.org/data/2.5/onecall?lat={weatherJason.coord.lat}&lon={weatherJason.coord.lon}&units=metric&lang=ru&appid=6e3c7091a8b557bb4861808f01c09db6"));
                Kek();
            }
            catch
            {
                await DisplayAlert("Что - то пошло не так ...", "Введите правильное название города", "Повторить");
            }

        }
        private async void OnEditorCompleted(object sender, EventArgs e)
        {
            try
            {
                string cityName = Editor.Text;
                weatherJason = JsonConvert.DeserializeObject<WeatherJson>(GetWeatherInfo($"http://api.openweathermap.org/data/2.5/weather?q={cityName}&lang=ru&units=metric&appid=6e3c7091a8b557bb4861808f01c09db6"));
                weatherJsonOneCall = JsonConvert.DeserializeObject<WeatherJasonOneCall>(GetWeatherInfo($"http://api.openweathermap.org/data/2.5/onecall?lat={weatherJason.coord.lat}&lon={weatherJason.coord.lon}&lang=ru&units=metric&appid=6e3c7091a8b557bb4861808f01c09db6"));
                Kek();
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
    }
}
