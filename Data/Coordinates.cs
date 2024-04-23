namespace STIN_Weather.Data;

public class Coordinates
{
    public double latitude { get; set; }
    public double longitude { get; set; }
    public Coordinates(double latitude, double longitude)
    {
        if(!IsValidCoords(longitude, latitude))
        {
            throw new ArgumentOutOfRangeException("Invalid coordinates");
        }
        this.latitude = latitude;
        this.longitude = longitude;
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
        return [latitude, longitude];
    }
}
