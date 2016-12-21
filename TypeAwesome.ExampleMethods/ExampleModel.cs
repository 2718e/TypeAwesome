using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeAwesome.ExampleMethods
{
    public class ExampleModel1 : ITypedJModel
    {

        public double Amount { get; set; } 

        public string Desciption { get; set; }

    }

    public class ExampleModel2 : ITypedJModel
    {

        public ExampleModel1[] Order { get; set; }

        public string CustomerName { get; set; }

    }

}
