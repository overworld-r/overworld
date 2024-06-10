namespace Overworld.Item
{
    public class DebugItem : ItemBase
    {
        public override string itemName => "DebugItem";
        public override string description => "This is a debug item.";
        public override int price => 100;

        protected override void Update() { }

        protected override void FixedUpdate() { }
    }
}
