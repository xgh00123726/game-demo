namespace GameBase.XCard
{
    public class XCDescribe
    {
        string orgin;
        string output;

        public XCDescribe(string origin)
        {
            orgin = origin;
        }

        public override string ToString()
        {
            output ??= XCDescribeParser.ToTextMeshContent(orgin);
            return output;
        }
    }
}
