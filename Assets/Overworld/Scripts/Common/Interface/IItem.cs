using UnityEngine;

public class IItem
{
    private interface ItemCommon
    {
        string name { get; }
        string description { get; }
        int price { get; }

        void Built();
        void Broken();
        void Enable();
        void Disable();
    }

    private interface ItemClickable { }

    private interface ItemBackpack
    {
        Vector2 backpackGridSize { get; }
        Vector2 backpackGridPosition { get; }
    }

    public abstract class IItemBlock : ItemCommon, ItemBackpack
    {
        public abstract string name { get; }
        public abstract string description { get; }
        public abstract int price { get; }

        public Vector2 backpackGridSize { get; }
        public Vector2 backpackGridPosition { get; }

        public void Built()
        {
            Enable();
        }

        public virtual void Broken()
        {
            Disable();
        }

        public virtual void Enable()
        {
            //Process when enabled
        }

        public virtual void Disable()
        {
            //Process when disabled
        }
    }
}