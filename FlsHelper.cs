using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq.Expressions;

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
                var query = string.Format(@"SELECT we.[Id], we.[Name], we.WeldingType, we.[WeldingPosition], we.[LotId] ,we.[IsFirstWelding] FROM [TestDataDb].[dbo].[Weldings] as we INNER JOIN [TestDataDb].[dbo].Lots as lot on lot.Id = we.LotId AND lot.Name = '{0}'", lotName);

                var thisCommand = _sqlConnection.CreateCommand();

                thisCommand.CommandText = query;

                using (var thisReader = thisCommand.ExecuteReader())
                {
                    while (thisReader.Read())
                    {
                        var newWelding = new Welding()
                        {
                            Id = (int)thisReader["Id"],
                            LotId = (int)thisReader["LotId"],
                            WeldingPosition = (int)thisReader["WeldingPosition"],
                            Name = thisReader["Name"].ToString(),
                            IsFirstWelding = (bool)thisReader["IsFirstWelding"],
                            WeldingType = thisReader["WeldingType"].ToString()
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

        public static IEnumerable<Bobbin> GetBobbins()
        {
            var query = @"SELECT [Name], [Id], [BobbinOrderId] FROM [TestDataDb].[dbo].[Bobbins]";

            var bobbins = new List<Bobbin>();

            using (_sqlConnection = new SqlConnection(ConnectionString))
            {
                SqlConnection.Open();

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

                GetBobbinsByBobbinOrderName("D_12345678-001");

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
                var query = string.Format(@"SELECT [Id], [Name], [BobbinId] FROM [TestDataDb].[dbo].[Lots]");

                var thisCommand = _sqlConnection.CreateCommand();

                thisCommand.CommandText = query;

                using (var thisReader = thisCommand.ExecuteReader())
                {
                    while (thisReader.Read())
                    {
                        var nameTemp = thisReader["Name"].ToString();
                        var idTemp = (int)thisReader["Id"];
                        var bobbinIdTemp = (int)thisReader["BobbinId"];

                        var tempLot = new Lot()
                        {
                            Id = idTemp,
                            Name = nameTemp,
                            BobbinId = bobbinIdTemp
                        };

                        lotNameList.Add(tempLot);

                    }
                }

                // SetExtractedValue("lotNames", String.Join(",", lotNameList));

                return lotNameList;
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
                var query = string.Format(@"SELECT [Id], [Name], [BobbinId] FROM [TestDataDb].[dbo].[Lots] WHERE [BobbinId] = '{0}' ", bobbinId);

                var thisCommand = _sqlConnection.CreateCommand();

                thisCommand.CommandText = query;

                using (var thisReader = thisCommand.ExecuteReader())
                {
                    while (thisReader.Read())
                    {
                        var nameTemp = thisReader["Name"].ToString();
                        var idTemp = (int)thisReader["Id"];
                        var bobbinIdTemp = (int)thisReader["BobbinId"];

                        var tempLot = new Lot()
                        {
                            Id = idTemp,
                            Name = nameTemp,
                            BobbinId = bobbinIdTemp
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
    }

    public class Welding
    {
        public int? LotId { get; set; }
        public int Id { get; set; }
        public bool? IsFirstWelding { get; set; }
        public int? WeldingPosition { get; set; }
        public string WeldingType { get; set; }
        public string Name { get; set; }
    }

    public class Bobbin
    {
        public int BobbinOrderId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<Lot> Lots { get; set; }
    }

    public class BobbinOrder
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }

    public class Lot
    {
        public int BobbinId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Welding> Weldings { get; set; }
    }
}