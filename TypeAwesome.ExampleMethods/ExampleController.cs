using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace TypeAwesome.ExampleMethods
{
    public class ExampleController : Controller
    {

        public TypedJsonNetResult<ExampleModel1> ExampleParameterlessMethod()
        {
            var result = new ExampleModel1 { Desciption = "Bacon", Amount = 9001 };
            return new TypedJsonNetResult<ExampleModel1>(result);
        }

        public TypedJsonNetResult<ExampleModel2> ExampleOneParameterMethod(ExampleModel1 inputModel)
        {
            var model = new ExampleModel2 {
                CustomerName = "A Person",
                Order = new ExampleModel1[] {
                    new ExampleModel1 { Desciption = "Toast", Amount = 2 },
                    new ExampleModel1 { Desciption = "Egg", Amount = 1 },
                    inputModel
                }
            };
            return new TypedJsonNetResult<ExampleModel2>(model);
        }




    }
}
