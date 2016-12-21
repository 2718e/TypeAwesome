using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Web.Mvc;

namespace TypeAwesome
{
    public class TypedJsonNetResult<TReturn> : ActionResult, ITypedJsonResult<TReturn> where TReturn : ITypedJModel
    {

        private readonly TReturn unserialized; 
            
        public override void ExecuteResult(ControllerContext context)
        {
            var response = context.HttpContext.Response;
            response.ContentType = "application/json";
            var serialized = JsonConvert.SerializeObject(unserialized);
            response.Write(serialized);
        }

        public TypedJsonNetResult (TReturn objectToSerialize) {
            this.unserialized = objectToSerialize;
        }

}

}
