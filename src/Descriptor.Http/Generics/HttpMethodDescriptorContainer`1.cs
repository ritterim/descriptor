using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace RimDev.Descriptor.Http.Generic
{
    public class HttpMethodDescriptorContainer<T> : HttpDescriptorContainer<T>
    {
        public HttpMethodDescriptorContainer()
        {
            Parameters = new List<HttpDescriptorContainer<T>>();
        }

        public ICollection<HttpDescriptorContainer<T>> Parameters { get; set; }

        public HttpMethodDescriptorContainer<T> Parameter<TProperty>(
            Expression<Func<T, TProperty>> parameter,
            string description = null,
            string type = null)
        {
            Parameters.Add(new HttpDescriptorContainer<T>()
            {
                Description = description,
                Name = ((MemberExpression)parameter.Body).Member.Name,
                Type = type
            });

            return this;
        }

        public new HttpMethodDescriptorContainer<T> SetDescription(string description)
        {
            base.SetDescription(description);

            return this;
        }

        public new HttpMethodDescriptorContainer<T> SetName(string name)
        {
            base.SetName(name);

            return this;
        }

        public new HttpMethodDescriptorContainer<T> SetRel(string rel)
        {
            Rel = rel;

            return this;
        }

        public new HttpMethodDescriptorContainer<T> SetType(string type)
        {
            base.SetType(type);

            return this;
        }

        public new HttpMethodDescriptorContainer<T> SetUri(string uri)
        {
            base.SetUri(uri);

            return this;
        }

        public new HttpMethodDescriptorContainer<T> SetVerb(string verb)
        {
            base.SetVerb(verb);

            return this;
        }
    }
}
