using IShop.Core;
using IShop.Core.Interface;
using System.Collections;
using System.Linq;

namespace Ishop.Infa
{
    public class Sms_config_Repo : Isms_config
    {
        Sms_configs_Context context = new Sms_configs_Context();

        public void Add(Sms_configs sms_Configs)
        {
            context.sms_Configs.Add(sms_Configs);
            context.SaveChanges();
        }

        public void Edit(Sms_configs sms_Configs)
        {
            context.Entry(sms_Configs).State = System.Data.Entity.EntityState.Modified;
        }

        public Sms_configs FindById(int Id)
        {
            var result = (from r in context.sms_Configs where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetSms_config()

        { return context.sms_Configs; }

        public void Remove(int Id)

        {
            Sms_configs d = context.sms_Configs.Find(Id);
            context.sms_Configs.Remove(d); context.SaveChanges();

        }
    }
}
