namespace RimDev.Descriptor
{
    public class DescriptorContainer : IDescriptor
    {
        public virtual string Description { get; set; }
        public virtual string Name { get; set; }
        public virtual string Type { get; set; }
    }
}
