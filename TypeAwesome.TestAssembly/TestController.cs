using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace TypeAwesome.TestAssembly
{
    public class TestController : Controller
    {

        /// <summary>
        /// Test a method with no parameters
        /// </summary>
        /// <returns>A Basic model class</returns>
        public TypedJsonNetResult<SimpleModel> ParameterlessMethod()
        {
            var result = new SimpleModel { Name = "Kittens", Amount = 9001 };
            return new TypedJsonNetResult<SimpleModel>(result);
        }

        /// <summary>
        /// Test passing a parameter, and returning a result that depends on the parameter
        /// </summary>
        /// <param name="inputModel">the parameter</param>
        /// <returns>A testmodel</returns>
        public TypedJsonNetResult<TestModel> OneParameterMethod(SimpleModel inputModel)
        {
            var nestedArray = new NestedArrayModel
            {
                Values = new double[][][] {
                    new double[][] {
                    new double[] {1.1,2.2},
                    new double[] {3.3,4.4}},
                new double[][] {
                    new double[] {5.5,6.6},
                    new double[] {7.7,8.8}}}
            };
            var model = new TestModel
            {
                Food = "Bananas",
                ALotOfStuff = 12345678910,
                Pets = new string[] { "cat", "dog", "bird" },
                SmallNumber = 10,
                HowMany = 12345,
                LargeNumber = 123456789.101112131415161718M,
                Letter = 'A',
                Buoyancy = 12.345f,
                Measurement = 1.38064852e-23,
                Model = inputModel,
                NestedArray = nestedArray
            };
            return new TypedJsonNetResult<TestModel>(model);
        }




    }
}
