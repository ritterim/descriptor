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

            var methodContainer = HydrateMethodDescriptionContainer<MethodDescriptorContainer<TModel>, TModel>(
                methodName,
                description,
                Convert<MethodDescriptorContainer<TModel>>(model));

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

        protected TContainer HydrateMethodDescriptionContainer<TContainer, TModel>(
            string methodName,
            string description = null,
            Action<object> model = null)
            where TContainer : class, new()
        {
            var methodContainer = new TContainer();

            if (methodContainer as DescriptorContainer<TModel> != null)
            {
                if (model != null)
                {
                    model(methodContainer);
                }

                (methodContainer as DescriptorContainer<TModel>).Description =
                    (methodContainer as DescriptorContainer<TModel>).Description
                        ?? description
                        ?? string.Format("This is {0}.", methodName);

                (methodContainer as DescriptorContainer<TModel>).Name =
                    (methodContainer as DescriptorContainer<TModel>).Name
                        ?? methodName;
            }

            return methodContainer;
        }

        public Action<object> Convert<T>(Action<T> action)
        {
            if (action == null)
            {
                return null;
            }
            else
            {
                return new Action<object>(x => action((T)x));
            }
        }

        public Descriptor<TClass> SetDescription(string description)
        {
            Instance.Description = description;

            return this;
        }

        public Descriptor<TClass> SetName(string name)
        {
            Instance.Name = name;

            return this;
        }

        public Descriptor<TClass> SetType(string type)
        {
            Instance.Type = type;

            return this;
        }
    }
}
