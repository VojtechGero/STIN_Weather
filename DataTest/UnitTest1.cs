using STIN_Weather.Data;
namespace DataTest;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void Constructor_ValidCoordinates_ShouldInitialize()
    {
        // Arrange
        double latitude = 45.0;
        double longitude = 90.0;

        // Act
        Coordinates coords = new Coordinates(latitude, longitude);

        // Assert
        Assert.AreEqual(latitude, coords.latitude);
        Assert.AreEqual(longitude, coords.longitude);
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
}