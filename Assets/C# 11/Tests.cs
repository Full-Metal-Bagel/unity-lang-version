// https://chatgpt.com/share/af9bd61a-d095-479b-b120-aba778501809
using System;
using NUnit.Framework;

public class CSharp11Features
{
    // 1. Generic attributes
    // https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-11#generic-attributes
    [GenericTypeAttribute<string>]
    public class GenericAttributes
    {
        public void Display() => Console.WriteLine("Generic attribute used.");
    }

    public class GenericTypeAttribute<T> : Attribute { }

    [Test]
    public void DisplayGenericAttribute()
    {
        var instance = new GenericAttributes();
        Assert.DoesNotThrow(() => instance.Display());
    }

    // // 2. Generic math support
    // // https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-11#generic-math-support
    // public interface IAdditionOperators<TSelf, TOther, TResult>
    // {
    //     static abstract TResult operator +(TSelf left, TOther right);
    // }
    //
    // public struct Point<T> : IAdditionOperators<Point<T>, Point<T>, Point<T>>
    //     where T : INumber<T>
    // {
    //     public T X { get; set; }
    //     public T Y { get; set; }
    //
    //     public static Point<T> operator +(Point<T> left, Point<T> right)
    //     {
    //         return new Point<T> { X = left.X + right.X, Y = left.Y + right.Y };
    //     }
    // }
    //
    // public static class GenericMathExample
    // {
    //     public static T MidPoint<T>(T left, T right) where T : INumber<T>
    //     {
    //         return (left + right) / T.CreateChecked(2);
    //     }
    // }

    // 3. Numeric IntPtr and UIntPtr
    // https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-11#numeric-intptr-and-uintptr
    [Test]
    public void NumericIntPtrTest()
    {
        Assert.AreEqual(typeof(IntPtr), typeof(nint));
        Assert.AreEqual(typeof(UIntPtr), typeof(nuint));
    }

    // 4. Newlines in string interpolations
    // https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-11#newlines-in-string-interpolations
    public class NewlinesInStringInterpolation
    {
        public string Example(string name)
        {
            return $"Hello, {name
                .ToUpper()
                .Substring(0, 2)}!";
        }
    }

    [Test]
    public void NewlinesInStringInterpolationTest()
    {
        var instance = new NewlinesInStringInterpolation();
        Assert.AreEqual("Hello, JO!", instance.Example("John"));
    }

    // 5. List patterns
    // https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-11#list-patterns
    public class ListPatternMatching
    {
        public bool CheckListPattern(int[] numbers) => numbers is [1, 2, 3];
    }

    [Test]
    public void ListPatternMatchingTest()
    {
        var instance = new ListPatternMatching();
        Assert.IsTrue(instance.CheckListPattern(new int[] { 1, 2, 3 }));
        Assert.IsFalse(instance.CheckListPattern(new int[] { 1, 2 }));
    }

    // 6. Improved method group conversion to delegate
    // https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-11#improved-method-group-conversion-to-delegate
    class CallFunction
    {
        public static void F() { }
        static void CallFunc(Action f) => f();

        public static void Test()
        {
            // C# 10
            // IL_0001: ldnull
            // IL_0002: ldftn        void CSharp10Features.CallFunction::F()
            // IL_0008: newobj       instance void [netstandard]System.Action::.ctor(object, native int)
            // IL_000d: call         void CSharp10Features.CallFunction::CallFunc(class [netstandard]System.Action)
                
            // C# 11
            // IL_0001: ldsfld       class [netstandard]System.Action CSharp11Features.CallFunction/'<>O'::'<0>__F'
            // IL_0006: dup
            // IL_0007: brtrue.s     IL_001c
            // IL_0009: pop
            // IL_000a: ldnull
            // IL_000b: ldftn        void CSharp11Features.CallFunction::F()
            // IL_0011: newobj       instance void [netstandard]System.Action::.ctor(object, native int)
            // IL_0016: dup
            // IL_0017: stsfld       class [netstandard]System.Action CSharp11Features.CallFunction/'<>O'::'<0>__F'
            // IL_001c: call         void CSharp11Features.CallFunction::CallFunc(class [netstandard]System.Action)

            CallFunc(F);
        }
    }

    [Test]
    public void CallFunctionTest()
    {
        Assert.DoesNotThrow(CallFunction.Test);
    }

    // 7. Raw string literals
    // https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-11#raw-string-literals
    public class RawStringLiterals
    {
        public string Example => """
                                 This is a raw string literal
                                 which can span multiple lines
                                 and preserve white spaces.
                                 """;
    }

    [Test]
    public void RawStringLiteralsTest()
    {
        var instance = new RawStringLiterals();
        Assert.AreEqual("This is a raw string literal\nwhich can span multiple lines\nand preserve white spaces.", instance.Example);
    }

    // 8. Auto-default struct
    // https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-11#auto-default-structs
    public struct AutoDefaultStruct
    {
        public int X { get; set; }
        public int Y { get; set; }

        public AutoDefaultStruct()
        {
            X = 0;
        }
    }

    [Test]
    public void AutoDefaultStructTest()
    {
        var instance = new AutoDefaultStruct();
        Assert.AreEqual(0, instance.X);
        Assert.AreEqual(0, instance.Y);
    }

    // 9. Pattern match Span<char> or ReadOnlySpan<char> on a constant string
    // https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-11#pattern-match-spanchar-or-readonlyspanchar-on-a-constant-string
    public class SpanPatternMatching
    {
        public bool IsHello(ReadOnlySpan<char> span) => span is "Hello";
    }

    [Test]
    public void SpanPatternMatchingTest()
    {
        var instance = new SpanPatternMatching();
        Assert.IsTrue(instance.IsHello("Hello".AsSpan()));
        Assert.IsFalse(instance.IsHello("World".AsSpan()));
    }

    // 10. Extended nameof scope
    // https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-11#extended-nameof-scope
    public class MyAttribute : Attribute
    {
        public MyAttribute(string name) { }
    }
    
    [MyAttribute(nameof(parameter))] void M(int parameter) { }
    [MyAttribute(nameof(TParameter))] void M<TParameter>() { }
    void M(int parameter, [MyAttribute(nameof(parameter))] int other) { }
    
    [Test]
    public void ExtendedNameOfScopeTest()
    {
        Assert.DoesNotThrow(() => M(42));
        Assert.DoesNotThrow(() => M<int>());
        Assert.DoesNotThrow(() => M(42, 84));
    }

    // 11. UTF-8 string literals
    // https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-11#utf-8-string-literals
    public class Utf8StringLiterals
    {
        public ReadOnlySpan<byte> Example => "This is a UTF-8 string literal"u8;
    }

    [Test]
    public void Utf8StringLiteralsTest()
    {
        var instance = new Utf8StringLiterals();
        Assert.AreEqual("This is a UTF-8 string literal", System.Text.Encoding.UTF8.GetString(instance.Example));
    }

    // 12. Required members
    // https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-11#required-members
    // public class Person
    // {
    //     public required string FirstName { get; set; }
    //     public required string LastName { get; set; }
    // }

    // 13. ref fields and scoped ref variables
    // https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-11#ref-fields-and-scoped-ref
    // public ref struct RefStruct
    // {
    //     public ref int Value;
    // }

    // public class RefFieldsExample
    // {
    //     private int _value = 42;
    //     public void ShowRefField()
    //     {
    //         RefStruct refStruct = new RefStruct { Value = ref _value };
    //         Console.WriteLine(ref refStruct.Value);
    //     }
    // }

    // 14. File local types
    // https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-11#file-local-types
    // file class FileScopedClass
    // {
    //     public void PrintMessage() => Console.WriteLine("This class is file-scoped.");
    // }
}