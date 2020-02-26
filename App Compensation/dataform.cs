using Nancy;
using Newtonsoft.Json;
using NLog;
using SimpleTCP;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace App_Compensation
{
    public partial class dataform : UserControl
    {
        //Set plot axis X
        private int MaxX = 60;

        //Use parameter
        private Weather_Info _Weather_Info;
        private DataControl _DataControl_Info;

        public dataform()
        {
            if (!this.DesignMode)
            {
                InitializeComponent();
            }

            //call to class data
            _Weather_Info = new Weather_Info();
            _DataControl_Info = new DataControl();
        }

        //When form loadding
        private void Dataform_Load(object sender, EventArgs e)
        {
            //set url to get data from sensor
            string result = null;
            string url = "http://192.168.70.61";

            //start get data
            GetTimer.Enabled = true;
            GetTimer.Start();

            //set button color
            StartBtn.BackColor = Color.LimeGreen;

            //set timer star
            timer2.Enabled = true;
            timer2.Start();

            //set size graph and set plot graph
            SetSize();
            stopwatch.Start();
        }

        //Button Start
        private void StartBtn_Click_1(object sender, EventArgs e)
        {
            //set button color when click start
            StartBtn.BackColor = Color.LimeGreen;
            StopBtn.BackColor = Color.Empty;

            //timer 1000 milisec start
            timer2.Enabled = true;
            timer2.Start();

            //start get data
            string result = null;
            string url = "http://192.168.70.61";

            //timer get data start
            GetTimer.Enabled = true;
            GetTimer.Start();
        }

        //Button Stop
        private void StopBtn_Click_1(object sender, EventArgs e)
        {
            //set button color when click stop
            StopBtn.BackColor = Color.OrangeRed;
            StartBtn.BackColor = Color.Empty;

            //timer get data start
            GetTimer.Enabled = false;
            GetTimer.Stop();

            //timer 1000 milisec stop
            timer2.Enabled = true;
            timer2.Stop();
        }

        //Function Windparameters
        void Wind0R1(List<string> array)
        {
            for (int i = 0; i < array.Count; i++)
            {
                //windDirection average
                if (array[i].Contains("Dm="))
                {
                    int Value = 0;
                    string numberOnly = Regex.Replace(array[i], "[^0-9.]", "");

                    if (int.TryParse(numberOnly, out Value))
                    {
                        _Weather_Info.Wind_Dir = Value;

                        Wind_directionAvr.Text = String.Format("{0:0.000}", _Weather_Info.Wind_Dir);
                       

                    }
                }

                //windSpeed average
                else if (array[i].Contains("Sm="))
                {
                    double Value = 0;
                    string numberOnly = Regex.Replace(array[i], "[^0-9.]", "");

                    if (double.TryParse(numberOnly, out Value))
                    {
                        _Weather_Info.Wind_Speed = Value;

                        Wind_speedAvr.Text = String.Format("{0:0.000}", _Weather_Info.Wind_Speed);
                    }
                }
            }
        }

        //Function Temperature and Humidity
        void PressTemHum0R2(List<string> array)
        {
            for (int i = 0; i < array.Count; i++)
            {
                //Temp
                if (array[i].Contains("Ta="))
                {
                    double Value = 0;
                    string numberOnly = Regex.Replace(array[i], "[^0-9.]", "");

                    if (double.TryParse(numberOnly, out Value))
                    {
                        _Weather_Info.Temperature = Value;
                        Air_temperature.Text = String.Format("{0:0.00}", _Weather_Info.Temperature);
                    }
                }

                //Humidity
                else if (array[i].Contains("Ua="))
                {
                    double Value = 0;
                    string numberOnly = Regex.Replace(array[i], "[^0-9.]", "");

                    if (double.TryParse(numberOnly, out Value))
                    {
                        _Weather_Info.Humidity = Value;
                        Relative_humidity.Text = String.Format("{0:0.00}", _Weather_Info.Humidity);
                    }
                }   
            }
        }

        //paremeter read data from HTTP protocal
        private string HTMLText = "";
        private bool IsDowloading = false;

        //Function download string from HTTPserver
        private void DownloadInformation()
        {
            GetTimer.Stop();

            WebClient Detail = new WebClient();
            Detail.DownloadStringCompleted += new DownloadStringCompletedEventHandler(Detail_DownloadStringCompleted);
            Detail.DownloadStringAsync(new Uri("http://192.168.70.61"));
        }

        //Condition when download data complete 
        void Detail_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
                HTMLText = e.Result;

            GetTimer.Start();
        }

        //Getdata timer
        private void GetTimer_Tick(object sender, EventArgs e)
        {
            DownloadInformation();
            List<string> DataLine = HTMLText.Split(new String[] { "0R" }, StringSplitOptions.None).ToList();

            if (DataLine.Count == 0)
                return;

            textBox_status.Text = HTMLText;

            if (DataLine.Count > 0)
            {
                for (int i = 0; i < DataLine.Count; i++)
                {
                    if (DataLine[i].Length > 2 && (Char.IsNumber(DataLine[i][0]) && DataLine[i][1] == ','))
                    {
                        int RIndex = Convert.ToInt32(DataLine[i][0].ToString());
                        List<string> DataArray = DataLine[i].Split(new char[] { ',' }).ToList();
                        DataArray.RemoveAt(0);

                        switch (RIndex)
                        {
                            case 1: Wind0R1(DataArray); break;
                            case 2: PressTemHum0R2(DataArray); break;
                        }
                    }
                }
            }

            

        }

        //Timerset 1000 milisec for writetextfile , callfunction clientsenddata for control and plotgraph
        private void Timer2_Tick(object sender, EventArgs e)
        {
            //callFunction
            setDayStatus();
            setTimesDisplay();
            RunPostAsync();

            //show monitor control
            WindDirectionControl.Text = String.Format("{0:0.000}", _Weather_Info.Wind_Dir);
            WindSpeedControl.Text = String.Format("{0:0.000}", _Weather_Info.Wind_Speed);

            //Write data to textfile
            if (_Weather_Info.WindDirCanWrite && _Weather_Info.WindSpeedCanWrite)
            {
                Logger loggers = LogManager.GetCurrentClassLogger();
                loggers.Info(_Weather_Info.WindDirRaw.ToString() + ";" + _Weather_Info.Wind_Dir.ToString() + ";" + _Weather_Info.Wind_Dir_Min.ToString() + ";" + _Weather_Info.Wind_Dir_Max.ToString() + ";" +
                      _Weather_Info.WindSpeedRaw.ToString() + ";" + _Weather_Info.Wind_Speed.ToString() + ";" + _Weather_Info.Wind_Speed_Min.ToString() + ";" + _Weather_Info.Wind_Speed_Max.ToString() + ";" +
                     _Weather_Info.Temperature.ToString() + ";" + _Weather_Info.Humidity.ToString() + ";" + _Weather_Info.AirPresure.ToString() + ";" + _DataControl_Info.WindSpeedControl.ToString());
            }

            //call class point to plotgraph
            DataPoints._Weather_Info = this._Weather_Info;

            //plotgraph control page
            if (!IsEnable)
            {
                PoltGraphDataControl();               
            }

            if (this._Weather_Info != null)
            {
                stopwatch.Start();
                TimeSpan timeSpan = stopwatch.Elapsed;
                double timess = timeSpan.TotalSeconds;

                WindspeedCon.AddPoint(timess, _Weather_Info.Wind_Speed);

                Datacontrol_Graph.GraphPane.XAxis.Scale.Max = timess + 1;        

                if (WindspeedCon.Points.Count < MaxX)
                {
                    Datacontrol_Graph.GraphPane.XAxis.Scale.Min = 1;
                }
                else
                {
                    Datacontrol_Graph.GraphPane.XAxis.Scale.Min = WindspeedCon.Points[0].X;
                }

                Datacontrol_Graph.AxisChange();
                Datacontrol_Graph.Invalidate();
            }
        }

        //set show monitor day and night
        private void setDayStatus()
        {
            TimeSpan suntime = new TimeSpan(06, 00, 0);
            TimeSpan nighttime = new TimeSpan(18, 00, 0);
            TimeSpan now = DateTime.Now.TimeOfDay;


            if (now > suntime)
            {
                Daystatus.Text = "DAY";
                WebClient wc = new WebClient();
                byte[] bytes = wc.DownloadData("http://localhost:8080/Picturetime/Day.png");
                MemoryStream ms = new MemoryStream(bytes);
                System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
                pictureDayStatus.Image = img;
            }

            if (now > nighttime)
            {
                Daystatus.Text = "NIGHT";
                WebClient wc = new WebClient();
                byte[] bytes = wc.DownloadData("http://localhost:8080/Picturetime/Night.png");
                MemoryStream ms = new MemoryStream(bytes);
                System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
                pictureDayStatus.Image = img;
            }

        }


        //set format times for show in monitor
        private void setTimesDisplay()
        {
            Timehourmin.Text = DateTime.Now.ToString("HH:mm");
            TimeSec.Text = DateTime.Now.ToString("ss");
            TimeDate.Text = DateTime.Now.ToString("dd MMM yyyy");
        }

        //Function inputWindSpeed for Controller
        private void InputControl_Click(object sender, EventArgs e)
        {
           

           
        }

        //set variable for plotgraph
        private bool IsEnable = false;
        LineItem WindspeedCon = null;
        LineItem WindDirectionCon = null;
        Stopwatch stopwatch = new Stopwatch();

        //plotgraph
        private void PoltGraphDataControl()
        {
            GraphPane myPane = Datacontrol_Graph.GraphPane;
            myPane.XAxis.Scale.Min = 0;
            myPane.XAxis.Scale.Max = 86400;
            myPane.YAxis.Scale.Min = 0;
            myPane.YAxis.Scale.MaxAuto = true;

            myPane.Title.Text = "Graph Compensation Parameter";
            myPane.XAxis.Title.Text = "Time";
            myPane.YAxis.Title.Text = "M/s";

            WindspeedCon = myPane.AddCurve("WindSpeed (m/s)", new RollingPointPairList(MaxX), Color.Red, SymbolType.None);
            WindspeedCon.Line.IsSmooth = false;
            WindspeedCon.Line.Width = 5F;

            Datacontrol_Graph.AxisChange();
            Datacontrol_Graph.Invalidate();

            IsEnable = true;
        }

        //set size graph
        private void SetSize()
        {
            Datacontrol_Graph.Location = new Point(0, 0);
            Datacontrol_Graph.IsShowPointValues = true;
            Datacontrol_Graph.Size = new Size(this.ClientRectangle.Width - 20, this.ClientRectangle.Height - 50);
        }

        //SendDatatoServerNancy
        static HttpClient client = new HttpClient();
        public void RunPostAsync()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //response data to server
            DataControl parameter = new DataControl();
            string value = String.Format("{0:0.000}", _Weather_Info.Wind_Speed);
            parameter.WindSpeedControl = double.Parse(value);

            //post data(format json) and convert class to json
            var res = client.PostAsync("http://localhost:8080/func1", new StringContent(JsonConvert.SerializeObject(parameter)));
            try
            {
                res.Result.EnsureSuccessStatusCode();
                Console.WriteLine("Response " + res.Result.Content.ReadAsStringAsync().Result + Environment.NewLine);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error " + res + " Error " +
                ex.ToString());
            }
        }
    }
}
