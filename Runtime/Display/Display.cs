using Silk.NET.Core.Contexts;
using Silk.NET.Windowing;

namespace Runtime.Display
{
    public class Display
    {
        /// <summary>
        /// 윈도우.
        /// </summary>
        public IWindow Handle;

        /// <summary>
        /// 네이티브 윈도우.
        /// </summary>
        public INativeWindow NativeHandle => Handle.Native;

        /// <summary>
        /// 생성자.
        /// </summary>
        /// <param name="property">윈도우 생성 프로퍼티.</param>
        public Display(DisplayCreateProperty property)
        {
            WindowOptions options = WindowOptions.Default;
            options.Title         = property.Title;
            options.Position      = property.Position;
            options.Size          = property.Size;
            options.VSync         = property.ShouldVSync;
            options.WindowBorder  = WindowBorder.Fixed;
            options.API           = GraphicsAPI.None;

            Handle = Window.Create(options);
        }

        public void Run()
        {
            Handle.Run();
        }

        public void Close()
        {
            Handle.Close();
        }
    }
}
