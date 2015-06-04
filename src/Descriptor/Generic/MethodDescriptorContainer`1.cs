using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace RimDev.Descriptor.Generic
{
    public class MethodDescriptorContainer<T> : DescriptorContainer<T>
    {
        public MethodDescriptorContainer()
        {
            Parameters = new List<DescriptorContainer<T>>();
        }

        public ICollection<DescriptorContainer<T>> Parameters { get; set; }

        public MethodDescriptorContainer<T> Parameter<TProperty>(
            Expression<Func<T, TProperty>> parameter,
            string description = null,
            string type = null)
        {
            // We want to get the full path of the expression.
            var name = ((MemberExpression)parameter.Body).ToString();

            Parameters.Add(new DescriptorContainer<T>()
            {
                Description = description,
                Name = name.Substring(name.IndexOf('.') + 1),
                Type = type
            });

            return this;
        }

        public new MethodDescriptorContainer<T> SetDescription(string description)
        {
            Description = description;

            return this;
        }

        public new MethodDescriptorContainer<T> SetName(string name)
        {
            Name = name;

            return this;
        }

        public new MethodDescriptorContainer<T> SetType(string type)
        {
            Type = type;

            return this;
        }
    }
}
