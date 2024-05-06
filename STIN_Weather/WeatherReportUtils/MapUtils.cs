using LeafletForBlazor;

namespace STIN_Weather.WeatherReportUtils;

public static class MapUtils
{
    
    
    public static RealTimeMap.LoadParameters GetParameters()
    {
        RealTimeMap.LoadParameters parameters = new RealTimeMap.LoadParameters()
        {
            location = new RealTimeMap.Location()
            {
                //liberec
                longitude = 15.07,
                latitude = 50.77,
            },
            zoom_level = 5,
            basemap = new RealTimeMap.Basemap()
            {
                basemap_layers = new List<RealTimeMap.BasemapConfigLayer>
                {
                        new RealTimeMap.BasemapConfigLayer()
                        {
                            url = "https://tiles.stadiamaps.com/tiles/osm_bright/{z}/{x}/{y}{r}.png",
                            attribution = "©Stadia",
                            title = "Terrain",
                            detect_retina = true
                        },
                        new RealTimeMap.BasemapConfigLayer()
                        {
                            url = "https://{s}.tile.thunderforest.com/spinal-map/{z}/{x}/{y}.png",
                            attribution = "©Thunderforest",
                            title = "Metal"
                        }
                }
            }
        };
        return parameters;
    }
    public static RealTimeMap.PointSymbol GetSymbol()
    {
        RealTimeMap.PointSymbol symbol = new RealTimeMap.PointSymbol()
        {
            color = "red",
            fillColor = "yellow",
            radius = 10,
            weight = 3,
            opacity = 1,
            fillOpacity = 1
        };
        return symbol;
    }

    public static double formatLongitude(double value)
    {
        bool neg = value < 0;
        double absVal = Math.Abs(value);
        int overReach = (int)(absVal / 180);
        double newValue = absVal - (overReach * 360);
        if (neg)
        {
            return -newValue;
        }
        return newValue;
    }
}
