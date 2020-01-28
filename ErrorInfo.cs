using System.ComponentModel;

namespace CubeSatCommSim.Model
{
    public class Error
    {
        public int id { get; set; }
        public string description { get; set; }
        public bool isFatal { get; set; }
    }
}