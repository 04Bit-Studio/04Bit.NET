using Silk.NET.Maths;
using Silk.NET.SDL;

namespace Runtime
{
    public class SDLDisplay : IDisposable
    , IDisplayProperty
    {
        /// <summary>
        /// SDL API.
        /// </summary>
        private readonly Sdl mBackend;

        /// <summary>
        /// SDL 윈도우 인스턴스.
        /// </summary>
        private unsafe readonly Window* mWindow;

        public unsafe string Title
        {
            get => mBackend.GetWindowTitleS(mWindow);
            set => mBackend.SetWindowTitle(mWindow, value);
        }

        /// <summary>
        /// 윈도우 위치 캐시.
        /// </summary>
        private Vector2D<int> mPositionCache;

        public Vector2D<int> Position
        {
            get => mPositionCache;
            set
            {
                unsafe
                {
                    mPositionCache = value;
                    mBackend.SetWindowPosition(mWindow, value.X, value.Y);
                }
            }
        }

        /// <summary>
        /// 윈도우 크기 캐시.
        /// </summary>
        private Vector2D<int> mSizeCache;

        public Vector2D<int> Size
        {
            get => mSizeCache;
            set
            {
                unsafe
                {
                    mSizeCache = value;
                    mBackend.SetWindowSize(mWindow, value.X, value.Y);
                }
            }
        }

        public unsafe bool IsResizable
        {
            get
            {
                uint flags = mBackend.GetWindowFlags(mWindow);
                return (flags & (uint) WindowFlags.Resizable) != 0;
            }
            set
            {
                SdlBool resize = value ? SdlBool.True : SdlBool.False;
                mBackend.SetWindowResizable(mWindow, resize);
            }
        }

        public unsafe bool IsBorderless
        {
            get
            {
                uint flags = mBackend.GetWindowFlags(mWindow);
                return (flags & (uint) WindowFlags.Borderless) != 0;
            }
            set
            {
                SdlBool borderless = value ? SdlBool.True : SdlBool.False;
                mBackend.SetWindowBordered(mWindow, borderless);
            }
        }

        public unsafe bool IsFullscreen
        {
            get
            {
                uint flags = mBackend.GetWindowFlags(mWindow);
                return (flags & (uint) WindowFlags.Fullscreen) != 0;
            }
            set
            {
                uint flag = (uint) (value ? WindowFlags.Fullscreen : WindowFlags.FullscreenDesktop);
                mBackend.SetWindowFullscreen(mWindow, flag);
            }
        }

        public bool IsVSync
        {
            get => mBackend.GLGetSwapInterval() != 0;
            set => mBackend.GLSetSwapInterval(value ? 1 : 0);
        }

        public SDLDisplay(DisplayCreateProperty property)
        {
            mBackend = Sdl.GetApi();

            if (mBackend.Init(Sdl.InitVideo | Sdl.InitEvents) != 0)
            {
                throw new InvalidOperationException($"Failed to initialize SDL Backend : {mBackend.GetErrorS()}");
            }

            unsafe
            {
                mWindow = mBackend.CreateWindow(property.Title,
                property.Position.X,
                property.Position.Y,
                property.Size.X,
                property.Size.Y,
                0);

                if (mWindow == null)
                {
                    throw new InvalidOperationException($"Failed to initialize SDL Window : {mBackend.GetErrorS()}");
                }

                Title = property.Title;
                Position = property.Position;
                Size = property.Size;
                IsResizable = property.ShouldResizable;
                IsBorderless = property.ShouldBorderless;
                IsVSync = property.ShouldVSync;
            }
        }

        public void Dispose()
        {
            unsafe
            {
                if (mWindow != null)
                {
                    mBackend.DestroyWindow(mWindow);
                }

                mBackend.Quit();
            }
        }
    }
}
