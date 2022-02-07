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
                var match = dbContext.Matches.FirstOrDefault(x => x.Id == id);
                if (match != null)
                {
                    match = new Match(matchdto);                 
                    dbContext.Entry(match).State = EntityState.Modified;
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
