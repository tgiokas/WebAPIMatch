using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace WebAPIMatch.Models
{
    public partial class MatchOdd
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[JsonIgnore]
        public int Id { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(MatchId))]
        public int MatchId { get; set; }

        [StringLength(1)]
        public string Specifier { get; set; }

        [Column(TypeName = "decimal(4, 2)")]
        public decimal Odd { get; set; }

        [JsonIgnore]
        [InverseProperty("MatchOdds")]
        public virtual Match Match { get; set; }

    }
}
