namespace GiphyCli
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using JetBrains.Annotations;
    using RestSharp;
    using RestSharp.Deserializers;

    public class GiphyApi
    {
        const string BaseUrl = "https://api.giphy.com";

        readonly string apiKey;

        public GiphyApi(string apiKey)
        {
            this.apiKey = apiKey;
        }

        [CanBeNull]
        public GifObject Search(string search)
        {
            var restRequest = new RestRequest();
            restRequest.Resource = "/v1/gifs/search?api_key={api_key}&q={q}&limit=1";
            restRequest.AddParameter("q", search, ParameterType.UrlSegment);
            var result = this.Execute<RawGifSearchResult>(restRequest);
            var rawGifObject = result.Data.FirstOrDefault();
            if (rawGifObject == null)
            {
                return null;
            }

            return new GifObject
            {
                Id = rawGifObject.Id,
                Slug = rawGifObject.Slug,
                Title = rawGifObject.Title,
                Url = rawGifObject.Url,
                GifUrl = rawGifObject.Images.OriginalGif.Url,
            };
        }

        private T Execute<T>(RestRequest request) where T : new()
        {
            var client = new RestClient();
            client.BaseUrl = new System.Uri(BaseUrl);
            request.AddParameter("api_key", this.apiKey, ParameterType.UrlSegment); // used on every request
            var response = client.Execute<T>(request);

            if (response.ErrorException != null)
            {
                const string message = "Error retrieving response. Check inner details for more info.";
                var giphyException = new ApplicationException(message, response.ErrorException);
                throw giphyException;
            }
            return response.Data;
        }

        private class RawGifSearchResult
        {
            public List<RawGifObject> Data { get; set; }
        }

        private class RawGifObject
        {
            // https://developers.giphy.com/docs/#gif-object
            public string Id { get; set; }

            public string Slug { get; set; }

            public string Title { get; set; }

            public string Url { get; set; }

            public RawGifImagesObject Images { get; set; }
        }

        private class RawGifImagesObject
        {
            [DeserializeAs(Name = "fixed_width")]
            public RawImageObject FixedWidthGif { get; set; }

            [DeserializeAs(Name = "original")]
            public RawImageObject OriginalGif { get; set; }
        }

        private class RawImageObject
        {
            public string Url { get; set; }
        }
    }

    public class GifObject
    {
        public string Id { get; set; }

        public string Slug { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        public string GifUrl { get; set; }
    }
}