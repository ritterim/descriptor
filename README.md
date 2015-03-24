# Descriptor [![Build Status](https://travis-ci.org/ritterim/descriptor.svg?branch=master)](https://travis-ci.org/ritterim/descriptor)

Provides a fluent interface for documenting code and formatting output.

**Please be aware this is in its infancy and may go through a series of breaking changes.**

## Usage

Given a class:

``` csharp
    public class MathLibrary
    {
        public object Add(AddModel model)
        {
            return model.Addend1 + model.Addend2;
        }

        public object Substract(SubtractModel model)
        {
            return model.Minuend - model.Subtrahend;
        }
  }
```

and related models:

``` csharp
    public class AddModel
    {
        public int Addend1 { get; set; }
        public int Addend2 { get; set; }
    }

    public class SubtractModel
    {
        public int Minuend { get; set; }
        public int Subtrahend { get; set; }
    }
```

the associated descriptor would look like this:

``` csharp
    using RimDev.Descriptor;

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
```

Or if you are working with Http-related resources (note the use of `rel` and `uri`)...

``` csharp
    using RimDev.Descriptor.Http;

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
                            .SetVerb("GET")
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
                            .SetVerb("GET")
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
```

To generate documentation, create a formatter-instance, add descriptor instances, and `Format`.

``` csharp
    using RimDev.Descriptor.Formatters.Json;

    class Program
    {
        static void Main(string[] args)
        {
            /**
             * You could also use `JsonFormatter`.
             * `WebApiJsonFormatter` uses conventions to clean-up output.
             * This includes not including null-value keys and converting `uri` properties to `href` for readability.
             */
            var formatter = new WebApiJsonFormatter();

            // Descriptor has a built-in assembly scanner for getting descriptors.
            AssemblyScanner.FindDescriptorsInAssemblyContaining<Program>()
                .ForEach(x => {
                    formatter.AddDescriptor(x);
                });

            var output = formatter.Format();

            System.Console.WriteLine(output);
        }
    }
```

## Contributing

Issue documentation and pull-requests are welcomed. This project follows the [fork & pull](https://help.github.com/articles/using-pull-requests/#fork--pull) model.
