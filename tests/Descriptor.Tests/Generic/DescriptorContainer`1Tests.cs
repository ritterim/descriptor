using Moq;
using RimDev.Descriptor.Generic;
using Xunit;
using Xunit.Extensions;

namespace RimDev.Descriptor.Tests
{
    public class DescriptorContainer_1Tests
    {
        public class SetDescription : DescriptorContainer_1Tests
        {
            [Theory,
            InlineData("test", "test"),
            InlineData("TEST", "TEST")]
            public void Should_set_property_without_modification(string input, string expected)
            {
                var mock = new Mock<DescriptorContainer<string>>();

                mock.Object.SetDescription(input);

                mock.VerifySet(x => x.Description = expected, Times.Once());
            }

            [Fact]
            public void Should_return_instance_with_set_property()
            {
                var sut = new DescriptorContainer<string>();

                var @return = sut.SetDescription("test");

                Assert.IsType<DescriptorContainer<string>>(@return);
                Assert.Equal("test", @return.Description);
            }
        }

        public class SetName : DescriptorContainer_1Tests
        {
            [Theory,
            InlineData("test", "test"),
            InlineData("TEST", "TEST")]
            public void Should_set_property_without_modification(string input, string expected)
            {
                var mock = new Mock<DescriptorContainer<string>>();

                mock.Object.SetName(input);

                mock.VerifySet(x => x.Name = expected, Times.Once());
            }

            [Fact]
            public void Should_return_instance_with_set_property()
            {
                var sut = new DescriptorContainer<string>();

                var @return = sut.SetName("test");

                Assert.Equal("test", @return.Name);
            }
        }

        public class SetType : DescriptorContainer_1Tests
        {
            [Theory,
            InlineData("test", "test"),
            InlineData("TEST", "TEST")]
            public void Should_set_property_without_modification(string input, string expected)
            {
                var mock = new Mock<DescriptorContainer<string>>();

                mock.Object.SetType(input);

                mock.VerifySet(x => x.Type = expected, Times.Once());
            }

            [Fact]
            public void Should_return_instance_with_set_property()
            {
                var sut = new DescriptorContainer<string>();

                var @return = sut.SetType("test");

                Assert.Equal("test", @return.Type);
            }
        }
    }
}
