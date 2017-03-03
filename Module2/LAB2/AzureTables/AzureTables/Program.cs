using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage.Queue;

//Don't forget to update the app.config file

namespace AzureTables
{
    class Program
    {
        static void Main(string[] args)
        {
            //Connect to the storage account
            string storageconnection = System.Configuration.ConfigurationManager.AppSettings.Get("StorageConnectionString");
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(storageconnection);

            //Create the table if it doesn't already exist
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("MyAzureTable");
            table.CreateIfNotExists();

            //Add a single item to the table
            //var newCar = new CarEntity(1, 2016, "Ford", "F150", "Silver");
            //var insert = TableOperation.Insert(newCar);
            //table.Execute(insert);

            //Perform a batch insert using a transaction
            //TableBatchOperation tbo = new TableBatchOperation();

            //CarEntity newcar = new CarEntity(2, 2012, "BMW", "X1", "Black");
            //tbo.Insert(newcar);

            //newcar = new CarEntity(3, 2012, "Honda", "Civic", "Yellow");
            //tbo.Insert(newcar);

            //newcar = new CarEntity(4, 2013, "BMW", "X1", "White");
            //tbo.Insert(newcar);

            //newcar = new CarEntity(5, 2014, "BMW", "X1", "Silver");
            //tbo.Insert(newcar);

            //table.ExecuteBatch(tbo);

            //Retrieve a specific item
            //TableOperation retrieve = TableOperation.Retrieve<CarEntity>("car", "1");
            //TableResult result = table.Execute(retrieve);

            //if (result.Result == null)
            //{
            //    Console.WriteLine("not found");
            //}
            //else
            //{
            //    Console.WriteLine("found the car " + ((CarEntity)result.Result).Make + " " + ((CarEntity)result.Result).Model);
            //}

            //Retrieve all items from the table
            //TableQuery<CarEntity> carquery = new TableQuery<CarEntity>().Take(5);
            //foreach (CarEntity thiscar in table.ExecuteQuery(carquery))
            //{
            //    Console.WriteLine(thiscar.Year.ToString() + " " + thiscar.Make + " " + thiscar.Model + " " + thiscar.Color);
            //}

            //Create a queue
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            CloudQueue myqueue = queueClient.GetQueueReference("myqueue");
            myqueue.CreateIfNotExists();

            //Add a message to the queue
            CloudQueueMessage setMessage = new CloudQueueMessage("This is a test message!");
            myqueue.AddMessage(setMessage);

            //Get the queue length
            myqueue.FetchAttributes();
            Console.WriteLine(myqueue.ApproximateMessageCount);

            //Peak at the message and leave on queue
            CloudQueueMessage peakMessage = myqueue.PeekMessage();
            Console.WriteLine(peakMessage.AsString);

            //Get message from the queue
            CloudQueueMessage message = myqueue.GetMessage();
            Console.WriteLine(message.AsString);

            //De-queue the message
            myqueue.DeleteMessage(message);

            //Delete the queue
            myqueue.Delete();

            Console.ReadKey();

        }
    }

    public class CarEntity : TableEntity
    {
        public CarEntity(int ID, int year, string make, string model, string color)
        {
            this.UniqueID = ID;
            this.Year = year;
            this.Make = make;
            this.Model = model;
            this.Color = color;
            this.PartitionKey = "car";
            this.RowKey = ID.ToString();
        }

        public CarEntity() { }

        public int UniqueID { get; set; }

        public int Year { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public string Color { get; set; }

    }
}
