using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using WebAPIMatch.Models;

namespace WebAPIMatch.Service
{
    public class ServiceMatch : IServiceMatch
    {
        DBEntities dbContext;

        public ServiceMatch(DBEntities _db)
        {
            dbContext = _db;
        }

        public IEnumerable<DTOMatch> GetMatches()
        {
            try
            {                
                var matches = dbContext.Matches.Include(x => x.MatchOdds).ToList();

                return (from match in matches
                        select new DTOMatch(match)).ToList();                       
            }
            catch (Exception)
            {
                throw;
            }
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

        public Match AddMatch(DTOMatch matchdto)
        {
            try
            {
                if (matchdto != null)
                {
                    Match match = new Match(matchdto);                    
                    dbContext.Matches.Add(match);
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

        public Match UpdateMatch(int id, DTOMatch matchdto)
        {
            try
            {
                var match = dbContext.Matches.Include("MatchOdds").FirstOrDefault(x => x.Id == id);
                if (match != null)
                {
                    // Find Removed MatchOdds from Specifiers 
                    List<string> currentMatchOdd = dbContext.MatchOdds
                        .Where(x => x.MatchId == id)
                        .Select(x => x.Specifier)
                        .ToList();
                    
                    List<string> newMatchOdd = matchdto.MatchOdds
                        .Select(Odd => Odd.Specifier)
                        .ToList();
                     
                    List<string> MatchOdds4delelete = currentMatchOdd
                        .Except(newMatchOdd)
                        .ToList();

                    // Delete Removed MatchOdds
                    foreach (var matchOdd4delete in MatchOdds4delelete)
                    {
                        MatchOdd MatchOdd4delete = dbContext.MatchOdds
                            .Where(x => x.MatchId == id && x.Specifier == matchOdd4delete)
                            .Single();
                        dbContext.Entry(MatchOdd4delete).State = EntityState.Deleted;
                    }

                    foreach (var matchOddDTO in matchdto.MatchOdds)
                    {
                        MatchOdd matchOdd = new MatchOdd
                        {
                            MatchId = id,
                            Odd = matchOddDTO.Odd,
                            Specifier = matchOddDTO.Specifier
                        };

                        // Insert New MatchOdd
                        if (match.MatchOdds.All(x => x.Specifier != matchOddDTO.Specifier))
                        {
                            dbContext.Entry(matchOdd).State = EntityState.Added;
                            dbContext.SaveChanges();
                            continue;
                        }

                        // Update existing MatchOdd      
                        var existingOdd = match.MatchOdds.FirstOrDefault(x => x.Specifier == matchOddDTO.Specifier);
                        if (existingOdd != null)
                        {                           
                            existingOdd.Odd = matchOddDTO.Odd;
                            dbContext.SaveChanges();
                            continue;
                        }
                    }

                    // Update Match 
                    match.Description = matchdto.Description;
                    match.MatchDate = matchdto.Date;
                    match.MatchTime = matchdto.Time;                   
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

        public Match DeleteMatch(int id)
        {
            try
            {
                var match = dbContext.Matches.FirstOrDefault(x => x.Id == id);
                if (match != null)
                {
                    dbContext.Entry(match).State = EntityState.Deleted;
                    dbContext.SaveChanges();

                    return match;
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
