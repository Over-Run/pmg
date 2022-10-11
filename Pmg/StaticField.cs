using Silk.NET.OpenGL;
using Silk.NET.Windowing;

namespace Pmg;
//用于存放基本的变量参数
public static class StaticField
{
    //窗体接口
    public static IWindow? Window1 { get; set; }
    //GL类
    public static GL? Gl { get; set; }
    //四个基本绘图参数
    public static uint Vbo { get; set; }
    public static uint Ebo { get; set; }
    public static uint Vao { get; set; }
    public static uint Shaders { get; set; }
    //传入的obj
    public static float[]? Vertices { get; set; }
    //vertex shader source
    public static string VertexShaderSource =>
        @"
        #version 330 core //Using version GLSL version 3.3
        layout (location = 0) in vec4 vPos;
        
        void main()
        {
            gl_Position = vec4(vPos.x, vPos.y, vPos.z, 1.0);
        }
        ";
    //fragment shader source
    public static string FragmentShaderSource =>
        @"
        #version 330 core
        out vec4 FragColor;
        void main()
        {
            FragColor = vec4(1.0f, 0.5f, 0.2f, 1.0f);
        }
        ";
}