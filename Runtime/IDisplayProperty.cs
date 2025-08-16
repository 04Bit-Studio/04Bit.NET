using Silk.NET.Maths;

namespace Runtime
{
    /// <summary>
    /// 디스플레이 내 포함되어야 할 속성을 선언합니다.
    /// </summary>
    public interface IDisplayProperty
    {
        /// <summary>
        /// 디스플레이 제목.
        /// </summary>
        public string Title
        {
            get;
            set;
        }

        /// <summary>
        /// 디스플레이 위치.
        /// </summary>
        public Vector2D<int> Position
        {
            get;
            set;
        }

        /// <summary>
        /// 디스플레이 크기.
        /// </summary>
        public Vector2D<int> Size
        {
            get;
            set;
        }

        /// <summary>
        /// 디스플레이 크기 조절 여부.
        /// </summary>
        public bool IsResizable
        {
            get;
            set;
        }

        /// <summary>
        /// 디스플레이 테두리 여부.
        /// </summary>
        public bool IsBorderless
        {
            get;
            set;
        }

        /// <summary>
        /// 디스플레이 V-Sync 활성화 여부.
        /// </summary>
        public bool IsVSync
        {
            get;
            set;
        }
    }
}
