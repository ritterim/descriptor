using System.Collections.Generic;

namespace RimDev.Descriptor
{
    public interface IMethodDescriptor : IDescriptor
    {
        ICollection<IDescriptor> Parameters { get; }
    }
}
