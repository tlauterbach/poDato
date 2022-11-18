// Generated from ReaderGenerator.cs
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PoDato {
	internal sealed partial class Reader : IReader {
		public bool OptionalString(string name, ref string value) {
			return DoOptional(name, ref value, TaterToString);
		}
		public bool OptionalStringList<T>(string name, ref T value) where T : ICollection<string>, new() {
			return DoOptionalCollection(name, ref value, TaterToString);
		}
		public bool OptionalStringReadOnlyList(string name, ref IReadOnlyList<string> value) {
			return DoOptionalReadOnlyList(name, ref value, TaterToString);
		}
		public bool OptionalStringArray(string name, ref string[] value) {
			return DoOptionalArray(name, ref value, TaterToString);
		}
		public bool OptionalStringMap<T>(string name, ref T value) where T : IDictionary<string,string>, new() {
			return DoOptionalDictionary(name, ref value, TaterToString);
		}
		public bool OptionalStringProxy<T>(string name, ref T value) where T : IReadProxy<string>, new() {
			return DoOptional(name, ref value, TaterToStringProxy<T>);
		}
		public bool OptionalStringProxyList<T,U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<string>, new() {
			return DoOptionalCollection(name, ref value, TaterToStringProxy<U>);
		}
		public bool OptionalStringProxyReadOnlyList<T>(string name, ref IReadOnlyList<T> value) where T : IReadProxy<string>, new() {
			return DoOptionalReadOnlyList(name, ref value, TaterToStringProxy<T>);
		}
		public bool OptionalStringProxyArray<T>(string name, ref T[] value) where T : IReadProxy<string>, new() {
			return DoOptionalArray(name, ref value, TaterToStringProxy<T>);
		}
		public bool OptionalStringProxyMap<T,U>(string name, ref T value) where T : IDictionary<string,U>, new() where U : IReadProxy<string>, new() {
			return DoOptionalDictionary(name, ref value, TaterToStringProxy<U>);
		}
		public void RequiredString(string name, ref string value) {
			DoRequired(name, ref value, TaterToString);
		}
		public void RequiredStringList<T>(string name, ref T value) where T : ICollection<string>, new() {
			DoRequiredCollection(name, ref value, TaterToString);
		}
		public void RequiredStringReadOnlyList(string name, ref IReadOnlyList<string> value) {
			DoRequiredReadOnlyList(name, ref value, TaterToString);
		}
		public void RequiredStringArray(string name, ref string[] value) {
			DoRequiredArray(name, ref value, TaterToString);
		}
		public void RequiredStringMap<T>(string name, ref T value) where T : IDictionary<string,string>, new() {
			DoRequiredDictionary(name, ref value, TaterToString);
		}
		public void RequiredStringProxy<T>(string name, ref T value) where T : IReadProxy<string>, new() {
			DoRequired(name, ref value, TaterToStringProxy<T>);
		}
		public void RequiredStringProxyList<T,U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<string>, new() {
			DoRequiredCollection(name, ref value, TaterToStringProxy<U>);
		}
		public void RequiredStringProxyReadOnlyList<T>(string name, ref IReadOnlyList<T> value) where T : IReadProxy<string>, new() {
			DoRequiredReadOnlyList(name, ref value, TaterToStringProxy<T>);
		}
		public void RequiredStringProxyArray<T>(string name, ref T[] value) where T : IReadProxy<string>, new() {
			DoRequiredArray(name, ref value, TaterToStringProxy<T>);
		}
		public void RequiredStringProxyMap<T,U>(string name, ref T value) where T : IDictionary<string, U>, new() where U : IReadProxy<string>, new() {
			DoRequiredDictionary(name, ref value, TaterToStringProxy<U>);
		}
		public bool OptionalInt32(string name, ref int value) {
			return DoOptional(name, ref value, TaterToInt32);
		}
		public bool OptionalInt32List<T>(string name, ref T value) where T : ICollection<int>, new() {
			return DoOptionalCollection(name, ref value, TaterToInt32);
		}
		public bool OptionalInt32ReadOnlyList(string name, ref IReadOnlyList<int> value) {
			return DoOptionalReadOnlyList(name, ref value, TaterToInt32);
		}
		public bool OptionalInt32Array(string name, ref int[] value) {
			return DoOptionalArray(name, ref value, TaterToInt32);
		}
		public bool OptionalInt32Map<T>(string name, ref T value) where T : IDictionary<string,int>, new() {
			return DoOptionalDictionary(name, ref value, TaterToInt32);
		}
		public bool OptionalInt32Proxy<T>(string name, ref T value) where T : IReadProxy<int>, new() {
			return DoOptional(name, ref value, TaterToInt32Proxy<T>);
		}
		public bool OptionalInt32ProxyList<T,U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<int>, new() {
			return DoOptionalCollection(name, ref value, TaterToInt32Proxy<U>);
		}
		public bool OptionalInt32ProxyReadOnlyList<T>(string name, ref IReadOnlyList<T> value) where T : IReadProxy<int>, new() {
			return DoOptionalReadOnlyList(name, ref value, TaterToInt32Proxy<T>);
		}
		public bool OptionalInt32ProxyArray<T>(string name, ref T[] value) where T : IReadProxy<int>, new() {
			return DoOptionalArray(name, ref value, TaterToInt32Proxy<T>);
		}
		public bool OptionalInt32ProxyMap<T,U>(string name, ref T value) where T : IDictionary<string,U>, new() where U : IReadProxy<int>, new() {
			return DoOptionalDictionary(name, ref value, TaterToInt32Proxy<U>);
		}
		public void RequiredInt32(string name, ref int value) {
			DoRequired(name, ref value, TaterToInt32);
		}
		public void RequiredInt32List<T>(string name, ref T value) where T : ICollection<int>, new() {
			DoRequiredCollection(name, ref value, TaterToInt32);
		}
		public void RequiredInt32ReadOnlyList(string name, ref IReadOnlyList<int> value) {
			DoRequiredReadOnlyList(name, ref value, TaterToInt32);
		}
		public void RequiredInt32Array(string name, ref int[] value) {
			DoRequiredArray(name, ref value, TaterToInt32);
		}
		public void RequiredInt32Map<T>(string name, ref T value) where T : IDictionary<string,int>, new() {
			DoRequiredDictionary(name, ref value, TaterToInt32);
		}
		public void RequiredInt32Proxy<T>(string name, ref T value) where T : IReadProxy<int>, new() {
			DoRequired(name, ref value, TaterToInt32Proxy<T>);
		}
		public void RequiredInt32ProxyList<T,U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<int>, new() {
			DoRequiredCollection(name, ref value, TaterToInt32Proxy<U>);
		}
		public void RequiredInt32ProxyReadOnlyList<T>(string name, ref IReadOnlyList<T> value) where T : IReadProxy<int>, new() {
			DoRequiredReadOnlyList(name, ref value, TaterToInt32Proxy<T>);
		}
		public void RequiredInt32ProxyArray<T>(string name, ref T[] value) where T : IReadProxy<int>, new() {
			DoRequiredArray(name, ref value, TaterToInt32Proxy<T>);
		}
		public void RequiredInt32ProxyMap<T,U>(string name, ref T value) where T : IDictionary<string, U>, new() where U : IReadProxy<int>, new() {
			DoRequiredDictionary(name, ref value, TaterToInt32Proxy<U>);
		}
		public bool OptionalInt16(string name, ref short value) {
			return DoOptional(name, ref value, TaterToInt16);
		}
		public bool OptionalInt16List<T>(string name, ref T value) where T : ICollection<short>, new() {
			return DoOptionalCollection(name, ref value, TaterToInt16);
		}
		public bool OptionalInt16ReadOnlyList(string name, ref IReadOnlyList<short> value) {
			return DoOptionalReadOnlyList(name, ref value, TaterToInt16);
		}
		public bool OptionalInt16Array(string name, ref short[] value) {
			return DoOptionalArray(name, ref value, TaterToInt16);
		}
		public bool OptionalInt16Map<T>(string name, ref T value) where T : IDictionary<string,short>, new() {
			return DoOptionalDictionary(name, ref value, TaterToInt16);
		}
		public bool OptionalInt16Proxy<T>(string name, ref T value) where T : IReadProxy<short>, new() {
			return DoOptional(name, ref value, TaterToInt16Proxy<T>);
		}
		public bool OptionalInt16ProxyList<T,U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<short>, new() {
			return DoOptionalCollection(name, ref value, TaterToInt16Proxy<U>);
		}
		public bool OptionalInt16ProxyReadOnlyList<T>(string name, ref IReadOnlyList<T> value) where T : IReadProxy<short>, new() {
			return DoOptionalReadOnlyList(name, ref value, TaterToInt16Proxy<T>);
		}
		public bool OptionalInt16ProxyArray<T>(string name, ref T[] value) where T : IReadProxy<short>, new() {
			return DoOptionalArray(name, ref value, TaterToInt16Proxy<T>);
		}
		public bool OptionalInt16ProxyMap<T,U>(string name, ref T value) where T : IDictionary<string,U>, new() where U : IReadProxy<short>, new() {
			return DoOptionalDictionary(name, ref value, TaterToInt16Proxy<U>);
		}
		public void RequiredInt16(string name, ref short value) {
			DoRequired(name, ref value, TaterToInt16);
		}
		public void RequiredInt16List<T>(string name, ref T value) where T : ICollection<short>, new() {
			DoRequiredCollection(name, ref value, TaterToInt16);
		}
		public void RequiredInt16ReadOnlyList(string name, ref IReadOnlyList<short> value) {
			DoRequiredReadOnlyList(name, ref value, TaterToInt16);
		}
		public void RequiredInt16Array(string name, ref short[] value) {
			DoRequiredArray(name, ref value, TaterToInt16);
		}
		public void RequiredInt16Map<T>(string name, ref T value) where T : IDictionary<string,short>, new() {
			DoRequiredDictionary(name, ref value, TaterToInt16);
		}
		public void RequiredInt16Proxy<T>(string name, ref T value) where T : IReadProxy<short>, new() {
			DoRequired(name, ref value, TaterToInt16Proxy<T>);
		}
		public void RequiredInt16ProxyList<T,U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<short>, new() {
			DoRequiredCollection(name, ref value, TaterToInt16Proxy<U>);
		}
		public void RequiredInt16ProxyReadOnlyList<T>(string name, ref IReadOnlyList<T> value) where T : IReadProxy<short>, new() {
			DoRequiredReadOnlyList(name, ref value, TaterToInt16Proxy<T>);
		}
		public void RequiredInt16ProxyArray<T>(string name, ref T[] value) where T : IReadProxy<short>, new() {
			DoRequiredArray(name, ref value, TaterToInt16Proxy<T>);
		}
		public void RequiredInt16ProxyMap<T,U>(string name, ref T value) where T : IDictionary<string, U>, new() where U : IReadProxy<short>, new() {
			DoRequiredDictionary(name, ref value, TaterToInt16Proxy<U>);
		}
		public bool OptionalInt64(string name, ref long value) {
			return DoOptional(name, ref value, TaterToInt64);
		}
		public bool OptionalInt64List<T>(string name, ref T value) where T : ICollection<long>, new() {
			return DoOptionalCollection(name, ref value, TaterToInt64);
		}
		public bool OptionalInt64ReadOnlyList(string name, ref IReadOnlyList<long> value) {
			return DoOptionalReadOnlyList(name, ref value, TaterToInt64);
		}
		public bool OptionalInt64Array(string name, ref long[] value) {
			return DoOptionalArray(name, ref value, TaterToInt64);
		}
		public bool OptionalInt64Map<T>(string name, ref T value) where T : IDictionary<string,long>, new() {
			return DoOptionalDictionary(name, ref value, TaterToInt64);
		}
		public bool OptionalInt64Proxy<T>(string name, ref T value) where T : IReadProxy<long>, new() {
			return DoOptional(name, ref value, TaterToInt64Proxy<T>);
		}
		public bool OptionalInt64ProxyList<T,U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<long>, new() {
			return DoOptionalCollection(name, ref value, TaterToInt64Proxy<U>);
		}
		public bool OptionalInt64ProxyReadOnlyList<T>(string name, ref IReadOnlyList<T> value) where T : IReadProxy<long>, new() {
			return DoOptionalReadOnlyList(name, ref value, TaterToInt64Proxy<T>);
		}
		public bool OptionalInt64ProxyArray<T>(string name, ref T[] value) where T : IReadProxy<long>, new() {
			return DoOptionalArray(name, ref value, TaterToInt64Proxy<T>);
		}
		public bool OptionalInt64ProxyMap<T,U>(string name, ref T value) where T : IDictionary<string,U>, new() where U : IReadProxy<long>, new() {
			return DoOptionalDictionary(name, ref value, TaterToInt64Proxy<U>);
		}
		public void RequiredInt64(string name, ref long value) {
			DoRequired(name, ref value, TaterToInt64);
		}
		public void RequiredInt64List<T>(string name, ref T value) where T : ICollection<long>, new() {
			DoRequiredCollection(name, ref value, TaterToInt64);
		}
		public void RequiredInt64ReadOnlyList(string name, ref IReadOnlyList<long> value) {
			DoRequiredReadOnlyList(name, ref value, TaterToInt64);
		}
		public void RequiredInt64Array(string name, ref long[] value) {
			DoRequiredArray(name, ref value, TaterToInt64);
		}
		public void RequiredInt64Map<T>(string name, ref T value) where T : IDictionary<string,long>, new() {
			DoRequiredDictionary(name, ref value, TaterToInt64);
		}
		public void RequiredInt64Proxy<T>(string name, ref T value) where T : IReadProxy<long>, new() {
			DoRequired(name, ref value, TaterToInt64Proxy<T>);
		}
		public void RequiredInt64ProxyList<T,U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<long>, new() {
			DoRequiredCollection(name, ref value, TaterToInt64Proxy<U>);
		}
		public void RequiredInt64ProxyReadOnlyList<T>(string name, ref IReadOnlyList<T> value) where T : IReadProxy<long>, new() {
			DoRequiredReadOnlyList(name, ref value, TaterToInt64Proxy<T>);
		}
		public void RequiredInt64ProxyArray<T>(string name, ref T[] value) where T : IReadProxy<long>, new() {
			DoRequiredArray(name, ref value, TaterToInt64Proxy<T>);
		}
		public void RequiredInt64ProxyMap<T,U>(string name, ref T value) where T : IDictionary<string, U>, new() where U : IReadProxy<long>, new() {
			DoRequiredDictionary(name, ref value, TaterToInt64Proxy<U>);
		}
		public bool OptionalUInt32(string name, ref uint value) {
			return DoOptional(name, ref value, TaterToUInt32);
		}
		public bool OptionalUInt32List<T>(string name, ref T value) where T : ICollection<uint>, new() {
			return DoOptionalCollection(name, ref value, TaterToUInt32);
		}
		public bool OptionalUInt32ReadOnlyList(string name, ref IReadOnlyList<uint> value) {
			return DoOptionalReadOnlyList(name, ref value, TaterToUInt32);
		}
		public bool OptionalUInt32Array(string name, ref uint[] value) {
			return DoOptionalArray(name, ref value, TaterToUInt32);
		}
		public bool OptionalUInt32Map<T>(string name, ref T value) where T : IDictionary<string,uint>, new() {
			return DoOptionalDictionary(name, ref value, TaterToUInt32);
		}
		public bool OptionalUInt32Proxy<T>(string name, ref T value) where T : IReadProxy<uint>, new() {
			return DoOptional(name, ref value, TaterToUInt32Proxy<T>);
		}
		public bool OptionalUInt32ProxyList<T,U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<uint>, new() {
			return DoOptionalCollection(name, ref value, TaterToUInt32Proxy<U>);
		}
		public bool OptionalUInt32ProxyReadOnlyList<T>(string name, ref IReadOnlyList<T> value) where T : IReadProxy<uint>, new() {
			return DoOptionalReadOnlyList(name, ref value, TaterToUInt32Proxy<T>);
		}
		public bool OptionalUInt32ProxyArray<T>(string name, ref T[] value) where T : IReadProxy<uint>, new() {
			return DoOptionalArray(name, ref value, TaterToUInt32Proxy<T>);
		}
		public bool OptionalUInt32ProxyMap<T,U>(string name, ref T value) where T : IDictionary<string,U>, new() where U : IReadProxy<uint>, new() {
			return DoOptionalDictionary(name, ref value, TaterToUInt32Proxy<U>);
		}
		public void RequiredUInt32(string name, ref uint value) {
			DoRequired(name, ref value, TaterToUInt32);
		}
		public void RequiredUInt32List<T>(string name, ref T value) where T : ICollection<uint>, new() {
			DoRequiredCollection(name, ref value, TaterToUInt32);
		}
		public void RequiredUInt32ReadOnlyList(string name, ref IReadOnlyList<uint> value) {
			DoRequiredReadOnlyList(name, ref value, TaterToUInt32);
		}
		public void RequiredUInt32Array(string name, ref uint[] value) {
			DoRequiredArray(name, ref value, TaterToUInt32);
		}
		public void RequiredUInt32Map<T>(string name, ref T value) where T : IDictionary<string,uint>, new() {
			DoRequiredDictionary(name, ref value, TaterToUInt32);
		}
		public void RequiredUInt32Proxy<T>(string name, ref T value) where T : IReadProxy<uint>, new() {
			DoRequired(name, ref value, TaterToUInt32Proxy<T>);
		}
		public void RequiredUInt32ProxyList<T,U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<uint>, new() {
			DoRequiredCollection(name, ref value, TaterToUInt32Proxy<U>);
		}
		public void RequiredUInt32ProxyReadOnlyList<T>(string name, ref IReadOnlyList<T> value) where T : IReadProxy<uint>, new() {
			DoRequiredReadOnlyList(name, ref value, TaterToUInt32Proxy<T>);
		}
		public void RequiredUInt32ProxyArray<T>(string name, ref T[] value) where T : IReadProxy<uint>, new() {
			DoRequiredArray(name, ref value, TaterToUInt32Proxy<T>);
		}
		public void RequiredUInt32ProxyMap<T,U>(string name, ref T value) where T : IDictionary<string, U>, new() where U : IReadProxy<uint>, new() {
			DoRequiredDictionary(name, ref value, TaterToUInt32Proxy<U>);
		}
		public bool OptionalUInt16(string name, ref ushort value) {
			return DoOptional(name, ref value, TaterToUInt16);
		}
		public bool OptionalUInt16List<T>(string name, ref T value) where T : ICollection<ushort>, new() {
			return DoOptionalCollection(name, ref value, TaterToUInt16);
		}
		public bool OptionalUInt16ReadOnlyList(string name, ref IReadOnlyList<ushort> value) {
			return DoOptionalReadOnlyList(name, ref value, TaterToUInt16);
		}
		public bool OptionalUInt16Array(string name, ref ushort[] value) {
			return DoOptionalArray(name, ref value, TaterToUInt16);
		}
		public bool OptionalUInt16Map<T>(string name, ref T value) where T : IDictionary<string,ushort>, new() {
			return DoOptionalDictionary(name, ref value, TaterToUInt16);
		}
		public bool OptionalUInt16Proxy<T>(string name, ref T value) where T : IReadProxy<ushort>, new() {
			return DoOptional(name, ref value, TaterToUInt16Proxy<T>);
		}
		public bool OptionalUInt16ProxyList<T,U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<ushort>, new() {
			return DoOptionalCollection(name, ref value, TaterToUInt16Proxy<U>);
		}
		public bool OptionalUInt16ProxyReadOnlyList<T>(string name, ref IReadOnlyList<T> value) where T : IReadProxy<ushort>, new() {
			return DoOptionalReadOnlyList(name, ref value, TaterToUInt16Proxy<T>);
		}
		public bool OptionalUInt16ProxyArray<T>(string name, ref T[] value) where T : IReadProxy<ushort>, new() {
			return DoOptionalArray(name, ref value, TaterToUInt16Proxy<T>);
		}
		public bool OptionalUInt16ProxyMap<T,U>(string name, ref T value) where T : IDictionary<string,U>, new() where U : IReadProxy<ushort>, new() {
			return DoOptionalDictionary(name, ref value, TaterToUInt16Proxy<U>);
		}
		public void RequiredUInt16(string name, ref ushort value) {
			DoRequired(name, ref value, TaterToUInt16);
		}
		public void RequiredUInt16List<T>(string name, ref T value) where T : ICollection<ushort>, new() {
			DoRequiredCollection(name, ref value, TaterToUInt16);
		}
		public void RequiredUInt16ReadOnlyList(string name, ref IReadOnlyList<ushort> value) {
			DoRequiredReadOnlyList(name, ref value, TaterToUInt16);
		}
		public void RequiredUInt16Array(string name, ref ushort[] value) {
			DoRequiredArray(name, ref value, TaterToUInt16);
		}
		public void RequiredUInt16Map<T>(string name, ref T value) where T : IDictionary<string,ushort>, new() {
			DoRequiredDictionary(name, ref value, TaterToUInt16);
		}
		public void RequiredUInt16Proxy<T>(string name, ref T value) where T : IReadProxy<ushort>, new() {
			DoRequired(name, ref value, TaterToUInt16Proxy<T>);
		}
		public void RequiredUInt16ProxyList<T,U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<ushort>, new() {
			DoRequiredCollection(name, ref value, TaterToUInt16Proxy<U>);
		}
		public void RequiredUInt16ProxyReadOnlyList<T>(string name, ref IReadOnlyList<T> value) where T : IReadProxy<ushort>, new() {
			DoRequiredReadOnlyList(name, ref value, TaterToUInt16Proxy<T>);
		}
		public void RequiredUInt16ProxyArray<T>(string name, ref T[] value) where T : IReadProxy<ushort>, new() {
			DoRequiredArray(name, ref value, TaterToUInt16Proxy<T>);
		}
		public void RequiredUInt16ProxyMap<T,U>(string name, ref T value) where T : IDictionary<string, U>, new() where U : IReadProxy<ushort>, new() {
			DoRequiredDictionary(name, ref value, TaterToUInt16Proxy<U>);
		}
		public bool OptionalUInt64(string name, ref ulong value) {
			return DoOptional(name, ref value, TaterToUInt64);
		}
		public bool OptionalUInt64List<T>(string name, ref T value) where T : ICollection<ulong>, new() {
			return DoOptionalCollection(name, ref value, TaterToUInt64);
		}
		public bool OptionalUInt64ReadOnlyList(string name, ref IReadOnlyList<ulong> value) {
			return DoOptionalReadOnlyList(name, ref value, TaterToUInt64);
		}
		public bool OptionalUInt64Array(string name, ref ulong[] value) {
			return DoOptionalArray(name, ref value, TaterToUInt64);
		}
		public bool OptionalUInt64Map<T>(string name, ref T value) where T : IDictionary<string,ulong>, new() {
			return DoOptionalDictionary(name, ref value, TaterToUInt64);
		}
		public bool OptionalUInt64Proxy<T>(string name, ref T value) where T : IReadProxy<ulong>, new() {
			return DoOptional(name, ref value, TaterToUInt64Proxy<T>);
		}
		public bool OptionalUInt64ProxyList<T,U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<ulong>, new() {
			return DoOptionalCollection(name, ref value, TaterToUInt64Proxy<U>);
		}
		public bool OptionalUInt64ProxyReadOnlyList<T>(string name, ref IReadOnlyList<T> value) where T : IReadProxy<ulong>, new() {
			return DoOptionalReadOnlyList(name, ref value, TaterToUInt64Proxy<T>);
		}
		public bool OptionalUInt64ProxyArray<T>(string name, ref T[] value) where T : IReadProxy<ulong>, new() {
			return DoOptionalArray(name, ref value, TaterToUInt64Proxy<T>);
		}
		public bool OptionalUInt64ProxyMap<T,U>(string name, ref T value) where T : IDictionary<string,U>, new() where U : IReadProxy<ulong>, new() {
			return DoOptionalDictionary(name, ref value, TaterToUInt64Proxy<U>);
		}
		public void RequiredUInt64(string name, ref ulong value) {
			DoRequired(name, ref value, TaterToUInt64);
		}
		public void RequiredUInt64List<T>(string name, ref T value) where T : ICollection<ulong>, new() {
			DoRequiredCollection(name, ref value, TaterToUInt64);
		}
		public void RequiredUInt64ReadOnlyList(string name, ref IReadOnlyList<ulong> value) {
			DoRequiredReadOnlyList(name, ref value, TaterToUInt64);
		}
		public void RequiredUInt64Array(string name, ref ulong[] value) {
			DoRequiredArray(name, ref value, TaterToUInt64);
		}
		public void RequiredUInt64Map<T>(string name, ref T value) where T : IDictionary<string,ulong>, new() {
			DoRequiredDictionary(name, ref value, TaterToUInt64);
		}
		public void RequiredUInt64Proxy<T>(string name, ref T value) where T : IReadProxy<ulong>, new() {
			DoRequired(name, ref value, TaterToUInt64Proxy<T>);
		}
		public void RequiredUInt64ProxyList<T,U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<ulong>, new() {
			DoRequiredCollection(name, ref value, TaterToUInt64Proxy<U>);
		}
		public void RequiredUInt64ProxyReadOnlyList<T>(string name, ref IReadOnlyList<T> value) where T : IReadProxy<ulong>, new() {
			DoRequiredReadOnlyList(name, ref value, TaterToUInt64Proxy<T>);
		}
		public void RequiredUInt64ProxyArray<T>(string name, ref T[] value) where T : IReadProxy<ulong>, new() {
			DoRequiredArray(name, ref value, TaterToUInt64Proxy<T>);
		}
		public void RequiredUInt64ProxyMap<T,U>(string name, ref T value) where T : IDictionary<string, U>, new() where U : IReadProxy<ulong>, new() {
			DoRequiredDictionary(name, ref value, TaterToUInt64Proxy<U>);
		}
		public bool OptionalSingle(string name, ref float value) {
			return DoOptional(name, ref value, TaterToSingle);
		}
		public bool OptionalSingleList<T>(string name, ref T value) where T : ICollection<float>, new() {
			return DoOptionalCollection(name, ref value, TaterToSingle);
		}
		public bool OptionalSingleReadOnlyList(string name, ref IReadOnlyList<float> value) {
			return DoOptionalReadOnlyList(name, ref value, TaterToSingle);
		}
		public bool OptionalSingleArray(string name, ref float[] value) {
			return DoOptionalArray(name, ref value, TaterToSingle);
		}
		public bool OptionalSingleMap<T>(string name, ref T value) where T : IDictionary<string,float>, new() {
			return DoOptionalDictionary(name, ref value, TaterToSingle);
		}
		public bool OptionalSingleProxy<T>(string name, ref T value) where T : IReadProxy<float>, new() {
			return DoOptional(name, ref value, TaterToSingleProxy<T>);
		}
		public bool OptionalSingleProxyList<T,U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<float>, new() {
			return DoOptionalCollection(name, ref value, TaterToSingleProxy<U>);
		}
		public bool OptionalSingleProxyReadOnlyList<T>(string name, ref IReadOnlyList<T> value) where T : IReadProxy<float>, new() {
			return DoOptionalReadOnlyList(name, ref value, TaterToSingleProxy<T>);
		}
		public bool OptionalSingleProxyArray<T>(string name, ref T[] value) where T : IReadProxy<float>, new() {
			return DoOptionalArray(name, ref value, TaterToSingleProxy<T>);
		}
		public bool OptionalSingleProxyMap<T,U>(string name, ref T value) where T : IDictionary<string,U>, new() where U : IReadProxy<float>, new() {
			return DoOptionalDictionary(name, ref value, TaterToSingleProxy<U>);
		}
		public void RequiredSingle(string name, ref float value) {
			DoRequired(name, ref value, TaterToSingle);
		}
		public void RequiredSingleList<T>(string name, ref T value) where T : ICollection<float>, new() {
			DoRequiredCollection(name, ref value, TaterToSingle);
		}
		public void RequiredSingleReadOnlyList(string name, ref IReadOnlyList<float> value) {
			DoRequiredReadOnlyList(name, ref value, TaterToSingle);
		}
		public void RequiredSingleArray(string name, ref float[] value) {
			DoRequiredArray(name, ref value, TaterToSingle);
		}
		public void RequiredSingleMap<T>(string name, ref T value) where T : IDictionary<string,float>, new() {
			DoRequiredDictionary(name, ref value, TaterToSingle);
		}
		public void RequiredSingleProxy<T>(string name, ref T value) where T : IReadProxy<float>, new() {
			DoRequired(name, ref value, TaterToSingleProxy<T>);
		}
		public void RequiredSingleProxyList<T,U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<float>, new() {
			DoRequiredCollection(name, ref value, TaterToSingleProxy<U>);
		}
		public void RequiredSingleProxyReadOnlyList<T>(string name, ref IReadOnlyList<T> value) where T : IReadProxy<float>, new() {
			DoRequiredReadOnlyList(name, ref value, TaterToSingleProxy<T>);
		}
		public void RequiredSingleProxyArray<T>(string name, ref T[] value) where T : IReadProxy<float>, new() {
			DoRequiredArray(name, ref value, TaterToSingleProxy<T>);
		}
		public void RequiredSingleProxyMap<T,U>(string name, ref T value) where T : IDictionary<string, U>, new() where U : IReadProxy<float>, new() {
			DoRequiredDictionary(name, ref value, TaterToSingleProxy<U>);
		}
		public bool OptionalDouble(string name, ref double value) {
			return DoOptional(name, ref value, TaterToDouble);
		}
		public bool OptionalDoubleList<T>(string name, ref T value) where T : ICollection<double>, new() {
			return DoOptionalCollection(name, ref value, TaterToDouble);
		}
		public bool OptionalDoubleReadOnlyList(string name, ref IReadOnlyList<double> value) {
			return DoOptionalReadOnlyList(name, ref value, TaterToDouble);
		}
		public bool OptionalDoubleArray(string name, ref double[] value) {
			return DoOptionalArray(name, ref value, TaterToDouble);
		}
		public bool OptionalDoubleMap<T>(string name, ref T value) where T : IDictionary<string,double>, new() {
			return DoOptionalDictionary(name, ref value, TaterToDouble);
		}
		public bool OptionalDoubleProxy<T>(string name, ref T value) where T : IReadProxy<double>, new() {
			return DoOptional(name, ref value, TaterToDoubleProxy<T>);
		}
		public bool OptionalDoubleProxyList<T,U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<double>, new() {
			return DoOptionalCollection(name, ref value, TaterToDoubleProxy<U>);
		}
		public bool OptionalDoubleProxyReadOnlyList<T>(string name, ref IReadOnlyList<T> value) where T : IReadProxy<double>, new() {
			return DoOptionalReadOnlyList(name, ref value, TaterToDoubleProxy<T>);
		}
		public bool OptionalDoubleProxyArray<T>(string name, ref T[] value) where T : IReadProxy<double>, new() {
			return DoOptionalArray(name, ref value, TaterToDoubleProxy<T>);
		}
		public bool OptionalDoubleProxyMap<T,U>(string name, ref T value) where T : IDictionary<string,U>, new() where U : IReadProxy<double>, new() {
			return DoOptionalDictionary(name, ref value, TaterToDoubleProxy<U>);
		}
		public void RequiredDouble(string name, ref double value) {
			DoRequired(name, ref value, TaterToDouble);
		}
		public void RequiredDoubleList<T>(string name, ref T value) where T : ICollection<double>, new() {
			DoRequiredCollection(name, ref value, TaterToDouble);
		}
		public void RequiredDoubleReadOnlyList(string name, ref IReadOnlyList<double> value) {
			DoRequiredReadOnlyList(name, ref value, TaterToDouble);
		}
		public void RequiredDoubleArray(string name, ref double[] value) {
			DoRequiredArray(name, ref value, TaterToDouble);
		}
		public void RequiredDoubleMap<T>(string name, ref T value) where T : IDictionary<string,double>, new() {
			DoRequiredDictionary(name, ref value, TaterToDouble);
		}
		public void RequiredDoubleProxy<T>(string name, ref T value) where T : IReadProxy<double>, new() {
			DoRequired(name, ref value, TaterToDoubleProxy<T>);
		}
		public void RequiredDoubleProxyList<T,U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<double>, new() {
			DoRequiredCollection(name, ref value, TaterToDoubleProxy<U>);
		}
		public void RequiredDoubleProxyReadOnlyList<T>(string name, ref IReadOnlyList<T> value) where T : IReadProxy<double>, new() {
			DoRequiredReadOnlyList(name, ref value, TaterToDoubleProxy<T>);
		}
		public void RequiredDoubleProxyArray<T>(string name, ref T[] value) where T : IReadProxy<double>, new() {
			DoRequiredArray(name, ref value, TaterToDoubleProxy<T>);
		}
		public void RequiredDoubleProxyMap<T,U>(string name, ref T value) where T : IDictionary<string, U>, new() where U : IReadProxy<double>, new() {
			DoRequiredDictionary(name, ref value, TaterToDoubleProxy<U>);
		}
		public bool OptionalByte(string name, ref byte value) {
			return DoOptional(name, ref value, TaterToByte);
		}
		public bool OptionalByteList<T>(string name, ref T value) where T : ICollection<byte>, new() {
			return DoOptionalCollection(name, ref value, TaterToByte);
		}
		public bool OptionalByteReadOnlyList(string name, ref IReadOnlyList<byte> value) {
			return DoOptionalReadOnlyList(name, ref value, TaterToByte);
		}
		public bool OptionalByteArray(string name, ref byte[] value) {
			return DoOptionalArray(name, ref value, TaterToByte);
		}
		public bool OptionalByteMap<T>(string name, ref T value) where T : IDictionary<string,byte>, new() {
			return DoOptionalDictionary(name, ref value, TaterToByte);
		}
		public bool OptionalByteProxy<T>(string name, ref T value) where T : IReadProxy<byte>, new() {
			return DoOptional(name, ref value, TaterToByteProxy<T>);
		}
		public bool OptionalByteProxyList<T,U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<byte>, new() {
			return DoOptionalCollection(name, ref value, TaterToByteProxy<U>);
		}
		public bool OptionalByteProxyReadOnlyList<T>(string name, ref IReadOnlyList<T> value) where T : IReadProxy<byte>, new() {
			return DoOptionalReadOnlyList(name, ref value, TaterToByteProxy<T>);
		}
		public bool OptionalByteProxyArray<T>(string name, ref T[] value) where T : IReadProxy<byte>, new() {
			return DoOptionalArray(name, ref value, TaterToByteProxy<T>);
		}
		public bool OptionalByteProxyMap<T,U>(string name, ref T value) where T : IDictionary<string,U>, new() where U : IReadProxy<byte>, new() {
			return DoOptionalDictionary(name, ref value, TaterToByteProxy<U>);
		}
		public void RequiredByte(string name, ref byte value) {
			DoRequired(name, ref value, TaterToByte);
		}
		public void RequiredByteList<T>(string name, ref T value) where T : ICollection<byte>, new() {
			DoRequiredCollection(name, ref value, TaterToByte);
		}
		public void RequiredByteReadOnlyList(string name, ref IReadOnlyList<byte> value) {
			DoRequiredReadOnlyList(name, ref value, TaterToByte);
		}
		public void RequiredByteArray(string name, ref byte[] value) {
			DoRequiredArray(name, ref value, TaterToByte);
		}
		public void RequiredByteMap<T>(string name, ref T value) where T : IDictionary<string,byte>, new() {
			DoRequiredDictionary(name, ref value, TaterToByte);
		}
		public void RequiredByteProxy<T>(string name, ref T value) where T : IReadProxy<byte>, new() {
			DoRequired(name, ref value, TaterToByteProxy<T>);
		}
		public void RequiredByteProxyList<T,U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<byte>, new() {
			DoRequiredCollection(name, ref value, TaterToByteProxy<U>);
		}
		public void RequiredByteProxyReadOnlyList<T>(string name, ref IReadOnlyList<T> value) where T : IReadProxy<byte>, new() {
			DoRequiredReadOnlyList(name, ref value, TaterToByteProxy<T>);
		}
		public void RequiredByteProxyArray<T>(string name, ref T[] value) where T : IReadProxy<byte>, new() {
			DoRequiredArray(name, ref value, TaterToByteProxy<T>);
		}
		public void RequiredByteProxyMap<T,U>(string name, ref T value) where T : IDictionary<string, U>, new() where U : IReadProxy<byte>, new() {
			DoRequiredDictionary(name, ref value, TaterToByteProxy<U>);
		}
		public bool OptionalSByte(string name, ref sbyte value) {
			return DoOptional(name, ref value, TaterToSByte);
		}
		public bool OptionalSByteList<T>(string name, ref T value) where T : ICollection<sbyte>, new() {
			return DoOptionalCollection(name, ref value, TaterToSByte);
		}
		public bool OptionalSByteReadOnlyList(string name, ref IReadOnlyList<sbyte> value) {
			return DoOptionalReadOnlyList(name, ref value, TaterToSByte);
		}
		public bool OptionalSByteArray(string name, ref sbyte[] value) {
			return DoOptionalArray(name, ref value, TaterToSByte);
		}
		public bool OptionalSByteMap<T>(string name, ref T value) where T : IDictionary<string,sbyte>, new() {
			return DoOptionalDictionary(name, ref value, TaterToSByte);
		}
		public bool OptionalSByteProxy<T>(string name, ref T value) where T : IReadProxy<sbyte>, new() {
			return DoOptional(name, ref value, TaterToSByteProxy<T>);
		}
		public bool OptionalSByteProxyList<T,U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<sbyte>, new() {
			return DoOptionalCollection(name, ref value, TaterToSByteProxy<U>);
		}
		public bool OptionalSByteProxyReadOnlyList<T>(string name, ref IReadOnlyList<T> value) where T : IReadProxy<sbyte>, new() {
			return DoOptionalReadOnlyList(name, ref value, TaterToSByteProxy<T>);
		}
		public bool OptionalSByteProxyArray<T>(string name, ref T[] value) where T : IReadProxy<sbyte>, new() {
			return DoOptionalArray(name, ref value, TaterToSByteProxy<T>);
		}
		public bool OptionalSByteProxyMap<T,U>(string name, ref T value) where T : IDictionary<string,U>, new() where U : IReadProxy<sbyte>, new() {
			return DoOptionalDictionary(name, ref value, TaterToSByteProxy<U>);
		}
		public void RequiredSByte(string name, ref sbyte value) {
			DoRequired(name, ref value, TaterToSByte);
		}
		public void RequiredSByteList<T>(string name, ref T value) where T : ICollection<sbyte>, new() {
			DoRequiredCollection(name, ref value, TaterToSByte);
		}
		public void RequiredSByteReadOnlyList(string name, ref IReadOnlyList<sbyte> value) {
			DoRequiredReadOnlyList(name, ref value, TaterToSByte);
		}
		public void RequiredSByteArray(string name, ref sbyte[] value) {
			DoRequiredArray(name, ref value, TaterToSByte);
		}
		public void RequiredSByteMap<T>(string name, ref T value) where T : IDictionary<string,sbyte>, new() {
			DoRequiredDictionary(name, ref value, TaterToSByte);
		}
		public void RequiredSByteProxy<T>(string name, ref T value) where T : IReadProxy<sbyte>, new() {
			DoRequired(name, ref value, TaterToSByteProxy<T>);
		}
		public void RequiredSByteProxyList<T,U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<sbyte>, new() {
			DoRequiredCollection(name, ref value, TaterToSByteProxy<U>);
		}
		public void RequiredSByteProxyReadOnlyList<T>(string name, ref IReadOnlyList<T> value) where T : IReadProxy<sbyte>, new() {
			DoRequiredReadOnlyList(name, ref value, TaterToSByteProxy<T>);
		}
		public void RequiredSByteProxyArray<T>(string name, ref T[] value) where T : IReadProxy<sbyte>, new() {
			DoRequiredArray(name, ref value, TaterToSByteProxy<T>);
		}
		public void RequiredSByteProxyMap<T,U>(string name, ref T value) where T : IDictionary<string, U>, new() where U : IReadProxy<sbyte>, new() {
			DoRequiredDictionary(name, ref value, TaterToSByteProxy<U>);
		}
		public bool OptionalBool(string name, ref bool value) {
			return DoOptional(name, ref value, TaterToBool);
		}
		public bool OptionalBoolList<T>(string name, ref T value) where T : ICollection<bool>, new() {
			return DoOptionalCollection(name, ref value, TaterToBool);
		}
		public bool OptionalBoolReadOnlyList(string name, ref IReadOnlyList<bool> value) {
			return DoOptionalReadOnlyList(name, ref value, TaterToBool);
		}
		public bool OptionalBoolArray(string name, ref bool[] value) {
			return DoOptionalArray(name, ref value, TaterToBool);
		}
		public bool OptionalBoolMap<T>(string name, ref T value) where T : IDictionary<string,bool>, new() {
			return DoOptionalDictionary(name, ref value, TaterToBool);
		}
		public bool OptionalBoolProxy<T>(string name, ref T value) where T : IReadProxy<bool>, new() {
			return DoOptional(name, ref value, TaterToBoolProxy<T>);
		}
		public bool OptionalBoolProxyList<T,U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<bool>, new() {
			return DoOptionalCollection(name, ref value, TaterToBoolProxy<U>);
		}
		public bool OptionalBoolProxyReadOnlyList<T>(string name, ref IReadOnlyList<T> value) where T : IReadProxy<bool>, new() {
			return DoOptionalReadOnlyList(name, ref value, TaterToBoolProxy<T>);
		}
		public bool OptionalBoolProxyArray<T>(string name, ref T[] value) where T : IReadProxy<bool>, new() {
			return DoOptionalArray(name, ref value, TaterToBoolProxy<T>);
		}
		public bool OptionalBoolProxyMap<T,U>(string name, ref T value) where T : IDictionary<string,U>, new() where U : IReadProxy<bool>, new() {
			return DoOptionalDictionary(name, ref value, TaterToBoolProxy<U>);
		}
		public void RequiredBool(string name, ref bool value) {
			DoRequired(name, ref value, TaterToBool);
		}
		public void RequiredBoolList<T>(string name, ref T value) where T : ICollection<bool>, new() {
			DoRequiredCollection(name, ref value, TaterToBool);
		}
		public void RequiredBoolReadOnlyList(string name, ref IReadOnlyList<bool> value) {
			DoRequiredReadOnlyList(name, ref value, TaterToBool);
		}
		public void RequiredBoolArray(string name, ref bool[] value) {
			DoRequiredArray(name, ref value, TaterToBool);
		}
		public void RequiredBoolMap<T>(string name, ref T value) where T : IDictionary<string,bool>, new() {
			DoRequiredDictionary(name, ref value, TaterToBool);
		}
		public void RequiredBoolProxy<T>(string name, ref T value) where T : IReadProxy<bool>, new() {
			DoRequired(name, ref value, TaterToBoolProxy<T>);
		}
		public void RequiredBoolProxyList<T,U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<bool>, new() {
			DoRequiredCollection(name, ref value, TaterToBoolProxy<U>);
		}
		public void RequiredBoolProxyReadOnlyList<T>(string name, ref IReadOnlyList<T> value) where T : IReadProxy<bool>, new() {
			DoRequiredReadOnlyList(name, ref value, TaterToBoolProxy<T>);
		}
		public void RequiredBoolProxyArray<T>(string name, ref T[] value) where T : IReadProxy<bool>, new() {
			DoRequiredArray(name, ref value, TaterToBoolProxy<T>);
		}
		public void RequiredBoolProxyMap<T,U>(string name, ref T value) where T : IDictionary<string, U>, new() where U : IReadProxy<bool>, new() {
			DoRequiredDictionary(name, ref value, TaterToBoolProxy<U>);
		}
		public bool OptionalVector2(string name, ref Vector2 value) {
			return DoOptional(name, ref value, TaterToVector2);
		}
		public bool OptionalVector2List<T>(string name, ref T value) where T : ICollection<Vector2>, new() {
			return DoOptionalCollection(name, ref value, TaterToVector2);
		}
		public bool OptionalVector2ReadOnlyList(string name, ref IReadOnlyList<Vector2> value) {
			return DoOptionalReadOnlyList(name, ref value, TaterToVector2);
		}
		public bool OptionalVector2Array(string name, ref Vector2[] value) {
			return DoOptionalArray(name, ref value, TaterToVector2);
		}
		public bool OptionalVector2Map<T>(string name, ref T value) where T : IDictionary<string,Vector2>, new() {
			return DoOptionalDictionary(name, ref value, TaterToVector2);
		}
		public void RequiredVector2(string name, ref Vector2 value) {
			DoRequired(name, ref value, TaterToVector2);
		}
		public void RequiredVector2List<T>(string name, ref T value) where T : ICollection<Vector2>, new() {
			DoRequiredCollection(name, ref value, TaterToVector2);
		}
		public void RequiredVector2ReadOnlyList(string name, ref IReadOnlyList<Vector2> value) {
			DoRequiredReadOnlyList(name, ref value, TaterToVector2);
		}
		public void RequiredVector2Array(string name, ref Vector2[] value) {
			DoRequiredArray(name, ref value, TaterToVector2);
		}
		public void RequiredVector2Map<T>(string name, ref T value) where T : IDictionary<string,Vector2>, new() {
			DoRequiredDictionary(name, ref value, TaterToVector2);
		}
		public bool OptionalVector3(string name, ref Vector3 value) {
			return DoOptional(name, ref value, TaterToVector3);
		}
		public bool OptionalVector3List<T>(string name, ref T value) where T : ICollection<Vector3>, new() {
			return DoOptionalCollection(name, ref value, TaterToVector3);
		}
		public bool OptionalVector3ReadOnlyList(string name, ref IReadOnlyList<Vector3> value) {
			return DoOptionalReadOnlyList(name, ref value, TaterToVector3);
		}
		public bool OptionalVector3Array(string name, ref Vector3[] value) {
			return DoOptionalArray(name, ref value, TaterToVector3);
		}
		public bool OptionalVector3Map<T>(string name, ref T value) where T : IDictionary<string,Vector3>, new() {
			return DoOptionalDictionary(name, ref value, TaterToVector3);
		}
		public void RequiredVector3(string name, ref Vector3 value) {
			DoRequired(name, ref value, TaterToVector3);
		}
		public void RequiredVector3List<T>(string name, ref T value) where T : ICollection<Vector3>, new() {
			DoRequiredCollection(name, ref value, TaterToVector3);
		}
		public void RequiredVector3ReadOnlyList(string name, ref IReadOnlyList<Vector3> value) {
			DoRequiredReadOnlyList(name, ref value, TaterToVector3);
		}
		public void RequiredVector3Array(string name, ref Vector3[] value) {
			DoRequiredArray(name, ref value, TaterToVector3);
		}
		public void RequiredVector3Map<T>(string name, ref T value) where T : IDictionary<string,Vector3>, new() {
			DoRequiredDictionary(name, ref value, TaterToVector3);
		}
		public bool OptionalVector4(string name, ref Vector4 value) {
			return DoOptional(name, ref value, TaterToVector4);
		}
		public bool OptionalVector4List<T>(string name, ref T value) where T : ICollection<Vector4>, new() {
			return DoOptionalCollection(name, ref value, TaterToVector4);
		}
		public bool OptionalVector4ReadOnlyList(string name, ref IReadOnlyList<Vector4> value) {
			return DoOptionalReadOnlyList(name, ref value, TaterToVector4);
		}
		public bool OptionalVector4Array(string name, ref Vector4[] value) {
			return DoOptionalArray(name, ref value, TaterToVector4);
		}
		public bool OptionalVector4Map<T>(string name, ref T value) where T : IDictionary<string,Vector4>, new() {
			return DoOptionalDictionary(name, ref value, TaterToVector4);
		}
		public void RequiredVector4(string name, ref Vector4 value) {
			DoRequired(name, ref value, TaterToVector4);
		}
		public void RequiredVector4List<T>(string name, ref T value) where T : ICollection<Vector4>, new() {
			DoRequiredCollection(name, ref value, TaterToVector4);
		}
		public void RequiredVector4ReadOnlyList(string name, ref IReadOnlyList<Vector4> value) {
			DoRequiredReadOnlyList(name, ref value, TaterToVector4);
		}
		public void RequiredVector4Array(string name, ref Vector4[] value) {
			DoRequiredArray(name, ref value, TaterToVector4);
		}
		public void RequiredVector4Map<T>(string name, ref T value) where T : IDictionary<string,Vector4>, new() {
			DoRequiredDictionary(name, ref value, TaterToVector4);
		}
		public bool OptionalQuaternion(string name, ref Quaternion value) {
			return DoOptional(name, ref value, TaterToQuaternion);
		}
		public bool OptionalQuaternionList<T>(string name, ref T value) where T : ICollection<Quaternion>, new() {
			return DoOptionalCollection(name, ref value, TaterToQuaternion);
		}
		public bool OptionalQuaternionReadOnlyList(string name, ref IReadOnlyList<Quaternion> value) {
			return DoOptionalReadOnlyList(name, ref value, TaterToQuaternion);
		}
		public bool OptionalQuaternionArray(string name, ref Quaternion[] value) {
			return DoOptionalArray(name, ref value, TaterToQuaternion);
		}
		public bool OptionalQuaternionMap<T>(string name, ref T value) where T : IDictionary<string,Quaternion>, new() {
			return DoOptionalDictionary(name, ref value, TaterToQuaternion);
		}
		public void RequiredQuaternion(string name, ref Quaternion value) {
			DoRequired(name, ref value, TaterToQuaternion);
		}
		public void RequiredQuaternionList<T>(string name, ref T value) where T : ICollection<Quaternion>, new() {
			DoRequiredCollection(name, ref value, TaterToQuaternion);
		}
		public void RequiredQuaternionReadOnlyList(string name, ref IReadOnlyList<Quaternion> value) {
			DoRequiredReadOnlyList(name, ref value, TaterToQuaternion);
		}
		public void RequiredQuaternionArray(string name, ref Quaternion[] value) {
			DoRequiredArray(name, ref value, TaterToQuaternion);
		}
		public void RequiredQuaternionMap<T>(string name, ref T value) where T : IDictionary<string,Quaternion>, new() {
			DoRequiredDictionary(name, ref value, TaterToQuaternion);
		}
		public bool OptionalVector2Int(string name, ref Vector2Int value) {
			return DoOptional(name, ref value, TaterToVector2Int);
		}
		public bool OptionalVector2IntList<T>(string name, ref T value) where T : ICollection<Vector2Int>, new() {
			return DoOptionalCollection(name, ref value, TaterToVector2Int);
		}
		public bool OptionalVector2IntReadOnlyList(string name, ref IReadOnlyList<Vector2Int> value) {
			return DoOptionalReadOnlyList(name, ref value, TaterToVector2Int);
		}
		public bool OptionalVector2IntArray(string name, ref Vector2Int[] value) {
			return DoOptionalArray(name, ref value, TaterToVector2Int);
		}
		public bool OptionalVector2IntMap<T>(string name, ref T value) where T : IDictionary<string,Vector2Int>, new() {
			return DoOptionalDictionary(name, ref value, TaterToVector2Int);
		}
		public void RequiredVector2Int(string name, ref Vector2Int value) {
			DoRequired(name, ref value, TaterToVector2Int);
		}
		public void RequiredVector2IntList<T>(string name, ref T value) where T : ICollection<Vector2Int>, new() {
			DoRequiredCollection(name, ref value, TaterToVector2Int);
		}
		public void RequiredVector2IntReadOnlyList(string name, ref IReadOnlyList<Vector2Int> value) {
			DoRequiredReadOnlyList(name, ref value, TaterToVector2Int);
		}
		public void RequiredVector2IntArray(string name, ref Vector2Int[] value) {
			DoRequiredArray(name, ref value, TaterToVector2Int);
		}
		public void RequiredVector2IntMap<T>(string name, ref T value) where T : IDictionary<string,Vector2Int>, new() {
			DoRequiredDictionary(name, ref value, TaterToVector2Int);
		}
		public bool OptionalVector3Int(string name, ref Vector3Int value) {
			return DoOptional(name, ref value, TaterToVector3Int);
		}
		public bool OptionalVector3IntList<T>(string name, ref T value) where T : ICollection<Vector3Int>, new() {
			return DoOptionalCollection(name, ref value, TaterToVector3Int);
		}
		public bool OptionalVector3IntReadOnlyList(string name, ref IReadOnlyList<Vector3Int> value) {
			return DoOptionalReadOnlyList(name, ref value, TaterToVector3Int);
		}
		public bool OptionalVector3IntArray(string name, ref Vector3Int[] value) {
			return DoOptionalArray(name, ref value, TaterToVector3Int);
		}
		public bool OptionalVector3IntMap<T>(string name, ref T value) where T : IDictionary<string,Vector3Int>, new() {
			return DoOptionalDictionary(name, ref value, TaterToVector3Int);
		}
		public void RequiredVector3Int(string name, ref Vector3Int value) {
			DoRequired(name, ref value, TaterToVector3Int);
		}
		public void RequiredVector3IntList<T>(string name, ref T value) where T : ICollection<Vector3Int>, new() {
			DoRequiredCollection(name, ref value, TaterToVector3Int);
		}
		public void RequiredVector3IntReadOnlyList(string name, ref IReadOnlyList<Vector3Int> value) {
			DoRequiredReadOnlyList(name, ref value, TaterToVector3Int);
		}
		public void RequiredVector3IntArray(string name, ref Vector3Int[] value) {
			DoRequiredArray(name, ref value, TaterToVector3Int);
		}
		public void RequiredVector3IntMap<T>(string name, ref T value) where T : IDictionary<string,Vector3Int>, new() {
			DoRequiredDictionary(name, ref value, TaterToVector3Int);
		}
		public bool OptionalRect(string name, ref Rect value) {
			return DoOptional(name, ref value, TaterToRect);
		}
		public bool OptionalRectList<T>(string name, ref T value) where T : ICollection<Rect>, new() {
			return DoOptionalCollection(name, ref value, TaterToRect);
		}
		public bool OptionalRectReadOnlyList(string name, ref IReadOnlyList<Rect> value) {
			return DoOptionalReadOnlyList(name, ref value, TaterToRect);
		}
		public bool OptionalRectArray(string name, ref Rect[] value) {
			return DoOptionalArray(name, ref value, TaterToRect);
		}
		public bool OptionalRectMap<T>(string name, ref T value) where T : IDictionary<string,Rect>, new() {
			return DoOptionalDictionary(name, ref value, TaterToRect);
		}
		public void RequiredRect(string name, ref Rect value) {
			DoRequired(name, ref value, TaterToRect);
		}
		public void RequiredRectList<T>(string name, ref T value) where T : ICollection<Rect>, new() {
			DoRequiredCollection(name, ref value, TaterToRect);
		}
		public void RequiredRectReadOnlyList(string name, ref IReadOnlyList<Rect> value) {
			DoRequiredReadOnlyList(name, ref value, TaterToRect);
		}
		public void RequiredRectArray(string name, ref Rect[] value) {
			DoRequiredArray(name, ref value, TaterToRect);
		}
		public void RequiredRectMap<T>(string name, ref T value) where T : IDictionary<string,Rect>, new() {
			DoRequiredDictionary(name, ref value, TaterToRect);
		}
		public bool OptionalRectInt(string name, ref RectInt value) {
			return DoOptional(name, ref value, TaterToRectInt);
		}
		public bool OptionalRectIntList<T>(string name, ref T value) where T : ICollection<RectInt>, new() {
			return DoOptionalCollection(name, ref value, TaterToRectInt);
		}
		public bool OptionalRectIntReadOnlyList(string name, ref IReadOnlyList<RectInt> value) {
			return DoOptionalReadOnlyList(name, ref value, TaterToRectInt);
		}
		public bool OptionalRectIntArray(string name, ref RectInt[] value) {
			return DoOptionalArray(name, ref value, TaterToRectInt);
		}
		public bool OptionalRectIntMap<T>(string name, ref T value) where T : IDictionary<string,RectInt>, new() {
			return DoOptionalDictionary(name, ref value, TaterToRectInt);
		}
		public void RequiredRectInt(string name, ref RectInt value) {
			DoRequired(name, ref value, TaterToRectInt);
		}
		public void RequiredRectIntList<T>(string name, ref T value) where T : ICollection<RectInt>, new() {
			DoRequiredCollection(name, ref value, TaterToRectInt);
		}
		public void RequiredRectIntReadOnlyList(string name, ref IReadOnlyList<RectInt> value) {
			DoRequiredReadOnlyList(name, ref value, TaterToRectInt);
		}
		public void RequiredRectIntArray(string name, ref RectInt[] value) {
			DoRequiredArray(name, ref value, TaterToRectInt);
		}
		public void RequiredRectIntMap<T>(string name, ref T value) where T : IDictionary<string,RectInt>, new() {
			DoRequiredDictionary(name, ref value, TaterToRectInt);
		}
		public bool OptionalColor(string name, ref Color value) {
			return DoOptional(name, ref value, TaterToColor);
		}
		public bool OptionalColorList<T>(string name, ref T value) where T : ICollection<Color>, new() {
			return DoOptionalCollection(name, ref value, TaterToColor);
		}
		public bool OptionalColorReadOnlyList(string name, ref IReadOnlyList<Color> value) {
			return DoOptionalReadOnlyList(name, ref value, TaterToColor);
		}
		public bool OptionalColorArray(string name, ref Color[] value) {
			return DoOptionalArray(name, ref value, TaterToColor);
		}
		public bool OptionalColorMap<T>(string name, ref T value) where T : IDictionary<string,Color>, new() {
			return DoOptionalDictionary(name, ref value, TaterToColor);
		}
		public void RequiredColor(string name, ref Color value) {
			DoRequired(name, ref value, TaterToColor);
		}
		public void RequiredColorList<T>(string name, ref T value) where T : ICollection<Color>, new() {
			DoRequiredCollection(name, ref value, TaterToColor);
		}
		public void RequiredColorReadOnlyList(string name, ref IReadOnlyList<Color> value) {
			DoRequiredReadOnlyList(name, ref value, TaterToColor);
		}
		public void RequiredColorArray(string name, ref Color[] value) {
			DoRequiredArray(name, ref value, TaterToColor);
		}
		public void RequiredColorMap<T>(string name, ref T value) where T : IDictionary<string,Color>, new() {
			DoRequiredDictionary(name, ref value, TaterToColor);
		}
		public bool OptionalColor32(string name, ref Color32 value) {
			return DoOptional(name, ref value, TaterToColor32);
		}
		public bool OptionalColor32List<T>(string name, ref T value) where T : ICollection<Color32>, new() {
			return DoOptionalCollection(name, ref value, TaterToColor32);
		}
		public bool OptionalColor32ReadOnlyList(string name, ref IReadOnlyList<Color32> value) {
			return DoOptionalReadOnlyList(name, ref value, TaterToColor32);
		}
		public bool OptionalColor32Array(string name, ref Color32[] value) {
			return DoOptionalArray(name, ref value, TaterToColor32);
		}
		public bool OptionalColor32Map<T>(string name, ref T value) where T : IDictionary<string,Color32>, new() {
			return DoOptionalDictionary(name, ref value, TaterToColor32);
		}
		public void RequiredColor32(string name, ref Color32 value) {
			DoRequired(name, ref value, TaterToColor32);
		}
		public void RequiredColor32List<T>(string name, ref T value) where T : ICollection<Color32>, new() {
			DoRequiredCollection(name, ref value, TaterToColor32);
		}
		public void RequiredColor32ReadOnlyList(string name, ref IReadOnlyList<Color32> value) {
			DoRequiredReadOnlyList(name, ref value, TaterToColor32);
		}
		public void RequiredColor32Array(string name, ref Color32[] value) {
			DoRequiredArray(name, ref value, TaterToColor32);
		}
		public void RequiredColor32Map<T>(string name, ref T value) where T : IDictionary<string,Color32>, new() {
			DoRequiredDictionary(name, ref value, TaterToColor32);
		}
	}
}
