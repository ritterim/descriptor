using System.Collections.Generic;
using RimDev.Descriptor;
using RimDev.Descriptor.Generic;

namespace Descriptor.Formatters
{
    public abstract class AbstractFormatter
    {
        protected AbstractFormatter()
        {
            descriptors = new List<IDescriptor<IDescriptorContainer>>();
        }

        protected ICollection<IDescriptor<IDescriptorContainer>> descriptors { get; private set; }

        public void AddDescriptor(IDescriptor<IDescriptorContainer> descriptor)
        {
            descriptors.Add(descriptor);
        }

        public abstract object Format();
    }
}
