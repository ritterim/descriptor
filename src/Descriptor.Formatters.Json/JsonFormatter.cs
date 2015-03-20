using Descriptor.Formatters;
using Newtonsoft.Json;

namespace RimDev.Descriptor.Formatters.Json
{
    public class JsonFormatter : AbstractFormatter
    {
        public override object Format()
        {
            var settings = new JsonSerializerSettings();

            return Format(settings);
        }

        public virtual object Format(JsonSerializerSettings settings)
        {
            return JsonConvert.SerializeObject(descriptors, settings);
        }
    }
}
