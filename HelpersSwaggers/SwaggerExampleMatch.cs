using Swashbuckle.AspNetCore.Filters;

namespace WebAPIMatch.Helpers.Swagger
{
    public class SwaggerExampleMatch : IExamplesProvider<SwaggerRequestMatch>
    {
        public SwaggerRequestMatch GetExamples()
        {
            return new SwaggerRequestMatch()
            {
                Description = "OSFP-PAO",
                Date = "2022-02-20",
                Time = "22:30",
                TeamA = "OSFP",
                TeamB = "PAO",
                Sport = "Football",
                MatchOdds = new[] { new SwaggerRequestMatchOdd{ Specifier = "1", Odd = "25.00" },
                                    new SwaggerRequestMatchOdd{ Specifier = "2", Odd = "25.00" },
                                    new SwaggerRequestMatchOdd{ Specifier = "X", Odd = "50.00" }}
            };
        }
    }
}
