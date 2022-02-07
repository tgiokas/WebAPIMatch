using System;
using System.Collections.Generic;

namespace WebAPIMatch.Helpers.Swagger
{
    public class SwaggerRequestMatch
    {           
        public string Description { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string TeamA { get; set; }
        public string TeamB { get; set; }
        public string Sport { get; set; }

        public ICollection<SwaggerRequestMatchOdd> MatchOdds { get; set; }
    }
}
