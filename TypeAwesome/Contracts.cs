using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeAwesome
{

    /// <summary>
    /// make a model class implement this to indicate it should be exported.
    /// </summary>
    public interface ITypedJModel { }

    /// <summary>
    /// ITypedJsonResult is used internally by the TypedJsonNetResult class to enable the constraint that returned
    /// models must be ITypedJModel
    /// </summary>
    public interface ITypedJsonResult { }

    /// <summary>
    /// ITypedJsonResult is used internally by the TypedJsonNetResult class to enable the constraint that returned
    /// models must be ITypedJModel
    /// </summary>
    public interface ITypedJsonResult<TModel> : ITypedJsonResult where TModel: ITypedJModel { }
}
