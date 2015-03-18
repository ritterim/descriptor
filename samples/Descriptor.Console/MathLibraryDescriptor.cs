namespace RimDev.Descriptor.Console
{
    public class MathLibraryDescriptor : Descriptor<MathLibrary>
    {
        public MathLibraryDescriptor()
        {
            Action<AddModel>(
                method: x => x.Add,
                description: "Adds two numbers and returns result",
                model: x =>
                {
                    x
                        .Parameter(
                        parameter: p => p.X,
                        description: "First-addend of operation",
                        type: "int")
                        .Parameter(
                        parameter: p => p.Y,
                        description: "Second-addend of operation",
                        type: "int");
                });
        }
    }
}
