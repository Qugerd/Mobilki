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

namespace Weather
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();            
          
            string url = "http://api.openweathermap.org/data/2.5/weather?q=Москва&lang=ru&units=metric&appid=6e3c7091a8b557bb4861808f01c09db6";

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

        private void Editor_TextChanged(object sender, TextChangedEventArgs e)
        {
          
        }

        private void ButtonCheck_Clicked(object sender, EventArgs e)
        {
            try
            {
                string city_name = Editor.Text;

                string url = "http://api.openweathermap.org/data/2.5/weather?q=" + city_name + "&lang=ru&units=metric&appid=6e3c7091a8b557bb4861808f01c09db6";
                string response;

                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

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
            catch (Exception) 
            { 
            }
            
        }
        private DateTime UnixTimeToDateTime(double UnixTime)
        {
            DateTime origin = new DateTime(1970, 1, 1, 6, 0, 0);
            return origin.AddSeconds(UnixTime);
        }
    }
}
