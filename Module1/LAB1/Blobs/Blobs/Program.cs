using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Shared.Protocol;

//Don't forget to update the connection string in the App.config file

namespace Blobs
{
    class Program
    {
        static void Main(string[] args)
        {
            string storageconnection = 
                System.Configuration.ConfigurationManager.AppSettings.Get("StorageConnectionString");
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(storageconnection);

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer container = blobClient.GetContainerReference("module2");

            container.CreateIfNotExists();

            ListBlobs(container);

            CreateCORSPolicy(blobClient);

            Console.ReadKey();
        }

        static void UploadBlob(CloudBlobContainer container)
        {
            CloudBlockBlob blockBlob = container.GetBlockBlobReference("mytestfile.txt");

            using (var fileStream = System.IO.File.OpenRead(@"c:\mytestfile.txt"))
            {
                blockBlob.UploadFromStream(fileStream);
            }

        }

        static void ListBlobs(CloudBlobContainer container)
        {
            var blobs = container.ListBlobs();
            foreach(var blob in blobs){
                Console.WriteLine(blob.Uri.ToString());
            }
        }

        static void ListAttributes(CloudBlobContainer container)
        {
            container.FetchAttributes();
            Console.WriteLine("Container name " + container.StorageUri.PrimaryUri.ToString());
            Console.WriteLine("Last modified " + container.Properties.LastModified.ToString());
        }

        static void SetMetadata(CloudBlobContainer container)
        {
            container.Metadata.Clear();
            container.Metadata.Add("Author", "Azure Student");
            container.Metadata["Created"] = DateTime.Now.ToString();
            container.SetMetadata();
        }

        static void ListMetadata(CloudBlobContainer container)
        {
            container.FetchAttributes();
            Console.WriteLine("Metadata:\n");
            foreach (var item in container.Metadata)
            {
                Console.WriteLine("Key " + item.Key);
                Console.WriteLine("Value " + item.Value + "\n\n");
            }
        }

        static void CopyBlob(CloudBlobContainer container)
        {
            CloudBlockBlob blockBlob = container.GetBlockBlobReference("mytestfile.txt");
            CloudBlockBlob copyToBlockBlob = container.GetBlockBlobReference("mytestfile-copy.txt");
            copyToBlockBlob.StartCopyAsync(new Uri(blockBlob.Uri.AbsoluteUri));
        }

        static void UploadBlobSubdirectory(CloudBlobContainer container)
        {
            CloudBlobDirectory directory = container.GetDirectoryReference("allfiles");
            CloudBlobDirectory subdirectory = directory.GetDirectoryReference("text-files");
            CloudBlockBlob blockBlob = subdirectory.GetBlockBlobReference("mytestfile.txt");

            using (var fileStream = System.IO.File.OpenRead(@"c:\mytestfile.txt"))
            {
                blockBlob.UploadFromStream(fileStream);
            }

        }

        static void CreateSharedAccessPolicy(CloudBlobContainer container)
        {
            //Create a new stored access policy and define its constraints.
            SharedAccessBlobPolicy sharedPolicy = new SharedAccessBlobPolicy()
            {
                SharedAccessExpiryTime = DateTime.UtcNow.AddHours(24),
                Permissions = SharedAccessBlobPermissions.Read | SharedAccessBlobPermissions.Write | SharedAccessBlobPermissions.List
            };

            //Get the container's existing permissions.
            BlobContainerPermissions permissions = new BlobContainerPermissions();

            //Add the new policy to the container's permissions.
            permissions.SharedAccessPolicies.Clear();
            permissions.SharedAccessPolicies.Add("PolicyName", sharedPolicy);
            container.SetPermissions(permissions);
        }

        static void CreateCORSPolicy(CloudBlobClient blobClient)
        {
            ServiceProperties sp = new ServiceProperties();
            sp.Cors.CorsRules.Add(new CorsRule()
            {
                AllowedMethods = CorsHttpMethods.Get,
                AllowedOrigins = new List<string>() { "http://localhost:8080/"},
                MaxAgeInSeconds = 3600,
            });
            blobClient.SetServiceProperties(sp);
        }

    }
}
