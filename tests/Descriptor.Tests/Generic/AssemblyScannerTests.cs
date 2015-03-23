using System;
using System.Linq;
using RimDev.Descriptor.Generic;
using Xunit;

namespace RimDev.Descriptor.Tests.Generic
{
    public class AssemblyScannerTests
    {
        [Fact]
        public void Should_instantiate_assembly_scanner()
        {
            var scanner = new AssemblyScanner(new Type[0]);
            Assert.NotNull(scanner);
        }

        [Fact]
        public void Should_have_a_default_activator()
        {
            var scanner = new AssemblyScanner(new Type[0]);
            Assert.NotNull(scanner.DescriptorActivator);
        }

        [Fact]
        public void Should_be_able_to_find_descriptors_in_asembly()
        {
            var scanner = AssemblyScanner.FindDescriptorsInAssemblyContaining<TestDescriptor>();
            Assert.Equal(1, scanner.Count());
        }

        [Fact]
        public void Should_be_able_to_foreach()
        {
            var scanner = AssemblyScanner.FindDescriptorsInAssemblyContaining<TestDescriptor>();
            var count = 0;

            scanner.ForEach(x => count++);

            Assert.Equal(1, count);
        }

        [Fact]
        public void Should_be_able_to_set_activator()
        {
            var count = 0;
            var scanner = AssemblyScanner
                .FindDescriptorsInAssemblyContaining<TestDescriptor>()
                .SetActivator(t =>
                {
                    count++;
                    return Activator.CreateInstance(t) as IDescriptor<IDescriptorContainer>;
                }).ToList();

            Assert.Equal(1, count);
        }

        public class TestDescriptor : IDescriptor<IDescriptorContainer>
        {
            public IDescriptorContainer Instance
            {
                get { return new TestDescriptorContainer(); }
            }
        }
    }

    public class TestDescriptorContainer : IDescriptorContainer
    {
        public string Name
        {
            get { return "Test"; }
        }
    }
}
