using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace OpenGLEngine
{
    public class ShapeFactory
    {
        public static Vertex[] CreateSolidCude(float side, OpenTK.Graphics.Color4 color)
        {
            side = side / 2f; // half side - and other half
            Vertex[] vertices =
  {
   new Vertex(new Vector4(-side, -side, -side, 1.0f),   color),
   new Vertex(new Vector4(-side, -side, side, 1.0f),    color),
   new Vertex(new Vector4(-side, side, -side, 1.0f),    color),
   new Vertex(new Vector4(-side, side, -side, 1.0f),    color),
   new Vertex(new Vector4(-side, -side, side, 1.0f),    color),
   new Vertex(new Vector4(-side, side, side, 1.0f),     color),

   new Vertex(new Vector4(side, -side, -side, 1.0f),    color),
   new Vertex(new Vector4(side, side, -side, 1.0f),     color),
   new Vertex(new Vector4(side, -side, side, 1.0f),     color),
   new Vertex(new Vector4(side, -side, side, 1.0f),     color),
   new Vertex(new Vector4(side, side, -side, 1.0f),     color),
   new Vertex(new Vector4(side, side, side, 1.0f),      color),

   new Vertex(new Vector4(-side, -side, -side, 1.0f),   color),
   new Vertex(new Vector4(side, -side, -side, 1.0f),    color),
   new Vertex(new Vector4(-side, -side, side, 1.0f),    color),
   new Vertex(new Vector4(-side, -side, side, 1.0f),    color),
   new Vertex(new Vector4(side, -side, -side, 1.0f),    color),
   new Vertex(new Vector4(side, -side, side, 1.0f),     color),

   new Vertex(new Vector4(-side, side, -side, 1.0f),    color),
   new Vertex(new Vector4(-side, side, side, 1.0f),     color),
   new Vertex(new Vector4(side, side, -side, 1.0f),     color),
   new Vertex(new Vector4(side, side, -side, 1.0f),     color),
   new Vertex(new Vector4(-side, side, side, 1.0f),     color),
   new Vertex(new Vector4(side, side, side, 1.0f),      color),

   new Vertex(new Vector4(-side, -side, -side, 1.0f),   color),
   new Vertex(new Vector4(-side, side, -side, 1.0f),    color),
   new Vertex(new Vector4(side, -side, -side, 1.0f),    color),
   new Vertex(new Vector4(side, -side, -side, 1.0f),    color),
   new Vertex(new Vector4(-side, side, -side, 1.0f),    color),
   new Vertex(new Vector4(side, side, -side, 1.0f),     color),

   new Vertex(new Vector4(-side, -side, side, 1.0f),    color),
   new Vertex(new Vector4(side, -side, side, 1.0f),     color),
   new Vertex(new Vector4(-side, side, side, 1.0f),     color),
   new Vertex(new Vector4(-side, side, side, 1.0f),     color),
   new Vertex(new Vector4(side, -side, side, 1.0f),     color),
   new Vertex(new Vector4(side, side, side, 1.0f),      color),
  };

            return vertices;
        }

        public static TexturedVertex[] CreateTexturedCube(float side, float textureWidth, float textureHeight)
        {
            float h = textureHeight;
            float w = textureWidth;
            side = side / 2f; // half side - and other half

            TexturedVertex[] vertices =
            {
        new TexturedVertex(new Vector4(-side, -side, -side, 1.0f),   new Vector2(0, 0)),
        new TexturedVertex(new Vector4(-side, -side, side, 1.0f),    new Vector2(0, h)),
        new TexturedVertex(new Vector4(-side, side, -side, 1.0f),    new Vector2(w, 0)),
        new TexturedVertex(new Vector4(-side, side, -side, 1.0f),    new Vector2(w, 0)),
        new TexturedVertex(new Vector4(-side, -side, side, 1.0f),    new Vector2(0, h)),
        new TexturedVertex(new Vector4(-side, side, side, 1.0f),     new Vector2(w, h)),

        new TexturedVertex(new Vector4(side, -side, -side, 1.0f),    new Vector2(0, 0)),
        new TexturedVertex(new Vector4(side, side, -side, 1.0f),     new Vector2(w, 0)),
        new TexturedVertex(new Vector4(side, -side, side, 1.0f),     new Vector2(0, h)),
        new TexturedVertex(new Vector4(side, -side, side, 1.0f),     new Vector2(0, h)),
        new TexturedVertex(new Vector4(side, side, -side, 1.0f),     new Vector2(w, 0)),
        new TexturedVertex(new Vector4(side, side, side, 1.0f),      new Vector2(w, h)),

        new TexturedVertex(new Vector4(-side, -side, -side, 1.0f),   new Vector2(0, 0)),
        new TexturedVertex(new Vector4(side, -side, -side, 1.0f),    new Vector2(w, 0)),
        new TexturedVertex(new Vector4(-side, -side, side, 1.0f),    new Vector2(0, h)),
        new TexturedVertex(new Vector4(-side, -side, side, 1.0f),    new Vector2(0, h)),
        new TexturedVertex(new Vector4(side, -side, -side, 1.0f),    new Vector2(w, 0)),
        new TexturedVertex(new Vector4(side, -side, side, 1.0f),     new Vector2(w, h)),

        new TexturedVertex(new Vector4(-side, side, -side, 1.0f),    new Vector2(0, 0)),
        new TexturedVertex(new Vector4(-side, side, side, 1.0f),     new Vector2(0, h)),
        new TexturedVertex(new Vector4(side, side, -side, 1.0f),     new Vector2(w, 0)),
        new TexturedVertex(new Vector4(side, side, -side, 1.0f),     new Vector2(w, 0)),
        new TexturedVertex(new Vector4(-side, side, side, 1.0f),     new Vector2(0, h)),
        new TexturedVertex(new Vector4(side, side, side, 1.0f),      new Vector2(w, h)),

        new TexturedVertex(new Vector4(-side, -side, -side, 1.0f),   new Vector2(0, 0)),
        new TexturedVertex(new Vector4(-side, side, -side, 1.0f),    new Vector2(0, h)),
        new TexturedVertex(new Vector4(side, -side, -side, 1.0f),    new Vector2(w, 0)),
        new TexturedVertex(new Vector4(side, -side, -side, 1.0f),    new Vector2(w, 0)),
        new TexturedVertex(new Vector4(-side, side, -side, 1.0f),    new Vector2(0, h)),
        new TexturedVertex(new Vector4(side, side, -side, 1.0f),     new Vector2(0, 0)),

        new TexturedVertex(new Vector4(-side, -side, side, 1.0f),    new Vector2(0, 0)),
        new TexturedVertex(new Vector4(side, -side, side, 1.0f),     new Vector2(w, 0)),
        new TexturedVertex(new Vector4(-side, side, side, 1.0f),     new Vector2(0, h)),
        new TexturedVertex(new Vector4(-side, side, side, 1.0f),     new Vector2(0, h)),
        new TexturedVertex(new Vector4(side, -side, side, 1.0f),     new Vector2(w, 0)),
        new TexturedVertex(new Vector4(side, side, side, 1.0f),      new Vector2(w, h)),
    };
            return vertices;
        }

        public static void LoadModel(string path)
        {
            List<Vector3> vertices = new List<Vector3>();

            List<Vector2> uvs = new List<Vector2>();

            List<Vector3> normals = new List<Vector3>();

            List<int> vertexIndicies = new List<int>();

            List<int> uvIndicies = new List<int>();

            List<int> normalIndicies = new List<int>();

            string src = System.IO.File.ReadAllText(path);

            // Seperate lines from the file
            List<string> lines = new List<string>(src.Split('\n'));

            foreach (var line in lines)
            {
                //load vertices
                if (line.StartsWith("v "))
                {
                    // Cut off beginning of line
                    string temp = line.Substring(2);

                    Vector3 vec = new Vector3();


                    if (temp.Count((char c) => c == ' ') == 2) // Check if there's enough elements for a vertex
                    {
                        string[] vertparts = temp.Split(' ');

                        // Attempt to parse each part of the vertice
                        bool success = float.TryParse(vertparts[0], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out vec.X);
                        success &= float.TryParse(vertparts[1], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out vec.Y);
                        success &= float.TryParse(vertparts[2], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out vec.Z);

                        if (!success)
                        {
                            Console.WriteLine("Error parsing vertex: {0}", line);
                        }
                    }
                    vertices.Add(vec);
                }
                //load uvs
                if (line.StartsWith("vt "))
                {
                    string temp = line.Substring(3);
                    Vector2 vec = new Vector2();
                    string[] vertparts = temp.Split(' ');

                    // Attempt to parse each part of the vertice
                    bool success = float.TryParse(vertparts[0], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out vec.X);
                    success &= float.TryParse(vertparts[1], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out vec.Y);
                    if (!success)
                    {
                        Console.WriteLine("Error parsing uv: {0}", line);
                    }
                    uvs.Add(vec);
                }
                //load normals
                if (line.StartsWith("vn "))
                {
                    // Cut off beginning of line
                    string temp = line.Substring(2);

                    Vector3 vec = new Vector3();


                    if (temp.Count((char c) => c == ' ') == 2) // Check if there's enough elements for a vertex
                    {
                        string[] vertparts = temp.Split(' ');

                        // Attempt to parse each part of the vertice
                        bool success = float.TryParse(vertparts[0], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out vec.X);
                        success &= float.TryParse(vertparts[1], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out vec.Y);
                        success &= float.TryParse(vertparts[2], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out vec.Z);

                        if (!success)
                        {
                            Console.WriteLine("Error parsing vertex: {0}", line);
                        }
                    }
                    normals.Add(vec);
                }
                //load faces
                if (line.StartsWith("f "))
                {
                    // Cut off beginning of line
                    String temp = line.Substring(2);

                    Tuple<int, int, int> face = new Tuple<int, int, int>(0, 0, 0);

                    if (temp.Count((char c) => c == ' ') == 2) // Check if there's enough elements for a face
                    {
                        string[] faceparts = temp.Split(' ');

                        int[] vertexIndex = new int[3]; int[] uvIndex = new int[3]; int[] normalIndex = new int[3];

                        bool success = false;

                        for (int i = 0; i < 3; i++)
                        {
                            string[] indexParts = faceparts[i].Split('/');
                            success &= int.TryParse(indexParts[i], System.Globalization.NumberStyles.Any, null, out vertexIndex[i]);
                            success &= int.TryParse(indexParts[i], System.Globalization.NumberStyles.Any, null, out uvIndex[i]);
                            success &= int.TryParse(indexParts[i], System.Globalization.NumberStyles.Any, null, out normalIndex[i]);
                        }

                        // If any of the parses failed, report the error
                        if (!success)
                        {
                            Console.WriteLine("Error parsing face: {0}", line);
                        }
                        else
                        {
                            vertexIndicies.AddRange(vertexIndex);

                            uvIndicies.AddRange(uvIndex);

                            normalIndicies.AddRange(normalIndex);
                        }
                    }
                }
            }

            return;
        }
    }
}
