using STIN_Weather.Data;
using STIN_Weather.Data.OpenMeteoData;
using STIN_Weather.WeatherReportUtils;
namespace Tests;

[TestClass]
public class MockLessTest
{

    [DataTestMethod]
    [DataRow(190, -170)] // Positive value beyond 180
    [DataRow(-190, 170)] // Negative value beyond -180
    [DataRow(0, 0)] // Zero
    [DataRow(180, 180)] // Positive maximum value
    [DataRow(360, 0)] // Positive value equal to 360
    [DataRow(-360, 0)] // Negative value equal to -360
    [DataRow(540, 180)] // Positive value greater than 360
    [DataRow(720, 0)] // Positive value greater than 360 and a multiple of 360
    public void FormatLongitude_ShouldReturnCorrectValue(double input, double expected)
    {
        // Arrange

        // Act
        double result = Coordinates.FormatLongitude(input);

        // Assert
        Assert.AreEqual(expected, result,$"{expected} se nerovná {result}");
    }

    [TestMethod]
    public void Constructor_ValidCoordinates_ShouldInitialize()
    {
        // Arrange
        double latitude = 45.0;
        double longitude = 90.0;

        // Act
        Coordinates coords = new Coordinates(latitude, longitude);

        // Assert
        Assert.AreEqual(latitude, coords.Latitude);
        Assert.AreEqual(longitude, coords.Longitude);
    }

    [DataTestMethod]
    [DataRow(0, "http://openweathermap.org/img/wn/01d@2x.png")]
    [DataRow(1, "http://openweathermap.org/img/wn/01d@2x.png")]
    [DataRow(2, "http://openweathermap.org/img/wn/02d@2x.png")]
    [DataRow(3, "http://openweathermap.org/img/wn/03d@2x.png")]
    [DataRow(45, "http://openweathermap.org/img/wn/50d@2x.png")]
    [DataRow(48, "http://openweathermap.org/img/wn/50d@2x.png")]
    [DataRow(51, "http://openweathermap.org/img/wn/09d@2x.png")]
    [DataRow(53, "http://openweathermap.org/img/wn/09d@2x.png")]
    [DataRow(55, "http://openweathermap.org/img/wn/09d@2x.png")]
    [DataRow(56, "http://openweathermap.org/img/wn/09d@2x.png")]
    [DataRow(57, "http://openweathermap.org/img/wn/09d@2x.png")]
    [DataRow(61, "http://openweathermap.org/img/wn/10d@2x.png")]
    [DataRow(63, "http://openweathermap.org/img/wn/10d@2x.png")]
    [DataRow(65, "http://openweathermap.org/img/wn/10d@2x.png")]
    [DataRow(66, "http://openweathermap.org/img/wn/10d@2x.png")]
    [DataRow(67, "http://openweathermap.org/img/wn/10d@2x.png")]
    [DataRow(71, "http://openweathermap.org/img/wn/13d@2x.png")]
    [DataRow(73, "http://openweathermap.org/img/wn/13d@2x.png")]
    [DataRow(75, "http://openweathermap.org/img/wn/13d@2x.png")]
    [DataRow(77, "http://openweathermap.org/img/wn/13d@2x.png")]
    [DataRow(80, "http://openweathermap.org/img/wn/09d@2x.png")]
    [DataRow(81, "http://openweathermap.org/img/wn/09d@2x.png")]
    [DataRow(82, "http://openweathermap.org/img/wn/09d@2x.png")]
    [DataRow(85, "http://openweathermap.org/img/wn/13d@2x.png")]
    [DataRow(86, "http://openweathermap.org/img/wn/13d@2x.png")]
    [DataRow(95, "http://openweathermap.org/img/wn/11d@2x.png")]
    [DataRow(96, "http://openweathermap.org/img/wn/11d@2x.png")]
    [DataRow(99, "http://openweathermap.org/img/wn/11d@2x.png")]
    public void GetImage_ReturnsCorrectUrl(int weatherCode, string expectedUrl)
    {
        // Arrange
        var actualUrl = WeatherUtils.GetImage(weatherCode);

        // Assert
        Assert.AreEqual(expectedUrl, actualUrl);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void Constructor_InvalidLatitude_ShouldThrowArgumentOutOfRangeException()
    {
        // Arrange
        double latitude = 91.0;
        double longitude = 50.0;

        // Act
        Coordinates coords = new Coordinates(latitude, longitude);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void Constructor_InvalidLongitude_ShouldThrowArgumentOutOfRangeException()
    {
        // Arrange
        double latitude = 45.0;
        double longitude = 190.0;

        // Act
        Coordinates coords = new Coordinates(latitude, longitude);
    }

    [TestMethod]
    public void ToArray_ShouldReturnCorrectArray()
    {
        // Arrange
        double latitude = 30.0;
        double longitude = 60.0;
        Coordinates coords = new Coordinates(latitude, longitude);

        // Act
        double[] result = coords.toArray();

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(2, result.Length);
        Assert.AreEqual(latitude, result[0]);
        Assert.AreEqual(longitude, result[1]);
    }

    [TestMethod]
    [DataRow(0, "Clear sky")]
    [DataRow(1, "Mainly clear")]
    [DataRow(2, "Partly cloudy")]
    [DataRow(3, "Overcast")]
    [DataRow(45, "Fog")]
    [DataRow(48, "Depositing rime fog")]
    [DataRow(51, "Light drizzle")]
    [DataRow(53, "Moderate drizzle")]
    [DataRow(55, "Dense drizzle")]
    [DataRow(56, "Light freezing Drizzle")]
    [DataRow(57, "Dense Freezing Drizzle")]
    [DataRow(61, "Slight rain")]
    [DataRow(63, "Moderate Rain")]
    [DataRow(65, "Heavy rain")]
    [DataRow(66, "Light freezing Rain")]
    [DataRow(67, "Heavy Freezing Rain")]
    [DataRow(71, "Slight snow fall")]
    [DataRow(73, "Moderate snow fall")]
    [DataRow(75, "Heavy snow fall")]
    [DataRow(77, "Snow grains")]
    [DataRow(80, "Slight rain showers")]
    [DataRow(81, "Moderate rain showers")]
    [DataRow(82, "Violent rain showers")]
    [DataRow(85, "Slight snow showers")]
    [DataRow(86, "Heavy snow showers")]
    [DataRow(95, "Thunderstorm")]
    [DataRow(96, "Thunderstorm with slight hail")]
    [DataRow(99, "Thunderstorm with heavy hail")]
    [DataRow(100, "Unknown")]
    public void TestParseCode(int code, string expectedDescription)
    {
        string actualDescription = WeatherUtils.ParseCode(code);
        Assert.AreEqual(expectedDescription, actualDescription);
    }

    [TestMethod]
    public void GetUniqueName_ReturnsModifiedName_WhenNameExists()
    {
        // Arrange
        string name = "CentralPark";
        var dummyCoords = new Coordinates(0, 0);
        List<SavedLocation> locations = new List<SavedLocation>
        {
            new SavedLocation(dummyCoords,"CentralPark",1),
            new SavedLocation(dummyCoords,"Yellowstone", 2),
            new SavedLocation(dummyCoords,"CentralPark", 3)
        };

        string expected = "CentralPark(1)";

        // Act
        var result = WeatherUtils.GetUniqueName(name, locations);

        // Assert
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void RemoveLocation_EmptyList_ReturnsEmptyList()
    {
        // Arrange
        var locations = new List<SavedLocation>();

        // Act
        var result = WeatherUtils.removeLocation(1, locations);

        // Assert
        Assert.AreEqual(0, result.Count);
    }

    [TestMethod]
    public void RemoveLocation_SingleItem_ReturnsEmptyList()
    {
        // Arrange
        var location = new SavedLocation(new Coordinates(10.0, 20.0), "Location 1", 1);
        var locations = new List<SavedLocation> { location };

        // Act
        var result = WeatherUtils.removeLocation(1, locations);

        // Assert
        Assert.AreEqual(0, result.Count);
    }

    public void NewUniqueNameTest()
    {
        // Arrange
        var location1 = new SavedLocation(new Coordinates(10.0, 20.0), "Location 1", 1);
        var location2 = new SavedLocation(new Coordinates(30.0, 40.0), "Location 2", 2);
        var location3 = new SavedLocation(new Coordinates(50.0, 60.0), "Location 3", 3);
        var locations = new List<SavedLocation> { location1, location2, location3 };
        string name = "Location 1";
        string expected = "Location 1(1)";


        // Act
        string newName = WeatherUtils.GetUniqueName(name, locations);


        // Assert
        Assert.AreEqual(expected, newName);
    }
    [TestMethod]
    public void dailyUnitsTest()
    {
        // Arrange
        var units = new DailyUnits();
        units.precipitation_sum = "1";
        units.temperature_2m_max = "2";
        units.time = "3";
        units.weather_code = "4";

        // Assert
        Assert.AreEqual(units.weather_code, "4");
        Assert.AreEqual(units.temperature_2m_max, "2");
        Assert.AreEqual(units.time, "3");
        Assert.AreEqual(units.precipitation_sum, "1");
    }


    [TestMethod]
    public void NewUniqueNameTestFromEmpty()
    {
        // Arrange
        var location1 = new SavedLocation(new Coordinates(10.0, 20.0), "Location 1", 1);
        var location2 = new SavedLocation(new Coordinates(30.0, 40.0), "Location 2", 2);
        var location3 = new SavedLocation(new Coordinates(50.0, 60.0), "Location 3", 3);
        var locations = new List<SavedLocation> { location1, location2, location3 };
        string name = "";
        string expected = "New location";


        // Act
        string newName=WeatherUtils.GetUniqueName(name, locations);


        // Assert
        Assert.AreEqual(expected, newName);
    }

    [TestMethod]
    public void RemoveLocation_MultipleItems_RemovesCorrectItem()
    {
        // Arrange
        var location1 = new SavedLocation(new Coordinates(10.0, 20.0), "Location 1", 1);
        var location2 = new SavedLocation(new Coordinates(30.0, 40.0), "Location 2", 2);
        var location3 = new SavedLocation(new Coordinates(50.0, 60.0), "Location 3", 3);
        var locations = new List<SavedLocation> { location1, location2, location3 };

        // Act
        var result = WeatherUtils.removeLocation(2, locations);

        // Assert
        Assert.AreEqual(2, result.Count);
        Assert.AreEqual(1, result[0].id);
        Assert.AreEqual(2, result[1].id);
        Assert.AreEqual("Location 1", result[0].name);
        Assert.AreEqual("Location 3", result[1].name);
    }


    [TestMethod]
    public void GetUniqueName_ReturnsOriginalName_WhenNameDoesNotExist()
    {
        // Arrange
        string name = "GrandCanyon";
        var dummyCoords = new Coordinates(0, 0);
        List<SavedLocation> locations = new List<SavedLocation>
        {
            new SavedLocation(dummyCoords,"CentralPark",1),
            new SavedLocation(dummyCoords,"Yellowstone", 2),
        };

        string expected = "GrandCanyon";

        // Act
        var result = WeatherUtils.GetUniqueName(name, locations);

        // Assert
        Assert.AreEqual(expected, result);
    }
    [TestMethod]
    public void RequestBuilder_CreatesCorrectInitialRequest_WithCoordinates()
    {
        // Arrange
        var coords = new Coordinates(40.7128, -74.0060);
        string expected = "?latitude=40.7128&longitude=-74.006";

        // Act
        var builder = new RequestBuilder(coords);
        var result = builder.GetRequest();

        // Assert
        Assert.AreEqual(expected, result);
    }
    [TestMethod]
    public void RequestBuilder_AppendsMultipleParameters_Correctly()
    {
        // Arrange
        var coords = new Coordinates (34.0522, -118.2437);
        string expected = "?latitude=34.0522&longitude=-118.2437&daily=temperature_2m_max&daily=weather_code&daily=precipitation_sum";

        // Act
        var builder = new RequestBuilder(coords)
            .DailyTemperatureMax()
            .DailyWeatherCode()
            .DailyPrecipitationSum();
        var result = builder.GetRequest();

        // Assert
        Assert.AreEqual(expected, result);
    }
    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void RequestBuilder_ThrowsException_WhenHistoricDaysAreNegative()
    {
        // Arrange
        var coords = new Coordinates (34.0522, -118.2437);
        var builder = new RequestBuilder(coords);

        // Act
        builder.HistoricDays(-1);  // This should throw an exception
    }
}