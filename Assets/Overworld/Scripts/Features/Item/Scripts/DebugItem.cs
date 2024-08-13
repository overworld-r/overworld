namespace Overworld.Item
{
    public class DebugItem : ItemBase
    {
        public bool CanBuild { get; set; } = true;

        public override string itemName => "DebugItem";
        public override string description => "This is a debug item.";
        public override int price => 100;

        protected void Update() { }

        protected void FixedUpdate() { }
    }
}