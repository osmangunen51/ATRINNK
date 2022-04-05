using Elasticsearch.Net;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace MakinaTurkiye.Logging.ElasticSearch
{
    public class ElasticSearchCommonLoggerProvider : Microsoft.Extensions.Logging.ILoggerProvider
    {

        #region fields

        private IElasticLowLevelClient _client;
        private readonly Uri _endpoint;
        private readonly string _indexPrefix;
        private readonly BlockingCollection<JObject> _queueToBePosted = new BlockingCollection<JObject>();

        private const string DocumentType = "doc";
        private Action<JObject> _scribeProcessor;

        #endregion

        /// <summary>
        /// prefix for the Index for traces
        /// </summary>
        private string Index => this._indexPrefix.ToLower() + "-" + DateTime.UtcNow.ToString("yyyy-MM-dd-HH");

        public ElasticSearchCommonLoggerProvider(ElasticSearchCommonLoggerOptions options)
        {
            _endpoint = options.ElasticsearchEndpoint;
            _indexPrefix = options.IndexName;

            Initialize();

        }

        public Microsoft.Extensions.Logging.ILogger CreateLogger(string categoryName)
        {
            return new ElasticSearchCommonLogger(categoryName, _scribeProcessor);
        }


        public IElasticLowLevelClient Client
        {
            get
            {
                if (_client != null)
                {
                    return _client;
                }
                else
                {
                    var singleNode = new SingleNodeConnectionPool(_endpoint);

                    var cc = new ConnectionConfiguration(singleNode, new ElasticSearchJsonNetSerializer())
                        .RequestTimeout(TimeSpan.FromSeconds(15))
                        .EnableHttpPipelining()
                        .ThrowExceptions();

                    this._client = new ElasticLowLevelClient(cc);
                    return this._client;
                }
            }
        }

        private void Initialize()
        {
            SetupObserverBatchy();
        }


        private void SetupObserverBatchy()
        {
            _scribeProcessor = a => WriteToQueueForProcessing(a);

            this._queueToBePosted.GetConsumingEnumerable()
                .ToObservable(Scheduler.Default)
                .Buffer(TimeSpan.FromSeconds(1), 10)
                .Subscribe(async x => await this.WriteDirectlyToESAsBatch(x));
        }

        private void WriteToQueueForProcessing(JObject jo)
        {
            this._queueToBePosted.Add(jo);
        }

        private async Task WriteDirectlyToESAsBatch(IEnumerable<JObject> jos)
        {
            if (!jos.Any())
                return;

            var indx = new { index = new { _index = Index, _type = DocumentType } };
            var indxC = Enumerable.Repeat(indx, jos.Count());

            var bb = jos.Zip(indxC, (f, s) => new object[] { s, f });
            IEnumerable<object> bbo = bb.SelectMany(a => a);

            PostData data = PostData.MultiJson(bbo);
            try
            {
                await Client.BulkAsync<VoidResponse>(Index, data);
            }
            catch (Exception ex)
            {

            }
        }


        private async Task WriteDirectlyToES(JObject jo)
        {
            try
            {
                await Client.IndexAsync<VoidResponse>(Index, DocumentType, jo.ToString());
            }
            catch (Exception ex)
            {

            }
        }

        #region IDisposable Support

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion

    }
}
