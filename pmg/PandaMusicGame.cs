using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;



namespace pmg;

public static class PandaMusicGame
{
    private static IWindow? _window;
    private static GL? _gl;

    private static uint _vbo;
    private static uint _ebo;
    private static uint _vao;
    private static uint _shader;

    private static readonly string VertexShaderSource = @"
        #version 330 core //Using version GLSL version 3.3
        layout (location = 0) in vec4 vPos;
        
        void main()
        {
            gl_Position = vec4(vPos.x, vPos.y, vPos.z, 1.0);
        }
        ";

    private static readonly string FragmentShaderSource = @"
        #version 330 core
        out vec4 FragColor;
        void main()
        {
            FragColor = vec4(1.0f, 0.5f, 0.2f, 1.0f);
        }
        ";

    private static readonly float[] Vertices =
        {
            //X    Y      Z
             0.5f,  0.5f, 0.0f,
             0.5f, -0.5f, 0.0f,
            -0.5f, -0.5f, 0.0f,
            -0.5f,  0.5f, 0.5f
        };

    private static readonly uint[] Indices =
        {
            0, 1, 3,
            1, 2, 3
        };

    private static void Main(string[] args) 
    { 
        var options = WindowOptions.Default;
        options.Size = new Vector2D<int>(800, 600);
        options.Title = "Panda music game";
        _window = Window.Create(options);

        _window.Load += OnLoad;
        _window.Render += OnRender;
        _window.Update += OnUpdate;
        _window.Closing += OnClose;

        _window.Run();
    }


    private static unsafe void OnLoad()
    { 
        const int uInt = (int)SizeofEnum.UInt;
        // ReSharper disable once InconsistentNaming
        const int __float = (int)SizeofEnum.Float;
        var input = _window?.CreateInput()!;
        foreach (var t in input.Keyboards)
        {
            t.KeyDown += KeyDown;
        }

            
        _gl = GL.GetApi(_window);

         
        _vao = _gl.GenVertexArray();
        _gl.BindVertexArray(_vao);

        _vbo = _gl.GenBuffer(); 
        _gl.BindBuffer(BufferTargetARB.ArrayBuffer, _vbo); 
        fixed (void* v = &Vertices[0])
        {
            
            _gl.BufferData(BufferTargetARB.ArrayBuffer, (nuint) (Vertices.Length * uInt), v, BufferUsageARB.StaticDraw); //Setting buffer data.
        }

            
        _ebo = _gl.GenBuffer(); 
        _gl.BindBuffer(BufferTargetARB.ElementArrayBuffer, _ebo); 
        fixed (void* i = &Indices[0])
        {
            _gl.BufferData(BufferTargetARB.ElementArrayBuffer, (nuint) (Indices.Length * uInt), i, BufferUsageARB.StaticDraw); //Setting buffer data.
        }

           
        var vertexShader = _gl.CreateShader(ShaderType.VertexShader);
        _gl.ShaderSource(vertexShader, VertexShaderSource);
        _gl.CompileShader(vertexShader);

            
        var infoLog = _gl.GetShaderInfoLog(vertexShader);
        if (!string.IsNullOrWhiteSpace(infoLog))
        {
            Console.WriteLine($"Error compiling vertex shader {infoLog}");
        }

            
        var fragmentShader = _gl.CreateShader(ShaderType.FragmentShader);
        _gl.ShaderSource(fragmentShader, FragmentShaderSource);
        _gl.CompileShader(fragmentShader);

            
        infoLog = _gl.GetShaderInfoLog(fragmentShader);
        if (!string.IsNullOrWhiteSpace(infoLog))
        {
            Console.WriteLine($"Error compiling fragment shader {infoLog}");
        }

            
        _shader = _gl.CreateProgram();
        _gl.AttachShader(_shader, vertexShader);
        _gl.AttachShader(_shader, fragmentShader);
        _gl.LinkProgram(_shader);

        
        _gl.GetProgram(_shader, GLEnum.LinkStatus, out var status);
        if (status == 0)
        {
            Console.WriteLine($"Error linking shader {_gl.GetProgramInfoLog(_shader)}");
        }

            
        _gl.DetachShader(_shader, vertexShader);
        _gl.DetachShader(_shader, fragmentShader);
        _gl.DeleteShader(vertexShader);
        _gl.DeleteShader(fragmentShader);

            
        _gl.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * __float, null);
        _gl.EnableVertexAttribArray(0);
    }

    private static unsafe void OnRender(double obj) 
    { 
        _gl?.Clear((uint) ClearBufferMask.ColorBufferBit);

           
        _gl?.BindVertexArray(_vao);
        _gl?.UseProgram(_shader);

           
        _gl?.DrawElements(PrimitiveType.Triangles, (uint) Indices.Length, DrawElementsType.UnsignedInt, null);
    }

    private static void OnUpdate(double obj)
    {

    }

    private static void OnClose()
    {
            
        _gl?.DeleteBuffer(_vbo);
        _gl?.DeleteBuffer(_ebo);
        _gl?.DeleteVertexArray(_vao);
        _gl?.DeleteProgram(_shader);
    }

    private static void KeyDown(IKeyboard arg1, Key arg2, int arg3)
    {
        if (arg2 == Key.Escape)
        {
            _window?.Close();
        }
    }
}