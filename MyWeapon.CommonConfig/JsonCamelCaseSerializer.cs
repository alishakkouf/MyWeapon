using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace MyWeapon.CommonConfig
{
    public static class JsonCamelCaseSerializer
    {
        public static string Serialize(object o)
        {
            return JsonConvert.SerializeObject(o, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }
    }
}
