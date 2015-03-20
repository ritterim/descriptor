using RimDev.Descriptor.Generic;

namespace RimDev.Descriptor.Http.Generic
{
    public class HttpDescriptorContainer<T> : DescriptorContainer<T>
    {
        public virtual string Rel { get; set; }
        public virtual string Uri { get; set; }

        public HttpDescriptorContainer<T> SetRel(string rel)
        {
            Rel = rel;

            return this;
        }

        public HttpDescriptorContainer<T> SetUri(string uri)
        {
            Uri = uri;

            return this;
        }
    }
}
