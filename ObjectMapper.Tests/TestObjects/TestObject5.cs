using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectMapper.Tests.TestObjects
{
    public class TestObject5
    {
        [Mapping("Property1")]
        public string MyPropertyA { get; set; }

        [Mapping("Property2")]
        public string MyPropertyB { get; set; }

        [Mapping("Property3")]
        public int MyPropertyC { get; set; }
    }
}
