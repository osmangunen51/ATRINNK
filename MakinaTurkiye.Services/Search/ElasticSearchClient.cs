using Nest;
using System.Collections.Generic;
using System.Linq;

namespace MakinaTurkiye.Services.Search
{

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Url { get; set; } = "";
        public string Category { get; set; } = "";
        public string Keyword { get; set; } = "";
        public CompletionField Suggest { get; set; }
    }

    public class ProductSuggestResponse
    {
        public IEnumerable<ProductSuggest> Suggests { get; set; }
    }

    public class ProductSuggest
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Category { get; set; } = "";
        public double Score { get; set; }
        public string Url { get; set; } = "";
    }

    public class ElasticSearchClient
    {
        readonly ElasticClient _elasticClient;
        public ElasticSearchClient(ConnectionSettings connectionSettings)
        {
            _elasticClient = new ElasticClient(connectionSettings);
        }
        public bool CreateIndex(string indexName)
        {
            var createIndexDescriptor = new CreateIndexDescriptor(indexName.ToLowerInvariant())
            .Mappings(ms => ms
                          .Map<Product>(m => m
                                .AutoMap()
                                .Properties(ps => ps
                                    .Completion(c => c
                                        .Name(p => p.Suggest))))

                         );
            //if (_elasticClient.IndexExists(indexName.ToLowerInvariant()).Exists)
            //{
            //    _elasticClient.DeleteIndex(indexName.ToLowerInvariant());
            //}

            var createIndexResponse =_elasticClient.Indices.Create(createIndexDescriptor);
            return createIndexResponse.IsValid;
        }
        public bool DeleteIndex(string indexName)
        {
            var DeleteIndexRequest = new DeleteIndexRequest(indexName.ToLowerInvariant());
            if (_elasticClient.Indices.Exists(indexName.ToLowerInvariant()).Exists)
            {
                var Sonuc=_elasticClient.Indices.Delete(DeleteIndexRequest);
                return Sonuc.IsValid;
            }
            return false;
        }

        public bool CheckIndex(string indexName)
        {
            bool Sonuc= _elasticClient.Indices.Exists(indexName.ToLowerInvariant()).Exists;
            return Sonuc;
        }


        public void IndexVeriYukle(string indexName, List<Product> Liste)
        {
            if (Liste.Count>0)
            {
                var Sonuc = _elasticClient.IndexMany<Product>(Liste, indexName);
            }
        }

        public ProductSuggestResponse Suggest(string indexName, string keyword)
        {
            ISearchResponse<Product> searchResponse = _elasticClient.Search<Product>(s => s
                                     .Index(indexName.ToLowerInvariant())
                                     .Suggest(su => su
                                          .Completion("suggestions", c => c
                                               .Prefix(keyword)
                                               .Field(f => f.Suggest)
                                               .Size(1000))
                                             ));

            List<ProductSuggest> SuggestsListesi = new List<ProductSuggest>();
            if (searchResponse.Suggest.ContainsKey("suggestions"))
            {
                var suggests = from suggest in searchResponse.Suggest["suggestions"]
                               from option in suggest.Options
                               select new ProductSuggest
                               {
                                   Name = option.Source.Name,
                                   Score = option.Score,
                                   Url=option.Source.Url,
                                   Category= option.Source.Category
                               };
                SuggestsListesi = suggests.ToList();
            }
            return new ProductSuggestResponse
            {
                Suggests = SuggestsListesi
            };
        }

        public ProductSuggestResponse Search(string indexName, string keyword)
        {
            IEnumerable<ProductSuggest> Liste = new List<ProductSuggest>();
            var SuggestsListesi = _elasticClient.Search<Product>(s => s
            .Index(indexName.ToLowerInvariant())
            .From(0)
            .Size(10000).
                Query(x=>x.Match(c => c
                .Field(p => p.Name)
                .Analyzer("standard")
                .Boost(1.1)
                .Query(keyword)
                .Fuzziness(Fuzziness.Auto)
                .FuzzyTranspositions()
                .FuzzyRewrite(MultiTermQueryRewrite.TopTermsBlendedFreqs(10))
                .Name("named_query")
            ))
            .Sort(ss => ss
                .Field(x=>x.Field(y=>y.Suggest.Weight)
                    .Order(SortOrder.Descending)
                    .MissingLast()
                    .UnmappedType(FieldType.Integer)
                    .Mode(SortMode.Max)
                )
            ))
            .Documents.Select(x=>new ProductSuggest() {
                Id=x.Id,
                Name=x.Name,
                Url=x.Url,
                Category=x.Category,
                Score= x.Suggest.Weight.Value
            });
            return new ProductSuggestResponse
            {
                Suggests = SuggestsListesi
            };
        }
    }
}
