using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using WebAPIMatch.Models;

namespace WebAPIMatch.Service
{
    public class MatchOddService : IServiceMatchOdd
    {
        DBEntities dbContext;

        public MatchOddService(DBEntities _db)
        {
            dbContext = _db;
        }

        public DTOMatch GetMatchById(int id)
        {
            try
            {
                var match = dbContext.Matches.Include("MatchOdds").FirstOrDefault(x => x.Id == id);
                if (match != null)
                {
                    return new DTOMatch(match);
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public MatchOdd GetMatchOddById(int id)
        {
            try
            {                
                var matchOdd = dbContext.MatchOdds.FirstOrDefault(x => x.Id == id);
                if (matchOdd != null)
                {
                    return matchOdd;
                }
                return null;                
            }
            catch (Exception)
            {
                throw;
            }
        }

        public MatchOdd AddMatchOdd(int id, DTOMatchOdd matchOddDTO)
        {
            try
            {
                var match = dbContext.Matches.Include("MatchOdds").FirstOrDefault(x => x.Id == id);
                if (match != null)
                {
                    MatchOdd matchOdd = new MatchOdd
                    {
                        MatchId = id,                        
                        Odd = matchOddDTO.Odd,
                        Specifier = matchOddDTO.Specifier
                    };
                    dbContext.MatchOdds.Add(matchOdd);
                    dbContext.SaveChanges();

                    return matchOdd;
                }
                return null;
            }
            catch (DbUpdateException)
            {
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public MatchOdd UpdateMatchOdd(int id, DTOMatchOdd matchOddDTO)
        {
            try
            {               
                var match = dbContext.MatchOdds.FirstOrDefault(x => x.Id == id);
                if (match != null)
                {    
                    match.Odd = matchOddDTO.Odd;
                    match.Specifier = matchOddDTO.Specifier;                   
                    dbContext.MatchOdds.Update(match);
                    dbContext.SaveChanges();

                    return match;
                }                
                return null;
            }
            catch (DbUpdateException)
            {
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public MatchOdd DeleteMatchOdd(int id)
        {
            try
            {
                var matchOdd = dbContext.MatchOdds.FirstOrDefault(x => x.Id == id);
                if (matchOdd != null)
                {
                    dbContext.Entry(matchOdd).State = EntityState.Deleted;
                    dbContext.SaveChanges();

                    return matchOdd;
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }

}
