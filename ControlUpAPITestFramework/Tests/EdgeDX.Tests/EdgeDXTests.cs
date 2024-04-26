using ControlUpAPITestFramework.HttpHelpers;
using ControlUpAPITestFramework.Models.API.CurrentAveragePrice;
using ControlUpAPITestFramework.Models.API.TickerPriceChangeStats;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace ControlUpAPITestFramework.Tests.EdgeDX.Tests
{
    public class EdgeDXTests : IAsyncLifetime
    {
        private string _accessKeyToken;

        private static readonly string GetAllTickerPriceChangeStatisticsEndpoint = $"{ApiTestConfiguration.Get().Url}ticker/24hr";
        private static readonly string GetSingleACurrencyAveragePriceEndpoint = $"{ApiTestConfiguration.Get().Url}avgPrice?symbol=";

        public async Task InitializeAsync()
        {
            // Retrieve Access Key for API Authentication
            _accessKeyToken = $"{ApiTestConfiguration.Get().AccessKey}";
        }

        /// <summary>
        /// Thinscale - GET - Largest PriceChangePercent - Happy Path - Valid Request
        /// Endpoint: GET {base_url}/ticker/24hr
        /// </summary>
        /// <returns></returns>

        [Fact]
        public async Task GetLargestPriceChangePercentSuccess()
        {
            // Arrange - Make GET Request to endpoint: /ticker/24hr
            var jsonResponse = await ApiTestBase.MakeGetRequestAsyncList<GetTickerPriceChangeStatsResponse>(GetAllTickerPriceChangeStatisticsEndpoint, _accessKeyToken, HttpStatusCode.OK);

            // Act - Retrieve the largest PriceChangePercent from the JSON Response
            var largestPriceChangePercent = jsonResponse.MaxBy(x => x.PriceChangePercent);

            //Assert
            Assert.True(largestPriceChangePercent is not null);
        }

        /// <summary>
        /// Thinscale - GET - Second Largest PriceChangePercent - Happy Path - Valid Request
        /// Endpoint: GET {base_url}/ticker/24hr
        /// </summary>
        /// <returns></returns>
        
        [Fact]
        public async Task GetSecondLargestPriceChangePercentSuccess()
        {
            // Arrange - Make GET Request to endpoint: /ticker/24hr
            var jsonResponse = await ApiTestBase.MakeGetRequestAsyncList<GetTickerPriceChangeStatsResponse>(GetAllTickerPriceChangeStatisticsEndpoint, _accessKeyToken, HttpStatusCode.OK);

            // Act - Retrieve the second largest PriceChangePercent from the JSON Response
            var secondLargestPriceChangePercent = jsonResponse.OrderByDescending(item => item.PriceChangePercent).Skip(1).FirstOrDefault();

            //Assert
            Assert.True(secondLargestPriceChangePercent is not null);
        }

        /// <summary>
        /// Thinscale - GET - Third Largest PriceChangePercent - Happy Path - Valid Request
        /// Endpoint: GET {base_url}/ticker/24hr
        /// </summary>
        /// <returns></returns>
        
        [Fact]
        public async Task GetThirdLargestPriceChangePercentSuccess()
        {
            // Arrange - Make GET Request to endpoint: /ticker/24hr
            var jsonResponse = await ApiTestBase.MakeGetRequestAsyncList<GetTickerPriceChangeStatsResponse>(GetAllTickerPriceChangeStatisticsEndpoint, _accessKeyToken, HttpStatusCode.OK);

            // Act - Retrieve the third largest PriceChangePercent from the JSON Response
            var thirdLargestPriceChangePercent = jsonResponse.OrderByDescending(item => item.PriceChangePercent).Skip(2).FirstOrDefault();

            //Assert
            Assert.True(thirdLargestPriceChangePercent is not null);
        }

        /// <summary>
        /// Thinscale - GET - Get Average Price For Largest Price Change Percent Currency - Happy Path - Valid Request
        /// Endpoint: GET {base_url}/avgPrice?symbol={currency}
        /// </summary>
        /// <returns></returns>
        
        [Fact]
        public async Task GetAveragePriceForLargestPriceChangePercentSuccess()
        {
            // Arrange - Make GET Request to endpoint: /ticker/24hr
            var jsonResponse = await ApiTestBase.MakeGetRequestAsyncList<GetTickerPriceChangeStatsResponse>(GetAllTickerPriceChangeStatisticsEndpoint, _accessKeyToken, HttpStatusCode.OK);
            
            // Retrieve the largest PriceChangePercent from the JSON Response
            var largestPriceChangePercent = jsonResponse.MaxBy(x => x.PriceChangePercent);

            // Retrieve the currency symbol from the largest PriceChangePercent JSON Response
            var currency = largestPriceChangePercent.Symbol;

            // Make GET request to the Average Price endpoint passing in the currency symbol extracted above
            var averagePriceResponse = await ApiTestBase.MakeGetRequestAsync<GetCurrentAveragePriceResponse>($"{GetSingleACurrencyAveragePriceEndpoint}{currency}", _accessKeyToken, HttpStatusCode.OK);

            // Act - averagePriceResponse
            Console.WriteLine(averagePriceResponse);

            //Assert
            Assert.True(averagePriceResponse is not null);
        }

        /// <summary>
        /// Thinscale - GET - Get Average Price For Second Largest Price Change Percent Currency - Happy Path - Valid Request
        /// Endpoint: GET {base_url}/avgPrice?symbol={currency}
        /// </summary>
        /// <returns></returns>
        
        [Fact]
        public async Task GetAveragePriceForSecondLargestPriceChangePercentSuccess()
        {
            // Arrange - Make GET Request to endpoint: /ticker/24hr
            var jsonResponse = await ApiTestBase.MakeGetRequestAsyncList<GetTickerPriceChangeStatsResponse>(GetAllTickerPriceChangeStatisticsEndpoint, _accessKeyToken, HttpStatusCode.OK);

            // Retrieve the second largest PriceChangePercent from the JSON Response
            var secondLargestPriceChangePercent = jsonResponse.OrderByDescending(item => item.PriceChangePercent).Skip(1).FirstOrDefault();

            // Retrieve the currency symbol from the second largest PriceChangePercent JSON Response
            var currency = secondLargestPriceChangePercent.Symbol;

            // Make GET request to the Average Price endpoint passing in the currency symbol extracted above
            var averagePriceResponse = await ApiTestBase.MakeGetRequestAsync<GetCurrentAveragePriceResponse>($"{GetSingleACurrencyAveragePriceEndpoint}{currency}", _accessKeyToken, HttpStatusCode.OK);

            // Act - Print averagePriceResponse
            Console.WriteLine(averagePriceResponse);

            //Assert
            Assert.True(averagePriceResponse is not null);
        }

        /// <summary>
        /// Thinscale - GET - Get Average Price For Third Largest Price Change Percent Currency - Happy Path - Valid Request
        /// Endpoint: GET {base_url}/avgPrice?symbol={currency}
        /// </summary>
        /// <returns></returns>
        
        [Fact]
        public async Task GetAveragePriceForThirdLargestPriceChangePercentSuccess()
        {
            // Arrange - Make GET Request to endpoint: /ticker/24hr
            var jsonResponse = await ApiTestBase.MakeGetRequestAsyncList<GetTickerPriceChangeStatsResponse>(GetAllTickerPriceChangeStatisticsEndpoint, _accessKeyToken, HttpStatusCode.OK);

            // Retrieve the third largest PriceChangePercent from the JSON Response
            var thirdLargestPriceChangePercent = jsonResponse.OrderByDescending(item => item.PriceChangePercent).Skip(1).FirstOrDefault();

            // Retrieve the currency symbol from the third largest PriceChangePercent JSON Response
            var currency = thirdLargestPriceChangePercent.Symbol;

            // Make GET request to the Average Price endpoint passing in the currency symbol extracted above
            var averagePriceResponse = await ApiTestBase.MakeGetRequestAsync<GetCurrentAveragePriceResponse>($"{GetSingleACurrencyAveragePriceEndpoint}{currency}", _accessKeyToken, HttpStatusCode.OK);

            // Act - Print averagePriceResponse
            Console.WriteLine(averagePriceResponse);

            //Assert
            Assert.True(averagePriceResponse is not null);
        }
        // Dispose - part of XUnit
        Task IAsyncLifetime.DisposeAsync()
        {
            return Task.CompletedTask;
        }
        public Task DisposeAsync => Task.CompletedTask;
    }
}
