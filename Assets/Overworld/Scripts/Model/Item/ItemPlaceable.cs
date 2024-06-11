namespace Overworld.Model
{
    public abstract class ItemPlaceable : ItemGeneration
    {
        public abstract string itemName { get; }
        public abstract string description { get; }
        public abstract int price { get; }
    }
}
