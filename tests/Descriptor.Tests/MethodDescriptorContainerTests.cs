using Xunit;

namespace RimDev.Descriptor.Tests
{
    public class MethodDescriptorContainerTests
    {
        public class Constructor : MethodDescriptorContainerTests
        {
            [Fact]
            public void Should_initialize_empty_parameter_list()
            {
                var sut = new MethodDescriptorContainer();

                Assert.NotNull(sut.Parameters);
            }
        }
    }
}
