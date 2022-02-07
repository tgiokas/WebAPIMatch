using System;
using System.Linq;
using System.Collections.Generic;

namespace WebAPIMatch.Models
{
    public class DTOMatch
    { 
        public int Id { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan Time { get; set; }

        public string TeamA { get; set; }

        public string TeamB { get; set; }

        public string Sport { get; set; }       

        public ICollection<DTOMatchOdd> MatchOdds { get; set; }

        public DTOMatch() { }

        public DTOMatch(Match match)
        {
            this.Id = match.Id;
            this.Description = match.Description;
            this.TeamA = match.TeamA;
            this.TeamB = match.TeamB;
            this.Sport = Enum.GetName(typeof(Sports), match.Sport).ToString();
            this.Date = match.MatchDate;
            this.Time = match.MatchTime;
            this.MatchOdds = match.MatchOdds.Select(matchodd => new DTOMatchOdd {Id=matchodd.Id, Specifier = matchodd.Specifier, Odd = matchodd.Odd }).ToList();
        }
    }

}
