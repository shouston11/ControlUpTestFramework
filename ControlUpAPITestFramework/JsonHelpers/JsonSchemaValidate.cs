using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace ControlUpAPITestFramework.JsonHelpers
{
    public class AssertJsonSchema
    {
        public static void Valid(string schemaFields = null, string jsonResponse = null)
        {
            JSchema schema = JSchema.Parse(schemaFields);
            JObject fields = JObject.Parse(jsonResponse);
            bool valid = fields.IsValid(schema);
        }
    }
}
