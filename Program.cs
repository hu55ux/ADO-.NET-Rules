/*
 
                                                        ADO .NET RULES
 
ADO - ActiveX Data Objects - Database-lər ilə Proqram təminatı arasında əlaqə yaratmaq üçün istifadə olunan bir texnologiyadır. 
ADO .NET isə .NET Framework daxilində verilənlər bazası ilə işləmək üçün istifadə olunan bir kitabxanadır.
Bu kod parçaları, ADO .NET istifadə edərək verilənlər bazası əməliyyatlarını həyata keçirmək üçün nümunələr təqdim edir.
 
Bu işləri görmək üçün bir neçə ADO classları varki bu classlar verilənlər bazası ilə əlaqə yaratmaq,
sorğular göndərmək və nəticələri oxumaq üçün istifadə olunur. Bunlara misal olaraq:

DBConnection: Verilənlər bazası ilə əlaqə yaratmaq üçün istifadə olunur.    
DBCommand: Verilənlər bazasında əməliyyatlar (sorğular, yeniləmələr və s.) həyata keçirmək üçün istifadə olunur.
DBDataReader: Verilənlər bazasından oxunan məlumatları oxumaq üçün istifadə olunur.
DBAdapter: Verilənlər bazası ilə əlaqə yaratmaq və məlumatları DataSet və ya DataTable kimi strukturlara doldurmaq üçün istifadə olunur.
 
 
ADO Datatypelarda varki bunlar :
DataTable: Verilənlər bazasından oxunan məlumatları saxlamaq üçün istifadə olunan bir cədvəl strukturu.
DataSet: Bir və ya daha çox DataTable-ları və əlaqələrini saxlamaq üçün istifadə olunan bir məlumat strukturu.

ADO Connection modes:
1. Connected Mode: Bu rejimdə, tətbiq verilənlər bazası ilə birbaşa əlaqə saxlayır. 
   Bu, verilənlər bazası əməliyyatlarını daha sürətli və effektiv edir, lakin əlaqə açıq qalmalıdır.

2. Disconnected Mode: Bu rejimdə, tətbiq verilənlər bazası ilə əlaqəni bağlayır və məlumatları DataSet və ya DataTable kimi strukturlarda saxlayır. 
   Bu, daha az resurs istifadə edir və verilənlər bazası ilə əlaqə açıq qalmağı tələb etmir. Sadəcə dəyişiklik olunan zaman yeniləmə əməliyyatları həyata keçirilir.
 
 
                                                            Connection Strings

Connection string - Database-ə qoşulmaq üçün yazılan və istifadə olunan bir sətirdir.
string ConnectionString = @"Server=ServerAdress; Database = DatabaseName; User Id = Users Id; Password = Users Password; Inteqrated Security = true"

Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False


Sadə Syntax:

using Microsoft.Data.SqlClient;
SqlConnection sqlConnecction = default;
string connectionString = @"Server= 15.0.4382.1-WIN-4A58BNDB1j2\user;Database=AdoTest;Trust Server Certificate = True";
sqlConnection = new(connectionString);
string insertQuery = "INSERT INTO Students (Name, Age) VALUES ('John Doe', 20)";
SqlCommand sqlCommand = new(insertQuery, sqlConnection);
try
{
sqlConnection.Open();
sqlCommand.ExecuteNonQuery();
}
finally
{
if (sqlConnection != null)
    {
        sqlConnection.Close();
    }
}

// This code snippet is a simple example of how to use ADO.NET to connect to a SQL Server database,

İndi code-umuzu using statement ilə yazaraq daha səliqəli və oxunaqlı edək.
using System;
using Microsoft.Data.SqlClient;

using (SqlConnection sqlConnection = new (connectionString))
{
sqlConnection.Open();
string firstName=Console.ReadLine();
string lastName=Console.ReadLine();

string insertQuery = @$"INSERT INTO Students (FirstName, LastName) VALUES ('{firstName}', '{lastName}')";

command.Connection = sqlConnection;
command.CommandText = insertQuery;
command.ExecuteNonQuery();
}

Indi isə Database-dən oxumaq üçün lazım olan code blokunu yazaq.

string connectionString = "Server=(localdb)\MSSQLLocalDB;Database=DatabaseName;Integrated Security = True;Trust Server Certificate = True";

SqlDataReader reader = null;
SqlCommand command =null;
using (SqlConnection sqlConnection = new(connectionString))
{
command new SqlCommand("SELECT * FROM Students", sqlConnection);
sqlConnection.Open();
reader = command.ExecuteReader();

while (reader.Read())
{
    Console.WriteLine($"ID: {reader["Id"]}, Name: {reader["Name"]}, Age: {reader["Age"]}");
}

reader.FieldCount(); // Bu metod, oxunan cədvəldəki sütun sayını qaytarır.



































































 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 */