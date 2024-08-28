using System;

namespace Overworld.Models
{
    public class LocationStatus
    {
        public enum Location
        {
            World,
            Bag,
        }

        public Location value;

        public LocationStatus(Location value)
        {
            this.value = value;
        }

        public void Match(Action world = null!, Action bag = null!)
        {
            if (world != null && value == Location.World)
                world();

            if (bag != null && value == Location.Bag)
                bag();
        }

        public T Match<T>(Func<T> world, Func<T> bag)
        {
            switch (value)
            {
                case Location.World:
                    return world();
                case Location.Bag:
                    return bag();
                default:
                    return world();
            }
        }
    }
}
