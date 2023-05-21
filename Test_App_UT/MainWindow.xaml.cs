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
using System.Net;
using System.IO;

namespace Test_App_UT
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string response; //переменная для хранения ответа о погоде
        string country; //переменная для хранения выбора городы
        public MainWindow()
        {
            InitializeComponent();   
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int i = CountryList.SelectedIndex;
            if (i==-1)
            {
                MessageBox.Show("Не выбран город"); //Прослушка выбора города и его проверка 
            } else {
                switch (i)
                {
                    case 0:
                        country = "Moscow";
                        break;
                    case 1:
                        country = "London";
                        break;
                    case 2:
                        country = "Kazan";
                        break;
                    case 3:
                        country = "Cairo";
                        break;
                    case 4:
                        country = "Istanbul";
                        break;
                    case 5:
                        country = "Tokyo";
                        break;
                } // Адрес запроса с указанием ключа
                string url = "https://api.openweathermap.org/data/2.5/weather?q=" + country + "&units=metric&appid=f18da61cab2134b255e956885f00b6b7";
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url); // Запрос погоды в заданном городе в метрической системе
                HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse(); // Получения ответа от сервера
                using (StreamReader streamReader = new StreamReader(webResponse.GetResponseStream()))
                {
                    response = streamReader.ReadToEnd(); // Считывание информации в строковую переменную
                }

            }

            int index = response.IndexOf("temp"); // Поиск в строке информации о температуре
            string temp = response.Substring(index + 6, 6); // вырез необходимой инф-ии 
            if (temp.Contains(",")) // Костыль для получения удобоваримой записи
             {
                temp.IndexOf(",");
                string s1 = temp.Substring(0, temp.IndexOf(","));
                temp = s1;
             }

            index = response.IndexOf("description"); // Поиск в строке краткой характеристики о текущей погоде
            string description = response.Substring(index + 14, 20);
            if (description.Contains(","))
             { 
                description.IndexOf(",");
                string s2 = description.Substring(0, description.IndexOf(",") - 1);
                description = s2;
             }
            index = response.IndexOf("speed"); // Поиск в строке информации о скорости ветра
            string speed = response.Substring(index + 7, 7);
            if (speed.Contains(","))
             { 
                speed.IndexOf(",");
                string s3 = speed.Substring(0, speed.IndexOf(","));
                speed = s3;
             }

            Temp.Content = "Температура = " + temp;
            Description.Content = "Погода = " + description;
            WindSpeed.Content = "Скорость ветра = " + speed;


            if (response == null) // Проверка на пустоту
             {
                MessageBox.Show("Не получены данные");
             }
            else
                TextBox1.Text = response; // Вывод ответа от сервера в строковом варианте
        }

        
    }
}
