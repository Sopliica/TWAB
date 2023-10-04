using MongoDB.Bson;

namespace TWAB.Models.Models;

public abstract class Document : IDocument
{
    public ObjectId Id { get; set; }
    public DateTime CreatedAt => Id.CreationTime;
}
