namespace QuickBite.Api.Entities;

public class Restaurant
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public bool IsOpen { get; set; }
}
