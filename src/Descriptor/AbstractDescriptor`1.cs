using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using RimDev.Descriptor.Generic;
using RimDev.Descriptor.Helpers;

namespace RimDev.Descriptor
{
    public abstract class AbstractDescriptor<T> : DescriptorContainer<T>
        where T : class, new()
    {
        public AbstractDescriptor(
            string name = null,
            string description = null,
            string type = null)
        {
            Name = name ?? typeof(T).Name;
            Description = description ?? string.Format("This is {0}.", Name);
            Type = type;

            Methods = new List<IMethodDescriptor>();
        }

        public ICollection<IMethodDescriptor> Methods { get; set; }

        public AbstractDescriptor<T> Action<TModel>(
            Expression<Func<T, Func<TModel, object>>> method,
            string description = null,
            Action<IMethodDescriptor<TModel>> model = null)
        {
            var methodInfo = ExtractMemberInfoFromExpression<TModel>(method);
            var methodName = methodInfo.Name;
            var methodContainer = new MethodDescriptorContainer<TModel>();

            if (model != null)
            {
                model(methodContainer);
            }

            var methodDescriptorContainer = GenerateMethodDescriptorContainer<TModel>(methodContainer);

            methodDescriptorContainer.Description =
                methodDescriptorContainer.Description
                    ?? description
                    ?? string.Format("This is {0}.", methodName);

            methodDescriptorContainer.Name =
                methodDescriptorContainer.Name
                    ?? methodName;

            Methods.Add(methodDescriptorContainer);

            return this;
        }

        protected IMethodDescriptor GenerateMethodDescriptorContainer<TModel>(
            IMethodDescriptor<TModel> methodContainer)
        {
            var methodDescriptorContainer = new MethodDescriptorContainer()
            {
                Description = methodContainer.Description,
                Name = methodContainer.Name,
                Type = methodContainer.Type,
                Parameters = methodContainer.Parameters
            };

            return methodDescriptorContainer;
        }

        protected MemberInfo ExtractMemberInfoFromExpression<TModel>(
            Expression<Func<T, Func<TModel, object>>> method)
        {
            var unaryExpression = (UnaryExpression)method.Body;

            var methodInfo = ExtractMethodInfoFromUnaryExpression(unaryExpression);

            return methodInfo;
        }

        protected MemberInfo ExtractMethodInfoFromUnaryExpression(
            UnaryExpression expression)
        {
            var methodCallExpression = (MethodCallExpression)expression.Operand;

            /**
             * Not entirely sure what causes issues between the MS and Mono implementations,
             * but the former throws a NRE when extracting the second-index from the `Arguments` property,
             * while the latter throws a similar NRE when using the `Object` property.
             */
            var constantExpression = EnvironmentHelper.IsRunningOnMono.Value
                ? (ConstantExpression)methodCallExpression.Arguments[2]
                : (ConstantExpression)methodCallExpression.Object;

            var methodInfo = (MethodInfo)constantExpression.Value;

            return methodInfo;
        }
    }
}
