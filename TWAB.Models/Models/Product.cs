namespace TWAB.Models.Models;
[BsonCollection("Product")]
public class Product : Document
{
    public string Name { get; set; }
    public string Category { get; set; }
}
