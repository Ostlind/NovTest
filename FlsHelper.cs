using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
namespace Nov_Test
{
    public static class Helper
    {
        private static SqlConnection SqlConnection { get; set; }

        private static SqlConnection _sqlConnection;

        static Helper()
        {
            SqlConnection = new SqlConnection(ConnectionString);
        }

        private const string ConnectionString = "Data Source=localhost; Initial Catalog=TestDataDb; Integrated Security=true;";

        public static IEnumerable<Welding> GetWeldings(string lotName)
        {
            using (_sqlConnection = new SqlConnection(ConnectionString))
            {
                //GetExtractedValue("bobbinName")
                _sqlConnection.Open();

                var weldings = new List<Welding>();
                //This is a simple SQL command that will go through all the values in the "City" column from the table "Table_1"
                var query = string.Format(@"SELECT we.[Id], we.[Name], we.WeldingType, we.[WeldingPosition], we.[LotId] ,we.[IsFirstWelding], we.[EquipmentId], we.[VisualInspectorUser], we.[MpiUser], we.[CutoutLength], we.[WeldingSequenceNumber] FROM [TestDataDb].[dbo].[Weldings] as we INNER JOIN [TestDataDb].[dbo].Lots as lot on lot.Id = we.LotId AND lot.Name = '{0}'", lotName);

                var thisCommand = _sqlConnection.CreateCommand();

                thisCommand.CommandText = query;

                using (var thisReader = thisCommand.ExecuteReader())
                {
                    while (thisReader.Read())
                    {
                        var newWelding = new Welding()
                        {
                            Id = (int)thisReader["Id"],
                            Name = thisReader["Name"].ToString(),
                            WeldingType = thisReader["WeldingType"].ToString(),
                            WeldingPosition = (int)thisReader["WeldingPosition"],
                            LotId = (int)thisReader["LotId"],
                            CutoutLength = (int)thisReader["CutoutLength"],
                            IsFirstWelding = (bool?)thisReader["IsFirstWelding"],
                            EquipmentId = (int)thisReader["EquipmentId"],
                            VisualInspectorUser = (string)thisReader["VisualInspectorUser"],
                            MpiUser = (string)thisReader["MpiUser"],
                            WeldingSequenceNumber = (int)thisReader["WeldingSequenceNumber"],



                        };

                        weldings.Add(newWelding);
                    }
                }

                // SetExtractedValue("lotNames", String.Join(",", lotNameList));

                return weldings;
            }
        }

        public static String nameof<T, TT>(T obj, Expression<Func<T, TT>> propertyAccessor)
        {
            if (propertyAccessor.Body.NodeType == ExpressionType.MemberAccess)
            {
                var memberExpression = propertyAccessor.Body as MemberExpression;
                if (memberExpression == null)
                    return null;
                return memberExpression.Member.Name;
            }
            return null;
        }
        public static Bobbin GetBobbinByBobbinOrderId(int bobbinOrderId)
        {
            
            Bobbin bobbin = null;

            var query = string.Format(@"SELECT [Name], [Id], [BobbinOrderId] FROM [TestDataDb].[dbo].[Bobbins] WHERE [BobbinOrderId] = {0}", bobbinOrderId);

            using (_sqlConnection = new SqlConnection(ConnectionString))
            {
                _sqlConnection.Open();

                var thisCommand = _sqlConnection.CreateCommand();

                thisCommand.CommandText = query;

                using (var thisReader = thisCommand.ExecuteReader())
                {
                    while (thisReader.Read())
                    {
                        bobbin = new Bobbin
                        {
                            Name = thisReader["Name"].ToString(),
                            Id = (int)thisReader["Id"],
                            BobbinOrderId = (int)thisReader["BobbinOrderId"],
                            Lots = null,
                        };
                    }
                }
            }

            return bobbin;
        }

        public static Bobbin GetBobbin(string bobbinName)
        {
            if (string.IsNullOrWhiteSpace(bobbinName))
            {
                throw new ArgumentException("Bobbin Name cannot be null or whitespace.");
            }

            Bobbin bobbin = null;

            var query = string.Format(@"SELECT [Name], [Id], [BobbinOrderId] FROM [TestDataDb].[dbo].[Bobbins] WHERE [Name] = '{0}'", bobbinName);

            using (_sqlConnection = new SqlConnection(ConnectionString))
            {
                _sqlConnection.Open();

                var thisCommand = _sqlConnection.CreateCommand();

                thisCommand.CommandText = query;

                using (var thisReader = thisCommand.ExecuteReader())
                {
                    while (thisReader.Read())
                    {
                        bobbin = new Bobbin
                        {
                            Name = thisReader["Name"].ToString(),
                            Id = (int)thisReader["Id"],
                            BobbinOrderId = (int)thisReader["BobbinOrderId"],
                            Lots = null,
                        };
                    }
                }
            }

            return bobbin;
        }

        public static Bobbin GetBobbinById(int bobbinId)
        {
            if (bobbinId == 0)
            {
                throw new ArgumentException("Bobbin Id cannot be zero");
            }

            Bobbin bobbin = null;

            var query = string.Format(@"SELECT [Name], [Id], [BobbinOrderId] FROM [TestDataDb].[dbo].[Bobbins] WHERE [Id] = {0}", bobbinId);

            using (_sqlConnection = new SqlConnection(ConnectionString))
            {
                _sqlConnection.Open();

                var thisCommand = _sqlConnection.CreateCommand();

                thisCommand.CommandText = query;

                using (var thisReader = thisCommand.ExecuteReader())
                {
                    while (thisReader.Read())
                    {
                        bobbin = new Bobbin
                        {
                            Name = thisReader["Name"].ToString(),
                            Id = (int)thisReader["Id"],
                            BobbinOrderId = (int)thisReader["BobbinOrderId"],
                            Lots = null,
                        };
                    }
                }
            }

            return bobbin;
        }





        public static async Task<string> DownloadBobbinOrder(string bobbinOrderId)
        {
            var serverAdress = "10.40.168.77";
            var url = string.Format("http://{0}/Af.SimaticIt.WebApi/api/SitBobbinOrdersController/StartBobbinOrder/{1}/NOV-KAL-SITE.NOV-PRODUCTION-AREA.KALEB1/noUser/false", serverAdress, bobbinOrderId);
            var uri = new Uri(url);
            string content = string.Empty;

            using (var client = new HttpClient())
            {
                try
                {
                    var httpResponseMessage = await client.PostAsync(uri, null);

                    if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
                    {
                        // Do something...
                    }

                    content = await httpResponseMessage.Content.ReadAsStringAsync();
                }
                catch (OperationCanceledException) { }
            }

            return content;
        }

        public static IEnumerable<Bobbin> GetBobbins()
        {
            var query = @"SELECT [Name], [Id], [BobbinOrderId] FROM [TestDataDb].[dbo].[Bobbins]";

            var bobbins = new List<Bobbin>();

            using (_sqlConnection = new SqlConnection(ConnectionString))
            {
                _sqlConnection.Open();

                SqlCommand thisCommand = _sqlConnection.CreateCommand();

                thisCommand.CommandText = query;

                using (var thisReader = thisCommand.ExecuteReader())
                {
                    while (thisReader.Read())
                    {
                        var newBobbin = new Bobbin
                        {
                            Name = thisReader["Name"].ToString(),
                            Id = (int)thisReader["Id"],
                            BobbinOrderId = (int)thisReader["BobbinOrderId"],
                            Lots = null,
                        };

                        bobbins.Add(newBobbin);
                    }
                }
            }

            return bobbins;
        }

        public static void WriteLogAsync(string text)
        {
            string directoryName = DateTime.Now.ToShortDateString();

            FileInfo fi = new FileInfo(".\\" + directoryName);

            bool exists = Directory.Exists(fi.FullName);

            if (!exists)
            {
                Directory.CreateDirectory(fi.FullName);
            }

            var pathstring = Path.Combine(fi.FullName, String.Format("{0}.txt", DateTime.Now.ToString("yyyy-MM-dd HH.mm")));

            // ".\\nov-test " + logFileName +".txt"
            using (StreamWriter outputFile = new StreamWriter(pathstring, true))
            {
                outputFile.WriteLine(DateTime.Now.ToLongTimeString() + ": " + text);
            }
        }


        public static IEnumerable<Bobbin> GetBobbinsByBobbinOrderName(string bobbinOrderName)
        {
            if (string.IsNullOrWhiteSpace(bobbinOrderName))
            {
                throw new ArgumentNullException("Bobbin order name cannot be null or empty");
            }

            var bobbinOrder = GetBobbindOrderByName(bobbinOrderName);

            using (_sqlConnection = new SqlConnection(ConnectionString))
            {
                //GetExtractedValue("bobbinName")
                _sqlConnection.Open();
                var bobbinsList = new List<Bobbin>();

                //This is a simple SQL command that will go through all the values in the "City" column from the table "Table_1"


                var query = string.Format(@"SELECT [Name], [Id] ,[BobbinOrderId] FROM [TestDataDb].[dbo].[Bobbins] WHERE [BobbinOrderId] = {0}", bobbinOrder.Id);

                SqlCommand thisCommand = _sqlConnection.CreateCommand();
                //This is a simple SQL command that will go through all the values in the "City" column from the table "Table_1"
                thisCommand.CommandText = query;

                using (var thisReader = thisCommand.ExecuteReader())
                {
                    while (thisReader.Read())
                    {
                        var id = (int)thisReader["Id"];

                        var name = thisReader["Name"].ToString();

                        var bobbinOrderId = (int)thisReader["BobbinOrderId"];

                        var newBobbin = new Bobbin
                        {
                            Id = id,
                            Name = name,
                            BobbinOrderId = bobbinOrderId
                        };

                        bobbinsList.Add(newBobbin);
                    }
                }

                // SetExtractedValue("lotNames", String.Join(",", lotNameList));

                return bobbinsList;
            }
        }

        public static BobbinOrder GetBobbindOrderByName(string bobbinOrderName)
        {
            if (string.IsNullOrWhiteSpace(bobbinOrderName))
            {
                throw new ArgumentException("Bobbin order name cannot be null or whitespace.");
            }

            using (_sqlConnection = new SqlConnection(ConnectionString))
            {
                //GetExtractedValue("bobbinName")
                _sqlConnection.Open();

                //This is a simple SQL command that will go through all the values in the "City" column from the table "Table_1"

                BobbinOrder bobbinOrder = null;

                var query = string.Format(@"SELECT [Id], [Name], [IsActive] FROM [TestDataDb].[dbo].[BobbinOrders] WHERE [Name] LIKE '{0}'", bobbinOrderName);

                SqlCommand thisCommand = _sqlConnection.CreateCommand();
                //This is a simple SQL command that will go through all the values in the "City" column from the table "Table_1"
                thisCommand.CommandText = query;

                using (var thisReader = thisCommand.ExecuteReader())
                {
                    while (thisReader.Read())
                    {
                        var id = (int)thisReader["Id"];
                        var name = thisReader["Name"].ToString();
                        var isActive = (bool)thisReader["IsActive"];

                        bobbinOrder = new BobbinOrder()
                        {
                            Id = id,
                            Name = name,
                            IsActive = isActive
                        };
                    }
                }

                // SetExtractedValue("lotNames", String.Join(",", lotNameList));

                return bobbinOrder;
            }
        }

        public static void Main(string[] args)
        {
            try
            {

                //GetBobbinsByBobbinOrderName("D_12345678-001");


                //var weldings = Helper.GetWeldings("coil-test-demo-01").OrderBy(w => w.WeldingSequenceNumber).ToList();

                WriteLogAsync("hejsane");

                // DownloadBobbinOrder("1731006-159").GetAwaiter().GetResult();
                //var value = Convert.ToInt32(false);
                //SetBobbinOrderCompletedStatus("1731006-151", false);

                //GetBobbindOrderByName("D_12345678-001");

                //var lot = GetLotByName("KAL1");

                //lot.Weldings = GetWeldings(lot.Name);

                //var bobbin = GetBobbin("KAL-EA021");

                //var lots = GetLotsByBobbinName(bobbin.Name);

                //var weldings = GetWeldings(lot.Name);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static Lot GetLotByName(string lotName)
        {
            if (string.IsNullOrWhiteSpace(lotName))
            {
                throw new ArgumentException("Lot name cannot be null or whitespace.");
            }

            var query = String.Format(@"SELECT [Id], [Name], [BobbinId] FROM [TestDataDb].[dbo].[Lots] WHERE [Name] LIKE '{0}'", lotName);

            Lot lotTemp = null;

            using (_sqlConnection = new SqlConnection(ConnectionString))
            {
                _sqlConnection.Open();

                SqlCommand thisCommand = _sqlConnection.CreateCommand();
                //This is a simple SQL command that will go through all the values in the "City" column from the table "Table_1"
                thisCommand.CommandText = query;

                using (var thisReader = thisCommand.ExecuteReader())
                {
                    while (thisReader.Read())
                    {
                        var id = (int)thisReader["Id"];
                        var name = thisReader["Name"].ToString();
                        var bobbinId = (int)thisReader["BobbinId"];

                        lotTemp = new Lot
                        {
                            Id = id,
                            Name = name,
                            BobbinId = bobbinId
                        };
                    }
                }
            }

            return lotTemp;
        }

        public static IEnumerable<Lot> GetLots()
        {
            using (_sqlConnection = new SqlConnection(ConnectionString))
            {
                _sqlConnection.Open();

                var lotNameList = new List<Lot>();

                //This is a simple SQL command that will go through all the values in the "City" column from the table "Table_1"
                var query = string.Format(@"SELECT [Id], [Name], [BobbinId], [BatchNumber], [IsUsed], [MaterialLeft], [CoilNumber],[Length], [Comment] FROM [TestDataDb].[dbo].[Lots]");

                var thisCommand = _sqlConnection.CreateCommand();

                thisCommand.CommandText = query;

                using (var thisReader = thisCommand.ExecuteReader())
                {
                    while (thisReader.Read())
                    {
                        var nameTemp = thisReader["Name"].ToString();
                        var idTemp = (int)thisReader["Id"];
                        var bobbinIdTemp = (int)thisReader["BobbinId"];
                        var batchNumberTemp = (string)thisReader["BatchNumber"];
                        var isUsedTemp = (bool)thisReader["IsUsed"];
                        var materialLeftTemp = (int)thisReader["MaterialLeft"];
                        var coilNumberTemp = (int)thisReader["CoilNumber"];
                        var lengthTemp = (int)thisReader["Length"];
                        var commentTemp = (string)thisReader["Comment"];


                        var tempLot = new Lot()
                        {
                            Id = idTemp,
                            Name = nameTemp,
                            BobbinId = bobbinIdTemp,
                            BatchNumber = batchNumberTemp,
                            IsUsed = isUsedTemp,
                            MaterialLeft = materialLeftTemp,
                            CoilNumber = coilNumberTemp,
                            Length = lengthTemp,
                            Comment = commentTemp
                        };

                        lotNameList.Add(tempLot);

                    }
                }

                // SetExtractedValue("lotNames", String.Join(",", lotNameList));

                return lotNameList;
            }
        }

        public static Welding GetFirstWelding(string lotName)
        {
            if (string.IsNullOrEmpty(lotName))
            {
                throw new ArgumentNullException("Lot name cannot be null or empty");
            }


            var firstWelding = GetWeldings(lotName).FirstOrDefault(w => w.IsFirstWelding.Value);




            return firstWelding;
        }
        public static void SetBobbinOrderCompletedStatus(string bobbinName, bool status)
        {

            if (string.IsNullOrWhiteSpace(bobbinName))
            {
                throw new ArgumentException("Bobbin name cannot be null or whitespace");
            }

            using (_sqlConnection = new SqlConnection(ConnectionString))
            {
                _sqlConnection.Open();


                var query = string.Format("UPDATE [dbo].[BobbinOrders] SET IsActive = {0} WHERE [Name] = N'{1}'", Convert.ToInt32(status), bobbinName);

                SqlCommand thisCommand = _sqlConnection.CreateCommand();

                thisCommand.CommandText = query;

                thisCommand.ExecuteNonQuery();
            }
        }

        public static IEnumerable<Lot> GetLotsByBobbinName(string bobbinName)
        {
            if (string.IsNullOrWhiteSpace(bobbinName))
            {
                throw new ArgumentException("Bobbin name cannot be null or whitespace.");
            }

            var bobbin = GetBobbin(bobbinName);

            if (bobbin == null)
            {
                return null;
            }

            var bobbinId = bobbin.Id;

            using (_sqlConnection = new SqlConnection(ConnectionString))
            {
                _sqlConnection.Open();

                var lotNameList = new List<Lot>();

                //This is a simple SQL command that will go through all the values in the "City" column from the table "Table_1"
                var query = string.Format(@"SELECT [Id], [Name], [BobbinId], [BatchNumber], [IsUsed], [MaterialLeft], [CoilNumber], [Length], [Comment] FROM [TestDataDb].[dbo].[Lots] WHERE [BobbinId] = '{0}' ", bobbinId);

                var thisCommand = _sqlConnection.CreateCommand();

                thisCommand.CommandText = query;

                using (var thisReader = thisCommand.ExecuteReader())
                {
                    while (thisReader.Read())
                    {
                        var nameTemp = thisReader["Name"].ToString();
                        var idTemp = (int)thisReader["Id"];
                        var bobbinIdTemp = (int)thisReader["BobbinId"];
                        var batchNumberTemp = (string)thisReader["BatchNumber"];
                        var isUsedTemp = (bool)thisReader["IsUsed"];
                        var materialLeftTemp = (int)thisReader["MaterialLeft"];
                        var coilNumberTemp = (int)thisReader["CoilNumber"];
                        var lengthTemp = (int)thisReader["Length"];
                        var commentTemp = (string)thisReader["Comment"];

                        var tempLot = new Lot()
                        {
                            Id = idTemp,
                            Name = nameTemp,
                            BobbinId = bobbinIdTemp,
                            BatchNumber = batchNumberTemp,
                            IsUsed = isUsedTemp,
                            MaterialLeft = materialLeftTemp,
                            CoilNumber = coilNumberTemp,
                            Length = lengthTemp,
                            Comment = commentTemp

                        };

                        lotNameList.Add(tempLot);

                        // Log.WriteLine("Value of log name column: " + lotName);
                        // Manager.Desktop.KeyBoard.TypeText(" " + lotName);
                    }
                }

                // SetExtractedValue("lotNames", String.Join(",", lotNameList));

                return lotNameList;
            }
        }

        public static IEnumerable<Lot> GetLotsByBobbinId(int bobbinId)
        {
            
          
            using (_sqlConnection = new SqlConnection(ConnectionString))
            {
                _sqlConnection.Open();

                var lotNameList = new List<Lot>();

                //This is a simple SQL command that will go through all the values in the "City" column from the table "Table_1"
                var query = string.Format(@"SELECT [Id], [Name], [BobbinId], [BatchNumber], [IsUsed], [MaterialLeft], [CoilNumber], [Length], [Comment] FROM [TestDataDb].[dbo].[Lots] WHERE [BobbinId] = {0}", bobbinId);

                var thisCommand = _sqlConnection.CreateCommand();

                thisCommand.CommandText = query;

                using (var thisReader = thisCommand.ExecuteReader())
                {
                    while (thisReader.Read())
                    {
                        var nameTemp = thisReader["Name"].ToString();
                        var idTemp = (int)thisReader["Id"];
                        var bobbinIdTemp = (int)thisReader["BobbinId"];
                        var batchNumberTemp = (string)thisReader["BatchNumber"];
                        var isUsedTemp = (bool)thisReader["IsUsed"];
                        var materialLeftTemp = (int)thisReader["MaterialLeft"];
                        var coilNumberTemp = (int)thisReader["CoilNumber"];
                        var lengthTemp = (int)thisReader["Length"];
                        var commentTemp = (string)thisReader["Comment"];

                        var tempLot = new Lot()
                        {
                            Id = idTemp,
                            Name = nameTemp,
                            BobbinId = bobbinIdTemp,
                            BatchNumber = batchNumberTemp,
                            IsUsed = isUsedTemp,
                            MaterialLeft = materialLeftTemp,
                            CoilNumber = coilNumberTemp,
                            Length = lengthTemp,
                            Comment = commentTemp

                        };

                        lotNameList.Add(tempLot);

                    }
                }

                return lotNameList;
            }
        }
    }


    [Serializable]
    public class Welding
    {
        public int? LotId { get; set; }
        public int Id { get; set; }
        public bool? IsFirstWelding { get; set; }
        public int? WeldingPosition { get; set; }
        public string WeldingType { get; set; }
        public string Name { get; set; }
        public int EquipmentId { get; set; }
        public string VisualInspectorUser { get; set; }
        public string MpiUser { get; set; }
        public int CutoutLength { get; set; }
        public int WeldingSequenceNumber { get; set; }
    }
    [Serializable]
    public class Bobbin
    {
        public int BobbinOrderId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<Lot> Lots { get; set; }
    }

    [Serializable]
    public class BobbinOrder
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }

    [Serializable]
    public class Lot
    {
        public int BobbinId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Welding> Weldings { get; set; }
        public bool? IsUsed { get; set; }
        public int? MaterialLeft { get; set; }
        public int? CoilNumber { get; set; }
        public string BatchNumber { get; set; }
        public int Length { get; set; }
        public string Comment { get; set; }
    }
}