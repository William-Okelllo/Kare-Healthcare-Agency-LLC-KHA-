using System.Runtime.Serialization;

namespace Ishop.Models
{
    [DataContract]
    public class DataPoint
    {
        public int x { get; set; }
        public int y { get; set; }
    }
}