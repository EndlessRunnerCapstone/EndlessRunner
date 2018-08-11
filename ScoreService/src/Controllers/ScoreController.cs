using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;


using ScoreService.Contracts;

namespace ScoreService.Controllers
{
    [Route("api/[controller]")]
    public class ScoresController : Controller
    {
        private string StorageConnection = Environment.GetEnvironmentVariable("STORAGE_ACCOUNT_CONNECTION");

        private const string ContainerName = "scores";

        private const string FileName = "scores.json";

        [HttpGet]
        public IEnumerable<Score> Get(int? top, int? skip)
        {
            if(top != null && top.Value < 1)
            {
                return Enumerable.Empty<Score>();
            }

            int numberOfResults = top.HasValue ? top.Value : 5;

            if(string.IsNullOrEmpty(StorageConnection) || !CloudStorageAccount.TryParse(StorageConnection, out CloudStorageAccount cloudStorageAccount))
            {
                cloudStorageAccount = CloudStorageAccount.DevelopmentStorageAccount;
            }

            var blobClient = cloudStorageAccount.CreateCloudBlobClient();

            var container = blobClient.GetContainerReference(ContainerName);

            var create = container.CreateIfNotExistsAsync().Result;

            var blob = container.GetBlockBlobReference(FileName);

            if(blob.ExistsAsync().Result)
            {
                var text = blob.DownloadTextAsync().Result;
                return Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<Score>>(text).OrderByDescending(x => x.Value).Take(numberOfResults);
            }
            else
            {
                blob.UploadTextAsync("[]").GetAwaiter().GetResult();
                return Enumerable.Empty<Score>();
            }
        }

        [HttpGet("{id}")]
        public Score Get(int position)
        {
            return new Score { User = "TestUser", Value = 100 };
        }

        [HttpPost]
        public void Post([FromBody]Score value)
        {
            if(string.IsNullOrEmpty(StorageConnection) || !CloudStorageAccount.TryParse(StorageConnection, out CloudStorageAccount cloudStorageAccount))
            {
                cloudStorageAccount = CloudStorageAccount.DevelopmentStorageAccount;
            }
            
            var blobClient = cloudStorageAccount.CreateCloudBlobClient();

            var container = blobClient.GetContainerReference(ContainerName);

            var create = container.CreateIfNotExistsAsync().Result;

            var blob = container.GetBlockBlobReference(FileName);

            if(blob.ExistsAsync().Result)
            {
                var text = blob.DownloadTextAsync().Result;
                var scores = Newtonsoft.Json.JsonConvert.DeserializeObject<IList<Score>>(text);

                if(!scores.Any(x => x.User == value.User))
                {
                    scores.Add(value);
                }
                else if(scores.FirstOrDefault(x => x.User == value.User).Value < value.Value)
                {
                    scores.FirstOrDefault(x => x.User == value.User).Value = value.Value;
                }
                else
                {
                    return;
                }

                text = Newtonsoft.Json.JsonConvert.SerializeObject(scores);

                blob.UploadTextAsync(text).GetAwaiter().GetResult();
            }
            else
            {
                var scores = new List<Score> { value };
                var scoresAsText = Newtonsoft.Json.JsonConvert.SerializeObject(scores);
                blob.UploadTextAsync(scoresAsText).GetAwaiter().GetResult();
            }
        }
    }
}
