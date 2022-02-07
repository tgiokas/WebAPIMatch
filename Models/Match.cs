using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace WebAPIMatch.Models
{
    [Table("Match")]
    public partial class Match
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public int Id { get; set; }

        [StringLength(20)]
        public string Description { get; set; }

        public DateTime MatchDate { get; set; }

        public TimeSpan MatchTime { get; set; }

        [StringLength(20)]
        public string TeamA { get; set; }

        [StringLength(20)]
        public string TeamB { get; set; }

        public int Sport { get; set; }

        [JsonIgnore]
        [InverseProperty("Match")]
        public ICollection<MatchOdd> MatchOdds { get; set; }

        public Match()
        {
            MatchOdds = new HashSet<MatchOdd>();
        }

        public Match(DTOMatch matchdto)
        {
            this.Id = matchdto.Id;
            this.Description = matchdto.Description;
            this.TeamA = matchdto.TeamA;
            this.TeamB = matchdto.TeamB;
            this.Sport = (int)Enum.Parse(typeof(Sports), matchdto.Sport);
            this.MatchDate = matchdto.Date;
            this.MatchTime = matchdto.Time;
            this.MatchOdds = matchdto.MatchOdds.Select(match => new MatchOdd { Id = match.Id, Specifier = match.Specifier, Odd = match.Odd }).ToList();
        }
    }
}
