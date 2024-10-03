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
	public  class Benitem_repo : IBenitem
	{
		benitem_context context = new benitem_context();

		public void Add(Benitem benitem)
		{
			context.benitems.Add(benitem);
			context.SaveChanges();
		}

		public void Edit(Benitem benitem)
		{
			context.Entry(benitem).State = System.Data.Entity.EntityState.Modified;
		}

		public Benitem FindById(int Id)
		{
			var result = (from r in context.benitems where r.Id == Id select r).FirstOrDefault();
			return result;
		}

		public IEnumerable GetBenitem()

		{ return context.benitems; }

		public void Remove(int Id)

		{
			Benitem d = context.benitems.Find(Id);
			context.benitems.Remove(d); context.SaveChanges();

		}
	}
}