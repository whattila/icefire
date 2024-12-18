using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace icefire.Services
{
    // This class contains a model entity (Book, Character, House or a List of these)
    // and a string of links leading to the first, previous, next and last page of results.
    // The class is used when we navigate between pages of entity groups on the entity list screens.
    class Result<T>
    {
        public string FirstLink { get; set; }
        public string PrevLink { get; set; }
        public string NextLink { get; set; }
        public string LastLink { get; set; }
        public T Entity { get; set; } 
        public Result(string links, T entity)
        {
            Entity = entity;

            // Now we set the [Relation]Link properties, similarly to https://gist.github.com/pimbrouwers/8f78e318ccfefff18f518a483997be29
            if (!string.IsNullOrWhiteSpace(links))
            {
                // Getting the individual links from the initial string.
                // There's a comma (,) at the end of each link, so we use that as separator.
                string[] linkStrings = links.Split(',');

                if (linkStrings != null && linkStrings.Any())
                {
                    // We get the relation type (first, previous, next, last) and the link itself from each of the link strings
                    foreach (string linkString in linkStrings)
                    {
                        // Those two match specific regular expressions, so we can find them with regexes.
                        var relMatch = Regex.Match(linkString, "(?<=rel=\").+?(?=\")", RegexOptions.IgnoreCase);
                        var linkMatch = Regex.Match(linkString, "(?<=<).+?(?=>)", RegexOptions.IgnoreCase);

                        if (relMatch.Success && linkMatch.Success)
                        {
                            string rel = relMatch.Value.ToUpper();
                            string link = linkMatch.Value;

                            switch (rel)
                            {
                                case "FIRST":
                                    FirstLink = link;
                                    break;
                                case "PREV":
                                    PrevLink = link;
                                    break;
                                case "NEXT":
                                    NextLink = link;
                                    break;
                                case "LAST":
                                    LastLink = link;
                                    break;
                            }
                        }
                    }
                }
            }
        }
    }

    class IceAndFireService
    {
        private async Task<T> GetAsync<T>(Uri uri)
        {
            using (var client = new HttpClient())
            {
                // The content of the response to the request is coverted to JSON, then to a model object.
                var response = await client.GetAsync(uri);
                var json = await response.Content.ReadAsStringAsync();
                T result = JsonConvert.DeserializeObject<T>(json);
                return result;
            }
        }
        private async Task<Result<T>> GetListAsync<T>(Uri uri)
        {
            using (var client = new HttpClient())
            {
                // The content of the response to the request is coverted to JSON, then to a model object.
                // We make a Result object from it and the link header.
                var response = await client.GetAsync(uri);
                var linkHeader = response.Headers.GetValues("Link").FirstOrDefault();
                var json = await response.Content.ReadAsStringAsync();
                T entity = JsonConvert.DeserializeObject<T>(json);
                Result<T> result = new Result<T>(linkHeader, entity);
                return result;
            }
        }

        private readonly Uri serverUrl = new Uri("https://anapioficeandfire.com");

        // Gets the first page of entities (10 entities in total) in the specified category (Book, Character or House).
        public async Task<Result<List<T>>> GetFirstGroupPageAsync<T>(string category)
        {
            return await GetListAsync<List<T>>(new Uri(serverUrl, $"api/{category}?page=1"));
        }

        // Gets a page of entities (10 entities in total) in a "T" category (Book, Character or House), at the given url.
        // It does not check the url at all.
        public async Task<Result<List<T>>> GetGroupPageAsync<T>(string url)
        {
            return await GetListAsync<List<T>>(new Uri(url));
        }

        // GetFirstGroupPageAsync is quite unnecessary. Maybe it would be better to have a getRoot function, 
        // and the call GetGroupPageAsync when I call GetFirstGroupPageAsync.
        // Maybe I could change to that after submitting.

        // Gets the specific entity (Book, Character or House), identified by an id number, at the given url.
        // It does not check the url at all.
        public async Task<T> GetSpecificEntity<T>(string url)
        {
            return await GetAsync<T>(new Uri(url));
        }
    }
}
