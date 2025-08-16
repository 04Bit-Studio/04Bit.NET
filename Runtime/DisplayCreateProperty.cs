using Silk.NET.Maths;

namespace Runtime
{
    /// <summary>
    /// 디스플레이 생성에 필요한 속성을 정의합니다.
    /// </summary>
    public readonly struct DisplayCreateProperty
    {
        /// <summary>
        /// 디스플레이 제목.
        /// </summary>
        public required string Title
        {
            get;
            init;
        }

        /// <summary>
        /// 디스플레이 생성 위치.
        /// </summary>
        public required Vector2D<int> Position
        {
            get;
            init;
        }

        /// <summary>
        /// 디스플레이 크기.
        /// </summary>
        public required Vector2D<int> Size
        {
            get;
            init;
        }
    }
}
