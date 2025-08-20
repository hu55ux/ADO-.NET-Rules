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


                                            

                                                                    Configuration file


Codumuzda istifadə etdiyimiz connection string-in birbaşa .cs faylında yazılması təhlükəsizlik baxımından düzgün olmayan bir yanaşmadır.
Ən yaxşı təcrübə olaraq, bu məlumatları konfiqurasiya faylında saxlamaq daha məqsədəuyğundur.
Configuration fayllar bir neçə növ ola bilər, məsələn:
// appsettings.json, web.config, app.config və s. Bu fayllar, tətbiqin konfiqurasiyasını saxlamaq üçün istifadə olunur.
Məsələn xml formatında bir app.config faylı aşağıdakı kimi ola bilər:
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <connectionStrings>
    <add name="DefaultConnection" connectionString="Server=(localdb)\MSSQLLocalDB;Database=DatabaseName;Integrated Security=True;Trust Server Certificate=True" providerName="System.Data.SqlClient"/>
  </connectionStrings>
</configuration>

Və bu config faylını codumuza integrasiya edək.
using System;
using System.Configuration;
using Microsoft.Data.SqlClient;

string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

using (SqlConnection sqlConnection = new(connectionString))

JSON formatında bir appsettings.json faylı aşağıdakı kimi ola bilər:
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=DatabaseName;Integrated Security=True;Trust Server Certificate=True"
  }

var builder = new ConfigurationBuilder();
builder.SetBasePath(Directory.GetCurrentDirectory())
       .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

var config = builder.Build();
string connectionString = config.GetConnectionString("DefaultConnection");


Biz bu üsullarla config faylı istifadə edən zaman bizə faylın tapılmaması problemi çıxır və bizdə bunun üçün config faylının properties bölməsində "Copy to Output Directory" seçimini "Copy if newer" olaraq təyin edirik.
Və bu bizə config faylının kodumuzun işlədiyi yerdə tapılmasını təmin edir.
İndi bir işlək code yazaq


List<Authors> authors = new List<Authors>();
using (var connection = new SqlConnection(connectionString)){
        connection.Open();
        SqlCommand command = new SqlCommand("SELECT * FROM Authors", connection);
        SqlDataReader reader = command.ExecuteReader();
        while(reader.Read()){
            authors.Add(new Author{
                Id =(int)reader["Id"],
                FirstName = reader["FirstName"].ToString(),
                LastName = reader["LastName"].ToString(),
            })
        }
}
authors.ForEach(author => 
{
    Console.WriteLine($"Id: {author.Id}, FirstName: {author.FirstName}, LastName: {author.LastName}");
});
// Bu kod parçaları, ADO .NET istifadə edərək verilənlər bazası əməliyyatlarını həyata keçirmək üçün nümunələr təqdim edir.


                                                        




                                                                            SQL Injection

SQL Injection - Verilənlər bazasına göndərilən sorğuların manipulyasiya edilməsi ilə həyata keçirilən bir hücum növüdür.
Bu hücum, istifadəçi tərəfindən daxil edilən məlumatların düzgün yoxlanılmaması və sanitizasiyasının edilməməsi nəticəsində baş verir.
SQL Injection hücumlarından qorunmaq üçün aşağıdakı üsullardan istifadə edə bilərsiniz:
1. Parametrli sorğular (Parameterized Queries): Bu üsul, istifadəçi tərəfindən daxil edilən məlumatların birbaşa SQL sorğusuna daxil edilməməsini təmin edir.
2. Stored Procedures: Verilənlər bazasında saxlanılan prosedurların istifadəsi, SQL Injection hücumlarına qarşı daha güvənli bir yanaşmadır.



1.      Parameterized Queries 

Bu üsul, istifadəçi tərəfindən daxil edilən məlumatların birbaşa SQL sorğusuna daxil edilməməsini təmin edir.
SqlParameter parameter = new SqlParameter("@Name", SqlDbType.NVarChar);
command.Parameters.Add(parameter);
// Parameterized Query nümunəsi
using System;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=DatabaseName;Integrated Security=True;Trust Server Certificate=True";
using (SqlConnection sqlConnection = new(connectionString))
{
    sqlConnection.Open();
   SqlCommand command = new SqlCommand(@$"SELECT * FROM Authors WHERE Id > @id
                                        AND FirstName NOT LIKE @firstName",connectionString);
    command.Parameters.Add("@id",System.Data.SqlDbType.Int).Value = Id;
    command.Parameters.Add("@firstName", System.Data.SqlDbType.NVarChar).Value = firstName;
    SqlDataReader reader = command.ExecuteReader();
    while (reader.Read())
    {
        Console.WriteLine($"Id: {reader["Id"]}, FirstName: {reader["FirstName"]}, LastName: {reader["LastName"]}");
    }


}


2.      Stored Procedures

Stored Procedures - Verilənlər bazasında saxlanılan prosedurların istifadəsi, SQL Injection hücumlarına qarşı daha güvənli bir yanaşmadır.

using (SqlConnection sqlConnection = new(connectionString))
{
    connection.Open();
    SqlCommand command = new (@"CREATE PROC GetBooksCount
                                @AuthorId int,@BooksCount int OUTPUT
                                AS 
                                BEGIN 
                                     SELECT @BooksCount = Count(*)
                                     FROM Books AS B JOIN Authors AS A ON B.AuthorId = A.Id
                                     WHERE A.Id = @AuthorId
                                END
")

    command.ExecuteNonQuery();

}

Procedure ilə birlikdə yazılan kodumuz.

using (SqlConnection sqlConnection = new(connectionString))
{
    connection.Open();
    SqlCommand command = new SqlCommand("GetBooksCount", sqlConnection);
    command.CommandType = System.Data.CommandType.StoredProcedure;
    command.Parameters.Add("@AuthorId",System.Data.SqlDbType.Int).Value = Console.ReadLine();
    SqlParameter outPutParameter = new SqlParameter("@BooksCount", System.Data.SqlDbType.Int);
    sqlParameter.Direction = System.Data.ParameterDirection.Output;
    command.Parameters.Add(outPutParameter);
    command.ExecuteNonQuery();
    Console.WriteLine(command.Parameters["@BooksCount"].Value);
}

 Bu kod parçaları, ADO .NET istifadə edərək verilənlər bazası əməliyyatlarını həyata keçirmək üçün nümunələr təqdim edir.



    




                                                Entity FrameWork Core 

EF Core - .NET Framework daxilində verilənlər bazası ilə işləmək üçün istifadə olunan bir ORM (Object-Relational Mapping) kitabxanasıdır.
EF Core, verilənlər bazası əməliyyatlarını daha asan və səmərəli bir şəkildə həyata keçirmək üçün istifadə olunur.
EF Core  ADO .NET-dən daha az sürətli olmasına baxmayaraq istifadəsi daha asandır və kodunuzu daha oxunaqlı edir.
EF Core, verilənlər bazası ilə işləmək üçün aşağıdakı əsas komponentləri istifadə edir:
// DbContext: Verilənlər bazası ilə əlaqə yaratmaq və əməliyyatları həyata keçirmək üçün istifadə olunan bir kontekstdir.
// DbSet: Verilənlər bazasında saxlanılan cədvəlləri təmsil edən bir kolleksiyadır.
// Migration: Verilənlər bazası sxemini dəyişdirmək üçün istifadə olunan bir mexanizmdir.
// DbContext, DbSet və Migration istifadə edərək verilənlər bazası əməliyyatlarını həyata keçirmək üçün aşağıdakı addımları izləyə bilərsiniz:

EF Core ilə işləyən zaman bizə NuGet paketi lazımdır.
1. Microsoft.EntityFrameworkCore.SqlServer - SQL Server ilə işləmək üçün istifadə olunan EF Core kitabxanasıdır.

EF Core ilə işləyən zaman bu addımlar yerinə yetirilir.

class StudentContext : DbContext // DbContext - EF Core ilə verilənlər bazası əməliyyatlarını həyata keçirmək üçün istifadə olunan bir kontekstdir.
{
    public DbSet<Student> Students { get; set; } // DbSet - Verilənlər bazasında saxlanılan cədvəlləri təmsil edən bir kolleksiyadır.
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=StudentDb;Integrated Security=True;Trust Server Certificate=True");
    }
}

İndi isə Student class-ını yazaq.
class Student // Student - Verilənlər bazasında saxlanılan bir cədvəli təmsil edən bir classdır.
{
    public int Id { get; set; } // Id - Cədvəldəki unikal identifikatoru təmsil edir.
    public string Name { get; set; } // Name - Tələbənin adını təmsil edir.
    public int Age { get; set; } // Age - Tələbənin yaşını təmsil edir.
}

Növbəti addımızda gəlin data əlavə edək:

using (StudentContext db = new()){
    Student student1 = new()
    {
        Name = "John Doe",
        Age = 20
    };
    db.Students.Add(student1); // Tələbəni Students cədvəlinə əlavə edir.
    db.SaveChanges(); // Dəyişiklikləri verilənlər bazasına yazır.
//  Və hal hazırda əlavə bir code bloku və ya commandlardan istifadə etmədən biz verilənlər bazasına data əlavə edə bilirik.
}


Növbəti addımımızda gəlin data oxuyaq:
using (StudentContext db = new())
{
    var students = db.Students.ToList(); // Students cədvəlindəki bütün tələbələri oxuyur.
    foreach (var student in students)
    {
        Console.WriteLine($"Id: {student.Id}, Name: {student.Name}, Age: {student.Age}");
    }

// Və ya 
    var student = db.Students.FirstOrDefault(s => s.Id == 1); // Id-si 1 olan tələbəni oxuyur.
    if (student != null)
    {
        Console.WriteLine($"Id: {student.Id}, Name: {student.Name}, Age: {student.Age}");
    }
}

Növbəti addımımızda gəlin bir neçə data əlavə edək:
using (StudentContext db = new())
{
    List<Student> students = new List<Student>
    {
        new Student { Name = "Alice", Age = 22 },
        new Student { Name = "Bob", Age = 23 },
        new Student { Name = "Charlie", Age = 24 }
    };
    db.Students.AddRange(students); // Bir neçə tələbəni Students cədvəlinə əlavə edir.
    db.SaveChanges(); // Dəyişiklikləri verilənlər bazasına yazır.
    Console.WriteLine("Students added successfully.");
}

Növbəti addım olaraq gəlin bir qrup datanı oxuyaq:

using (StudentContext db = new())
{
    var students = db.Students.Where(s => s.Age > 21).ToList(); // Yaşı 21-dən böyük olan tələbələri oxuyur.
    foreach (var student in students)
    {
        Console.WriteLine($"Id: {student.Id}, Name: {student.Name}, Age: {student.Age}");
    }
    
}


Növbəti addım olaraq gəlin data silək:

using (StudentContext db = new())
{
    var student = db.Students.FirstOrDefault(s => s.Id == 1); // Id-si 1 olan tələbəni tapır.
    if (student != null)
    {
        db.Students.Remove(student); // Tələbəni Students cədvəlindən silir.
        db.SaveChanges(); // Dəyişiklikləri verilənlər bazasına yazır.
        Console.WriteLine("Student deleted successfully.");
    }
    else
    {
        Console.WriteLine("Student not found.");
    }
}

Növbəti addım olaraq gəlin bir neçə data silək:
using (StudentContext db = new())
{
    var students = db.Students.Where(s => s.Age < 22).ToList(); // Yaşı 22-dən kiçik olan tələbələri tapır.
    if (students.Count > 0)
    {
        db.Students.RemoveRange(students); // Tələbələri Students cədvəlindən silir.
        db.SaveChanges(); // Dəyişiklikləri verilənlər bazasına yazır.
        Console.WriteLine("Students deleted successfully.");
    }
    else
    {
        Console.WriteLine("No students found to delete.");
    }
}

Növbəti addım olaraq gəlin data yeniləyək:

using (StudentContext db = new())  
{
    var student = db.Students.FirstOrDefault(s => s.Id == 1); // Id-si 1 olan tələbəni tapır.
    if (student != null)
    {
        student.Name = "Updated Name"; // Tələbənin adını yeniləyir.
        student.Age = 25; // Tələbənin yaşını yeniləyir.
        db.SaveChanges(); // Dəyişiklikləri verilənlər bazasına yazır.
        Console.WriteLine("Student updated successfully.");
    }
    else
    {
        Console.WriteLine("Student not found.");
    }
}

Bəzi böyük databaselərlə işləyən zaman bizə databasedə olan hər bir Entity üçün bir class yaratmaq lazım gəlir ki buda bizə uzun və yorucu bir iş ola bilər.
İlk öncə lazım olan 3 NuGet paketini əlavə edirik:
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
Bu zaman biz Project-i terminalda açaraq aşağıdakı komandanı icra edirik
İlk öncə dotnet ef tool-unu quraşdırırıq:
dotnet tool install --global dotnet-ef
Sonra isə aşağıdakı komandanı icra edirik:
dotnet ef dbcontext scaffold "Server=(localdb)\MSSQLLocalDB;Database=StudentDb;Integrated Security=True;Trust Server Certificate=True" Microsoft.EntityFrameworkCore.SqlServer

Proqramlaşdırmada bu addımlara Reverse Engineering deyilir Və bu addım bizə verilənlər bazasındakı bütün cədvəlləri, sütunları və əlaqələri təmsil edən class-ları avtomatik olaraq yaradır.



































































































 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 */