using System;
using GLFW;
using LearnOpenGLBook_CSharp.Library.Types;
using OpenGL;

namespace LearnOpenGLBook_CSharp.Library
{
    public abstract class Game
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public string WindowTitle { get; }

        public Color BackgroundColor { get; set; }

        protected Window Window { get; private set; }
        private bool shouldClose = false;

        public Game(int width, int height, string windowTitle)
        {
            Width = width;
            Height = height;
            WindowTitle = windowTitle;
        }
        
        public void GameLoop()
        {
            // Initialize OpenGL and GLFW
            Gl.Initialize();
            Glfw.Init();
            // Set Window Hints
            Glfw.WindowHint(Hint.ClientApi, ClientApi.OpenGL);
            Glfw.WindowHint(Hint.ContextVersionMajor, 4);
            Glfw.WindowHint(Hint.ContextVersionMinor, 6);
            Glfw.WindowHint(Hint.OpenglProfile, Profile.Core);
            Glfw.WindowHint(Hint.Doublebuffer, true);
            Glfw.WindowHint(Hint.Decorated, true);

            Window = Glfw.CreateWindow(Width, Height, WindowTitle,
                Monitor.None, Window.None
            );
            if (Window == Window.None)
            {
                Console.Error.WriteLine("Failed to create GLFW Window");
                Glfw.Terminate();
                Environment.Exit(1001);
            }
            
            Glfw.MakeContextCurrent(Window);
            Glfw.SwapInterval(1);
            
            Gl.Viewport(0, 0, Width, Height);
            Glfw.SetFramebufferSizeCallback(Window, OnResizeCallback);
            
            Init();

            while (!Glfw.WindowShouldClose(Window))
            {
                PreUpdate();
                
                Gl.ClearColor(BackgroundColor.red, BackgroundColor.green, 
                    BackgroundColor.blue, BackgroundColor.alpha);
                Gl.Clear(ClearBufferMask.ColorBufferBit);
                
                Draw();
                Update();
                
                Glfw.SwapBuffers(Window);
                Glfw.PollEvents();
            }
            
            Quit();
            
            Glfw.Terminate();
        }
        
        public void CloseGame()
        {
            shouldClose = true;
            Glfw.SetWindowShouldClose(Window, true);
        }

        private void OnResizeCallback(IntPtr window, int width, int height)
        {
            Width = width;
            Height = height;
            Gl.Viewport(0, 0, width, height);
            OnResizeGame(width, height);
        }
        
        protected virtual void Init()
        {
            
        }

        protected virtual void Draw()
        {
            
        }

        protected virtual void OnResizeGame(int newWidth, int newHeight)
        {
            
        }

        protected virtual void PreUpdate()
        {
            
        }

        protected virtual void Update()
        {
            
        }

        protected virtual void Quit()
        {
            
        }
        
    }
}