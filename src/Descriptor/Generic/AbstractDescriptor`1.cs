using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using RimDev.Descriptor.Helpers;

namespace RimDev.Descriptor.Generic
{
    public abstract class AbstractDescriptor<TClass, TInstanceContainer> : IDescriptor<TInstanceContainer>
        where TInstanceContainer : class, IDescriptorContainer, new()
    {
        public AbstractDescriptor()
        {
            Instance = new TInstanceContainer();
            Methods = new List<object>();
        }

        public TInstanceContainer Instance { get; private set; }
        public ICollection<object> Methods { get; private set; }

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
