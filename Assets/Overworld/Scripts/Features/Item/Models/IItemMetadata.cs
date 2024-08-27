namespace Overworld.Features.Item.Models
{
    public interface IItemMetadata
    {
        ItemMetadata metadata { get; internal set; }
    }

    public class ItemMetadata
    {
        public string name { get; private set; } = "";
        public string description { get; private set; } = "";
        public float price { get; private set; } = 0.0f;

        public ItemMetadata(string name, string description, float price)
        {
            this.name = name;
            this.description = description;
            this.price = price;
        }
    }
}
