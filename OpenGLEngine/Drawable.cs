using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using System.Drawing;

namespace OpenGLEngine
{
    abstract class SceneObject : IDisposable
    {
        protected Vector3 Rotation;

        protected Vector3 Location;

        public readonly string Name;

        protected bool initialized = false;

        protected double livedTime = 0.0f;

        public virtual void Bind() { }

        public virtual void Render() { }

        public virtual void Update(FrameEventArgs e) { }

        protected virtual void HandleKeyboardInput() { }

        protected virtual void Dispose(bool disposing) { }

        public void Dispose() { Dispose(true); }



        public SceneObject(Vector3 location, Vector3 rotation, string name)
        {
            Rotation = rotation;
            Location = location;
            Name = name;
        }
    }

    class Drawable :SceneObject
    {
        protected readonly int vertexArray;

        protected readonly int vertexBuffer;

        protected readonly int verticeCount = 0;

        protected int shaderProgram;

        public Matrix4 modelView;       

         protected bool canBeControlled = true;

        public bool CanBeControlled { get => canBeControlled; set => canBeControlled = value; }

        public Drawable(Vertex[] vertices, Vector3 _location, Vector3 _rotation,int shaderProgram,bool CanBeControlled):base(_location,_rotation,"noname")
        {
           
            this.CanBeControlled = CanBeControlled;
            this.shaderProgram = shaderProgram;

            verticeCount = vertices.Length;
            vertexArray = GL.GenVertexArray();
            vertexBuffer = GL.GenBuffer();

            GL.BindVertexArray(vertexArray);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexArray);

            // create first buffer: vertex
            GL.NamedBufferStorage(
                vertexBuffer,
                Vertex.Size * vertices.Length,        // the size needed by this buffer
                vertices,                           // data to initialize with
                BufferStorageFlags.MapWriteBit);    // at this point we will only write to the buffer


            GL.VertexArrayAttribBinding(vertexArray, 0, 0);
            GL.EnableVertexArrayAttrib(vertexArray, 0);
            GL.VertexArrayAttribFormat(
                vertexArray,
                0,                      // attribute index, from the shader location = 0
                4,                      // size of attribute, vec4
                VertexAttribType.Float, // contains floats
                false,                  // does not need to be normalized as it is already, floats ignore this flag anyway
                0);                     // relative offset, first item


            GL.VertexArrayAttribBinding(vertexArray, 1, 0);
            GL.EnableVertexArrayAttrib(vertexArray, 1);
            GL.VertexArrayAttribFormat(
                vertexArray,
                1,                      // attribute index, from the shader location = 1
                4,                      // size of attribute, vec4
                VertexAttribType.Float, // contains floats
                false,                  // does not need to be normalized as it is already, floats ignore this flag anyway
                16);                     // relative offset after a vec4

            // link the vertex array and buffer and provide the stride as size of Vertex
            GL.VertexArrayVertexBuffer(vertexArray, 0, vertexBuffer, IntPtr.Zero, Vertex.Size);
            initialized = true;
        }

      

        public override void Bind()
        {
            GL.UseProgram(shaderProgram);
            GL.UniformMatrix4(21, false, ref modelView);

            GL.BindVertexArray(vertexArray);
        }

        public override void Render()
        {  
            GL.DrawArrays(PrimitiveType.Triangles, 0, verticeCount);
        }

        public override  void Update(FrameEventArgs e)
        {
            livedTime += e.Time;
            var k = (float)livedTime * 0.05f;
            var r1 = Matrix4.CreateRotationX(/*k * 13.0f*/ Rotation.X);
            var r2 = Matrix4.CreateRotationY(/*k * 13.0f*/Rotation.Y);
            var r3 = Matrix4.CreateRotationZ(Rotation.Z);
            var t1 = Matrix4.CreateTranslation(Location);
            modelView = r1 * r2 * r3 * t1;

            HandleKeyboardInput();
        }

        protected override  void HandleKeyboardInput()
        {
            var keyState = OpenTK.Input.Keyboard.GetState();

           

            if (canBeControlled)
            {
               

                //rotation
                if (keyState.IsKeyDown(OpenTK.Input.Key.Up))
                {
                    Rotation.X += 0.1f;
                }

                if (keyState.IsKeyDown(OpenTK.Input.Key.Down))
                {
                    Rotation.X -= 0.1f;
                }

                if (keyState.IsKeyDown(OpenTK.Input.Key.Right))
                {
                    Rotation.Y += 0.1f;
                }

                if (keyState.IsKeyDown(OpenTK.Input.Key.Left))
                {
                    Rotation.Y -= 0.1f;
                }

                //location

                if (keyState.IsKeyDown(OpenTK.Input.Key.Space))
                {
                    Location.Z += 0.01f;
                }

                if (keyState.IsKeyDown(OpenTK.Input.Key.ControlLeft))
                {
                    Location.Z -= 0.01f;

                    System.Console.WriteLine(Location.Z);
                }

                if (keyState.IsKeyDown(OpenTK.Input.Key.W))
                {
                    Location.Y += 0.1f;
                }

                if (keyState.IsKeyDown(OpenTK.Input.Key.S))
                {
                    Location.Y -= 0.1f;
                }

                if (keyState.IsKeyDown(OpenTK.Input.Key.D))
                {
                    Location.X += 0.1f;
                }

                if (keyState.IsKeyDown(OpenTK.Input.Key.A))
                {
                    Location.X -= 0.1f;
                }
            }
        }

        protected override  void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (initialized)
                {
                    GL.DeleteBuffer(vertexBuffer);
                    GL.DeleteVertexArray(vertexArray);
                    initialized = false;
                }
            }
        }


    }

    class TexturedDrawable : SceneObject
    {
        private int _texture;

        protected int shaderProgram;

        protected readonly int vertexArray;

        protected readonly int vertexBuffer;

        protected readonly int verticeCount = 0;

             protected bool canBeControlled = true;

        public Matrix4 modelView;


        public TexturedDrawable(TexturedVertex[] vertices, Vector3 _location, Vector3 _rotation, int shaderProgram, string filename, bool CanBeControlled) :base(_location, _rotation,"noname")
        {
            verticeCount = vertices.Length;
            this.shaderProgram = shaderProgram;

            vertexArray = GL.GenVertexArray();
            vertexBuffer = GL.GenBuffer();

            GL.BindVertexArray(vertexArray);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexArray);

            // create first buffer: vertex
            GL.NamedBufferStorage(
                vertexBuffer,
                TexturedVertex.Size * vertices.Length,        // the size needed by this buffer
                vertices,                           // data to initialize with
                BufferStorageFlags.MapWriteBit);    // at this point we will only write to the buffer

            GL.VertexArrayAttribBinding(vertexArray, 0, 0);
            GL.EnableVertexArrayAttrib(vertexArray, 0);
            GL.VertexArrayAttribFormat(
                vertexArray,
                0,                      // attribute index, from the shader location = 0
                4,                      // size of attribute, vec4
                VertexAttribType.Float, // contains floats
                false,                  // does not need to be normalized as it is already, floats ignore this flag anyway
                0);                     // relative offset, first item, in bytes

            GL.VertexArrayAttribBinding(vertexArray, 1, 0);
            GL.EnableVertexArrayAttrib(vertexArray, 1);
            GL.VertexArrayAttribFormat(
                vertexArray,
                1,                      // attribute index, from the shader location = 1
                2,                      // size of attribute, vec2
                VertexAttribType.Float, // contains floats
                false,                  // does not need to be normalized as it is already, floats ignore this flag anyway
                16);                     // relative offset after a vec4, in bytes

            // link the vertex array and buffer and provide the stride as size of Vertex
            GL.VertexArrayVertexBuffer(vertexArray, 0, vertexBuffer, IntPtr.Zero, TexturedVertex.Size);

            _texture = InitTextures(filename);
        }

        protected int InitTextures(string filename)
        {
            int width, height;
            var data = LoadTexture(filename, out width, out height);
            int texture = 0;
            GL.CreateTextures(TextureTarget.Texture2D, 1, out texture);
            GL.TextureStorage2D(
                            texture,
                            1,                           // levels of mipmapping
                            SizedInternalFormat.Rgba32f, // format of texture
                            width,
                            height);

            GL.BindTexture(TextureTarget.Texture2D, texture);
            GL.TextureSubImage2D(texture,
                0,                  // this is level 0
                0,                  // x offset
                0,                  // y offset
                width,
                height,
                PixelFormat.Rgba,
                PixelType.Float,
                data);

            return texture;
        }

        protected float[] LoadTexture(string filename, out int width, out int height)
        {
            float[] r;
            using (var bmp = (Bitmap)Image.FromFile(filename))
            {
                width = bmp.Width;
                height = bmp.Height;
                r = new float[width * height * 4];
                int index = 0;
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        var pixel = bmp.GetPixel(x, y);
                        r[index++] = pixel.R / 255f;
                        r[index++] = pixel.G / 255f;
                        r[index++] = pixel.B / 255f;
                        r[index++] = pixel.A / 255f;
                    }
                }
            }
            return r;
        }

        public override void Bind()
        {
            GL.UseProgram(shaderProgram);
            GL.UniformMatrix4(21, false, ref modelView);
            GL.BindVertexArray(vertexArray);
            GL.BindTexture(TextureTarget.Texture2D, _texture);
        }

        public override  void Render()
        {
            GL.DrawArrays(PrimitiveType.Triangles, 0, verticeCount);
        }

        public override void Update(FrameEventArgs e)
        {
            livedTime += e.Time;
            var k = (float)livedTime * 0.05f;
            var r1 = Matrix4.CreateRotationX(/*k * 13.0f*/ Rotation.X);
            var r2 = Matrix4.CreateRotationY(/*k * 13.0f*/Rotation.Y);
            var r3 = Matrix4.CreateRotationZ(Rotation.Z);
            var t1 = Matrix4.CreateTranslation(Location);
            modelView = r1 * r2 * r3 * t1;

            HandleKeyboardInput();
        }

        protected override void HandleKeyboardInput()
        {
            var keyState = OpenTK.Input.Keyboard.GetState();



            if (canBeControlled)
            {


                //rotation
                if (keyState.IsKeyDown(OpenTK.Input.Key.Up))
                {
                    Rotation.X += 0.1f;
                }

                if (keyState.IsKeyDown(OpenTK.Input.Key.Down))
                {
                    Rotation.X -= 0.1f;
                }

                if (keyState.IsKeyDown(OpenTK.Input.Key.Right))
                {
                    Rotation.Y += 0.1f;
                }

                if (keyState.IsKeyDown(OpenTK.Input.Key.Left))
                {
                    Rotation.Y -= 0.1f;
                }

                //location

                if (keyState.IsKeyDown(OpenTK.Input.Key.Space))
                {
                    Location.Z += 0.01f;
                }

                if (keyState.IsKeyDown(OpenTK.Input.Key.ControlLeft))
                {
                    Location.Z -= 0.01f;

                    System.Console.WriteLine(Location.Z);
                }

                if (keyState.IsKeyDown(OpenTK.Input.Key.W))
                {
                    Location.Y += 0.1f;
                }

                if (keyState.IsKeyDown(OpenTK.Input.Key.S))
                {
                    Location.Y -= 0.1f;
                }

                if (keyState.IsKeyDown(OpenTK.Input.Key.D))
                {
                    Location.X += 0.1f;
                }

                if (keyState.IsKeyDown(OpenTK.Input.Key.A))
                {
                    Location.X -= 0.1f;
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (initialized)
                {
                    GL.DeleteBuffer(vertexBuffer);
                    GL.DeleteVertexArray(vertexArray);
                    initialized = false;
                }
            }
        }

    }

}
