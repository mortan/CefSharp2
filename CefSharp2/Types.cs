using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Documents;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CefSharp2
{
    public class Types
    {
        public class PushNotification
        {
            [JsonProperty("New")]
            public List<string> SearchTokens { get; set; }
        }

    public partial class SearchTokenRequestResponse
    {
        [JsonProperty("result")]
        public List<Result> Result { get; set; }
    }

    public partial class Result
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("listing")]
        public Listing Listing { get; set; }

        [JsonProperty("item")]
        public Item Item { get; set; }
    }

    public partial class Item
    {
        [JsonProperty("verified")]
        public bool Verified { get; set; }

        [JsonProperty("w")]
        public long W { get; set; }

        [JsonProperty("h")]
        public long H { get; set; }

        [JsonProperty("ilvl")]
        public long Ilvl { get; set; }

        [JsonProperty("icon")]
        public Uri Icon { get; set; }

        [JsonProperty("league")]
        public string League { get; set; }

        [JsonProperty("sockets")]
        public List<Socket> Sockets { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("typeLine")]
        public string TypeLine { get; set; }

        [JsonProperty("identified")]
        public bool Identified { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }

        [JsonProperty("properties")]
        public List<Property> Properties { get; set; }

        [JsonProperty("requirements")]
        public List<Property> Requirements { get; set; }

        [JsonProperty("implicitMods")]
        public List<string> ImplicitMods { get; set; }

        [JsonProperty("explicitMods")]
        public List<string> ExplicitMods { get; set; }

        [JsonProperty("flavourText")]
        public List<string> FlavourText { get; set; }

        [JsonProperty("frameType")]
        public long FrameType { get; set; }

        [JsonProperty("category")]
        public Category Category { get; set; }

        [JsonProperty("extended")]
        public Extended Extended { get; set; }
    }

    public partial class Category
    {
        [JsonProperty("weapons")]
        public List<string> Weapons { get; set; }
    }

    public partial class Extended
    {
        [JsonProperty("dps")]
        public double Dps { get; set; }

        [JsonProperty("pdps")]
        public double Pdps { get; set; }

        [JsonProperty("edps")]
        public long Edps { get; set; }

        [JsonProperty("mods")]
        public Mods Mods { get; set; }

        [JsonProperty("hashes")]
        public Hashes Hashes { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }

    public partial class Hashes
    {
        [JsonProperty("implicit")]
        public List<List<ExplicitUnion>> Implicit { get; set; }

        [JsonProperty("explicit")]
        public List<List<ExplicitUnion>> Explicit { get; set; }
    }

    public partial class Mods
    {
        [JsonProperty("implicit")]
        public List<ExplicitClass> Implicit { get; set; }

        [JsonProperty("explicit")]
        public List<ExplicitClass> Explicit { get; set; }
    }

    public partial class ExplicitClass
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("tier")]
        public string Tier { get; set; }

        [JsonProperty("magnitudes")]
        public List<Magnitude> Magnitudes { get; set; }
    }

    public partial class Magnitude
    {
        [JsonProperty("hash")]
        public string Hash { get; set; }

        [JsonProperty("min")]
        public double Min { get; set; }

        [JsonProperty("max")]
        public double Max { get; set; }
    }

    public partial class Property
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("values")]
        public List<List<Value>> Values { get; set; }

        [JsonProperty("displayMode")]
        public long DisplayMode { get; set; }

        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public long? Type { get; set; }
    }

    public partial class Socket
    {
        [JsonProperty("group")]
        public long Group { get; set; }

        [JsonProperty("attr")]
        public string Attr { get; set; }

        [JsonProperty("sColour")]
        public string SColour { get; set; }
    }

    public partial class Listing
    {
        [JsonProperty("method")]
        public string Method { get; set; }

        [JsonProperty("indexed")]
        public DateTimeOffset Indexed { get; set; }

        [JsonProperty("stash")]
        public Stash Stash { get; set; }

        [JsonProperty("whisper")]
        public string Whisper { get; set; }

        [JsonProperty("account")]
        public Account Account { get; set; }

        [JsonProperty("price")]
        public Price Price { get; set; }
    }

    public partial class Account
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("lastCharacterName")]
        public string LastCharacterName { get; set; }

        [JsonProperty("online")]
        public Online Online { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }
    }

    public partial class Online
    {
        [JsonProperty("league")]
        public string League { get; set; }
    }

    public partial class Price
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("amount")]
        public long Amount { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }
    }

    public partial class Stash
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("x")]
        public long X { get; set; }

        [JsonProperty("y")]
        public long Y { get; set; }
    }

    public partial struct ExplicitUnion
    {
        public List<long> IntegerArray;
        public string String;

        public static implicit operator ExplicitUnion(List<long> IntegerArray) => new ExplicitUnion { IntegerArray = IntegerArray };
        public static implicit operator ExplicitUnion(string String) => new ExplicitUnion { String = String };
    }

    public partial struct Value
    {
        public long? Integer;
        public string String;

        public static implicit operator Value(long Integer) => new Value { Integer = Integer };
        public static implicit operator Value(string String) => new Value { String = String };
    }

    public partial class SearchTokenRequestResponse
    {
        public static SearchTokenRequestResponse FromJson(string json) => JsonConvert.DeserializeObject<SearchTokenRequestResponse>(json, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                ExplicitUnionConverter.Singleton,
                ValueConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ExplicitUnionConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(ExplicitUnion) || t == typeof(ExplicitUnion?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.String:
                case JsonToken.Date:
                    var stringValue = serializer.Deserialize<string>(reader);
                    return new ExplicitUnion { String = stringValue };
                case JsonToken.StartArray:
                    var arrayValue = serializer.Deserialize<List<long>>(reader);
                    return new ExplicitUnion { IntegerArray = arrayValue };
            }
            throw new Exception("Cannot unmarshal type ExplicitUnion");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (ExplicitUnion)untypedValue;
            if (value.String != null)
            {
                serializer.Serialize(writer, value.String);
                return;
            }
            if (value.IntegerArray != null)
            {
                serializer.Serialize(writer, value.IntegerArray);
                return;
            }
            throw new Exception("Cannot marshal type ExplicitUnion");
        }

        public static readonly ExplicitUnionConverter Singleton = new ExplicitUnionConverter();
    }

    internal class ValueConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Value) || t == typeof(Value?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.Integer:
                    var integerValue = serializer.Deserialize<long>(reader);
                    return new Value { Integer = integerValue };
                case JsonToken.String:
                case JsonToken.Date:
                    var stringValue = serializer.Deserialize<string>(reader);
                    return new Value { String = stringValue };
            }
            throw new Exception("Cannot unmarshal type Value");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (Value)untypedValue;
            if (value.Integer != null)
            {
                serializer.Serialize(writer, value.Integer.Value);
                return;
            }
            if (value.String != null)
            {
                serializer.Serialize(writer, value.String);
                return;
            }
            throw new Exception("Cannot marshal type Value");
        }

        public static readonly ValueConverter Singleton = new ValueConverter();
    }
    }
}