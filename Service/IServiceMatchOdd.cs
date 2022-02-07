using System.Collections.Generic;

using WebAPIMatch.Models;

namespace WebAPIMatch.Service
{
    public interface IServiceMatchOdd
    {
        DTOMatch GetMatchById(int id);

        MatchOdd GetMatchOddById(int id);

        MatchOdd AddMatchOdd(int id, DTOMatchOdd matchOddDTO);

        MatchOdd UpdateMatchOdd(int id, DTOMatchOdd matchOddDTO);

        MatchOdd DeleteMatchOdd(int id);
    }
}
