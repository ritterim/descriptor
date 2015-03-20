using RimDev.Descriptor.Formatters.Json;

namespace RimDev.Descriptor.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var formatter = new WebApiJsonFormatter();

            AssemblyScanner.FindDescriptorsInAssemblyContaining<Program>()
                .ForEach(x => {
                    formatter.AddDescriptor(x);
                });

            var output = formatter.Format();

            System.Console.WriteLine(output);
        }
    }
}
