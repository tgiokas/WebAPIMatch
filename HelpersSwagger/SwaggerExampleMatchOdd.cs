using Swashbuckle.AspNetCore.Filters;

namespace WebAPIMatch.Helpers.Swagger
{
    public class SwaggerExampleMatchOdd : IExamplesProvider<SwaggerRequestMatchOdd>
    {
        public SwaggerRequestMatchOdd GetExamples()
        {
            return new SwaggerRequestMatchOdd { Specifier = "X", Odd = "80.00" };
        }
    }
}
