using System;
using System.Collections.Generic;

namespace SEOXML.Models
{
    public class AssemblyController
    {
        public string Controller { get; set; }
        public string Action { get; set; }
        public IEnumerable<Attribute> Attributes { get; set; }
        public string ReturnType { get; set; }
    }
}