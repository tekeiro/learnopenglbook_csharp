using System.Linq;
using OpenGL;

namespace LearnOpenGLBook_CSharp.Library.Shaders
{
    /// <summary>
    /// A GLSL shader
    /// </summary>
    public class Shader
    {
        public string[] Source { get; private set; }
        public ShaderType ShaderType { get; private set; }
        public uint InternalId { get; private set; }

        private Shader()
        { }

        public static Shader FromString(ShaderType shaderType, string glslSource)
        {
            var shader = new Shader();
            shader.ShaderType = shaderType;
            shader.Source = glslSource.Split("\n").Select(s => s + "\n").ToArray();
            shader.InitAndCompile();
            return shader;
        }

        private void InitAndCompile()
        {
            InternalId = Gl.CreateShader(ShaderType);
            Gl.ShaderSource(InternalId, Source);
            Gl.CompileShader(InternalId);
        }
        
    }
}