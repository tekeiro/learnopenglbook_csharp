using System;
using GLFW;
using OpenGL;

namespace LearnOpenGLBook_CSharp
{
    class Chapter04
    {
        public void Run()
        {
            Glfw.Init();
            
            Glfw.WindowHint(Hint.ClientApi, ClientApi.OpenGL);
            Glfw.WindowHint(Hint.ContextVersionMajor, 3);
            Glfw.WindowHint(Hint.ContextVersionMinor, 3);
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

            while (!Glfw.WindowShouldClose(window))
            {
                // Input
                ProcessInput(window);
                
                // Rendering
                Gl.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
                Gl.Clear(ClearBufferMask.ColorBufferBit);
                
                // Check and call events and Swap Buffers
                Glfw.SwapBuffers(window);
                Glfw.PollEvents();
            }
            
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