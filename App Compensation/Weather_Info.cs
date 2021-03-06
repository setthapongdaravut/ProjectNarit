﻿using System;
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
        private double torque = 0.0;
        private double scaling = 0.0;
        Delspike ws = new Delspike();
        Compensation compensation = new Compensation(); //งูกินหาง ควยยยย มึงเรียก object มั่วววว!!!!!

        private double[,] Wind_Speed_Matrix = new double[,] { { 0 }, { 0 } };

        public double _Wind_Speed = 0.0;
        public double Wind_Speed
        {
            get { return _Wind_Speed; }
            set
            {
                //if (WindSpeedKF == null)
                //    WindSpeedKF = new KalmanFilter();

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
                scaling = compensation.Compensate(_Wind_Speed, _Wind_Dir);
                torque = compensation.Torque(_Wind_Speed, scaling);
                Console.WriteLine("Scaling factor = {0}",scaling);
                Console.WriteLine("Torque = {0}",torque);
                
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

        
    }
}
