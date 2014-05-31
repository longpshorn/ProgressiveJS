using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.IO;
using System.Text;

namespace ProgressiveJS.UX
{
    public abstract class JsonNetSerializer
    {
        public string ToJson()
        {
            return this.Serialize();
        }

        public string Serialize() {
            var settings = new JsonSerializerSettings
            {
                Formatting = Formatting.None,
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            settings.Converters.Add(new StringEnumConverter());

            var sb = new StringBuilder();
            var writer = new StringWriter(sb);
            var jsonWriter = new JsonTextWriter(writer) { Formatting = Formatting.None };

            var serializer = Newtonsoft.Json.JsonSerializer.Create(settings);
            serializer.Serialize(jsonWriter, this);

            jsonWriter.Flush();
            return sb.ToString();
        }
    }
}