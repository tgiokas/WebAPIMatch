using System.Collections.Generic;

using WebAPIMatch.Models;

namespace WebAPIMatch.Service
{
    public interface IServiceMatch
    {
        IEnumerable<DTOMatch> GetMatches();

        DTOMatch GetMatchById(int id);

        Match AddMatch(DTOMatch matchDTO);

        Match UpdateMatch(int id, DTOMatch matchDTO);

        Match DeleteMatch(int id);
    }
}
