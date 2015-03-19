namespace RimDev.Descriptor.Generic
{
    public interface IDescriptor<out TInstance>
        where TInstance : IDescriptorContainer
    {
        TInstance Instance { get; }
    }
}
