using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
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
using System.Text.Json;

namespace MilitariaPytanieRekrutacyjne2
{
    /// <summary>
    /// Klasa MainWindow reprezentuje główne okno aplikacji.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Konstruktor klasy MainWindow.
        /// Inicjalizuje komponenty interfejsu użytkownika oraz przypisuje instancję klasy MainViewModel jako kontekst danych dla okna.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainViewModel();
        }
    }

    /// <summary>
    /// Klasa MainViewModel reprezentuje model widoku dla głównego okna aplikacji.
    /// </summary>
    public class MainViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Area> _areas;

        /// <summary>
        /// Kolekcja obszarów dostępnych w aplikacji.
        /// </summary>
        public ObservableCollection<Area> Areas
        {
            get { return _areas; }
            set
            {
                _areas = value;
                OnPropertyChanged(nameof(Areas));
            }
        }

        /// <summary>
        /// Konstruktor klasy MainViewModel.
        /// Inicjalizuje kolekcję obszarów oraz rozpoczyna asynchroniczne ładowanie danych z API.
        /// </summary>
        public MainViewModel()
        {
            Areas = new ObservableCollection<Area>();
            Task.Run(() => LoadAreasAsync());
        }

        private static readonly HttpClient httpClient = new HttpClient();

        /// <summary>
        /// Metoda asynchroniczna, która pobiera dane z API i aktualizuje kolekcję Area.
        /// </summary>
        public async Task LoadAreasAsync()
        {
            try
            {
                var response = await httpClient.GetAsync("https://api-dbw.stat.gov.pl/api/1.1.0/area/area-area?lang=pl");

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var areaList = JsonSerializer.Deserialize<List<Area>>(jsonString);
                    Areas = new ObservableCollection<Area>(areaList);
                }
                else
                {
                    // Obsługa niepowodzenia odpowiedzi HTTP
                    Console.WriteLine($"Nie udało się pobrać danych z API. Kod stanu: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas ładowania danych: {ex.Message}");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
