using System;

namespace ProgTekEksamenRest.model
{
    public class Meassurement
    {
        private int _id;
        private double _pressure;
        private double _humidity;
        private double _temperature;
        private DateTime _timeStamp;
        public Meassurement(int Id, double Pressure, double Humidity, double Temperature, DateTime TimeStamp)
        {
            _id = Id;
            _pressure = Pressure;
            _humidity = Humidity;
            _temperature = Temperature;
            _timeStamp = TimeStamp;
        }

        public int Id { get; set; }
        public double Pressure { get; set; }
        public double Humidity { get; set; }
        public double Temperature { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}