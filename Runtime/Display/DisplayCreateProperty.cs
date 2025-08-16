using Silk.NET.Maths;

namespace Runtime.Display
{
    public record DisplayCreateProperty
    {
        public string Title;

        public Vector2D<int> Position;

        public Vector2D<int> Size;

        public bool ShouldVSync;

        public bool ShouldBorderless;
    }
}
