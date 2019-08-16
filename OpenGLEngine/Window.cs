using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace OpenGLEngine
{
    class Window : OpenTK.GameWindow
    {
        private PolygonMode polygonMode = PolygonMode.Fill;

        private ShaderProgram solidColorProgram;

        private ShaderProgram textureProgram;

        private double timeSinceStart = 0;

        private Matrix4 _projectionMatrix;

        private void CreateProjection()
        {

            var aspectRatio = (float)Width / Height;
            _projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(
                60 * ((float)Math.PI / 180f), // field of view angle, in radians
                aspectRatio,                // current window aspect ratio
                0.1f,                       // near plane
                4000f);                     // far plane
        }

        private List<SceneObject> SceneObjects = new List<SceneObject>();

        private int CompileShader(ShaderType type, string path)
        {
            var shader = GL.CreateShader(type);
            GL.ShaderSource(shader, System.IO.File.ReadAllText(path));
            GL.CompileShader(shader);
            var info = GL.GetShaderInfoLog(shader);
            if (!string.IsNullOrWhiteSpace(info))
            {
                Console.WriteLine($"GL.CompileShader [{type}] had info log: {info}");
            }
            return shader;
        }

        private int CreateProgram(string VertextShaderPath, string FragmentShaderPath)
        {
            var shaders = new List<int>();

            shaders.Add(CompileShader(ShaderType.VertexShader, VertextShaderPath));

            shaders.Add(CompileShader(ShaderType.FragmentShader, FragmentShaderPath));

            var program = GL.CreateProgram();

            foreach (var shader in shaders)
            {
                GL.AttachShader(program, shader);
            }

            GL.LinkProgram(program);
            var info = GL.GetProgramInfoLog(program);
            if (!string.IsNullOrWhiteSpace(info))
            {
                Console.WriteLine($"GL.LinkProgram had info log: {info}");
            }

            foreach (var shader in shaders)
            {
                GL.DetachShader(program, shader);
                GL.DeleteShader(shader);
            }
            return program;
        }



        protected void HandleKeyboardInput()
        {
            var keyState = OpenTK.Input.Keyboard.GetState();
            if (keyState.IsKeyDown(OpenTK.Input.Key.Escape))
            {
                this.Exit();
            }
            if (keyState.IsKeyDown(OpenTK.Input.Key.Space))
            {
               
            }
            if (keyState.IsKeyDown(OpenTK.Input.Key.M))
            {
                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Point);
            }
            if (keyState.IsKeyDown(OpenTK.Input.Key.Comma))
            {
                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
            }
            if (keyState.IsKeyDown(OpenTK.Input.Key.Period))
            {
                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
            }
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            timeSinceStart += e.Time;

            HandleKeyboardInput();

            foreach (var sceneObject in SceneObjects)
            {
                sceneObject.Update(e);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Width, Height);
            CreateProjection();
        }

        private void OnClosed(object sender, EventArgs eventArgs)
        {
            Exit();
        }

        public override void Exit()
        {
            System.Console.WriteLine("Exit Called For window");
            foreach (var sceneObject in SceneObjects)
            {
                sceneObject.Dispose();
            }
            textureProgram.Dispose();
            solidColorProgram.Dispose();

            base.Exit();
        }



        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            CreateProjection();
            ShapeFactory.LoadModel(@"Components\models\couch.obj");
          // solidColorProgram = CreateProgram(@"Components\Shaders\vertexShader.vert", @"Components\Shaders\fragmentShader.frag");
            solidColorProgram = new ShaderProgram();
            solidColorProgram.AddShader(ShaderType.VertexShader, @"Components\Shaders\vertexShader.vert");
            solidColorProgram.AddShader(ShaderType.FragmentShader, @"Components\Shaders\fragmentShader.frag");
            solidColorProgram.Link();

            textureProgram = new ShaderProgram();
            textureProgram.AddShader(ShaderType.VertexShader, @"Components\Shaders\texturedShader.vert");
            textureProgram.AddShader(ShaderType.FragmentShader, @"Components\Shaders\texturedShader.frag");
            textureProgram.Link();


            Vertex[] vertices1 = ShapeFactory.CreateSolidCude(0.2f, OpenTK.Graphics.Color4.Blue);

            Vertex[] vertices2 = ShapeFactory.CreateSolidCude(0.2f, OpenTK.Graphics.Color4.Red);

            // SceneObjects.Add(new Drawable(vertices1, new Vector3(0, 0, 0), new Vector3(0, 0, 0), solidColorProgram.Id, false));
           

            SceneObjects.Add(new TexturedDrawable(ShapeFactory.CreateTexturedCube(0.2f, 256, 256), new Vector3(0, 0.5f, -0.5f), new Vector3(0, 0, 0), textureProgram.Id, @"Components\Textures\dev\reflectivity_50.png", true));
            SceneObjects.Add(new Drawable(vertices2, new Vector3(0, -0.5f, -1.0f), new Vector3(0, 0, 0), solidColorProgram.Id, false));
            SceneObjects.Add(new TexturedDrawable(ShapeFactory.CreateTexturedCube(0.2f, 256, 256), new Vector3(0,0,0), new Vector3(0, 0, 0), textureProgram.Id, @"Components\Textures\dev\dev_measurewall01a.png", true));




            GL.PolygonMode(MaterialFace.FrontAndBack, polygonMode);
            GL.PatchParameter(PatchParameterInt.PatchVertices, 3);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Texture2D);
            Closed += OnClosed;

        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            OpenTK.Graphics.Color4 backgroundColor;
            backgroundColor = OpenTK.Graphics.Color4.Black;

            GL.ClearColor(backgroundColor);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            //GL.UseProgram(solidColorProgram);

            GL.UniformMatrix4(20, false, ref _projectionMatrix);
            //shader atrribs
            //GL.VertexAttrib1(0, timeSinceStart);

            //Vector4 position;
            //position.X = (float)Math.Sin(timeSinceStart) * 0.5f;
            //position.Y = (float)Math.Cos(timeSinceStart) * 0.5f;
            //position.Z = 0.0f;
            //position.W = 1.0f;
            //GL.VertexAttrib4(1, position);


            foreach (var sceneObject in SceneObjects)
            {
                sceneObject.Bind();
                GL.UniformMatrix4(20, false, ref _projectionMatrix);
                sceneObject.Render();
            }

            //GL.DrawArrays(PrimitiveType.Points, 0, 1);
            //GL.PointSize(10);

            SwapBuffers();
        }

        public Window() : base(1280, // initial width
        720, // initial height
       OpenTK.Graphics.GraphicsMode.Default,
        "App",  // initial title
        GameWindowFlags.Default,
        DisplayDevice.Default,
        4, // OpenGL major version
        6, // OpenGL minor version
          OpenTK.Graphics.GraphicsContextFlags.ForwardCompatible)
        {
            Title += ": OpenGL Version: " + GL.GetString(StringName.Version);
        }
    }
}

