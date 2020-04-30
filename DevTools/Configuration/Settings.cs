using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevTools.Configuration
{
    public class Settings
    {
        public Mongo Mongo { get; set; }
        public string JiraAddress { get; set; }
    }
}
