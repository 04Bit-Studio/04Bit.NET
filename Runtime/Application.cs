using Silk.NET.OpenGL;
using Silk.NET.GLFW;
using Silk.NET.Windowing;
using Silk.NET.Windowing.Sdl;

namespace Runtime
{
    public class Application
    {
        public GLFWDisplay display;

        public Application()
        {
            using (display = new GLFWDisplay(default))
            {

            }
        }
    }
}
