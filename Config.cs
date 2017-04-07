using System.Collections.Generic;
using SystemEndpointsDotnetCore.Models;

namespace App.Flowershop.Items
{
    public class Config
    {
        public string DataApi { get; set; }

        public string CartApi { get; set; }

        public List<Endpoint> hosts { get; set; } = new List<Endpoint>();
    }
}
