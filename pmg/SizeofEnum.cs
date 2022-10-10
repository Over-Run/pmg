namespace pmg;

public enum SizeofEnum
{
    Byte = 1,
    Sbyte = SizeofEnum.Byte,
    Bool = SizeofEnum.Byte,
    Char = 2,
    Ushort = SizeofEnum.Char,
    Short = SizeofEnum.Char,
    Int16 = SizeofEnum.Char,
    UInt = 4,
    Int = SizeofEnum.UInt,
    Int32 = SizeofEnum.UInt,
    Float = SizeofEnum.UInt,
    Ulong = 8,
    Long = SizeofEnum.Ulong,
    Int64 = SizeofEnum.Ulong,
    Double = SizeofEnum.Ulong,
    Decimal = 16

    


}