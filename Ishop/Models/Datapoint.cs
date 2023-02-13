using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Ishop.Models
{
	[DataContract]
	public class DataPoint
	{
		public int x { get; set; }
		public int y { get; set; }
	}
}