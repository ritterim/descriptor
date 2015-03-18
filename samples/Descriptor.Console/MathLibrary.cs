namespace RimDev.Descriptor.Console
{
    public class MathLibrary
    {
        public object Add(AddModel model)
        {
            return model.X + model.Y;
        }
    }

    public class AddModel
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}
