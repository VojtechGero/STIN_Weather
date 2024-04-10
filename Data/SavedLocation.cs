﻿namespace STIN_Weather.Data
{
    public class SavedLocation
    {
        public double latitude {  get; set; }
        public double longitude { get; set; }
        public string name { get; set; }
        public int id { get; set; }

        public SavedLocation(Coordinates coords, string name)
        {
            this.latitude = coords.latitude;
            this.longitude = coords.longitude;
            this.name = name;
        }
    }
}
