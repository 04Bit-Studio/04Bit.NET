using Silk.NET.GLFW;
using Silk.NET.Maths;

namespace Runtime
{
    /// <summary>
    /// GLFW 기반의 디스플레이를 제공합니다.
    /// </summary>
    public sealed class GLFWDisplay : IDisposable
                                    , IDisplayProperty
    {
        /// <summary>
        /// GLFW 백엔드.
        /// </summary>
        private readonly Glfw mBackend;

        /// <summary>
        /// GLFW 윈도우 핸들.
        /// </summary>
        private readonly unsafe WindowHandle* mWindow;

        /// <summary>
        /// 윈도우 타이틀.
        /// (네이티브에서는 타이틀 가져오기가 되는데 Slik.NET에서는 안됨;;)
        /// </summary>
        private string mTitle;

        public string Title
        {
            get => mTitle;
            set
            {
                unsafe
                {
                    mTitle = value;
                    mBackend.SetWindowTitle(mWindow, mTitle);
                }
            }
        }

        public unsafe Vector2D<int> Position
        {
            get
            {
                mBackend.GetWindowPos(mWindow, out int x, out int y);
                return new Vector2D<int>(x, y);
            }
            set => mBackend.SetWindowPos(mWindow, value.X, value.Y);
        }

        public unsafe Vector2D<int> Size
        {
            get
            {
                    mBackend.GetWindowSize(mWindow, out int width, out int height);
                    return new Vector2D<int>(width, height);
            }
            set => mBackend.SetWindowSize(mWindow, value.X, value.Y);
        }

        public unsafe bool IsResizable
        {
            get => mBackend.GetWindowAttrib(mWindow, WindowAttributeGetter.Resizable);
            set => mBackend.SetWindowAttrib(mWindow,WindowAttributeSetter.Resizable, value);
        }

        public unsafe bool IsBorderless
        {
            get => mBackend.GetWindowAttrib(mWindow, WindowAttributeGetter.Decorated);
            set => mBackend.SetWindowAttrib(mWindow, WindowAttributeSetter.Decorated, value);
        }

        /// <summary>
        /// V-Sync 활성화 여부.
        /// (GLFW 내 해당 값을 가져오는 API가 없음.)
        /// </summary>
        private bool mSholudVSync;

        public bool IsVSync
        {
            get => mSholudVSync;
            set
            {
                mSholudVSync = value;
                mBackend.SwapInterval(value ? 1 : 0);
            }
        }

        public GLFWDisplay(DisplayCreateProperty property)
        {
            mBackend = Glfw.GetApi();

            if (!mBackend.Init())
            {
                throw new InvalidOperationException("Failed to initialize GLFW.");
            }

            mBackend.WindowHint(WindowHintInt.ContextVersionMinor, 3);
            mBackend.WindowHint(WindowHintInt.ContextVersionMajor, 3);
            mBackend.WindowHint(WindowHintOpenGlProfile.OpenGlProfile, OpenGlProfile.Core);
            mBackend.WindowHint(WindowHintBool.Resizable, true);

            unsafe
            {
                int    width  = property.Size.X;
                int    height = property.Size.Y;
                string title  = (mTitle = property.Title) ?? throw new NullReferenceException();

                mWindow = mBackend.CreateWindow(width,
                                                height,
                                                title,
                                                null,
                                                null);

                if (mWindow == null)
                {
                    mBackend.Terminate();
                    throw new InvalidOperationException("Failed to create GLFW window.");
                }

                mBackend.MakeContextCurrent(mWindow);
                mBackend.SwapInterval(1);
            }
        }

        public void Dispose()
        {
            unsafe
            {
                mBackend.DestroyWindow(mWindow);
                mBackend.Terminate();
            }
        }
    }
}
