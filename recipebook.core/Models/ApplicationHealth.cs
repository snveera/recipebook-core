using System;
using System.Collections.Generic;

namespace recipebook.core.Models
{
    public class ApplicationHealth
    {
        public string Version { get; set; }
        public DateTime CurrentDateTime { get; set; }
        public Dictionary<string, bool> DependencyStatus { get; set; }
        public bool Status { get; set; }
    }
}
