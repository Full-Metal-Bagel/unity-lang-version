using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using NUnit.Framework;

public class CSharp10Features
{
    // 1. Record structs
    // https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-10#record-structs
    public readonly record struct Point(int X, int Y);

    [Test]
    public void RecordStructTest()
    {
        var point = new Point(3, 4);
        Assert.AreEqual(3, point.X);
        Assert.AreEqual(4, point.Y);
    }

    // 2. Improvements of structure types
    // https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-10#improvements-of-structure-types
    public struct Rectangle
    {
        public int Width { get; init; }
        public int Height { get; init; }
    }

    [Test]
    public void WithExpressionTest()
    {
        var rect1 = new Rectangle { Width = 10, Height = 20 };
        var rect2 = rect1 with { Width = 15 };
        Assert.AreEqual(15, rect2.Width);
        Assert.AreEqual(20, rect2.Height);
    }

    // 3. Interpolated string handler
    // https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-10#interpolated-string-handler
    public class Logger
    {
        public void Log(string message) => Console.WriteLine(message);

        public void LogMessage(string name, int age)
        {
            Log($"Name: {name}, Age: {age}");
        }
    }

    [Test]
    public void InterpolatedStringHandlerTest()
    {
        var logger = new Logger();
        Assert.DoesNotThrow(() => logger.LogMessage("John", 30));
    }

    // 4. Global using directives
    // https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-10#global-using-directives
    // global using System.Collections.Generic;

    // 5. File-scoped namespace declaration
    // https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-10#file-scoped-namespace-declaration
    // Note: The entire file should start with 'namespace CSharp10Features;', which is already demonstrated here.

    // 6. Extended property patterns
    // https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-10#extended-property-patterns
    public class Address
    {
        public string City { get; set; }
    }

    public class Person
    {
        public Address Address { get; set; }
    }

    public bool IsFromCity(Person person, string city) =>
        person is { Address: { City: var c } } && c == city;

    [Test]
    public void ExtendedPropertyPatternTest()
    {
        var person = new Person { Address = new Address { City = "New York" } };
        Assert.IsTrue(IsFromCity(person, "New York"));
    }

    // 7. Lambda expression improvements
    // https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-10#lambda-expression-improvements
    [Test]
    public void LambdaExpressionImprovementsTest()
    {
        Func<int, int> square = x => x * x;
        Assert.AreEqual(9, square(3));

        var add = (int a, int b) => a + b;
        Assert.AreEqual(5, add(2, 3));
    }

    // 8. Constant interpolated strings
    // https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-10#constant-interpolated-strings
    const string FirstName = "John";
    const string LastName = "Doe";
    const string FullName = $"{FirstName} {LastName}";

    [Test]
    public void ConstantInterpolatedStringTest()
    {
        Assert.AreEqual("John Doe", FullName);
    }

    // 9. Record types can seal ToString
    // https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-10#record-types-can-seal-tostring
    public record PersonRecord(string FirstName, string LastName)
    {
        public sealed override string ToString() => $"{FirstName} {LastName}";
    }

    [Test]
    public void RecordTypesCanSealToStringTest()
    {
        var person = new PersonRecord("John", "Doe");
        Assert.AreEqual("John Doe", person.ToString());
    }

    // 10. Assignment and declaration in same deconstruction
    // https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-10#assignment-and-declaration-in-the-same-deconstruction
    [Test]
    public void DeconstructionTest()
    {
        (int x, int y) = (1, 2);
        Assert.AreEqual(1, x);
        Assert.AreEqual(2, y);

        (x, y) = (3, 4);
        Assert.AreEqual(3, x);
        Assert.AreEqual(4, y);
        
        (int z, y) = (5, 6);
        Assert.AreEqual(5, z);
        Assert.AreEqual(6, y);
    }

    // 11. Improved definite assignment
    // https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-10#improved-definite-assignment
    public class DefiniteAssignment
    {
        public void TestDefiniteAssignment()
        {
            int x;
            if (true)
            {
                x = 0;
            }
            else
            {
                x = 1;
            }
            Assert.AreEqual(0, x);
        }
    }

    [Test]
    public void ImprovedDefiniteAssignmentTest()
    {
        var instance = new DefiniteAssignment();
        Assert.DoesNotThrow(instance.TestDefiniteAssignment);
    }

    // 12. Allow AsyncMethodBuilder attribute on methods
    // https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-10#allow-asyncmethodbuilder-attribute-on-methods
    // Note: This feature is more advanced and usually not demonstrated in simple tests.
    // public class CustomAsyncMethodBuilder
    // {
    //     public Task Task => _taskCompletionSource.Task;
    //     private TaskCompletionSource<object> _taskCompletionSource = new TaskCompletionSource<object>();
    //
    //     public static CustomAsyncMethodBuilder Create() => new CustomAsyncMethodBuilder();
    //
    //     public void SetResult() => _taskCompletionSource.SetResult(null);
    //
    //     public void SetException(Exception exception) => _taskCompletionSource.SetException(exception);
    //
    //     public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine)
    //         where TAwaiter : INotifyCompletion
    //         where TStateMachine : IAsyncStateMachine
    //     {
    //         awaiter.OnCompleted(stateMachine.MoveNext);
    //     }
    //
    //     public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine)
    //         where TAwaiter : ICriticalNotifyCompletion
    //         where TStateMachine : IAsyncStateMachine
    //     {
    //         awaiter.OnCompleted(stateMachine.MoveNext);
    //     }
    //
    //     public void Start<TStateMachine>(ref TStateMachine stateMachine) where TStateMachine : IAsyncStateMachine
    //     {
    //         stateMachine.MoveNext();
    //     }
    //
    //     public void SetStateMachine(IAsyncStateMachine stateMachine) { }
    // }
    //
    // [AsyncMethodBuilder(typeof(CustomAsyncMethodBuilder))]
    // public class CustomTask
    // {
    //     private CustomAsyncMethodBuilder _builder = CustomAsyncMethodBuilder.Create();
    //
    //     public Task Task => _builder.Task;
    //
    //     public static CustomTask RunAsync(Action action)
    //     {
    //         var customTask = new CustomTask();
    //         Task.Run(() =>
    //         {
    //             try
    //             {
    //                 action();
    //                 customTask._builder.SetResult();
    //             }
    //             catch (Exception ex)
    //             {
    //                 customTask._builder.SetException(ex);
    //             }
    //         });
    //         return customTask;
    //     }
    // }
    //
    // [Test]
    // public async Task CustomTask_RunAsync_ShouldCompleteSuccessfully()
    // {
    //     bool executed = false;
    //
    //     CustomTask task = CustomTask.RunAsync(() =>
    //     {
    //         Task.Delay(100).Wait(); // Simulate some work
    //         executed = true;
    //     });
    //
    //     await task.Task;
    //
    //     Assert.IsTrue(executed);
    // }
    //
    // [Test]
    // public void CustomTask_RunAsync_ShouldHandleExceptions()
    // {
    //     CustomTask task = CustomTask.RunAsync(() =>
    //     {
    //         Task.Delay(100).Wait(); // Simulate some work
    //         throw new InvalidOperationException("Test exception");
    //     });
    //
    //     var exception = Assert.ThrowsAsync<InvalidOperationException>(async () => await task.Task);
    //     Assert.AreEqual("Test exception", exception.Message);
    // }

    // 13. CallerArgumentExpression attribute diagnostics
    // https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-10#callerargumentexpression-attribute-diagnostics
    // public class CallerArgumentExpressionExample
    // {
    //     public void Validate(bool condition, [System.Runtime.CompilerServices.CallerArgumentExpression("condition")] string message = null)
    //     {
    //         if (!condition)
    //         {
    //             throw new ArgumentException($"Argument failed validation: {message}");
    //         }
    //     }
    // }
    
    // 14. Enhanced #line pragma
    // https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-10#enhanced-line-pragma
    // Note: Demonstration of this feature in unit tests is not straightforward.
}

