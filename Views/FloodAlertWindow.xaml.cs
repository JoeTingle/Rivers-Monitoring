using Microsoft.Maps.MapControl.WPF;
using Rivers.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
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
using System.Windows.Shapes;
using static Rivers.MainWindow;

namespace Rivers.Views
{
    /// <summary>
    /// Interaction logic for FloodAlertWindow.xaml
    /// </summary>
    public partial class FloodAlertWindow : Window
    {
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

        #region Flood Alert Polygon
        // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
        public class PolygonFeature
        {
            [JsonPropertyName("type")]
            public string type { get; set; }

            [JsonPropertyName("geometry")]
            public PolygonGeometry geometry { get; set; }

            [JsonPropertyName("properties")]
            public PolygonProperties properties { get; set; }
        }

        public class PolygonGeometry
        {
            [JsonPropertyName("type")]
            public string type { get; set; }

            [JsonPropertyName("coordinates")]
            public List<List<List<double>>> coordinates { get; set; }
        }

        public class PolygonProperties
        {
            [JsonPropertyName("AREA")]
            public string AREA { get; set; }

            [JsonPropertyName("FWS_TACODE")]
            public string FWS_TACODE { get; set; }

            [JsonPropertyName("TA_NAME")]
            public string TA_NAME { get; set; }

            [JsonPropertyName("DESCRIP")]
            public string DESCRIP { get; set; }

            [JsonPropertyName("LA_NAME")]
            public string LA_NAME { get; set; }

            [JsonPropertyName("PARENT")]
            public string PARENT { get; set; }

            [JsonPropertyName("QDIAL")]
            public string QDIAL { get; set; }

            [JsonPropertyName("RIVER_SEA")]
            public string RIVER_SEA { get; set; }
        }

        public class PolygonRoot
        {
            [JsonPropertyName("type")]
            public string type { get; set; }

            [JsonPropertyName("features")]
            public List<PolygonFeature> features { get; set; }

            [JsonPropertyName("crs")]
            public object crs { get; set; }
        }
        #endregion
        public FloodRoot FloodAlerts;
        private MainWindow.FloodRoot currentFloodAlerts;

        public List<Location> FloodLocations = new List<Location>();

        public PolygonRoot currentPoly;

        public MainWindow mainWindowInst;


        public FloodAlertWindow()
        {
            InitializeComponent();
            mainWindowInst = Window.GetWindow(App.Current.MainWindow) as MainWindow;
            currentFloodAlerts = mainWindowInst.currentFloodAlerts;
            addNewPolygon();
        }

        public async Task addNewPolygon()
        {
            foreach (var item in currentFloodAlerts.items)
            {
                FloodAlertControlBar newAlertPanel = new FloodAlertControlBar();
                newAlertPanel.DescriptiveText.Text = item.description;
                StackPanel1.Children.Add(newAlertPanel);


                string url = item.polygon;

                var httpRequest = (HttpWebRequest)WebRequest.Create(url);


                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    if (!result.Contains("MultiPolygon"))
                    {
                        currentPoly = System.Text.Json.JsonSerializer.Deserialize<PolygonRoot>(result);
                    }

                }

                if (currentPoly != null)
                {
                    Console.WriteLine(httpResponse.StatusCode);
                    if (httpResponse.StatusCode == HttpStatusCode.OK)
                    {
                        MapPolygon polygon = new MapPolygon();
                        polygon.Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Blue);
                        polygon.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Green);
                        polygon.StrokeThickness = 4;
                        polygon.Opacity = 0.5;
                        polygon.Locations = new LocationCollection();


                        for (int i = 0; i < currentPoly.features[0].geometry.coordinates[0].Count; i++)
                        {
                            polygon.Locations.Add(new Location(currentPoly.features[0].geometry.coordinates[0][i][1], currentPoly.features[0].geometry.coordinates[0][i][0]));
                        }


                        myMap.Children.Add(polygon);
                    }
                }

            }


        }
    }
}
