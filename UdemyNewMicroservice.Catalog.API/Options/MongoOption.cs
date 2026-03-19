using System.ComponentModel.DataAnnotations;

namespace UdemyNewMicroservice.Catalog.API.Options;

public class MongoOption
{
    [Required] public string Database { get; set; } = default!;
    [Required] public string ConnectionString { get; set; } = default!;


}
