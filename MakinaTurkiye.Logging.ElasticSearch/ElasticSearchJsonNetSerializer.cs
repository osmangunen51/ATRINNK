using Elasticsearch.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MakinaTurkiye.Logging.ElasticSearch
{
    public class ElasticSearchJsonNetSerializer : IElasticsearchSerializer
    {
        private static readonly Encoding ExpectedEncoding = new UTF8Encoding(false);

        protected virtual int BufferSize => 1024;

        private readonly JsonSerializerSettings _settings;
        private JsonSerializer _defaultSerializer;

        public ElasticSearchJsonNetSerializer(JsonSerializerSettings settings = null)
        {
            _settings = settings ?? CreateSettings();
            this._defaultSerializer = JsonSerializer.Create(_settings);
        }

        public void Serialize<T>(T data, Stream stream, SerializationFormatting formatting = SerializationFormatting.None)
        {
            using (var writer = new StreamWriter(stream, ExpectedEncoding, BufferSize, leaveOpen: true))
            using (var jsonWriter = new JsonTextWriter(writer))
            {
                _defaultSerializer.Serialize(jsonWriter, data);
                writer.Flush();
                jsonWriter.Flush();
            }
        }

        public async Task SerializeAsync<T>(T data, Stream stream, SerializationFormatting formatting = SerializationFormatting.None, 
                                        CancellationToken cancellationToken = default(CancellationToken))
        {
            Serialize(data, stream);
            await Task.CompletedTask;
        }

        public object Deserialize(Type type, Stream stream)
        {
            var settings = this._settings;
            return Deserialize(type,stream, settings);
        }

        public async Task<object> DeserializeAsync(Type type, Stream stream, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Task.FromResult(Deserialize(type,stream));
        }

        public T Deserialize<T>(Stream stream)
        {
            var settings = this._settings;
            return Deserialize<T>(stream, settings);
        }

        public async Task<T> DeserializeAsync<T>(Stream stream, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Task.FromResult(Deserialize<T>(stream));
        }


        private object Deserialize(Type type,Stream stream, JsonSerializerSettings settings = null)
        {
            settings = settings ?? this._settings;
            var serializer = JsonSerializer.Create(settings);
            var jsonTextReader = new JsonTextReader(new StreamReader(stream));
            var result = serializer.Deserialize(jsonTextReader,type);
            return result;
        }

        private T Deserialize<T>(Stream stream, JsonSerializerSettings settings = null)
        {
            settings = settings ?? this._settings;
            var serializer = JsonSerializer.Create(settings);
            var jsonTextReader = new JsonTextReader(new StreamReader(stream));
            var t = serializer.Deserialize<T>(jsonTextReader);
            return t;
        }

        private JsonSerializerSettings CreateSettings()
        {
            var settings = new JsonSerializerSettings()
            {
                DefaultValueHandling = DefaultValueHandling.Include,
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            };
            return settings;
        }
    }
}
