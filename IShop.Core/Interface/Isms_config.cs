using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface Isms_config
    {
        void Add(Sms_configs sms_Configs);
        void Edit(Sms_configs sms_Configs);
        void Remove(int Id);
        IEnumerable GetSms_config(); Sms_configs FindById(int Id);
    }
}
