using NUnit.Framework;
using RestSharp;
using RestSharp.Serialization.Json;

namespace DataDrivenExcelTests
{
    public class DataDrivenTests
    {
        const string zippopotamusUrl = "https://api.zippopotam.us/";

        [TestCase("BG", "5000", "Veliko Turnovo")]
        [TestCase("ES", "28025", "Madrid")]
        [TestCase("BG", "9000", "Varna")]
        [TestCase("BG", "1000", "Sofija")]
        [TestCase("CA", "M5S", "Toronto")]
        public void ZippopotamusApiTests(string countryCode, string zipCode, string city)
        {
            // Arrange
            var restClient = new RestClient(zippopotamusUrl);
            var httpRequest = new RestRequest(countryCode + "/" + zipCode);

            // Act
            var httpResponce = restClient.Execute(httpRequest);
            var location = new JsonDeserializer().Deserialize<Location>(httpResponce);

            // Assert
            StringAssert.Contains(city, location.Places[0].PlaceName);
        }

    }
}