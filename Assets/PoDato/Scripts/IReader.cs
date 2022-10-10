using System;
using System.Collections.Generic;
using UnityEngine;

namespace PoDato {

	public interface IReader {

		// IReadable
		bool OptionalObject<T>(string name, ref T value) where T : IReadable, new();
		bool OptionalObjectList<T, U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadable, new();
		bool OptionalObjectMap<T, U>(string name, ref T value) where T : IDictionary<string, U>, new() where U : IReadable, new();
		void RequiredObject<T>(string name, ref T value) where T : IReadable, new();
		void RequiredObjectList<T, U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadable, new();
		void RequiredObjectMap<T, U>(string name, ref T value) where T : IDictionary<string, U>, new() where U : IReadable, new();

		// Enum
		bool OptionalEnum<T>(string name, ref T value) where T : struct, Enum;
		bool OptionalEnumList<T,U>(string name, ref T value) where T : ICollection<U>, new() where U : struct, Enum;
		bool OptionalEnumMap<T,U>(string name, ref T value) where T : IDictionary<string, U>, new() where U : struct, Enum;
		void RequiredEnum<T>(string name, ref T value) where T : struct, Enum;
		void RequiredEnumList<T,U>(string name, ref T value) where T : ICollection<U>, new() where U : struct, Enum;
		void RequiredEnumMap<T,U>(string name, ref T value) where T : IDictionary<string, U>, new() where U : struct, Enum;

		// string
		bool OptionalString(string name, ref string value);
		bool OptionalStringList<T>(string name, ref T value) where T : ICollection<string>, new();
		bool OptionalStringMap<T>(string name, ref T value) where T : IDictionary<string, string>, new();
		bool OptionalStringProxy<T>(string name, ref T value) where T : IReadProxy<string>, new();
		bool OptionalStringProxyList<T,U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<string>, new();
		bool OptionalStringProxyMap<T,U>(string name, ref T value) where T : IDictionary<string, U>, new() where U : IReadProxy<string>, new();
		void RequiredString(string name, ref string value);
		void RequiredStringList<T>(string name, ref T value) where T : ICollection<string>, new();
		void RequiredStringMap<T>(string name, ref T value) where T : IDictionary<string, string>, new();
		void RequiredStringProxy<T>(string name, ref T value) where T : IReadProxy<string>, new();
		void RequiredStringProxyList<T,U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<string>, new();
		void RequiredStringProxyMap<T,U>(string name, ref T value) where T : IDictionary<string, U>, new() where U : IReadProxy<string>, new();

		// int
		bool OptionalInt32(string name, ref int value);
		bool OptionalInt32List<T>(string name, ref T value) where T : ICollection<int>, new();
		bool OptionalInt32Map<T>(string name, ref T value) where T : IDictionary<string, int>, new();
		bool OptionalInt32Proxy<T>(string name, ref T value) where T : IReadProxy<int>, new();
		bool OptionalInt32ProxyList<T,U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<int>, new();
		bool OptionalInt32ProxyMap<T,U>(string name, ref T value) where T : IDictionary<string, U>, new() where U : IReadProxy<int>, new();
		void RequiredInt32(string name, ref int value);
		void RequiredInt32List<T>(string name, ref T value) where T : ICollection<int>, new();
		void RequiredInt32Map<T>(string name, ref T value) where T : IDictionary<string, int>, new();
		void RequiredInt32Proxy<T>(string name, ref T value) where T : IReadProxy<int>, new();
		void RequiredInt32ProxyList<T,U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<int>, new();
		void RequiredInt32ProxyMap<T,U>(string name, ref T value) where T : IDictionary<string, U>, new() where U : IReadProxy<int>, new();

		// bool
		bool OptionalBool(string name, ref bool value);
		bool OptionalBoolList<T>(string name, ref T value) where T : ICollection<bool>, new();
		bool OptionalBoolMap<T>(string name, ref T value) where T : IDictionary<string, bool>, new();
		bool OptionalBoolProxy<T>(string name, ref T value) where T : IReadProxy<bool>, new();
		bool OptionalBoolProxyList<T,U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<bool>, new();
		bool OptionalBoolProxyMap<T,U>(string name, ref T value) where T : IDictionary<string, U>, new() where U : IReadProxy<bool>, new();
		void RequiredBool(string name, ref bool value);
		void RequiredBoolList<T>(string name, ref T value) where T : ICollection<bool>, new();
		void RequiredBoolMap<T>(string name, ref T value) where T : IDictionary<string, bool>, new();
		void RequiredBoolProxy<T>(string name, ref T value) where T : IReadProxy<bool>, new();
		void RequiredBoolProxyList<T,U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<bool>, new();
		void RequiredBoolProxyMap<T,U>(string name, ref T value) where T : IDictionary<string, U>, new() where U : IReadProxy<bool>, new();

	}

}