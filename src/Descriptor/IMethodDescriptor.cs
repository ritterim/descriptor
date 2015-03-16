using System.Collections.Generic;

namespace Ritter.Descriptor
{
    public interface IMethodDescriptor : IDescriptor
    {
        ICollection<IDescriptor> Parameters { get; }
    }
}
