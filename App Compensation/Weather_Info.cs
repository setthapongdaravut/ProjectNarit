using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StarMathLib;
using System.Diagnostics;
using System.Timers;

namespace App_Compensation
{
    public class Weather_Info
    {
        //Use KalmanFilter Function
        KalmanFilterWD WindDirKFWD = new KalmanFilterWD();

        //get,set variable 
        public bool WindDirCanWrite = false;
        public bool WindSpeedCanWrite = false;
        public string NameParameter { get; set; }
        public double Wind_Dir_Max { get; private set; }
        public double Wind_Dir_Min { get; private set; }
        public double WindDirRaw { get; set; }
        public double WindSpeedRaw { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public double AirPresure { get; set; }


        //set data filtering (windDirection)
        private List<double> TWindMinMax = new List<double>();
        private List<double> WindHIS = new List<double>();
        private double InitWind = 0.0;
        private int WindCount = 10;
        private double _Wind_Dir = 0.0;
        private double varianceDir = 0.0;
        private double[,] Wind_Direction_Matrix = new double[,] { { 0 }, { 0 } };
        
        
        public double Wind_Dir
        {
            get { return _Wind_Dir; }
            set
            {
                if (WindCount > 0)
                {
                    WindHIS.Add(ws.Del_WD(value));
                    WindCount--;
                    return;
                }
                else if (WindCount == 0)
                {
                    varianceDir = Variance(WindHIS);
                    InitWind = WindHIS.Sum() / 10.0;
                    WindDirKFWD.Q = new double[,] { { 1, 0 }, { 0, 2 } }; 
                    WindDirKFWD.R = new double[,] { { 10, 0 }, { 0, 1 } };
                    WindDirKFWD.GetMatrixWD(1.0, varianceDir, InitWind, 1.0);

                    WindCount = -1;
                }

             
                WindDirRaw = ws.Del_WD(value);
                Wind_Direction_Matrix = WindDirKFWD.KalmanFWD(ws.Del_WD(value));
                _Wind_Dir = Wind_Direction_Matrix[0, 0];
                WindDirCanWrite = true;

                if (TWindMinMax.Count > 60)
                    TWindMinMax.RemoveAt(0);

                TWindMinMax.Add(_Wind_Dir);
                Wind_Dir_Max = TWindMinMax.Max();
                Wind_Dir_Min = TWindMinMax.Min();
            }
        }

        //set data filering (windSpeed)
        public double Wind_Speed_Max { get; private set; }
        public double Wind_Speed_Min { get; private set; }

        KalmanFilter WindSpeedKF = new KalmanFilter();
        private List<double> TWindSpeedMinMax = new List<double>();
        private List<double> WindSpeedHIS = new List<double>();
        private double InitWindSpeed = 0.0;
        private int WindSpeedCount = 10;
        private double varianceSpeed = 0.0;
        Delspike ws = new Delspike();

        private double[,] Wind_Speed_Matrix = new double[,] { { 0 }, { 0 } };

        public double _Wind_Speed = 0.0;
        public double Wind_Speed
        {
            get { return _Wind_Speed; }
            set
            {
                if (WindSpeedCount > 0)
                {
                    WindSpeedHIS.Add(ws.Del_WS(value));
                    WindSpeedCount--;

                    _Wind_Speed = 0.0;
                    return;
                }
                else if (WindSpeedCount == 0) 
                {
                    varianceSpeed = Variance(WindSpeedHIS);
                    InitWindSpeed = WindSpeedHIS.Sum() / 10.0;
                    WindSpeedKF.Q = new double[,] { { 7, 0 }, { 0, 1 } };
                    WindSpeedKF.R = new double[,] { { 20, 0 }, { 0, 2 } };
                    WindSpeedKF.GetMatrixWS(1.0, varianceSpeed, InitWindSpeed, 1.0);

                    WindSpeedCount = -1;
                }
                WindSpeedRaw = ws.Del_WS(value);
                Wind_Speed_Matrix = WindSpeedKF.KalmanFWS(ws.Del_WS(value));
                _Wind_Speed = Wind_Speed_Matrix[0, 0];
                WindSpeedCanWrite = true;

                if (TWindSpeedMinMax.Count > 60)
                    TWindSpeedMinMax.RemoveAt(0);

                TWindSpeedMinMax.Add(_Wind_Speed);
                Wind_Speed_Max = TWindSpeedMinMax.Max();
                Wind_Speed_Min = TWindSpeedMinMax.Min();
            }
        }

        //Variance Finding
        private double Variance(List<double> values)
        {
            double avg = values.Average();
            return (values.Average(v => Math.Pow(v - avg, 2)));
        }

        private double[,] Compensate(double _Wind_Speed, double _Wind_Dir)
        {
            double ws = 0.0;
            double wd = 0.0;
            double angle = Math.PI * wd / 180; // เรียก weather_info.windspeed เข้ามาเป็น input.
            double x = ws * Math.Cos(angle);
            double y = ws * Math.Sin(angle);
            double[,] cor = new double[2, 1];

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 1; j++)
                {
                    if (i == 0)
                    {
                        cor[i, j] = x;
                    }
                    else
                    {
                        cor[i, j] = y;
                    }
                }
            }
            return cor;

        }

        //Torque compensate.
        //private double Torque(double v)
        //{
        //    double z = 10.0;
        //    double z0 = 0.03;
        //    double k = 2.5 * Math.Pow(Math.Log(z / z0), -2);
        //    double alpha = Math.Sqrt(6 * k);
        //    double kt = 2 * k * alpha * Math.Pow(v, 2);

        //    return kt;

        //}
    }
}
