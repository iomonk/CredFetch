using System.Collections.Generic;
using Newtonsoft.Json;

namespace CredFetch.Models;

public static class ConfigProperties
{
    public static Dictionary<string, string>? FromJson(string json)
    {
        return JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
    }
}