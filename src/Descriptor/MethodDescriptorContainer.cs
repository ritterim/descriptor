using System.Collections.Generic;

namespace RimDev.Descriptor
{
    public class MethodDescriptorContainer : DescriptorContainer, IMethodDescriptor
    {
        public MethodDescriptorContainer()
        {
            Parameters = new List<IDescriptor>();
        }

        public ICollection<IDescriptor> Parameters { get; set; }
    }
}
