using System;
using System.Linq.Expressions;
using RimDev.Descriptor.Generic;

namespace RimDev.Descriptor
{
    public class HttpDescriptor<TClass> : Descriptor<TClass>
        where TClass : class, new()
    {
        public HttpDescriptor(
            string name = null,
            string description = null,
            string type = null)
            :base(name, description, type)
        {
        }

        public HttpDescriptor<TClass> Action<TModel>(
            Expression<Func<TClass, Func<TModel, object>>> method,
            string description = null,
            string rel = null,
            string uri = null,
            Action<HttpMethodDescriptorContainer<TModel>> model = null)
        {
            var methodInfo = ExtractMemberInfoFromExpression<TModel>(method);
            var methodName = methodInfo.Name;
            var methodContainer = HydrateMethodDescriptionContainer<HttpMethodDescriptorContainer<TModel>, TModel>(
                methodName,
                description,
                Convert<HttpMethodDescriptorContainer<TModel>>(model));

            methodContainer.Rel =
                methodContainer.Rel
                    ?? rel
                    ?? "n/a";

            methodContainer.Uri =
                methodContainer.Uri
                    ?? uri
                    ?? "n/a";

            Methods.Add(methodContainer);

            return this;
        }

        public new HttpDescriptor<TClass> SetDescription(string description)
        {
            base.SetDescription(description);

            return this;
        }

        public new HttpDescriptor<TClass> SetName(string name)
        {
            base.SetName(name);

            return this;
        }

        public new HttpDescriptor<TClass> SetType(string type)
        {
            base.SetType(type);

            return this;
        }
    }
}
