namespace RimDev.Descriptor.Generic
{
    public class DescriptorContainer<T> : DescriptorContainer, IDescriptor<T>
    {
        public IDescriptor<T> SetDescription(string description)
        {
            Description = description;

            return this;
        }

        public IDescriptor<T> SetName(string name)
        {
            Name = name;

            return this;
        }

        public IDescriptor<T> SetType(string type)
        {
            Type = type;

            return this;
        }
    }
}
