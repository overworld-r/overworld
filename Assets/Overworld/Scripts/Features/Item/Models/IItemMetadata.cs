namespace Overworld.Features.Item.Models
{
    public interface IItemMetadata
    {
        string itemName { get; internal set; }
        string description { get; internal set; }
        int price { get; internal set; }
    }
}
