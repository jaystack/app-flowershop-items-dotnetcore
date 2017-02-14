using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemEndpoints.Models;

namespace App.Flowershop.Items
{
    public class Config
    {
        public string DataApi { get; set; }

        public string CartApi { get; set; }

        public List<Endpoint> hosts
        {
            get; set;
        }
    }
}
