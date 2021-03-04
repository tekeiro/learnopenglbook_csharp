using System;
using System.Drawing;
using System.Linq;
using System.Text;
using GLFW;
using OpenGL;

namespace LearnOpenGLBook_CSharp
{
    class Chapter05
    {
        private string[] vertexShaderSource = @"
            #version 330 core
            layout (location = 0) in vec3 aPos;
            void main()
            {
                gl_Position = vec4(aPos.x, aPos.y, aPos.z, 1.0);
            }".Split("\n").Select(s => s + "\n").ToArray();

        private string[] fragmentShaderSource = @"
            #version 330 core
            out vec4 FragColor;
            void main()
            {
                FragColor = vec4(1.0f, 0.5f, 0.2f, 1.0f);
            }".Split("\n").Select(s => s + "\n").ToArray();
        
        
        
        private float[] vertices = new[] {
            -0.5f, -0.5f, 0.0f,
            0.5f, -0.5f, 0.0f,
            0.0f, 0.5f, 0.0f
        };

        public void Run()
        {
            //  Init
            // -------------------------------------------
            Gl.Initialize();
            Glfw.Init();
            
            Glfw.WindowHint(Hint.ClientApi, ClientApi.OpenGL);
            Glfw.WindowHint(Hint.ContextVersionMajor, 4);
            Glfw.WindowHint(Hint.ContextVersionMinor, 6);
            Glfw.WindowHint(Hint.OpenglProfile, Profile.Core);
            Glfw.WindowHint(Hint.Doublebuffer, true);
            Glfw.WindowHint(Hint.Decorated, true);
            Glfw.WindowHint(Hint.OpenglForwardCompatible, true);
            

            var window = Glfw.CreateWindow(800, 600, 
                "Learn OpenGL Book", Monitor.None, Window.None);
            if (window == Window.None)
            {
                Console.Error.WriteLine("Failed to create GLFW Window");
                Glfw.Terminate();
                return;
            }
            Glfw.MakeContextCurrent(window);
            Glfw.SwapInterval(1);
            
            Gl.Viewport(0, 0, 800, 600);
            Glfw.SetFramebufferSizeCallback(window, OnViewportResize);
            // -------------------------------------------
            
            
            // Build and Compile our shader program
            // ----------------------------------
            //  Vertex shader
            var vertexShader = Gl.CreateShader(ShaderType.VertexShader);
            Gl.ShaderSource(vertexShader, vertexShaderSource);
            Gl.CompileShader(vertexShader);
            //  Fragment Shader
            var fragmentShader = Gl.CreateShader(ShaderType.FragmentShader);
            Gl.ShaderSource(fragmentShader, fragmentShaderSource);
            Gl.CompileShader(fragmentShader);
            //  Link shaders
            var shaderProgram = Gl.CreateProgram();
            Gl.AttachShader(shaderProgram, vertexShader);
            Gl.AttachShader(shaderProgram, fragmentShader);
            Gl.LinkProgram(shaderProgram);
            //  Delete not useful shaders
            Gl.DeleteShader(vertexShader);
            Gl.DeleteShader(fragmentShader);
            
            
            // Set up vertex data (and buffer(s)) and configure vertex attributes
            var vao = Gl.GenVertexArray();
            var vbo = Gl.GenBuffer();
            // bind the Vertex Array Object first, then bind and set vertex buffer(s), and then configure vertex attributes(s).
            Gl.BindVertexArray(vao);
            //  
            Gl.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            Gl.BufferData(BufferTarget.ArrayBuffer, (uint)(4 * vertices.Length), vertices, BufferUsage.StaticDraw);
            // 
            Gl.VertexAttribPointer(0, 3, VertexAttribType.Float, false, 3 * sizeof(float), IntPtr.Zero);
            Gl.EnableVertexAttribArray(0);
            // note that this is allowed, the call to glVertexAttribPointer registered VBO as the vertex attribute's bound vertex buffer object so afterwards we can safely unbind
            Gl.BindBuffer(BufferTarget.ArrayBuffer, 0);
            // You can unbind the VAO afterwards so other VAO calls won't accidentally modify this VAO, but this rarely happens. Modifying other
            // VAOs requires a call to glBindVertexArray anyways so we generally don't unbind VAOs (nor VBOs) when it's not directly necessary.
            Gl.BindVertexArray(0);

            while (!Glfw.WindowShouldClose(window))
            {
                // Input
                ProcessInput(window);
                
                // Rendering
                Gl.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
                Gl.Clear(ClearBufferMask.ColorBufferBit);
                
                // Draw rectangle
                Gl.UseProgram(shaderProgram);
                Gl.BindVertexArray(vao);
                Gl.DrawArrays(PrimitiveType.Triangles, 0, 3);
                
                // Check and call events and Swap Buffers
                Glfw.SwapBuffers(window);
                Glfw.PollEvents();
            }
            
            Gl.DeleteVertexArrays(vao);
            Gl.DeleteBuffers(vbo);
            Gl.DeleteProgram(shaderProgram);
            
            Glfw.Terminate();
        }

        private void OnViewportResize(IntPtr window, int width, int height)
        {
            Gl.Viewport(0, 0, width, height);
        }

        private void ProcessInput(Window window)
        {
            if (Glfw.GetKey(window, Keys.Escape) == InputState.Press)
            {
                Glfw.SetWindowShouldClose(window, true);
            }
        }

    }
}