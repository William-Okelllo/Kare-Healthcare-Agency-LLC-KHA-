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
    public class ProfileRepo : Iprofile
    {
        ProfileContext context = new ProfileContext();

        public void Add(Profile profile)
        {
            context.profiles.Add(profile);
            context.SaveChanges();
        }

        public void Edit(Profile profile)
        {
            context.Entry(profile).State = System.Data.Entity.EntityState.Modified;
        }

        public Profile FindById(int Id)
        {
            var result = (from r in context.profiles where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetProfiles()

        { return context.profiles; }

        public void Remove(int Id)

        {
            Profile d = context.profiles.Find(Id);
            context.profiles.Remove(d); context.SaveChanges();

        }
    }
}
