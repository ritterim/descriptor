using System;
using System.Linq.Expressions;
using System.Reflection;
using RimDev.Descriptor.Generic;

namespace RimDev.Descriptor
{
    public class Descriptor<TClass> : AbstractDescriptor<TClass, DescriptorContainer<TClass>>
        where TClass : class, new()
    {
        public Descriptor(
            string name = null,
            string description = null,
            string type = null)
        {
            Instance.Name = name ?? typeof(TClass).Name;
            Instance.Description = description ?? string.Format("This is {0}.", Instance.Name);
            Instance.Type = type;
        }

        public Descriptor<TClass> Action<TModel>(
            Expression<Func<TClass, Func<TModel, object>>> method,
            string description = null,
            Action<MethodDescriptorContainer<TModel>> model = null)
        {
            var methodInfo = ExtractMemberInfoFromExpression<TModel>(method);
            var methodName = methodInfo.Name;
            var methodContainer = new MethodDescriptorContainer<TModel>();

            if (model != null)
            {
                model(methodContainer);
            }

            methodContainer.Description =
                methodContainer.Description
                    ?? description
                    ?? string.Format("This is {0}.", methodName);

            methodContainer.Name =
                methodContainer.Name
                    ?? methodName;

            Methods.Add(methodContainer);

            return this;
        }

        protected MemberInfo ExtractMemberInfoFromExpression<TModel>(
            Expression<Func<TClass, Func<TModel, object>>> method)
        {
            var unaryExpression = (UnaryExpression)method.Body;

            var methodInfo = ExtractMethodInfoFromUnaryExpression(unaryExpression);

            return methodInfo;
        }
    }
}
