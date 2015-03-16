using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Ritter.Descriptor.Generic;
using Ritter.Descriptor.Helpers;

namespace Ritter.Descriptor
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
            UnaryExpression unaryExpr = (UnaryExpression)method.Body;
            MethodCallExpression methodCallExpr = (MethodCallExpression)unaryExpr.Operand;

            /**
             * Not entirely sure what causes issues between the MS and Mono implementations,
             * but the former throws a NRE when extracting the second-index from the `Arguments` property,
             * while the latter throws a similar NRE when using the `Object` property.
             */
            ConstantExpression constantExpr = EnvironmentHelper.IsRunningOnMono.Value
                ? (ConstantExpression)methodCallExpr.Arguments[2]
                : (ConstantExpression)methodCallExpr.Object;

            MethodInfo methodInfo = (MethodInfo)constantExpr.Value;

            var methodName = methodInfo.Name;
            var methodContainer = new MethodDescriptorContainer<TModel>();

            if (model != null)
            {
                model(methodContainer);
            }

            Methods.Add(new MethodDescriptorContainer()
            {
                Description = description
                    ?? methodContainer.Description
                    ?? string.Format("This is {0}.", methodName),
                Name = methodContainer.Name ?? methodName,
                Type = methodContainer.Type,
                Parameters = methodContainer.Parameters
            });

            return this;
        }
    }
}
