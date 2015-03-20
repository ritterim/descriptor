using RimDev.Descriptor.Http;

namespace RimDev.Descriptor.Console
{
    public class MathLibraryDescriptor : Descriptor<MathLibrary>
    {
        public MathLibraryDescriptor()
        {
            this
                .SetName("Math Library")
                .SetDescription("Provides a way to do math operations on sets of numbers.")
                .Action<AddModel>(
                    method: x => x.Add,
                    description: "Adds two numbers and returns result",
                    model: x =>
                    {
                        x
                            .SetDescription("This overrides the description set as Action-parameter")
                            .Parameter(
                                parameter: p => p.Addend1,
                                description: "First-addend of operation",
                                type: "int")
                            .Parameter(
                                parameter: p => p.Addend2,
                                description: "Second-addend of operation",
                                type: "int");
                    });
        }
    }

    public class HttpMathLibraryDescriptor : HttpDescriptor<MathLibrary>
    {
        public HttpMathLibraryDescriptor()
            :base(
            name: "Math Library",
            description: "Provides a way to do math operations on sets of numbers.")
        {
            this
                .Action<AddModel>(
                    method: x => x.Add,
                    description: "Adds two numbers and returns result",
                    rel: "self",
                    uri: "http://www.example.com/add",
                    model: x =>
                    {
                        x
                            .Parameter(
                                parameter: p => p.Addend1,
                                description: "First-addend of operation",
                                type: "int")
                            .Parameter(
                                parameter: p => p.Addend2,
                                description: "Second-addend of operation",
                                type: "int");
                    })
                .Action<SubtractModel>(
                    method: x => x.Substract,
                    description: "Subtracts two numbers and returns result",
                    rel: "self",
                    uri: "http://www.example.com/subtract",
                    model: x =>
                    {
                        x
                            .Parameter(
                                parameter: p => p.Minuend,
                                description: "number subtrahend is subtracted from",
                                type: "int")
                            .Parameter(
                                parameter: p => p.Subtrahend,
                                description: "number being subtracted",
                                type: "int");
                    });
        }
    }
}
