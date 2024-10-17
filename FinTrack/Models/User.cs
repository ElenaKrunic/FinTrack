
using FinTrack.Models;
using System.Text.Json.Serialization;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }

    [JsonIgnore]
    public ICollection<Transaction> Transactions { get; set; }

}
