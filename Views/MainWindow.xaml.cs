using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Text.Json.Serialization;
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
using System.Net.Http.Json;
using Newtonsoft.Json;
using System.Text.Json;
using System.IO;
using System.Net;
using LiveCharts;
using LiveCharts.Wpf;
using System.Reflection.Emit;
using LiveCharts.Helpers;
using Rivers.Views;
using System.Windows.Media.Animation;
using Rivers.Controls;
using System.Diagnostics;

namespace Rivers
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region API
        // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
        public class HighestRecent
        {
            [JsonPropertyName("@id")]
            public string id { get; set; }

            [JsonPropertyName("dateTime")]
            public DateTime dateTime { get; set; }

            [JsonPropertyName("value")]
            public double value { get; set; }
        }

        public class Items
        {
            [JsonPropertyName("@id")]
            public string id { get; set; }

            [JsonPropertyName("RLOIid")]
            public string RLOIid { get; set; }

            [JsonPropertyName("catchmentName")]
            public string catchmentName { get; set; }

            [JsonPropertyName("dateOpened")]
            public string dateOpened { get; set; }

            [JsonPropertyName("eaAreaName")]
            public string eaAreaName { get; set; }

            [JsonPropertyName("eaRegionName")]
            public string eaRegionName { get; set; }

            [JsonPropertyName("easting")]
            public int easting { get; set; }

            [JsonPropertyName("gridReference")]
            public string gridReference { get; set; }

            [JsonPropertyName("label")]
            public string label { get; set; }

            [JsonPropertyName("lat")]
            public double lat { get; set; }

            [JsonPropertyName("long")]
            public double @long { get; set; }

            [JsonPropertyName("measures")]
            public List<Measure> measures { get; set; }

            [JsonPropertyName("northing")]
            public int northing { get; set; }

            [JsonPropertyName("notation")]
            public string notation { get; set; }

            [JsonPropertyName("riverName")]
            public string riverName { get; set; }

            [JsonPropertyName("stageScale")]
            public StageScale stageScale { get; set; }

            [JsonPropertyName("stationReference")]
            public string stationReference { get; set; }

            [JsonPropertyName("status")]
            public string status { get; set; }

            [JsonPropertyName("statusDate")]
            public DateTime statusDate { get; set; }

            [JsonPropertyName("town")]
            public string town { get; set; }

            [JsonPropertyName("type")]
            public List<string> type { get; set; }

            [JsonPropertyName("wiskiID")]
            public string wiskiID { get; set; }
        }

        public class LatestReading
        {
            [JsonPropertyName("@id")]
            public string id { get; set; }

            [JsonPropertyName("date")]
            public string date { get; set; }

            [JsonPropertyName("dateTime")]
            public DateTime dateTime { get; set; }

            [JsonPropertyName("measure")]
            public string measure { get; set; }

            [JsonPropertyName("value")]
            public double value { get; set; }
        }

        public class MaxOnRecord
        {
            [JsonPropertyName("@id")]
            public string id { get; set; }

            [JsonPropertyName("dateTime")]
            public DateTime dateTime { get; set; }

            [JsonPropertyName("value")]
            public double value { get; set; }
        }

        public class Measure
        {
            [JsonPropertyName("@id")]
            public string id { get; set; }

            [JsonPropertyName("datumType")]
            public string datumType { get; set; }

            [JsonPropertyName("label")]
            public string label { get; set; }

            [JsonPropertyName("notation")]
            public string notation { get; set; }

            [JsonPropertyName("parameter")]
            public string parameter { get; set; }

            [JsonPropertyName("parameterName")]
            public string parameterName { get; set; }

            [JsonPropertyName("period")]
            public int period { get; set; }

            [JsonPropertyName("qualifier")]
            public string qualifier { get; set; }

            [JsonPropertyName("station")]
            public string station { get; set; }

            [JsonPropertyName("stationReference")]
            public string stationReference { get; set; }

            [JsonPropertyName("type")]
            public List<string> type { get; set; }

            [JsonPropertyName("unit")]
            public string unit { get; set; }

            [JsonPropertyName("unitName")]
            public string unitName { get; set; }

            [JsonPropertyName("valueType")]
            public string valueType { get; set; }

            [JsonPropertyName("latestReading")]
            public LatestReading latestReading { get; set; }

            [JsonPropertyName("localDatumMeasure")]
            public string localDatumMeasure { get; set; }
        }

        public class Meta
        {
            [JsonPropertyName("publisher")]
            public string publisher { get; set; }

            [JsonPropertyName("licence")]
            public string licence { get; set; }

            [JsonPropertyName("documentation")]
            public string documentation { get; set; }

            [JsonPropertyName("version")]
            public string version { get; set; }

            [JsonPropertyName("comment")]
            public string comment { get; set; }

            [JsonPropertyName("hasFormat")]
            public List<string> hasFormat { get; set; }
        }

        public class MinOnRecord
        {
            [JsonPropertyName("@id")]
            public string id { get; set; }

            [JsonPropertyName("dateTime")]
            public DateTime dateTime { get; set; }

            [JsonPropertyName("value")]
            public float value { get; set; }
        }

        public class Root
        {
            [JsonPropertyName("@context")]
            public string context { get; set; }

            [JsonPropertyName("meta")]
            public Meta meta { get; set; }

            [JsonPropertyName("items")]
            public Items items { get; set; }
        }

        public class StageScale
        {
            [JsonPropertyName("@id")]
            public string id { get; set; }

            [JsonPropertyName("datum")]
            public double datum { get; set; }

            [JsonPropertyName("highestRecent")]
            public HighestRecent highestRecent { get; set; }

            [JsonPropertyName("maxOnRecord")]
            public MaxOnRecord maxOnRecord { get; set; }

            [JsonPropertyName("minOnRecord")]
            public MinOnRecord minOnRecord { get; set; }

            [JsonPropertyName("scaleMax")]
            public int scaleMax { get; set; }

            [JsonPropertyName("typicalRangeHigh")]
            public double typicalRangeHigh { get; set; }

            [JsonPropertyName("typicalRangeLow")]
            public double typicalRangeLow { get; set; }
        }


        #endregion

        #region API Todays Readings
        // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
        public class TodayReadingItem
        {
            [JsonPropertyName("@id")]
            public string id { get; set; }

            [JsonPropertyName("dateTime")]
            public DateTime dateTime { get; set; }

            [JsonPropertyName("measure")]
            public string measure { get; set; }

            [JsonPropertyName("value")]
            public double value { get; set; }
        }

        public class TodayReadingMeta
        {
            [JsonPropertyName("publisher")]
            public string publisher { get; set; }

            [JsonPropertyName("licence")]
            public string licence { get; set; }

            [JsonPropertyName("documentation")]
            public string documentation { get; set; }

            [JsonPropertyName("version")]
            public string version { get; set; }

            [JsonPropertyName("comment")]
            public string comment { get; set; }

            [JsonPropertyName("hasFormat")]
            public List<string> hasFormat { get; set; }
        }

        public class TodayReadingRoot
        {
            [JsonPropertyName("@context")]
            public string context { get; set; }

            [JsonPropertyName("meta")]
            public TodayReadingMeta meta { get; set; }

            [JsonPropertyName("items")]
            public List<TodayReadingItem> items { get; set; }
        }


        #endregion

        #region Flood Alert API
        // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
        public class FloodItem
        {
            [JsonPropertyName("@id")]
            public string id { get; set; }

            [JsonPropertyName("county")]
            public string county { get; set; }

            [JsonPropertyName("description")]
            public string description { get; set; }

            [JsonPropertyName("eaAreaName")]
            public string eaAreaName { get; set; }

            [JsonPropertyName("fwdCode")]
            public string fwdCode { get; set; }

            [JsonPropertyName("label")]
            public string label { get; set; }

            [JsonPropertyName("lat")]
            public double lat { get; set; }

            [JsonPropertyName("long")]
            public double @long { get; set; }

            [JsonPropertyName("notation")]
            public string notation { get; set; }

            [JsonPropertyName("polygon")]
            public string polygon { get; set; }

            [JsonPropertyName("quickDialNumber")]
            public string quickDialNumber { get; set; }

            [JsonPropertyName("riverOrSea")]
            public string riverOrSea { get; set; }

            [JsonPropertyName("floodWatchArea")]
            public string floodWatchArea { get; set; }
        }

        public class FloodMeta
        {
            [JsonPropertyName("publisher")]
            public string publisher { get; set; }

            [JsonPropertyName("licence")]
            public string licence { get; set; }

            [JsonPropertyName("documentation")]
            public string documentation { get; set; }

            [JsonPropertyName("version")]
            public string version { get; set; }

            [JsonPropertyName("comment")]
            public string comment { get; set; }

            [JsonPropertyName("hasFormat")]
            public List<string> hasFormat { get; set; }

            [JsonPropertyName("limit")]
            public int limit { get; set; }
        }

        public class FloodRoot
        {
            [JsonPropertyName("@context")]
            public string context { get; set; }

            [JsonPropertyName("meta")]
            public FloodMeta meta { get; set; }

            [JsonPropertyName("items")]
            public List<FloodItem> items { get; set; }
        }
        #endregion

        #region Searched Rivers API

        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class SearchItem
        {
            [JsonProperty("@id")]
            public string id { get; set; }
            public string RLOIid { get; set; }
            public string catchmentName { get; set; }
            public string dateOpened { get; set; }
            public int easting { get; set; }
            public string label { get; set; }
            public double lat { get; set; }
            public double @long { get; set; }
            public List<SearchMeasure> measures { get; set; }
            public int northing { get; set; }
            public string notation { get; set; }
            public string riverName { get; set; }
            public string stageScale { get; set; }
            public string stationReference { get; set; }
            public string status { get; set; }
            public string town { get; set; }
            public string wiskiID { get; set; }
        }

        public class SearchMeasure
        {
            [JsonProperty("@id")]
            public string id { get; set; }
            public string parameter { get; set; }
            public string parameterName { get; set; }
            public int period { get; set; }
            public string qualifier { get; set; }
            public string unitName { get; set; }
        }

        public class SearchMeta
        {
            public string publisher { get; set; }
            public string licence { get; set; }
            public string documentation { get; set; }
            public string version { get; set; }
            public string comment { get; set; }
            public List<string> hasFormat { get; set; }
        }

        public class SearchRoot
        {
            [JsonProperty("@context")]
            public string context { get; set; }
            public SearchMeta meta { get; set; }
            public List<SearchItem> items { get; set; }
        }


        #endregion

        Root currentRiver;

        public SearchRoot searchedRivers;

        TodayReadingRoot todayReading;

        public FloodRoot currentFloodAlerts;

        public SeriesCollection SeriesCollection { get; set; }
        //public string[] Labels { get; set; }

        public List<DateTime> Labels = new List<DateTime>();

        public List<double> ReadingValues = new List<double>();
        public Func<double, string> YFormatter { get; set; }
        public Func<double, string> XFormatter { get; set; }
        
        System.Threading.Timer timer;

        public MainWindow()
        {
            InitializeComponent();
            FloodAlertGrid.Opacity = 0;
            APICall();
            CheckForAlerts();

            timer = new System.Threading.Timer(
                e => APICall(),
                null,
                TimeSpan.Zero,
                TimeSpan.FromMinutes(15));

        }

        public void CheckForAlerts()
        {
            FloodAlertCall();

        }


        public async Task FloodAlertCall()
        {
            await Task.Delay(2000);
            var url = "https://environment.data.gov.uk/flood-monitoring/id/floodAreas?lat=51.915339&long=-2.587573&dist=5";

            var httpRequest = (HttpWebRequest)WebRequest.Create(url);


            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                currentFloodAlerts = System.Text.Json.JsonSerializer.Deserialize<FloodRoot>(result);
            }

            Console.WriteLine(httpResponse.StatusCode);
            if (httpResponse.StatusCode == HttpStatusCode.OK)
            {
                if (currentFloodAlerts.items.Count > 0)
                {
                    FloodAlertGrid.Visibility = Visibility.Visible;

                    DoubleAnimation ani = new DoubleAnimation(1, TimeSpan.FromSeconds(1));
                    //ani.Completed += new EventHandler(myanim_Completed);
                    FloodAlertGrid.BeginAnimation(Grid.OpacityProperty, ani);
                }

            }
        }

        public async Task APICall()
        {
            var url = "https://environment.data.gov.uk/flood-monitoring/id/stations/055817_TG_323";

            var httpRequest = (HttpWebRequest)WebRequest.Create(url);


            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                currentRiver = System.Text.Json.JsonSerializer.Deserialize<Root>(result);
            }

            Console.WriteLine(httpResponse.StatusCode);
            if (httpResponse.StatusCode == HttpStatusCode.OK)
            {               
                APICallTodayReadings(currentRiver.items.id);
                AdjustTextValues();
            }

        }

        public async Task APICall(string ID)
        {
            var url = "https://environment.data.gov.uk/flood-monitoring/id/stations/" + ID;
            //var url = "https://environment.data.gov.uk/flood-monitoring/id/stations/055817_TG_323";

            Debug.WriteLine(url);
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);


            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                int test = result.IndexOf("measures");

                if (result[test+12] != '[')
                {
                    test = test + 11;
                    result = result.Insert(test, "[");

                    int test2 = result.IndexOf("northing");
                    test2 = test2 - 7;
                    result = result.Insert(test2, "]");
                }

                //currentRiver = System.Text.Json.JsonSerializer.Deserialize<Root>(result);
                try
                {
                    currentRiver = null;
                    currentRiver = JsonConvert.DeserializeObject<Root>(result);
                }
                catch (JsonReaderException msg)
                {
                    Debug.WriteLine(msg.Message);
                    //Input string '-0.137' is not a valid integer. Path 'items.stageScale.minOnRecord.value', line 93, position 24.
                    //throw;
                }
                catch (JsonSerializationException msg)
                {
                    Debug.WriteLine(msg.Message);
                    //throw;
                }
            }

            Console.WriteLine(httpResponse.StatusCode);
            if (httpResponse.StatusCode == HttpStatusCode.OK)
            {
                APICallTodayReadings(currentRiver.items.id);
                AdjustTextValues();
                UpdateLineGraph();
            }

        }

        public async Task APICallTodayReadings(string id)
        {
            var url = id + "/readings?today";

            var httpRequest = (HttpWebRequest)WebRequest.Create(url);


            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                todayReading = System.Text.Json.JsonSerializer.Deserialize<TodayReadingRoot>(result);
            }

            Console.WriteLine(httpResponse.StatusCode);
            if (httpResponse.StatusCode == HttpStatusCode.OK)
            {
                UpdateLineGraph();
            }

        }

        public async void UpdateLineGraph()
        {
            // Dispatcher Invokes Fixes Following Error
            // ~ The calling thread must be STA, because many UI components require this.
            Application.Current.Dispatcher.Invoke((Action)delegate {

                try
                {
                    if (Chart.Series != null)
                    {
                        Chart.Series.Clear();
                        //Chart.AxisX.Clear();
                        //Chart.AxisY.Clear();
                    }

                    DataContext = null;
                    Labels.Clear();
                    ReadingValues.Clear();
                    if (SeriesCollection != null)
                    {
                        SeriesCollection.Clear();
                    }

                }
                catch (Exception)
                {

                    throw;
                }


                foreach (var item in todayReading.items)
                {
                    Labels.Add(item.dateTime);
                    ReadingValues.Add(Math.Round(item.value, 4));
                }



                SeriesCollection = new SeriesCollection
                {
                    new LineSeries
                    {
                        Title = "River Levels",
                        LineSmoothness = 1, //0: straight lines, 1: really smooth lines
                        Values = ReadingValues.AsChartValues()
                    }
                };

                yAxis.LabelFormatter = value => value.ToString("0.###");
                xAxis.LabelFormatter = value => value.ToString("g");
                XFormatter = value => value.ToString("g");

                DataContext = this;

            });
        }

        public void AdjustTextValues()
        {
            
            riverName_txt.Text = currentRiver.items.riverName;
            riverDescription_txt.Text = "Catchment Area : " + currentRiver.items.catchmentName;
            if (currentRiver.items.measures.Count >= 2)
            {
                currentRiverLevel_txt.Text = "Current Level : " + currentRiver.items.measures[1].latestReading.value + "m";
            }
            else
            {
                currentRiverLevel_txt.Text = "Current Level : " + currentRiver.items.measures[0].latestReading.value + "m";
            }

            if (currentRiver.items.measures[1].latestReading.value > todayReading.items[todayReading.items.Count-2].value)
            {
                currentRiverLevel_txt.Foreground = Brushes.Green;
            }
            else
            {
                currentRiverLevel_txt.Foreground = Brushes.Red;
            }
            latestReadingTime_txt.Text = currentRiver.items.measures[1].latestReading.dateTime.ToString();
        }

        public async Task SearchRivers(string searchParams)
        {
            var url = "https://environment.data.gov.uk/flood-monitoring/id/stations?search=" + searchParams;

            var httpRequest = (HttpWebRequest)WebRequest.Create(url);


            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                searchedRivers = System.Text.Json.JsonSerializer.Deserialize<SearchRoot>(result);
            }

            Console.WriteLine(httpResponse.StatusCode);
            if (httpResponse.StatusCode == HttpStatusCode.OK)
            {
                if (searchedRivers !=null)
                {
                    foreach (var item in searchedRivers.items)
                    {
                        RiverSearchBar newRiver = new RiverSearchBar();
                        newRiver.RiverName.Text = item.label;

                        newRiver.riverID = item.notation;



                        StackPanel1.Children.Add(newRiver);
                    }
                }
            }
        }

        private void refresh_btn_Click(object sender, RoutedEventArgs e)
        {
            APICall();
        }

        private void FloodAlertGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //Open Flood Alert Menu
            FloodAlertWindow alertWindow = new FloodAlertWindow();
            alertWindow.Show();
        }

        private void SearchTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Clear();
            StackPanel1.Children.Clear();
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                StackPanel1.Children.Clear();
                SearchRivers(StringExtensions.FirstCharToUpper( SearchTextBox.Text));
            }
        }
    }

    public static class StringExtensions
    {
        public static string FirstCharToUpper(this string input) =>
            input switch
            {
                null => throw new ArgumentNullException(nameof(input)),
                "" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
                _ => string.Concat(input[0].ToString().ToUpper(), input.AsSpan(1))
            };
    }
}
