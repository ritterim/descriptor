using System;
using System.Linq;
using Xunit;
using Xunit.Extensions;

namespace Ritter.Descriptor.Tests
{
    public class AbstractDescriptor_1Tests
    {
        public class Constructor : AbstractDescriptor_1Tests
        {
            [Fact]
            public void Should_set_description_based_on_type_instance_if_none_provided()
            {
                var sut = new TestAbstractDescriptor();

                Assert.Equal("This is TestType.", sut.Description);
            }

            [Fact]
            public void Should_set_name_to_type_instance_if_none_provided()
            {
                var sut = new TestAbstractDescriptor();

                Assert.Equal("TestType", sut.Name);
            }

            [Theory,
            InlineData("fake description", "fake description"),
            InlineData(null, "This is TestType.")]
            public void Should_set_description_from_parameter(
                string description,
                string expectedDescription)
            {
                var sut = new TestAbstractDescriptor(description: description);

                Assert.Equal(expectedDescription, sut.Description);
            }

            [Theory,
            InlineData("fake name", "fake name"),
            InlineData(null, "TestType")]
            public void Should_set_name_from_parameter(
                string name,
                string expectedName)
            {
                var sut = new TestAbstractDescriptor(name: name);

                Assert.Equal(expectedName, sut.Name);
            }

            [Theory,
            InlineData("fake type", "fake type"),
            InlineData(null, null)]
            public void Should_set_type_from_parameter(
                string type,
                string expectedType)
            {
                var sut = new TestAbstractDescriptor(type: type);

                Assert.Equal(expectedType, sut.Type);
            }
        }

        public class Action : AbstractDescriptor_1Tests
        {
            [Fact]
            public void Should_get_value_of_model_expression()
            {
                var sut = new TestAbstractDescriptor();

                var @result = sut.Action<TestTypeModel>(x => x.TestAction);

                Assert.Equal(1, @result.Methods.Count());
                Assert.Equal("TestAction", @result.Methods.First().Name);
            }

            [Fact]
            public void Should_return_instance()
            {
                var sut = new TestAbstractDescriptor();

                var @result = sut.Action<TestTypeModel>(x => x.TestAction);

                Assert.IsType<TestAbstractDescriptor>(@result);
                Assert.IsAssignableFrom<AbstractDescriptor<TestType>>(@result);
            }

            [Fact]
            public void Should_set_description_if_parameter_provided()
            {
                var sut = new TestAbstractDescriptor();

                var @result = sut.Action<TestTypeModel>(
                    method: x => x.TestAction,
                    description: "fake description for TestAction");

                Assert.Equal(1, @result.Methods.Count());
                Assert.Equal(
                    "fake description for TestAction",
                    @result.Methods.First().Description);
            }

            [Fact]
            public void Should_fallback_to_method_container_description_if_none_provided_as_parameter()
            {
                var sut = new TestAbstractDescriptor();

                var @result = sut.Action<TestTypeModel>(
                    method: x => x.TestAction,
                    model: model => {
                        model.SetDescription("fake description set in container");
                    });

                Assert.Equal(1, @result.Methods.Count());
                Assert.Equal(
                    "fake description set in container",
                    @result.Methods.First().Description);
            }

            [Fact]
            public void Should_fallback_if_no_description_provided()
            {
                var sut = new TestAbstractDescriptor();

                var @result = sut.Action<TestTypeModel>(x => x.TestAction);

                Assert.Equal(1, @result.Methods.Count());
                Assert.Equal("This is TestAction.", @result.Methods.First().Description);
            }

            [Fact]
            public void Should_set_name_if_provided_via_parameter_action()
            {
                var sut = new TestAbstractDescriptor();

                var @result = sut.Action<TestTypeModel>(
                    method: x => x.TestAction,
                    model: model =>
                    {
                        model.SetName("fake name set in container");
                    });

                Assert.Equal(1, @result.Methods.Count());
                Assert.Equal(
                    "fake name set in container",
                    @result.Methods.First().Name);
            }

            [Fact]
            public void Should_fallback_name_to_resolved_method_parameter_if_none_provided()
            {
                var sut = new TestAbstractDescriptor();

                var @result = sut.Action<TestTypeModel>(method: x => x.TestAction);

                Assert.Equal(1, @result.Methods.Count());
                Assert.Equal(
                    "TestAction",
                    @result.Methods.First().Name);
            }

            [Fact]
            public void Should_set_type_if_set_via_parameter_action()
            {
                var sut = new TestAbstractDescriptor();

                var @result = sut.Action<TestTypeModel>(
                    method: x => x.TestAction,
                    model: model =>
                    {
                        model.SetType("fake type set in container");
                    });

                Assert.Equal(1, @result.Methods.Count());
                Assert.Equal(
                    "fake type set in container",
                    @result.Methods.First().Type);
            }

            [Fact]
            public void Should_set_parameters_if_provided_via_parameter_action()
            {
                var sut = new TestAbstractDescriptor();

                var @result = sut.Action<TestTypeModel>(
                    method: x => x.TestAction,
                    model: model =>
                    {
                        model.Parameter(parameter: x => x.TestProperty);
                    });

                Assert.Equal(1, @result.Methods.Count());
                Assert.Equal(1, @result.Methods.First().Parameters.Count());
                Assert.Equal(
                    "TestProperty",
                    @result.Methods.First().Parameters.First().Name);
            }
        }

        private class TestAbstractDescriptor : AbstractDescriptor<TestType>
        {
            public TestAbstractDescriptor() { }

            public TestAbstractDescriptor(
                string name = null,
                string description = null,
                string type = null)
                : base(name, description, type)
            { }
        }

        private class TestType
        {
            public object TestAction(TestTypeModel model)
            {
                throw new NotImplementedException();
            }
        }

        private class TestTypeModel
        {
            public string TestProperty { get; set; }
        }
    }
}
