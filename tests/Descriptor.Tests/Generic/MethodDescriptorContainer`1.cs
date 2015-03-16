using System.Linq;
using Moq;
using Ritter.Descriptor.Generic;
using Xunit;
using Xunit.Extensions;

namespace Ritter.Descriptor.Tests
{
    public class MethodDescriptorContainer_1Tests
    {
        public class SetDescription : MethodDescriptorContainer_1Tests
        {
            [Theory,
            InlineData("test", "test"),
            InlineData("TEST", "TEST")]
            public void Should_set_property_without_modification(string input, string expected)
            {
                var mock = new Mock<MethodDescriptorContainer<string>>();

                mock.Object.SetDescription(input);

                mock.VerifySet(x => x.Description = expected, Times.Once());
            }

            [Fact]
            public void Should_return_instance_with_set_property()
            {
                var sut = new MethodDescriptorContainer<string>();

                var @return = sut.SetDescription("test");

                Assert.IsType<MethodDescriptorContainer<string>>(@return);
                Assert.Equal("test", @return.Description);
            }
        }

        public class SetName : MethodDescriptorContainer_1Tests
        {
            [Theory,
            InlineData("test", "test"),
            InlineData("TEST", "TEST")]
            public void Should_set_property_without_modification(string input, string expected)
            {
                var mock = new Mock<MethodDescriptorContainer<string>>();

                mock.Object.SetName(input);

                mock.VerifySet(x => x.Name = expected, Times.Once());
            }

            [Fact]
            public void Should_return_instance_with_set_property()
            {
                var sut = new MethodDescriptorContainer<string>();

                var @return = sut.SetName("test");

                Assert.Equal("test", @return.Name);
            }
        }

        public class SetType : MethodDescriptorContainer_1Tests
        {
            [Theory,
            InlineData("test", "test"),
            InlineData("TEST", "TEST")]
            public void Should_set_property_without_modification(string input, string expected)
            {
                var mock = new Mock<MethodDescriptorContainer<string>>();

                mock.Object.SetType(input);

                mock.VerifySet(x => x.Type = expected, Times.Once());
            }

            [Fact]
            public void Should_return_instance_with_set_property()
            {
                var sut = new MethodDescriptorContainer<string>();

                var @return = sut.SetType("test");

                Assert.Equal("test", @return.Type);
            }
        }

        public class Parameter : MethodDescriptorContainer_1Tests
        {
            [Fact]
            public void Should_add_parameter_data_to_list()
            {
                var sut = new MethodDescriptorContainer<TestParameter>();

                sut.Parameter(x => x.FirstName, "description", "type");

                var parameter = sut.Parameters.FirstOrDefault();

                Assert.NotNull(parameter);
                Assert.Equal("FirstName", parameter.Name);
                Assert.Equal("description", parameter.Description);
                Assert.Equal("type", parameter.Type);
            }

            [Fact]
            public void Should_return_instance_with_set_parameter()
            {
                var sut = new MethodDescriptorContainer<TestParameter>();

                var @return = sut.Parameter(x => x.FirstName, "description", "type");
                
                Assert.NotNull(@return);

                var parameter = @return.Parameters.FirstOrDefault();

                Assert.NotNull(parameter);
                Assert.Equal("FirstName", parameter.Name);
                Assert.Equal("description", parameter.Description);
                Assert.Equal("type", parameter.Type);
            }

            private class TestParameter
            {
                public string FirstName { get; set; }
                public string LastName { get; set; }
            }
        }
    }
}
