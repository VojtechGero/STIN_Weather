namespace STIN_Weather.Data;

public class Coordinates
{
    public double latitude { get; set; }
    public double longitude { get; set; }
    public Coordinates(double latitude, double longitude)
    {
        
        this.latitude = latitude;
        this.longitude = formatLongitude(longitude);
    }

    private double formatLongitude(double value)
    {
        bool neg=value<0;
        double newValue = Math.Abs(value);
        while (newValue > 180)
        {
            newValue -= 360;
        }
        if (neg)
        {
            return -newValue;
        }
        return newValue;
        
    }

    public double[] toArray()
    {
        return [latitude, longitude];
    }
}
