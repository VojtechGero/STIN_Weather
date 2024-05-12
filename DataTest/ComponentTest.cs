using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

using Moq;
using STIN_Weather.Components.Account;
using STIN_Weather.Data;
using STIN_Weather.WeatherReportUtils;

namespace Tests;
[TestClass]
public class ComponentTest
{

    

    [TestMethod]
    public async Task TestCallApiWithHistoricData()
    {
        // Arrange
        
        var mockApi = new Mock<WeatherApi>();
        var coordinates = new Coordinates (34.05, -118.25 );
        var today = DateOnly.FromDateTime(DateTime.Now);
        var dummyData = new List<DailyForecast>
        {
            new DailyForecast("Rainy", today.AddDays(-1), 22, 5.4)
        };
        mockApi.Setup(api => api.requestWeather(It.IsAny<string>())).ReturnsAsync(dummyData);

        // Act
        var (data, dates) = await WeatherUtils.CallApi(mockApi.Object, coordinates, 1);

        // Assert
        Assert.AreEqual(1, data.Count);
        Assert.AreEqual("Rainy", data[0].description);
        Assert.AreEqual(22, data[0].temperatureMax);
        Assert.AreEqual(5.4, data[0].precipitationSum);
        Assert.AreEqual(today.AddDays(-1), data[0].date);
        Assert.IsFalse(dates[0].Contains("Today"));
    }

    [TestMethod]
    public async Task RequestWeatherShouldThrowHttpRequestException()
    {
        var api=new WeatherApi();
        var request = "?latitude=1134.0522&longitude=-99118.2437&daily=temperature_2m_max&daily=weather_code&daily=precipitation_sum";
        await Assert.ThrowsExceptionAsync<HttpRequestException>(() => api.requestWeather(request));
    }

    [TestMethod]
    public void ShowWeatherTable_WhenUseHistoricIsTrue_ReturnsHistoricSeven()
    {
        // Arrange
        var coords = new Coordinates(34.05, -118.25); // Valid coordinates for Los Angeles
        bool useHistoric = true;

        // Act
        var result = WeatherUtils.ShowWeatherTable(coords, useHistoric);

        // Assert
        Assert.AreEqual(7, result.Item2, "The historic value should be 7 when useHistoric is true.");
        Assert.AreEqual(true, result.Item3, "The boolean return should always be true.");
        Assert.IsNotNull(result.Item1, "Coordinates should not be null.");
    }

    [TestMethod]
    public void ShowWeatherTable_WhenUseHistoricIsFalse_ReturnsHistoricZero()
    {
        // Arrange
        var coords = new Coordinates(34.05, -118.25); // Valid coordinates for Los Angeles
        bool useHistoric = false;

        // Act
        var result = WeatherUtils.ShowWeatherTable(coords, useHistoric);

        // Assert
        Assert.AreEqual(0, result.Item2, "The historic value should be 0 when useHistoric is false.");
        Assert.AreEqual(true, result.Item3, "The boolean return should always be true.");
        Assert.IsNotNull(result.Item1, "Coordinates should not be null.");
    }

}
