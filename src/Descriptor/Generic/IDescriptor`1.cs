namespace Ritter.Descriptor.Generic
{
    public interface IDescriptor<T> : IDescriptor
    {
        IDescriptor<T> SetDescription(string description);
        IDescriptor<T> SetName(string name);
        IDescriptor<T> SetType(string type);
    }
}
