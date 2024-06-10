namespace Overworld.Item
{
    public abstract class ItemBase : ItemGeneration
    {
        public abstract string itemName { get; }
        public abstract string description { get; }
        public abstract int price { get; }
    }
}
