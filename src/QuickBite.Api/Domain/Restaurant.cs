namespace QuickBite.Api.Domain;

public class Restaurant
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public bool IsOpen { get; set; }
}
