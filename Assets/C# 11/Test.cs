using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace MyNamespace;

class GenericAttribute<T> : Attribute { }

[GenericAttribute<Foo>]
interface Foo { }

public interface IParseable<TSelf>
    where TSelf : IParseable<TSelf>
{
    static abstract TSelf Parse(string s, IFormatProvider? provider);
    static abstract bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, out TSelf result);
}