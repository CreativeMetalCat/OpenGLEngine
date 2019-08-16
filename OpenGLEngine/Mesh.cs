using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace OpenGLEngine
{
    class Mesh
    {
        public List<Vector4> vertices = new List<Vector4>();

        public List<Vector3> textureVertices = new List<Vector3>();

        public List<Vector3> normals = new List<Vector3>();

        public List<int> vertexIndices = new List<int>();

        public List<int> textureIndices = new List<int>();

        public List<int> normalIndices = new List<int>();
    }
}
