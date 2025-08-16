using Moq;
using Xunit;
using Silk.NET.Maths;
using Silk.NET.GLFW;

namespace Runtime.Tests
{
    public class GLFWDisplayAllPropertyTests
    {
        private DisplayCreateProperty MakeDefaultProperty()
        {
            return new DisplayCreateProperty
            {
                Title = "GLFW Test",
                Position = new Vector2D<int>(100, 100),
                Size = new Vector2D<int>(800, 600),
                ShouldResizable = true,
                ShouldBorderless = false,
                ShouldVSync = true
            };
        }

        [Fact]
        public void Test_객체_생성시_예외_발생하는지()
        {
            var mock = new Mock<Glfw>();
            mock.Setup(x => x.Init()).Returns(true);

            var property = MakeDefaultProperty();
            var display = new GLFWDisplay(property, mock.Object); // ← Glfw 주입용 생성자 필요

            Assert.NotNull(display);
        }

        [Fact]
        public void 모든_프로퍼티가_정상적으로_작동함()
        {
            var property = MakeDefaultProperty();
            using var display = new GLFWDisplay(property);

            // Title
            display.Title = "Changed Title";
            Assert.Equal("Changed Title", display.Title);

            // Position
            var newPos = new Vector2D<int>(200, 150);
            display.Position = newPos;
            Assert.Equal(newPos, display.Position);

            // Size
            var newSize = new Vector2D<int>(1024, 768);
            display.Size = newSize;
            Assert.Equal(newSize, display.Size);

            // IsResizable
            display.IsResizable = false;
            Assert.False(display.IsResizable);
            display.IsResizable = true;
            Assert.True(display.IsResizable);

            // IsBorderless
            display.IsBorderless = true;
            Assert.True(display.IsBorderless);
            display.IsBorderless = false;
            Assert.False(display.IsBorderless);

            // IsVSync
            display.IsVSync = false;
            Assert.False(display.IsVSync);
            display.IsVSync = true;
            Assert.True(display.IsVSync);
        }
    }
}
