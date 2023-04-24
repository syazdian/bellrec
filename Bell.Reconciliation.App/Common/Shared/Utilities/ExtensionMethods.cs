﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static Bell.Reconciliation.App.Shared.Utilities.Deserializer;

namespace Bell.Reconciliation.App.Shared.Utilities;

public static class ExtensionMethods
{
    public static string ToJson(this object obj)
    {
        return JsonSerializer.Serialize(obj);
    }

    public static T? JsonDeserialize<T>(this string json)
    {
        var options = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            NumberHandling = JsonNumberHandling.WriteAsString
        };
        options.Converters.Add(new LongToStringJsonConverter());
        if (json == null)
        {
            throw new ArgumentNullException(nameof(json));
        }
        var res = JsonSerializer.Deserialize<T>(json, options);
        return res;
    }

    public static T JsonDeserialize<T>(this JsonObject json)
    {
        var options = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            NumberHandling = JsonNumberHandling.WriteAsString
        };
        options.Converters.Add(new LongToStringJsonConverter());

        if (json == null)
            throw new ArgumentNullException(nameof(json));
        return JsonSerializer.Deserialize<T>(json, options);
    }
}