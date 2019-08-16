using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;


public struct Vertex
{
    public const int Size = (4 + 4) * 4; // size of struct in bytes

    private readonly Vector4 position;

    private readonly OpenTK.Graphics.Color4 color;

    public Vertex(Vector4 position, Color4 color)
    {
        this.position = position;
        this.color = color;
    }
}

public struct TexturedVertex
{
    public const int Size = (4 + 2) * 4; // size of struct in bytes

    private readonly Vector4 _position;
    private readonly Vector2 _textureCoordinate;

    public TexturedVertex(Vector4 position, Vector2 textureCoordinate)
    {
        _position = position;
        _textureCoordinate = textureCoordinate;
    }
}


