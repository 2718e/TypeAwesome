using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeAwesome.TestAssembly
{
    
    public class SimpleModel : ITypedJModel
    {
        public string Name { get; set; }

        public double Amount { get; set; }
    }

    /// <summary>
    /// 3D array so we can test if nested arrays are mapped
    /// </summary>
    public class NestedArrayModel : ITypedJModel
    {
        public double[][][] Values { get; set; }
    }

    /// <summary>
    /// Want this class to have all the types that should be mappable, so one test can check it works
    /// </summary>
    public class TestModel : ITypedJModel
    {

        // Check arrays of arrays work
        public NestedArrayModel NestedArray { get; set; }

        // check that a nested Model works
        public SimpleModel Model { get; set; }

        // check a normal array works
        public string[] Pets { get; set; }

        // Check the primitives work
        public string Food { get; set; }
        // actually - possibly should disallow decimal seeing as there would be precision loss in conversion...
        public decimal LargeNumber { get; set; }
        public int HowMany { get; set; }
        public float Buoyancy { get; set; }
        public double Measurement { get; set; }
        public byte SmallNumber { get; set; }
        public char Letter { get; set; }
        public long ALotOfStuff { get; set; }


    }



}
