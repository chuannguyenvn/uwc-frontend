public struct Padding
{
    public int Top { get; set; }
    public int Bottom { get; set; }
    public int Left { get; set; }
    public int Right { get; set; }

    public Padding(int left, int right, int top, int bottom)
    {
        Top = top;
        Bottom = bottom;
        Left = left;
        Right = right;
    }
}