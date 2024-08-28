namespace Overworld.Features.Item.Models
{
    public interface IResourceMetadata
    {
        ResourceMetadata metadata { get; internal set; }
    }

    public class ResourceMetadata
    {
        public int stone { get; private set; } = default!;
        public int wood { get; private set; } = default!;
        public int iron { get; private set; } = default!;

        public ResourceMetadata(int stone = 0, int wood = 0, int iron = 0)
        {
            this.stone = stone;
            this.wood = wood;
            this.iron = iron;
        }
    }
}
