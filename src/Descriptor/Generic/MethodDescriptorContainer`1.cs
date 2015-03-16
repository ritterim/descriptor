using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Ritter.Descriptor.Generic
{
    public class MethodDescriptorContainer<T> : DescriptorContainer<T>, IMethodDescriptor<T>
    {
        public MethodDescriptorContainer()
        {
            Parameters = new List<IDescriptor>();
        }

        public ICollection<IDescriptor> Parameters { get; set; }

        public IMethodDescriptor<T> Parameter<TProperty>(
            Expression<Func<T, TProperty>> parameter,
            string description = null,
            string type = null)
        {
            Parameters.Add(new DescriptorContainer()
            {
                Description = description,
                Name = ((MemberExpression)parameter.Body).Member.Name,
                Type = type
            });

            return this;
        }

        public new IMethodDescriptor<T> SetDescription(string description)
        {
            Description = description;

            return this;
        }

        public new IMethodDescriptor<T> SetName(string name)
        {
            Name = name;

            return this;
        }

        public new IMethodDescriptor<T> SetType(string type)
        {
            Type = type;

            return this;
        }
    }
}
