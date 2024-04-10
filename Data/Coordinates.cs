namespace STIN_Weather.Data;

public class Coordinates
{
    public double latitude { get; set; }
    public double longitude { get; set; }
    public Coordinates(double latitude, double longitude)
    {
        
        this.latitude = latitude;
        this.longitude = longitude;
    }

    public double[] toArray()
    {
        return [latitude, longitude];
    }
}
