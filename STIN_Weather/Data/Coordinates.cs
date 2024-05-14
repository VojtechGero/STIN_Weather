namespace STIN_Weather.Data;

public class Coordinates
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public Coordinates(double Latitude, double Longitude)
    {
        if(!IsValidCoords(Longitude, Latitude))
        {
            throw new ArgumentOutOfRangeException("Invalid coordinates");
        }
        this.Latitude = Latitude;
        this.Longitude = Longitude;
    }

    public static double FormatLongitude(double value)
    {
        double normalizedValue = value % 360;
        if (normalizedValue > 180)
        {
            normalizedValue -= 360;
        }
        else if (normalizedValue <= -180)
        {
            normalizedValue += 360;
        }
        return normalizedValue;
    }

    private bool IsValidCoords(double longitude, double latitude)
    {
        if (longitude < -180 || longitude > 180)
        {
            return false;
        }
        if (latitude < -90 || latitude > 90)
        {
            return false;
        }
        return true;
    }
    public double[] toArray()
    {
        return [Latitude, Longitude];
    }
}
