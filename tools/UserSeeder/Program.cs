// ============================================================================
// UserSeeder — insert a test user into AspNetUsers with a REAL Identity hash.
//
// Usage (from D:\SurveyBasket):
//   dotnet run --project tools/UserSeeder -- <email> <password> <firstName> <lastName>
//
// Example:
//   dotnet run --project tools/UserSeeder -- mohammed@survey-basket.com P@ssword123 Mohammed Abdeltawab
//
// Why this tool exists:
//   Identity stores PasswordHash (PBKDF2, salted) — you cannot hand-write it
//   in SQL. PasswordHasher<T> here is the SAME hasher UserManager uses,
//   so CheckPasswordAsync will accept the password at login.
// ============================================================================

using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;

if (args.Length < 4)
{
    Console.WriteLine("Usage: dotnet run --project tools/UserSeeder -- <email> <password> <firstName> <lastName>");
    return 1;
}

var (email, password, firstName, lastName) = (args[0], args[1], args[2], args[3]);

// Same connection string as SurveyBasket.Api/appsettings.json
const string connectionString =
    @"Server=(localdb)\MSSQLLocalDB;Database=SurveyBasket;Trusted_Connection=True;Encrypt=False";

// 1) Hash the password exactly like Identity does (PBKDF2 + random salt).
//    The user object passed to HashPassword is not used by the default hasher,
//    so a dummy is fine.
var hasher = new PasswordHasher<object>();
var passwordHash = hasher.HashPassword(new object(), password);

// 2) Build the required Identity column values.
var id = Guid.NewGuid().ToString();                 // PK — Identity uses string GUIDs
var securityStamp = Guid.NewGuid().ToString("N").ToUpperInvariant(); // changes when credentials change
var concurrencyStamp = Guid.NewGuid().ToString();   // optimistic-concurrency token

await using var connection = new SqlConnection(connectionString);
await connection.OpenAsync();

// Refuse duplicates (unique index is on NormalizedEmail/NormalizedUserName)
await using (var check = new SqlCommand(
    "SELECT COUNT(*) FROM AspNetUsers WHERE NormalizedEmail = @NormalizedEmail", connection))
{
    check.Parameters.AddWithValue("@NormalizedEmail", email.ToUpperInvariant());
    if ((int)(await check.ExecuteScalarAsync())! > 0)
    {
        Console.WriteLine($"User {email} already exists — nothing inserted.");
        return 0;
    }
}

// 3) Insert. Note: UserName = Email (course convention), Normalized* = UPPERCASE
//    (Identity looks users up by the normalized columns).
const string sql = """
    INSERT INTO AspNetUsers
        (Id, FirstName, LastName,
         UserName, NormalizedUserName, Email, NormalizedEmail, EmailConfirmed,
         PasswordHash, SecurityStamp, ConcurrencyStamp,
         PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnabled, AccessFailedCount)
    VALUES
        (@Id, @FirstName, @LastName,
         @Email, @NormalizedEmail, @Email, @NormalizedEmail, 1,
         @PasswordHash, @SecurityStamp, @ConcurrencyStamp,
         0, 0, 1, 0)
    """;

await using (var insert = new SqlCommand(sql, connection))
{
    insert.Parameters.AddWithValue("@Id", id);
    insert.Parameters.AddWithValue("@FirstName", firstName);
    insert.Parameters.AddWithValue("@LastName", lastName);
    insert.Parameters.AddWithValue("@Email", email);
    insert.Parameters.AddWithValue("@NormalizedEmail", email.ToUpperInvariant());
    insert.Parameters.AddWithValue("@PasswordHash", passwordHash);
    insert.Parameters.AddWithValue("@SecurityStamp", securityStamp);
    insert.Parameters.AddWithValue("@ConcurrencyStamp", concurrencyStamp);

    await insert.ExecuteNonQueryAsync();
}

Console.WriteLine($"✔ Created user {email}  (Id: {id})");
Console.WriteLine($"  Login with: {email} / {password}");
return 0;
