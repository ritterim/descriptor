namespace RimDev.Descriptor.Console
{
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
}
