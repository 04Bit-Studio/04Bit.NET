using Silk.NET.Maths;
using Silk.NET.Windowing;
using Runtime.Display;
using Xunit;

public class DisplayTests
{
    [Fact]
    public void Display_Should_Initialize()
    {
        var property = new DisplayCreateProperty
        {
            Title = "Test Window",
            Position = new Vector2D<int>(0, 0),
            Size = new Vector2D<int>(100, 100),
            ShouldVSync = true,
            ShouldBorderless = false
        };

        var display = new Display(property);

        Assert.Equal("Test Window", display.Handle.Title);
        Assert.True(display.Handle.VSync);
        Assert.Equal(WindowBorder.Resizable, display.Handle.WindowBorder);
    }

    [Fact]
    public void Display_Should_Close()
    {
        bool loadCalled = false;
        bool closeCalled = false;

        var property = new DisplayCreateProperty
        {
            Title = "Test Window",
            Position = new Vector2D<int>(0, 0),
            Size = new Vector2D<int>(100, 100),
            ShouldVSync = true,
            ShouldBorderless = false
        };

        var display = new Display(property);

        display.Handle.Load += () =>
        {
            loadCalled = true;
        };
        display.Handle.Closing += () =>
        {
            closeCalled = true;
        };

        display.Handle.Run();
        display.Handle.Close();

        Assert.True(loadCalled);
        Assert.True(closeCalled);
    }
}
