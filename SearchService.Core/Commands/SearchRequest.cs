using System.Collections.Generic;

namespace SearchService.Core.Commands
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