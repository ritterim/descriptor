using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using RimDev.Descriptor.Generic;

namespace RimDev.Descriptor
{
    public class AssemblyScanner : IEnumerable<IDescriptor<IDescriptorContainer>>
    {
        public AssemblyScanner(IEnumerable<Type> scannerTypes)
        {
            this.scannerTypes = scannerTypes;
            SetActivator(t => Activator.CreateInstance(t) as IDescriptor<IDescriptorContainer>);
        }

        private readonly IEnumerable<Type> scannerTypes;

        public Func<Type, IDescriptor<IDescriptorContainer>> DescriptorActivator { get; set; }

        public static AssemblyScanner FindDescriptorsInAssembly(Assembly assembly)
        {
            return new AssemblyScanner(assembly.GetExportedTypes());
        }

        public static AssemblyScanner FindDescriptorsInAssemblyContaining<T>()
        {
            return FindDescriptorsInAssembly(typeof(T).Assembly);
        }

        public AssemblyScanner SetActivator(Func<Type, IDescriptor<IDescriptorContainer>> activator)
        {
            DescriptorActivator = activator;
            return this;
        }

        private IEnumerable<IDescriptor<IDescriptorContainer>> Execute()
        {
            var scannerInstances = from scannerType in scannerTypes
                        let scannerInterfaces = scannerType.GetInterfaces()
                        let genericScannerInterfaces = scannerInterfaces.Where(
                            i => i.IsGenericType
                                && i.GetGenericTypeDefinition() == typeof(IDescriptor<>))
                        let matchingScannerInterface = genericScannerInterfaces.FirstOrDefault()
                        where matchingScannerInterface != null
                        select DescriptorActivator(scannerType);

            return scannerInstances;
        }

        public void ForEach(Action<IDescriptor<IDescriptorContainer>> action)
        {
            foreach (var result in this)
            {
                action(result);
            }
        }

        public IEnumerator<IDescriptor<IDescriptorContainer>> GetEnumerator()
        {
            return Execute().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
