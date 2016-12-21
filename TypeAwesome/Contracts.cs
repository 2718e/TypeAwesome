using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeAwesome
{
    public interface ITypedJModel { }

    public interface ITypedJsonResult { }

    public interface ITypedJsonResult<TModel> : ITypedJsonResult where TModel: ITypedJModel { }
}
