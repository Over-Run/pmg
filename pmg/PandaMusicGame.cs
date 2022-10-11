using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using static pmg.StaticField;

namespace pmg;

public static class PandaMusicGame
{
    private static readonly uint[] Indices =
        {
            0, 1, 3,
            1, 2, 3
        };

    private static void Main(string[] args) 
    {
        var options = WindowOptions.Default;
        options.Size = new Vector2D<int>(1000, 1000);
        options.Title = "Panda music game";
        Window1 = Window.Create(options);
        Vertices = new [] {
            //X    Y      Z
            0.5f,  0.5f, 0.0f,
            0.5f, -0.5f, 0.0f,
            -0.5f, -0.5f, 0.0f,
            -0.5f,  0.5f, 0.0f
        };
        Window1.Load += OnLoad;
        Window1.Render += OnRender;
        Window1.Update += OnUpdate;
        Window1.Closing += OnClose;
        

        Window1.Run();
    }


    private static unsafe void OnLoad()
    { 
        const int uInt = (int)SizeofEnum.UInt;
        const int __float = (int)SizeofEnum.Float;
        var input = Window1?.CreateInput()!;
        foreach (var t in input.Keyboards)
        {
            t.KeyDown += KeyDown;
            t.KeyUp += KeyUp;
        }

            
        Gl = GL.GetApi(Window1);

         
        Vao = Gl.GenVertexArray();
        Gl.BindVertexArray(Vao);

        Vbo = Gl.GenBuffer(); 
        Gl.BindBuffer(BufferTargetARB.ArrayBuffer, Vbo); 
        fixed (void* v = &Vertices![0])
        {
            
            Gl.BufferData(BufferTargetARB.ArrayBuffer, (nuint) (Vertices.Length * uInt), v, BufferUsageARB.StaticDraw); //Setting buffer data.
        }

            
        Ebo = Gl.GenBuffer(); 
        Gl.BindBuffer(BufferTargetARB.ElementArrayBuffer, Ebo); 
        fixed (void* i = &Indices[0])
        {
            Gl.BufferData(BufferTargetARB.ElementArrayBuffer, (nuint) (Indices.Length * uInt), i, BufferUsageARB.StaticDraw); //Setting buffer data.
        }

           
        var vertexShader = Gl.CreateShader(ShaderType.VertexShader);
        Gl.ShaderSource(vertexShader, VertexShaderSource);
        Gl.CompileShader(vertexShader);

            
        var infoLog = Gl.GetShaderInfoLog(vertexShader);
        if (!string.IsNullOrWhiteSpace(infoLog))
        {
            Console.WriteLine($"Error compiling vertex shader {infoLog}");
        }

            
        var fragmentShader = Gl.CreateShader(ShaderType.FragmentShader);
        Gl.ShaderSource(fragmentShader, FragmentShaderSource);
        Gl.CompileShader(fragmentShader);

            
        infoLog = Gl.GetShaderInfoLog(fragmentShader);
        if (!string.IsNullOrWhiteSpace(infoLog))
        {
            Console.WriteLine($"Error compiling fragment shader {infoLog}");
        }

            
        Shaders = Gl.CreateProgram();
        Gl.AttachShader(Shaders, vertexShader);
        Gl.AttachShader(Shaders, fragmentShader);
        Gl.LinkProgram(Shaders);

        
        Gl.GetProgram(Shaders, GLEnum.LinkStatus, out var status);
        if (status == 0)
        {
            Console.WriteLine($"Error linking shader {Gl.GetProgramInfoLog(Shaders)}");
        }

            
        Gl.DetachShader(Shaders, vertexShader);
        Gl.DetachShader(Shaders, fragmentShader);
        Gl.DeleteShader(vertexShader);
        Gl.DeleteShader(fragmentShader);

            
        Gl.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * __float, null);
        Gl.EnableVertexAttribArray(0);
    }

    private static unsafe void OnRender(double obj) 
    { 
        Gl?.Clear((uint) ClearBufferMask.ColorBufferBit);

           
        Gl?.BindVertexArray(Vao);
        Gl?.UseProgram(Shaders);

           
        Gl?.DrawElements(PrimitiveType.Triangles, (uint) Indices.Length, DrawElementsType.UnsignedInt, null);
    }

    private static void OnUpdate(double obj)
    {
    }
    
    

    private static void OnClose()
    {
        StaticField.Gl?.DeleteBuffer(StaticField.Vbo);
        StaticField.Gl?.DeleteBuffer(StaticField.Ebo);
        StaticField.Gl?.DeleteVertexArray(StaticField.Vao);
        StaticField.Gl?.DeleteProgram(StaticField.Shaders);
    }

    private static void KeyDown(IKeyboard arg1, Key arg2, int arg3)
    {
        if (arg2 == Key.Escape)
        {
            StaticField.Window1?.Close();
        }
    }

    private static void KeyUp(IKeyboard arg1, Key arg2, int arg3)
    {
        if (arg2 == Key.Escape)
        {
            StaticField.Window1?.Close();
        }
    }
}