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




                                                            Code First vs Database First

Code First və Database First - Verilənlər bazası dizaynı və inkişafı üçün istifadə olunan iki fərqli yanaşmadır.

Code First - Bu yanaşmada, əvvəlcə proqram kodu yazılır və sonra bu koddan verilənlər bazası sxemi avtomatik olaraq yaradılır.
Database First - Bu yanaşmada, əvvəlcə verilənlər bazası dizayn edilir və sonra bu dizayndan proqram kodu avtomatik olaraq yaradılır.


                                                            Entity Framework Core 

EF Core - Entity Framework Core - .NET Framework daxilində verilənlər bazası ilə işləmək üçün istifadə olunan bir ORM (Object-Relational Mapping) kitabxanasıdır.
Bu kitabxana, verilənlər bazası əməliyyatlarını daha asan və səmərəli bir şəkildə həyata keçirmək üçün istifadə olunur və artıq code yazarkən bir SQL əməliyyatı yazmağa ehtiyac qalmır.
Bunun üçün biz LİNQ for Entities-dən istifadə edirik. Yazdığımız hər bir code sətri arxa planda bir SQL əməliyyatı yaradır. Və bu əməliyyatlar verilənlər bazasında icra olunur. Bununla biz həm vaxt qazanırıq həm də kodumuz daha oxunaqlı olur.
using ADO_NET_64._LINQ_For_Entity; Kitabxanasından istifadə edirik.

using LibraryContext context = new();

Gəlin bu methodlara ətraflı baxaq:
1.Where: Bu metod, verilənlər bazasından müəyyən bir şərtə uyğun gələn qeydləri seçmək üçün istifadə olunur.
context.Books.Where(b => b.AuthorId == 1).ToList().ForEach(Console.WriteLine());

2. EF.Functions.Like: Bu metod, verilənlər bazasında müəyyən bir şərtə uyğun gələn qeydləri seçmək üçün istifadə olunur.
context.Books.Where(b => EF.Functions.Like(b.Title, "%C#%")).ToList().ForEach(Console.WriteLine());

3. First, FirstOrDefault, Single, SingleOrDefault: Bu metodlar, verilənlər bazasından müəyyən bir şərtə uyğun gələn ilk və ya tək qeydi seçmək üçün istifadə olunur.
context.Books.First(b => b.Id == 1); // Id-si 1 olan kitabı seçir.
context.Books.FirstOrDefault(b => b.Id == 1); // Id-si 1 olan kitabı seçir və əgər belə bir kitab yoxdursa, null qaytarır.
context.Books.Single(b => b.Id == 1); // Id-si 1 olan tək kitabı seçir. Əgər belə bir kitab yoxdursa və ya bir neçə kitab varsa, xəta verir.
context.Books.SingleOrDefault(b => b.Id == 1); // Id-si 1 olan tək kitabı seçir və əgər belə bir kitab yoxdursa, null qaytarır. Əgər bir neçə kitab varsa, xəta verir.

4. Find: Bu metod, verilənlər bazasından müəyyən bir şərtə uyğun gələn qeydi seçmək üçün istifadə olunur.
context.Books.Find(1); // Id-si 1 olan kitabı seçir.

5.All, Any: Bu metodlar, verilənlər bazasında müəyyən bir şərtə uyğun gələn bütün qeydləri və ya heç bir qeyd olmadığını yoxlamaq üçün istifadə olunur.
context.Books.All(b => b.AuthorId == 1); // Bütün kitabların AuthorId-si 1-dir.
context.Books.Any(b => b.AuthorId == 1); // Hər hansı bir kitabın AuthorId-si 1-dir.

6.Select: Bu metod, verilənlər bazasından müəyyən bir şərtə uyğun gələn qeydləri seçmək və müəyyən sahələri götürmək üçün istifadə olunur.
context.Books.Select(b => new { b.Title, b.PublishedYear }).ToList().ForEach(b => Console.WriteLine($"{b.Title} - {b.PublishedYear}"));


7.OrderBy, OrderByDescending, ThenBy, ThenByDescending: Bu metodlar, verilənlər bazasından seçilən qeydləri müəyyən bir sahəyə görə sıralamaq üçün istifadə olunur.
context.Books.OrderBy(b => b.PublishedYear).ToList().ForEach(Console.WriteLine); // Kitabları PublishedYear sahəsinə görə artan sırada sıralayır.
context.Books.OrderByDescending(b => b.PublishedYear).ToList().ForEach(Console.WriteLine); // Kitabları PublishedYear sahəsinə görə azalan sırada sıralayır.
context.Books.OrderBy(b => b.AuthorId).ThenBy(b => b.PublishedYear).ToList().ForEach(Console.WriteLine); // Kitabları əvvəlcə AuthorId sahəsinə görə, sonra isə PublishedYear sahəsinə görə artan sırada sıralayır.
context.Books.OrderByDescending(b => b.AuthorId).ThenByDescending(b => b.PublishedYear).ToList().ForEach(Console.WriteLine); // Kitabları əvvəlcə AuthorId sahəsinə görə, sonra isə PublishedYear sahəsinə görə azalan sırada sıralayır.

8. Join: Bu metod, verilənlər bazasındakı iki və ya daha çox cədvəli birləşdirmək üçün istifadə olunur.
context.Books.Join(context.Authors, b => b.AuthorId, a => a.Id, (b, a) => new { b.Title, AuthorName = a.FirstName + " " + a.LastName }).ToList().ForEach(b => Console.WriteLine($"{b.Title} - {b.AuthorName}"));

9. Take,TakeWhile, Skip, SkipWhile: Bu metodlar, verilənlər bazasından seçilən qeydlərin müəyyən bir hissəsini götürmək və ya atmaq üçün istifadə olunur.
context.Books.Take(5).ToList().ForEach(Console.WriteLine); // İlk 5 kitabı götürür.
context.Books.TakeWhile(b => b.PublishedYear < 2000).ToList().ForEach(Console.WriteLine); // PublishedYear sahəsi 2000-dən kiçik olan kitabları götürür.
context.Books.Skip(5).ToList().ForEach(Console.WriteLine); // İlk 5 kitabı atlayır və qalan kitabları götürür.
context.Books.SkipWhile(b => b.PublishedYear < 2000).ToList().ForEach(Console.WriteLine); // PublishedYear sahəsi 2000-dən kiçik olan kitabları atlayır və qalan kitabları götürür.

Bu və digər LINQ metodları ilə verilənlər bazası əməliyyatlarını daha asan və səmərəli bir şəkildə həyata keçirə bilərsiniz.





                                                            SQL Datatypes  - C# Datatypes

int - int (int32)
bigint - long (int64)
smallint - short (int16)
tinyint - byte (uint8)

bit - bool (boolean)

decimal, numeric, money, smallmoney - decimal
float - double (double)
real - float (single)

date - DateTime (date only)
datetime, datetime2, smalldatetime, time - DateTime (date and time)

char, varchar, text, nchar, nvarchar, ntext - string (String)

binary, varbinary, image - byte[] (byte array)
uniqueidentifier - Guid (Globally Unique Identifier)


                                                                  Table Relationships

Table Relationships - Verilənlər bazasında cədvəllər arasında əlaqələri təmsil edən bir konsepsiyadır.

1. One-to-One Relationship: Bu əlaqə növündə, bir cədvəldəki hər bir qeyd yalnız digər cədvəldəki bir qeyd ilə əlaqələndirilir. Məsələn, hər bir istifadəçi yalnız bir profil ilə əlaqələndirilir.
2. One-to-Many Relationship: Bu əlaqə növündə, bir cədvəldəki hər bir qeyd digər cədvəldəki bir neçə qeyd ilə əlaqələndirilir. Məsələn, bir müəllif bir neçə kitab ilə əlaqələndirilir.
3. Many-to-Many Relationship: Bu əlaqə növündə, bir cədvəldəki hər bir qeyd digər cədvəldəki bir neçə qeyd ilə əlaqələndirilir və əksinə. Məsələn, bir tələbə bir neçə kurs ilə əlaqələndirilir və bir kurs bir neçə tələbə ilə əlaqələndirilir. Bu əlaqə növü üçün əlavə bir cədvəl (junction table) istifadə olunur.


1. One-to-One Relationship - bu əlaqə növündə bizə sadəcə iki class bəs edir ki düzgün şəkildə bağlayaq.

class StudetnContext : DbContext
{
    public StudentContext()
    {
        Database.EnsureCreated(); // Bu metod, verilənlər bazasının yaradıldığını təmin edir. Əgər verilənlər bazası mövcud deyilsə, onu yaradır.
        Database.EnsureDeleted(); // Bu metod, verilənlər bazasının silindiyini təmin edir. Əgər verilənlər bazası mövcud deyilsə, heç bir şey etmir.
    }
    public DbSet<Student> Students { get; set; }
    public DbSet<StudentCard> StudentCard { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=StudentDb;Integrated Security=True;Trust Server Certificate=True");
    }
}

class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
}

class StudentCard
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int StudentId { get; set; } // Foreign Key
}

using StudentContext context = new();
Student student1 = new()
{
    Name = "John Doe",
    Age = 20
};

StudentCard studentCard1 = new()
{
    StartDate = DateTime.Now,
    EndDate = DateTime.Now.AddYears(1)
    StudentId = student1.Id // Foreign Key
};
context.StudentCard.Add(studentCard1
context.Students.Add(student1);
context.SaveChanges();

Oxumaq üçün:
var studentWithCard = context.Students
    .Include(s => s.StudentCard) // Include metodu, əlaqəli cədvəlləri yükləmək üçün istifadə olunur.
    .FirstOrDefault(s => s.Id == student1.Id);




2. One-to-Many Relationship - bu əlaqə növündə bizə iki class və bir kolleksiya bəs edir ki düzgün şəkildə bağlayaq. Yəni əgər bir müəllifin bir neçə kitabı varsa bu zaman biz müəllif classında bir List<Book> Books {get;set;} kolleksiyasını əlavə edirik. Buda bizə düzgün one-to-many relationship yaratmağa kömək edir.

class GroupContext : DbContext
{
    public GroupContext()
    {
        Database.EnsureCreated(); // Bu metod, verilənlər bazasının yaradıldığını təmin edir. Əgər verilənlər bazası mövcud deyilsə, onu yaradır.
        Database.EnsureDeleted(); // Bu metod, verilənlər bazasının silindiyini təmin edir. Əgər verilənlər bazası mövcud deyilsə, heç bir şey etmir.
    }
    public DbSet<Student> Students { get; set; }
    public DbSet<Group> Groups { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=GroupDb;Integrated Security=True;Trust Server Certificate=True");
    }
}

class Student{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public int GroupId { get; set; } // Foreign Key
}

class Group{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Student> Students { get; set; }
}

using (GroupContext context = new())
{   
    var Group1 = new Group
    {
        Name = "Group 1",
        Students = new List<Student>
        {
            new Student { Name = "John Doe", Age = 20 },
            new Student { Name = "Jane Doe", Age = 22 }
        }
    };
    
    context.Groups.Add(Group1);
    context.SaveChanges();

}










3. Many-to-Many Relationship - bu əlaqə növündə bizə üç class bəs edir ki düzgün şəkildə bağlayaq. Yəni əgər bir tələbənin bir neçə kursu varsa və bir kursun bir neçə tələbəsi varsa bu zaman biz tələbə və kurs class-larını birləşdirən əlavə bir class yaradırıq məsələn StudentCourse class-ı. Və bu class-da həm StudentId
həm də CourseId saxlanılır. Və hər iki class-da bir List şəklində kolleksiya saxlanılır ki bu da bizə düzgün many-to-many relationship yaratmağa kömək edir. Və biz yazılan codu run etdikdə many-to-many əlaqəsində lazım olan 3cü  cədvəl avtomatik şəkildə yaranır

class User
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string LastName { get; set; }

    public List<SocialNetwork> SocialNetworks { get; set; } = new(); // Many-to-Many Relationship

    public override string ToString()
    {
        return $"Id: {Id}, Name: {Name}, LastName: {LastName}";
    }
}
     
class SocialNetwork
{
    public int Id { get; set; }

    public string Name { get; set; }

    public List<User> Users { get; set; } = new(); // Many-to-Many Relationship

    public override string ToString()
    {
        return $"Id: {Id}, Name: {Name}";
    }
}

class SocialNetworkContext : DbContext
{
    public SocialNetworkContext()
    {
        Database.EnsureCreated(); // Bu metod, verilənlər bazasının yaradıldığını təmin edir. Əgər verilənlər bazası mövcud deyilsə, onu yaradır.
        Database.EnsureDeleted(); // Bu metod, verilənlər bazasının silindiyini təmin edir. Əgər verilənlər bazası mövcud deyilsə, heç bir şey etmir.
    }
    public DbSet<User> Users { get; set; }
    public DbSet<SocialNetwork> SocialNetworks { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=SocialNetworkDb;Integrated Security=True;Trust Server Certificate=True");
    }
}

using (SocialNetworkContext context = new())
{
    SocialNetwork facebook = new SocialNetwork { Name = "Facebook" };
    SocialNetwork twitter = new SocialNetwork { Name = "Twitter" };
    User user1 = new User { Name = "John", LastName = "Doe"};
    User user2 = new User { Name = "Jane", LastName = "Doe"};
    facebook.Users.Add(user1);
    facebook.Users.Add(user2);
    twitter.Users.Add(user1);
    context.SocialNetworks.AddRange(facebook, twitter);
    context.SaveChanges();

}




                                                                Annotations

Annotations - Entity Framework Core-da verilənlər bazası sxemini və davranışını təyin etmək üçün istifadə olunan atributlardır.

class Teacher
{
    public int Id { get; set; }
    public string firstName { get; set; }
    public string lastName { get; set; }
    public float salary { get; set; }
    public override string ToString()
    {
        return $"Id: {Id}, FirstName: {firstName}, LastName: {lastName}";
    }
}

class Student{

    public int Id { get; set; }
    public string firstName { get; set; }
    public string lastName { get; set; }
}

class Group
{
    public int Id { get; set; }
    public string GroupName { get; set; }
    public int GroupRating { get; set; }
    public int CourseYear { get; set; }
    public List<Student> Students { get; set; } = new();
    public override string ToString()
    {
        return $"Id: {Id}, GroupName: {GroupName}";
    }

}

class Department{
public int Id { get; set; }
public string DepartmentName { get; set; }
public List<Teacher> Teachers { get; set; } = new();
}

class Faculty{
    public int Id { get; set; }
    public string FacultyName { get; set; }
public List<Group> Groups { get; set; } = new();

}

class SchoolContext : DbContext
{
    public SchoolContext()
    {
        Database.EnsureCreated(); // Bu metod, verilənlər bazasının yaradıldığını təmin edir. Əgər verilənlər bazası mövcud deyilsə, onu yaradır.
        Database.EnsureDeleted(); // Bu metod, verilənlər bazasının silindiyini təmin edir. Əgər verilənlər bazası mövcud deyilsə, heç bir şey etmir.
    }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Faculty> Faculties { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=SchoolDb;Integrated Security=True;Trust Server Certificate=True");
    }
}

using SchoolContext database=new();

Student student = new()
{
    firstName="John",
    LastName="Walker"
};
database.Add(student);

Group group=new()
{
    GroupName="FSDM24_4",
    GroupRating= 1,
    CourseYear=2,
    Students = [student]
};

database.Groups.Add(group);

database.Faculties.Add(new(){FacultyName=Programming});

Department department = new(){DepartmentName="Development"};

Teacher teacher =new()
{
    FirstName="Isa",
    LastName="Memmedli",
    Salary = 15000
};

Teacher teacher1 =new()
{
    FirstName="Nadir",
    LastName="Zamanov",
    Salary = 15000
};

database.Teachers.AddRange(teacher,teacher1);


[] - ilə biz attribut əlavə edirik.

? - Bu işarə ilə biz hər hansı bir data null ola bilər deyə bilirik. Məsələn əgər bir tələbənin middle name-i yoxdursa biz bu zaman string? MiddleName {get;set;} yazırıq ki bu zaman bu data null ola bilər.

[Required] - Bu annotation ilə biz bir data-nın mütləq doldurulması lazım olduğunu bildiririk. Məsələn əgər bir tələbənin adı mütləq doldurulmalıdırsa biz bu zaman [Required] public string FirstName {get;set;} yazırıq.
Əlavə olaraq data əgər nullable olsa belə [Required] ilə biz bu data-nın mütləq doldurulması lazım olduğunu bildiririk. 

[MaxLength(50)] - Bu annotation ilə biz bir data-nın maksimum uzunluğunu təyin edirik. Məsələn əgər bir tələbənin adı maksimum 50 simvol ola bilərsə biz bu zaman [MaxLength(50)] public string FirstName {get;set;} yazırıq.

[ForeignKey("GroupId")] - Bu annotation ilə biz bir data-nın foreign key olduğunu bildiririk. Məsələn əgər bir tələbənin groupId-si foreign key-dirsə biz bu zaman [ForeignKey("GroupId")] public int GroupId {get;set;} yazırıq.

[Column("First_Name")] - Bu annotation ilə biz bir data-nın verilənlər bazasında hansı sütunda saxlanılacağını təyin edirik. Məsələn əgər bir tələbənin adı verilənlər bazasında First_Name sütununda saxlanılacaqsa biz 
cbu zaman [Column("First_Name")] public string FirstName {get;set;} yazırıq.

Navigation Properties - Bu xüsusiyyətlər, bir cədvəldəki qeydlərin digər cədvəllərdəki əlaqəli qeydlərə asanlıqla daxil olmasını təmin edir.
Məsələn əgər bir tələbənin bir qrupu varsa biz bu zaman Student class-ında public virtual Group Group {get;set;} yazırıq ki bu zaman biz bir tələbənin qrupuna asanlıqla daxil ola bilək.

Biz class yaranan zaman əgər bir Id sözü işlənən bir property yaradırıqsa bu avtomatik Primary Key olaraq təyin olunur. Amma biz Id hissəciyini istifadə etmək istəmiriksə bu zaman:
[Key] - Bu annotation ilə biz bir data-nın primary key olduğunu bildiririk. Məsələn əgər bir tələbənin Id-si primary key-dirsə biz bu zaman [Key] public int StudentId {get;set;} yazırıq.

[Column(TypeName="varchar(100)")] - Bu annotation ilə biz bir data-nın verilənlər bazasında hansı tipdə saxlanılacağını təyin edirik. Məsələn əgər bir tələbənin adı verilənlər bazasında varchar(100) tipində 
saxlanılacaqsa biz bu zaman [Column(TypeName="varchar(100)")] public string FirstName {get;set;} yazırıq.

[Range(1-5)] - Bu annotation ilə biz bir data-nın müəyyən bir aralıqda olmasını təmin edirik. Məsələn əgər bir qrupun reytinqi 1-dən 5-ə qədər olmalıdırsa biz bu zaman [Range(1,5)] public int GroupRating {get;set;} yazırıq.
Amma bu annotation yalnız integer və float tiplərində işləyir. 
Bu annotation istifadə oluna bilməsi üçün EFCore.CheckConstraints NuGet paketi əlavə olunmalıdır Və OnModelCreating metodunda 
optionsBuilder
        .UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=SchoolDb;Integrated Security=True;Trust Server Certificate=True"
        .UseValidationCheckConstraints();  // Bu metod, verilənlər bazasında yoxlamaların tətbiq olunmasını təmin edir.
Və artıq biz istifadə etdiyimiz hər bir şərtləri Check Constraints kimi verilənlər bazasında da tətbiq edə bilərik.





                                                                        Fluent API

Fluent API - Entity Framework Core-da verilənlər bazası sxemini və davranışını təyin etmək üçün istifadə olunan bir proqramlaşdırma yanaşmasıdır.
Yəni biz class-larımızda annotation-lar istifadə etmək əvəzinə OnModelCreating metodunda Fluent API istifadə edərək verilənlər bazası sxemini və davranışını təyin edirik.

 protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Group
        modelBuilder 
            .Entity<Group>() - Bu metod, Group entity-sini təmsil edir.
            .Property(x => x.GroupName) - Bu metod, Group entity-sinin GroupName property-sini təmsil edir.
            .IsRequired() - Bu metod, GroupName property-sinin mütləq doldurulması lazım olduğunu bildirir.
            .HasMaxLength(20); - Bu metod, GroupName property-sinin maksimum uzunluğunu 20 simvol olaraq təyin edir.

        modelBuilder
            .Entity<Group>() - Bu metod, Group entity-sini təmsil edir.
            .HasIndex(x => x.GroupName) - Bu metod, GroupName property-sinə indeks əlavə edir.
            .IsUnique(); - Bu metod, GroupName property-sinin unikal olmasını təmin edir.

        modelBuilder
            .Entity<Group>() - Bu metod, Group entity-sini təmsil edir.
            .Property(x => x.Id) - Bu metod, Group entity-sinin Id property-sini təmsil edir.
            .ValueGeneratedOnAdd(); - Bu metod, Id property-sinin avtomatik olaraq artırılmasını təmin edir.

        modelBuilder
            .Entity<Group>() - Bu metod, Group entity-sini təmsil edir.
            .ToTable(x => x.HasCheckConstraint("CK_CourseYear", 
            "CourseYear >= 1 AND CourseYear <= 4")); - Bu metod, CourseYear property-sinin 1-dən 4-ə qədər olmasını təmin edir.

        modelBuilder
            .Entity<Group>() - Bu metod, Group entity-sini təmsil edir.
            .ToTable(x => x.HasCheckConstraint("CK_GroupRating",
            "GroupRating >= 0 AND GroupRating <= 12")); - Bu metod, GroupRating property-sinin 0-dan 12-ə qədər olmasını təmin edir.

        // Teacher
        modelBuilder
            .Entity<Teacher>() - Bu metod, Teacher entity-sini təmsil edir.
            .Property(x => x.TeacherId) - Bu metod, Teacher entity-sinin TeacherId property-sini təmsil edir.
            .HasColumnName("Id") - Bu metod, TeacherId property-sinin verilənlər bazasında Id sütununda saxlanılacağını təyin edir.
            .ValueGeneratedOnAdd(); - Bu metod, TeacherId property-sinin avtomatik olaraq artırılmasını təmin edir.

        modelBuilder
            .Entity<Teacher>() - Bu metod, Teacher entity-sini təmsil edir.
            .Property(x => x.FirstName) - Bu metod, Teacher entity-sinin FirstName property-sini təmsil edir.
            .IsRequired() - Bu metod, FirstName property-sinin mütləq doldurulması lazım olduğunu bildirir.
            .HasMaxLength(20); - Bu metod, FirstName property-sinin maksimum uzunluğunu 20 simvol olaraq təyin edir.

        modelBuilder
            .Entity<Teacher>() - Bu metod, Teacher entity-sini təmsil edir.
            .Property(x => x.LastName) - Bu metod, Teacher entity-sinin LastName property-sini təmsil edir.
            .IsRequired() - Bu metod, LastName property-sinin mütləq doldurulması lazım olduğunu bildirir.
            .HasMaxLength(30); - Bu metod, LastName property-sinin maksimum uzunluğunu 30 simvol olaraq təyin edir.

        modelBuilder
           .Entity<Teacher>() - Bu metod, Teacher entity-sini təmsil edir.
           .Property(x => x.Email) - Bu metod, Teacher entity-sinin Email property-sini təmsil edir.
           .IsRequired() - Bu metod, Email property-sinin mütləq doldurulması lazım olduğunu bildirir.
           .HasColumnType("varchar") - Bu metod, Email property-sinin verilənlər bazasında varchar tipində saxlanılacağını təyin edir.
           .HasMaxLength(50); - Bu metod, Email property-sinin maksimum uzunluğunu 50 simvol olaraq təyin edir.

        modelBuilder
            .Entity<Teacher>() - Bu metod, Teacher entity-sini təmsil edir.
            .HasIndex(x => x.Email) - Bu metod, Email property-sinə indeks əlavə edir.
            .IsUnique() - Bu metod, Email property-sinin unikal olmasını təmin edir.
            .HasDatabaseName("UQ_Email"); - Bu metod, Email property-sinin unikal indeksinin adını UQ_Email olaraq təyin edir.

        // Student
        modelBuilder
            .Entity<Student>() - Bu metod, Student entity-sini təmsil edir.
            .HasOne(s => s.Group) - Bu metod, Student entity-sinin Group navigation property-sini təmsil edir.
            .WithMany(g => g.Students) - Bu metod, Group entity-sinin Students navigation property-sini təmsil edir.
            .HasForeignKey(s => s.GroupId) - Bu metod, Student entity-sinin GroupId property-sinin foreign key olduğunu bildirir.
            .OnDelete(DeleteBehavior.NoAction) - Bu metod, bir qrup silindikdə ona aid tələbələrin silinməməsini təmin edir.
            .HasConstraintName("FK_Groups"); - Bu metod, foreign key constraint-in adını FK_Groups olaraq təyin edir.

        modelBuilder
           .Entity<Student>() - Bu metod, Student entity-sini təmsil edir.
           .Property(x => x.FirstName) - Bu metod, Student entity-sinin FirstName property-sini təmsil edir.
           .IsRequired() - Bu metod, FirstName property-sinin mütləq doldurulması lazım olduğunu bildirir.
           .HasMaxLength(20); - Bu metod, FirstName property-sinin maksimum uzunluğunu 20 simvol olaraq təyin edir.

        modelBuilder
           .Entity<Student>() - Bu metod, Student entity-sini təmsil edir.
           .Property(x => x.LastName) - Bu metod, Student entity-sinin LastName property-sini təmsil edir.
           .IsRequired() - Bu metod, LastName property-sinin mütləq doldurulması lazım olduğunu bildirir.
           .HasMaxLength(30); - Bu metod, LastName property-sinin maksimum uzunluğunu 30 simvol olaraq təyin edir.

        modelBuilder
           .Entity<Student>() - Bu metod, Student entity-sini təmsil edir.
           .Property(x => x.GroupId) - Bu metod, Student entity-sinin GroupId property-sini təmsil edir.
           .IsRequired() - Bu metod, GroupId property-sinin mütləq doldurulması lazım olduğunu bildirir.
           .HasColumnName("Id_Group"); - Bu metod, GroupId property-sinin verilənlər bazasında Id_Group sütununda saxlanılacağını təyin edir.

    }






































































 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 */