using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

public class ReaderGenerator {

	private const string LINE_END = "\r\n";
	private static int m_indentLevel = 0;

	private class Definition {
		public string Name { get; }
		public string Type { get; }
		public Definition(string name, string type) {
			Name = name;
			Type = type;
		}
	}

	private static readonly List<Definition> m_primitives = new List<Definition>() {
		new Definition("String", "string"),
		new Definition("Int32", "int"),
		new Definition("Int16", "short"),
		new Definition("Int64", "long"),
		new Definition("UInt32", "uint"),
		new Definition("UInt16", "ushort"),
		new Definition("UInt64", "ulong"),
		new Definition("Single", "float"),
		new Definition("Double", "double"),
		new Definition("Byte", "byte"),
		new Definition("SByte", "sbyte"),
		new Definition("Bool", "bool")
	};
	private static readonly List<Definition> m_unity = new List<Definition>() {
		new Definition("Vector2", "Vector2"),
		new Definition("Vector3", "Vector3"),
		new Definition("Vector4", "Vector4"),
		new Definition("Quaternion", "Quaternion"),
		new Definition("Vector2Int", "Vector2Int"),
		new Definition("Vector3Int", "Vector3Int"),
		new Definition("Rect","Rect"),
		new Definition("RectInt", "RectInt"),
		new Definition("Color", "Color"),
		new Definition("Color32", "Color32")
	};
	private static readonly List<string> m_primitiveFunctions = new List<string>() {
		"bool Optional{0}(string name, ref {1} value)",
		"bool Optional{0}List<T>(string name, ref T value) where T : ICollection<{1}>, new()",
		"bool Optional{0}Array(string name, ref {1}[] value)",
		"bool Optional{0}Map<T>(string name, ref T value) where T : IDictionary<string,{1}>, new()",
		"bool Optional{0}Proxy<T>(string name, ref T value) where T : IReadProxy<{1}>, new()",
		"bool Optional{0}ProxyList<T,U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<{1}>, new()",
		"bool Optional{0}ProxyArray<T>(string name, ref T[] value) where T : IReadProxy<{1}>, new()",
		"bool Optional{0}ProxyMap<T,U>(string name, ref T value) where T : IDictionary<string,U>, new() where U : IReadProxy<{1}>, new()",
		"void Required{0}(string name, ref {1} value)",
		"void Required{0}List<T>(string name, ref T value) where T : ICollection<{1}>, new()",
		"void Required{0}Array(string name, ref {1}[] value)",
		"void Required{0}Map<T>(string name, ref T value) where T : IDictionary<string,{1}>, new()",
		"void Required{0}Proxy<T>(string name, ref T value) where T : IReadProxy<{1}>, new()",
		"void Required{0}ProxyList<T,U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<{1}>, new()",
		"void Required{0}ProxyArray<T>(string name, ref T[] value) where T : IReadProxy<{1}>, new()",
		"void Required{0}ProxyMap<T,U>(string name, ref T value) where T : IDictionary<string, U>, new() where U : IReadProxy<{1}>, new()"
	};
	private static readonly List<string> m_primitiveBodies = new List<string>() {
		"return DoOptional(name, ref value, TaterTo{0});",
		"return DoOptionalCollection(name, ref value, TaterTo{0});",
		"return DoOptionalArray(name, ref value, TaterTo{0});",
		"return DoOptionalDictionary(name, ref value, TaterTo{0});",
		"return DoOptional(name, ref value, TaterTo{0}Proxy<T>);",
		"return DoOptionalCollection(name, ref value, TaterTo{0}Proxy<U>);",
		"return DoOptionalArray(name, ref value, TaterTo{0}Proxy<T>);",
		"return DoOptionalDictionary(name, ref value, TaterTo{0}Proxy<U>);",
		"DoRequired(name, ref value, TaterTo{0});",
		"DoRequiredCollection(name, ref value, TaterTo{0});",
		"DoRequiredArray(name, ref value, TaterTo{0});",
		"DoRequiredDictionary(name, ref value, TaterTo{0});",
		"DoRequired(name, ref value, TaterTo{0}Proxy<T>);",
		"DoRequiredCollection(name, ref value, TaterTo{0}Proxy<U>);",
		"DoRequiredArray(name, ref value, TaterTo{0}Proxy<T>);",
		"DoRequiredDictionary(name, ref value, TaterTo{0}Proxy<U>);",
	};

	private static readonly List<string> m_unityFunctions = new List<string>() {
		"bool Optional{0}(string name, ref {1} value)",
		"bool Optional{0}List<T>(string name, ref T value) where T : ICollection<{1}>, new()",
		"bool Optional{0}Array(string name, ref {1}[] value)",
		"bool Optional{0}Map<T>(string name, ref T value) where T : IDictionary<string,{1}>, new()",
		"void Required{0}(string name, ref {1} value)",
		"void Required{0}List<T>(string name, ref T value) where T : ICollection<{1}>, new()",
		"void Required{0}Array(string name, ref {1}[] value)",
		"void Required{0}Map<T>(string name, ref T value) where T : IDictionary<string,{1}>, new()",
	};
	private static readonly List<string> m_unityBodies = new List<string>() {
		"return DoOptional(name, ref value, TaterTo{0});",
		"return DoOptionalCollection(name, ref value, TaterTo{0});",
		"return DoOptionalArray(name, ref value, TaterTo{0});",
		"return DoOptionalDictionary(name, ref value, TaterTo{0});",
		"DoRequired(name, ref value, TaterTo{0});",
		"DoRequiredCollection(name, ref value, TaterTo{0});",
		"DoRequiredArray(name, ref value, TaterTo{0});",
		"DoRequiredDictionary(name, ref value, TaterTo{0});",
	};

	private static readonly List<string> m_complex = new List<string>() {
		"bool OptionalObject<T>(string name, ref T value) where T : IReadable, new()",
		"bool OptionalObjectList<T, U>(string name, ref T value) where T : ICollection<U>, new () where U : IReadable, new ()",
		"bool OptionalObjectArray<T>(string name, ref T[] value) where T : IReadable, new()",
		"bool OptionalObjectMap<T, U>(string name, ref T value) where T : IDictionary<string, U>, new() where U : IReadable, new()",
		"void RequiredObject<T>(string name, ref T value) where T : IReadable, new()",
		"void RequiredObjectList<T, U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadable, new()",
		"void RequiredObjectMap<T, U>(string name, ref T value) where T : IDictionary<string, U>, new() where U : IReadable, new()",
		"void RequiredObjectArray<T>(string name, ref T[] value) where T : IReadable, new()",
		"bool OptionalEnum<T>(string name, ref T value) where T : struct, Enum",
		"bool OptionalEnumList<T, U>(string name, ref T value) where T : ICollection<U>, new() where U : struct, Enum",
		"bool OptionalEnumArray<T>(string name, ref T[] value) where T : struct, Enum",
		"bool OptionalEnumMap<T, U>(string name, ref T value) where T : IDictionary<string, U>, new() where U : struct, Enum",
		"void RequiredEnum<T>(string name, ref T value) where T : struct, Enum",
		"void RequiredEnumList<T, U>(string name, ref T value) where T : ICollection<U>, new() where U : struct, Enum",
		"void RequiredEnumArray<T>(string name, ref T[] value) where T : struct, Enum",
		"void RequiredEnumMap<T, U>(string name, ref T value) where T : IDictionary<string, U>, new() where U : struct, Enum"
	};

	[MenuItem("Tools/Generate Reader")]
	private static void Run() {

		StringBuilder builder = new StringBuilder();

		// IReader.cs

		AppendHeader(builder);
		AppendUsing(builder, "System");
		AppendUsing(builder, "System.Collections.Generic");
		AppendUsing(builder, "UnityEngine");
		builder.Append(LINE_END);
		builder.Append("namespace PoDato {").Append(LINE_END);
		IncreaseIndent();
		AppendIndent(builder);
		builder.Append("public interface IReader {").Append(LINE_END);
		IncreaseIndent();
		foreach (string complex in m_complex) {
			AppendIndent(builder);
			builder.Append(complex).Append(";").Append(LINE_END);
		}
		foreach (Definition definition in m_primitives) {
			AppendInterfaceFunctions(builder, definition, m_primitiveFunctions);
		}
		foreach (Definition definition in m_unity) {
			AppendInterfaceFunctions(builder, definition, m_unityFunctions);
		}
		DecreaseIndent();
		AppendIndent(builder);
		builder.Append('}').Append(LINE_END);
		DecreaseIndent();
		builder.Append('}').Append(LINE_END);

		File.WriteAllText(string.Concat(Application.dataPath, "/PoDato/Scripts/IReader.cs"), builder.ToString());

		// Reader.Generated.cs
		
		builder.Clear();
		AppendHeader(builder);
		AppendUsing(builder, "System");
		AppendUsing(builder, "System.Collections.Generic");
		AppendUsing(builder, "UnityEngine");
		builder.Append(LINE_END);
		builder.Append("namespace PoDato {").Append(LINE_END);
		IncreaseIndent();
		AppendIndent(builder);
		builder.Append("internal sealed partial class Reader : IReader {").Append(LINE_END);
		IncreaseIndent();
		foreach (Definition definition in m_primitives) {
			AppendImplementationFunctions(builder, definition, m_primitiveFunctions, m_primitiveBodies);
		}
		foreach (Definition definition in m_unity) {
			AppendImplementationFunctions(builder, definition, m_unityFunctions, m_unityBodies);
		}
		DecreaseIndent();
		AppendIndent(builder);
		builder.Append('}').Append(LINE_END);
		DecreaseIndent();
		builder.Append('}').Append(LINE_END);

		File.WriteAllText(string.Concat(Application.dataPath, "/PoDato/Scripts/Internal/Serializer/Reader.Generated.cs"), builder.ToString());
	}

	private static void IncreaseIndent() {
		m_indentLevel++;
	}
	private static void DecreaseIndent() {
		m_indentLevel--;
	}
	private static void AppendIndent(StringBuilder builder) {
		int value = m_indentLevel;
		while (value-- > 0) {
			builder.Append('\t');
		}
	}
	private static void AppendHeader(StringBuilder builder) {
		builder.Append("// Generated from ReaderGenerator.cs").Append(LINE_END);
	}
	private static void AppendUsing(StringBuilder builder, string name) {
		builder.Append("using ").Append(name).Append(';').Append(LINE_END);
	}

	private static void AppendInterfaceFunctions(StringBuilder builder, Definition definition, List<string> functions) {
		string NAME = definition.Name;
		string TYPE = definition.Type;

		// comment header
		AppendIndent(builder);
		builder.Append("// ").Append(TYPE).Append(LINE_END);
		// functions
		foreach (string function in functions) {
			AppendIndent(builder);
			builder.Append(string.Format(function, NAME, TYPE)).Append(";");
			builder.Append(LINE_END);
		}
	}

	private static void AppendImplementationFunctions(StringBuilder builder, Definition definition, List<string> functions, List<string> bodies) {

		for (int ix = 0; ix < functions.Count; ix++) {
			AppendIndent(builder);
			builder.Append("public ");
			builder.Append(string.Format(functions[ix], definition.Name, definition.Type));
			builder.Append(" {").Append(LINE_END);
			IncreaseIndent();
			AppendIndent(builder);
			builder.Append(string.Format(bodies[ix], definition.Name, definition.Type)).Append(LINE_END);
			DecreaseIndent();
			AppendIndent(builder);
			builder.Append("}").Append(LINE_END);
		}
	}

	
}
