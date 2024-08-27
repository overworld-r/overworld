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
    }
}