namespace TWAB.Models.Models;
[BsonCollection("Users")]
public class User : Document
{
    public string Name { get; set; }
}
