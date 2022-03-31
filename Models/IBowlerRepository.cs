using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bowlers.Models
{
    public interface IBowlerRepository
    {
        IQueryable<Bowler> Bowlers { get; }

        public void SaveBowler(Bowler b);

        public void CreateBowler(Bowler b);

        public void DeleteBowler(Bowler b);
    }
}
