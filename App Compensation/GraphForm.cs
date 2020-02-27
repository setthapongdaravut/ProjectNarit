using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;
using System.Diagnostics;
using System.Net;
using System.Text.RegularExpressions;

namespace App_Compensation
{
    public partial class GraphForm : UserControl
    {
        //call weather parameter
        private Weather_Info _Weather_Info = new Weather_Info();

        //Set Line for plotgraph
        LineItem windDirRaw = null;
        LineItem windDirKMF = null;
        LineItem windSpRaw = null;
        LineItem windSpKMF = null;
        LineItem temp = null;
        LineItem humid = null;

        //set time for plot axis X
        Stopwatch stopwatch = new Stopwatch();
        private bool IsEnable = false;

        public GraphForm()
        {
            if (!this.DesignMode)
            {
                InitializeComponent();
            }
            _Weather_Info = new Weather_Info();
        }

        //plotgraph windDirection
        private void PoltGraphWindDirection()
        {
            //set axis X and Y
            GraphPane myPane = WindDir_Graph.GraphPane;
            myPane.XAxis.Scale.Min = 0;
            myPane.XAxis.Scale.Max = 500;
            myPane.YAxis.Scale.Min = 0;
            myPane.YAxis.Scale.MaxAuto = true;

            //set text title and text axis X,Y
            myPane.Title.Text = "Graph WindDirection";
            myPane.XAxis.Title.Text = "Time";
            myPane.YAxis.Title.Text = "degree";

            //set colorline and nameline
            windDirRaw = myPane.AddCurve("WindDirection : Raw", new RollingPointPairList(500), Color.Red, SymbolType.None);
            windDirRaw.Line.IsSmooth = true;
            windDirRaw.Line.Width = 3F;

            //set colorline and nameline
            windDirKMF = myPane.AddCurve("WindDirection : KalmanFilter", new RollingPointPairList(500), Color.CadetBlue, SymbolType.None);
            windDirKMF.Line.IsSmooth = true;
            windDirKMF.Line.Width = 3F;

            WindDir_Graph.AxisChange();
            WindDir_Graph.Invalidate();

            IsEnable = true;
        }

        //plotgraph windSpeed
        private void PoltGraphWindSpeed()
        {
            //set axis X and Y
            GraphPane myPane = WindSpeed_Graph.GraphPane;
            myPane.XAxis.Scale.Min = 0;
            myPane.XAxis.Scale.Max = 500;
            myPane.YAxis.Scale.Min = 0;
            myPane.YAxis.Scale.MaxAuto = true;

            //set text title and text axis X,Y
            myPane.Title.Text = "Graph WindSpeed";
            myPane.XAxis.Title.Text = "Time";
            myPane.YAxis.Title.Text = "M/s";

            //set colorline and nameline
            windSpRaw = myPane.AddCurve("WindSpeed : Raw", new RollingPointPairList(500), Color.DarkKhaki, SymbolType.None);
            windSpRaw.Line.IsSmooth = true;
            windSpRaw.Line.Width = 3F;

            //set colorline and nameline
            windSpKMF = myPane.AddCurve("WindSpeed : KalmanFilter", new RollingPointPairList(500), Color.DarkViolet, SymbolType.None);
            windSpKMF.Line.IsSmooth = true;
            windSpKMF.Line.Width = 3F;

            WindSpeed_Graph.AxisChange();
            WindSpeed_Graph.Invalidate();

            IsEnable = true;
        }

        //plotgraph Temperature
        private void PoltGraphTemperature()
        {
            //set axis X and Y
            GraphPane myPane = Temper_Graph.GraphPane;
            myPane.XAxis.Scale.Min = 0;
            myPane.XAxis.Scale.Max = 500;
            myPane.YAxis.Scale.Min = 0;
            myPane.YAxis.Scale.MaxAuto = true;

            //set text title and text axis X,Y
            myPane.Title.Text = "Graph Temperature";
            myPane.XAxis.Title.Text = "Time";
            myPane.YAxis.Title.Text = "Celsius";

            //set colorline and nameline
            temp = myPane.AddCurve("Temperature (c)", new RollingPointPairList(500), Color.Chocolate, SymbolType.None);
            temp.Line.IsSmooth = true;
            temp.Line.Width = 3F;


            Temper_Graph.AxisChange();
            Temper_Graph.Invalidate();

            IsEnable = true;
        }

        //plotgraph Humidity
        private void PoltGraphHumidity()
        {
            //set axis X and Y
            GraphPane myPane = Humidity_Graph.GraphPane;
            myPane.XAxis.Scale.Min = 0;
            myPane.XAxis.Scale.Max = 500;
            myPane.YAxis.Scale.Min = 0;
            myPane.YAxis.Scale.MaxAuto = true;

            //set text title and text axis X,Y
            myPane.Title.Text = "Graph Humidity";
            myPane.XAxis.Title.Text = "Time";
            myPane.YAxis.Title.Text = "percent (%)";

            //set colorline and nameline
            humid = myPane.AddCurve("Temperature (c)", new RollingPointPairList(500), Color.Pink, SymbolType.None);
            humid.Line.IsSmooth = true;
            humid.Line.Width = 3F;

            Humidity_Graph.AxisChange();
            Humidity_Graph.Invalidate();

            IsEnable = true;
        }

        //setsizr of graph
        private void SetSize()
        {
            WindDir_Graph.Location = new Point(0, 0);
            WindDir_Graph.IsShowPointValues = true;
            WindDir_Graph.Size = new Size(this.ClientRectangle.Width - 20, this.ClientRectangle.Height - 50);

            WindSpeed_Graph.Location = new Point(0, 0);
            WindSpeed_Graph.IsShowPointValues = true;
            WindSpeed_Graph.Size = new Size(this.ClientRectangle.Width - 20, this.ClientRectangle.Height - 50);

            Temper_Graph.Location = new Point(0, 0);
            Temper_Graph.IsShowPointValues = true;
            Temper_Graph.Size = new Size(this.ClientRectangle.Width - 20, this.ClientRectangle.Height - 50);

            Humidity_Graph.Location = new Point(0, 0);
            Humidity_Graph.IsShowPointValues = true;
            Humidity_Graph.Size = new Size(this.ClientRectangle.Width - 20, this.ClientRectangle.Height - 50);
        }

        private void GraphForm_Load(object sender, EventArgs e)
        {
            SetSize();
            timer1.Enabled = true;
            timer1.Start();

            timer2.Enabled = true;
            timer2.Start();

            stopwatch.Start();

        }

        //timer to plotgraph
        private void Timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Start();
            setTimesDisplay();

            this._Weather_Info = DataPoints._Weather_Info;

            if (!IsEnable) {
                PoltGraphWindDirection();
                PoltGraphWindSpeed();
                PoltGraphTemperature();
                PoltGraphHumidity();
            }
                
            if (this._Weather_Info != null)
            {

                stopwatch.Start();
                TimeSpan timeSpan = stopwatch.Elapsed;
                double timess = timeSpan.TotalSeconds;

                windDirRaw.AddPoint(timess, _Weather_Info.WindDirRaw);
                windDirKMF.AddPoint(timess, _Weather_Info.Wind_Dir);

                windSpRaw.AddPoint(timess, _Weather_Info.WindSpeedRaw);
                windSpKMF.AddPoint(timess, _Weather_Info.Wind_Speed);

                temp.AddPoint(timess, _Weather_Info.Temperature);

                humid.AddPoint(timess, _Weather_Info.Humidity);

                WindDir_Graph.GraphPane.XAxis.Scale.Max = timess + 1;
                WindSpeed_Graph.GraphPane.XAxis.Scale.Max = timess + 1;
                Temper_Graph.GraphPane.XAxis.Scale.Max = timess + 1;
                Humidity_Graph.GraphPane.XAxis.Scale.Max = timess + 1;

                if (windDirRaw.Points.Count < 500 && windDirKMF.Points.Count < 500 
                    && windSpRaw.Points.Count <500 && windSpKMF.Points.Count < 500 
                    && temp.Points.Count < 500 && humid.Points.Count < 500)
                {
                    WindDir_Graph.GraphPane.XAxis.Scale.Min = 1;
                    WindSpeed_Graph.GraphPane.XAxis.Scale.Min = 1;
                    Temper_Graph.GraphPane.XAxis.Scale.Min = 1;
                    Humidity_Graph.GraphPane.XAxis.Scale.Min = 1;
                }
                else
                {
                    WindDir_Graph.GraphPane.XAxis.Scale.Min = windDirRaw.Points[0].X;
                    WindDir_Graph.GraphPane.XAxis.Scale.Min = windDirKMF.Points[0].X;

                    WindSpeed_Graph.GraphPane.XAxis.Scale.Min = windSpRaw.Points[0].X;
                    WindSpeed_Graph.GraphPane.XAxis.Scale.Min = windSpKMF.Points[0].X;

                    Temper_Graph.GraphPane.XAxis.Scale.Min = temp.Points[0].X;

                    Humidity_Graph.GraphPane.XAxis.Scale.Min = temp.Points[0].X;
                }

                WindDir_Graph.AxisChange();
                WindDir_Graph.Invalidate();

                WindSpeed_Graph.AxisChange();
                WindSpeed_Graph.Invalidate();

                Temper_Graph.AxisChange();
                Temper_Graph.Invalidate();

                Humidity_Graph.AxisChange();
                Humidity_Graph.Invalidate();
            }
        }

        //set times show
        private void setTimesDisplay()
        {
            Timehourmin.Text = DateTime.Now.ToString("HH:mm");
            TimeSec.Text = DateTime.Now.ToString("ss");
            TimeDate.Text = DateTime.Now.ToString("dd MMM yyyy");
        }

        //function to show values wind
        void Wind0R1(List<string> array)
        {
            for (int i = 0; i < array.Count; i++)
            {
                if (array[i].Contains("Dm="))
                {
                    double Value = -1;
                    string numberOnly = Regex.Replace(array[i], "[^0-9.]", "");

                    if (double.TryParse(numberOnly, out Value))
                    {
                        if (Value != -1)
                        {
                            _Weather_Info.Wind_Dir = Value;
                            WindDirRaw_data.Text = String.Format("{0:0.000}", _Weather_Info.WindDirRaw);
                            WindDirKF_data.Text = String.Format("{0:0.000}", _Weather_Info.Wind_Dir);
                        }
                    }
                }
                else if (array[i].Contains("Sm="))
                {
                    double Value;
                    string numberOnly = Regex.Replace(array[i], "[^0-9.]", "");

                    if (double.TryParse(numberOnly, out Value))
                    {
                        if (Value != null)
                        {
                            _Weather_Info.Wind_Speed = Value;
                            WindSpeedRaw_data.Text = String.Format("{0:0.000}", _Weather_Info.WindSpeedRaw);
                            WindSpeedKF_data.Text = String.Format("{0:0.000}", _Weather_Info.Wind_Speed);
                        }
                        
                    }
                }
            }
        }

        //function to show values temperature and humidity
        void PressTemHum0R2(List<string> array)
        {

            for (int i = 0; i < array.Count; i++)
            {
                if (array[i].Contains("Ta="))
                {
                    double Value;
                    string numberOnly = Regex.Replace(array[i], "[^0-9.]", "");

                    if (double.TryParse(numberOnly, out Value))
                    {
                        if (Value != null)
                        {
                            _Weather_Info.Temperature = Value;
                            Temp_data.Text = String.Format("{0:0.00}", _Weather_Info.Temperature);
                        }
                        
                    }
                }
                else if (array[i].Contains("Ua="))
                {
                    double Value;
                    string numberOnly = Regex.Replace(array[i], "[^0-9.]", "");

                    if (double.TryParse(numberOnly, out Value))
                    {
                        if (Value != null)
                        {
                            _Weather_Info.Humidity = Value;
                            Humid_data.Text = String.Format("{0:0.00}", _Weather_Info.Humidity);
                        }
                       
                    }
                }
            }
        }

        //Downloadtext for shoe logstatus
        private string HTMLText = "";
        private bool IsDowloading = false;
        private void DownloadInformation() 
        {
            timer2.Stop();

            WebClient Detail = new WebClient();
            Detail.DownloadStringCompleted += new DownloadStringCompletedEventHandler(Detail_DownloadStringCompleted);
            Detail.DownloadStringAsync(new Uri("http://192.168.70.61?q="+DateTime.Now.Ticks));
        }
        void Detail_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
                HTMLText = e.Result;

            timer2.Start();
        }

        //timer for request value form sensor
        private void Timer2_Tick(object sender, EventArgs e)
        {
            timer2.Enabled = true;
            timer2.Start();

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


    }
}
