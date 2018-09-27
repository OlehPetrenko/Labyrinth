namespace Assets.Classes.PathFinder
{
    /// <summary>
    /// The node of the path.
    /// </summary>
    public sealed class PathNode
    {
        public Point Position { get; set; }

        public PathNode CameFrom { get; set; }

        public int PathLengthFromStart { get; set; }
        public int HeuristicEstimatePathLength { get; set; }

        public int EstimateFullPathLength
        {
            get { return PathLengthFromStart + HeuristicEstimatePathLength; }
        }
    }

}
