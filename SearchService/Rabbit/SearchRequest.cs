using System.Collections.Generic;

namespace SearchService.Rabbit
{
    public class SearchRequest
    {
        public string Text { get; set; }
    }

    public class SearchResponse
    {
        public List<object> Data { get; set; }
    }


}