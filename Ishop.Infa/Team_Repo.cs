using IShop.Core.Interface;
using IShop.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class Team_Repo : Iteam
    {
        Team_Context context = new Team_Context();

        public void Add(Team team)
        {
            context.teams.Add(team);
            context.SaveChanges();
        }

        public void Edit(Team team)
        {
            context.Entry(team).State = System.Data.Entity.EntityState.Modified;
        }

        public Team FindById(int Id)
        {
            var result = (from r in context.teams where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetTeam()

        { return context.teams; }

        public void Remove(int Id)

        {
            Team d = context.teams.Find(Id);
            context.teams.Remove(d); context.SaveChanges();

        }
    }
}