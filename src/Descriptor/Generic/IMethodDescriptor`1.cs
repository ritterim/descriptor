using System;
using System.Linq.Expressions;

namespace Ritter.Descriptor.Generic
{
    public interface IMethodDescriptor<T> : IMethodDescriptor
    {
        IMethodDescriptor<T> Parameter<TProperty>(
            Expression<Func<T, TProperty>> parameter,
            string description = null,
            string type = null);

        IMethodDescriptor<T> SetDescription(string description);
        IMethodDescriptor<T> SetName(string name);
        IMethodDescriptor<T> SetType(string type);
    }
}
