using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace RimDev.Descriptor.Formatters.Json
{
    public class WebApiJsonFormatter : JsonFormatter
    {
        public override object Format()
        {
            var settings = new JsonSerializerSettings()
            {
                ContractResolver = new HttpContractResolver(),
                NullValueHandling = NullValueHandling.Ignore
            };

            return Format(settings);
        }

        public class HttpContractResolver : CamelCasePropertyNamesContractResolver
        {
            protected override string ResolvePropertyName(string propertyName)
            {
                var name = base.ResolvePropertyName(propertyName);

                name = string.Equals(name, "uri", StringComparison.InvariantCultureIgnoreCase) ? "href" : name;

                return name;
            }
        }
    }
}
