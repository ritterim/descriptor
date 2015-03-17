# Descriptor

Provides a fluent interface for documenting code and formatting output.

## Usage

Given a class:

``` csharp
  public class MathLibrary
  {
      public int Add(AddModel model)
      {
          return model.X + model.Y;
      }
  }
```

and a related model:

``` csharp
  public class AddModel
  {
      public int X { get; set; }
      public int Y { get; set; }
  }
```

the associated descriptor-class would look like this:

``` csharp
  public class MathLibraryDescriptor : AbstractDescriptor<MathLibrary>
  {
      public MathLibraryDescriptor()
      {
          Action<AddModel>(
              method: m => m.Add,
              description: "This action adds two numbers and returns the result.",
              model: model => model
                  .Parameter(
                      parameter: p => p.X,
                      description: "first-addend of equation",
                      type: "int")
                  .Parameter(
                      parameter: p => p.Y,
                      description: "second-addend of equation",
                      type: "int"));
      }
  }
```

## Contributing

Issue documentation and pull-requests are welcomed. This project follows the [fork & pull](https://help.github.com/articles/using-pull-requests/#fork--pull) model.