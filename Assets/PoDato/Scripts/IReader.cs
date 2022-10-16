// Generated from ReaderGenerator.cs
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PoDato {
	public interface IReader {
		bool OptionalObject<T>(string name, ref T value) where T : IReadable, new();
		bool OptionalObjectList<T, U>(string name, ref T value) where T : ICollection<U>, new () where U : IReadable, new ();
		bool OptionalObjectArray<T>(string name, ref T[] value) where T : IReadable, new();
		bool OptionalObjectMap<T, U>(string name, ref T value) where T : IDictionary<string, U>, new() where U : IReadable, new();
		void RequiredObject<T>(string name, ref T value) where T : IReadable, new();
		void RequiredObjectList<T, U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadable, new();
		void RequiredObjectMap<T, U>(string name, ref T value) where T : IDictionary<string, U>, new() where U : IReadable, new();
		void RequiredObjectArray<T>(string name, ref T[] value) where T : IReadable, new();
		bool OptionalEnum<T>(string name, ref T value) where T : struct, Enum;
		bool OptionalEnumList<T, U>(string name, ref T value) where T : ICollection<U>, new() where U : struct, Enum;
		bool OptionalEnumArray<T>(string name, ref T[] value) where T : struct, Enum;
		bool OptionalEnumMap<T, U>(string name, ref T value) where T : IDictionary<string, U>, new() where U : struct, Enum;
		void RequiredEnum<T>(string name, ref T value) where T : struct, Enum;
		void RequiredEnumList<T, U>(string name, ref T value) where T : ICollection<U>, new() where U : struct, Enum;
		void RequiredEnumArray<T>(string name, ref T[] value) where T : struct, Enum;
		void RequiredEnumMap<T, U>(string name, ref T value) where T : IDictionary<string, U>, new() where U : struct, Enum;
		// string
		bool OptionalString(string name, ref string value);
		bool OptionalStringList<T>(string name, ref T value) where T : ICollection<string>, new();
		bool OptionalStringArray(string name, ref string[] value);
		bool OptionalStringMap<T>(string name, ref T value) where T : IDictionary<string,string>, new();
		bool OptionalStringProxy<T>(string name, ref T value) where T : IReadProxy<string>, new();
		bool OptionalStringProxyList<T,U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<string>, new();
		bool OptionalStringProxyArray<T>(string name, ref T[] value) where T : IReadProxy<string>, new();
		bool OptionalStringProxyMap<T,U>(string name, ref T value) where T : IDictionary<string,U>, new() where U : IReadProxy<string>, new();
		void RequiredString(string name, ref string value);
		void RequiredStringList<T>(string name, ref T value) where T : ICollection<string>, new();
		void RequiredStringArray(string name, ref string[] value);
		void RequiredStringMap<T>(string name, ref T value) where T : IDictionary<string,string>, new();
		void RequiredStringProxy<T>(string name, ref T value) where T : IReadProxy<string>, new();
		void RequiredStringProxyList<T,U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<string>, new();
		void RequiredStringProxyArray<T>(string name, ref T[] value) where T : IReadProxy<string>, new();
		void RequiredStringProxyMap<T,U>(string name, ref T value) where T : IDictionary<string, U>, new() where U : IReadProxy<string>, new();
		// int
		bool OptionalInt32(string name, ref int value);
		bool OptionalInt32List<T>(string name, ref T value) where T : ICollection<int>, new();
		bool OptionalInt32Array(string name, ref int[] value);
		bool OptionalInt32Map<T>(string name, ref T value) where T : IDictionary<string,int>, new();
		bool OptionalInt32Proxy<T>(string name, ref T value) where T : IReadProxy<int>, new();
		bool OptionalInt32ProxyList<T,U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<int>, new();
		bool OptionalInt32ProxyArray<T>(string name, ref T[] value) where T : IReadProxy<int>, new();
		bool OptionalInt32ProxyMap<T,U>(string name, ref T value) where T : IDictionary<string,U>, new() where U : IReadProxy<int>, new();
		void RequiredInt32(string name, ref int value);
		void RequiredInt32List<T>(string name, ref T value) where T : ICollection<int>, new();
		void RequiredInt32Array(string name, ref int[] value);
		void RequiredInt32Map<T>(string name, ref T value) where T : IDictionary<string,int>, new();
		void RequiredInt32Proxy<T>(string name, ref T value) where T : IReadProxy<int>, new();
		void RequiredInt32ProxyList<T,U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<int>, new();
		void RequiredInt32ProxyArray<T>(string name, ref T[] value) where T : IReadProxy<int>, new();
		void RequiredInt32ProxyMap<T,U>(string name, ref T value) where T : IDictionary<string, U>, new() where U : IReadProxy<int>, new();
		// short
		bool OptionalInt16(string name, ref short value);
		bool OptionalInt16List<T>(string name, ref T value) where T : ICollection<short>, new();
		bool OptionalInt16Array(string name, ref short[] value);
		bool OptionalInt16Map<T>(string name, ref T value) where T : IDictionary<string,short>, new();
		bool OptionalInt16Proxy<T>(string name, ref T value) where T : IReadProxy<short>, new();
		bool OptionalInt16ProxyList<T,U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<short>, new();
		bool OptionalInt16ProxyArray<T>(string name, ref T[] value) where T : IReadProxy<short>, new();
		bool OptionalInt16ProxyMap<T,U>(string name, ref T value) where T : IDictionary<string,U>, new() where U : IReadProxy<short>, new();
		void RequiredInt16(string name, ref short value);
		void RequiredInt16List<T>(string name, ref T value) where T : ICollection<short>, new();
		void RequiredInt16Array(string name, ref short[] value);
		void RequiredInt16Map<T>(string name, ref T value) where T : IDictionary<string,short>, new();
		void RequiredInt16Proxy<T>(string name, ref T value) where T : IReadProxy<short>, new();
		void RequiredInt16ProxyList<T,U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<short>, new();
		void RequiredInt16ProxyArray<T>(string name, ref T[] value) where T : IReadProxy<short>, new();
		void RequiredInt16ProxyMap<T,U>(string name, ref T value) where T : IDictionary<string, U>, new() where U : IReadProxy<short>, new();
		// long
		bool OptionalInt64(string name, ref long value);
		bool OptionalInt64List<T>(string name, ref T value) where T : ICollection<long>, new();
		bool OptionalInt64Array(string name, ref long[] value);
		bool OptionalInt64Map<T>(string name, ref T value) where T : IDictionary<string,long>, new();
		bool OptionalInt64Proxy<T>(string name, ref T value) where T : IReadProxy<long>, new();
		bool OptionalInt64ProxyList<T,U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<long>, new();
		bool OptionalInt64ProxyArray<T>(string name, ref T[] value) where T : IReadProxy<long>, new();
		bool OptionalInt64ProxyMap<T,U>(string name, ref T value) where T : IDictionary<string,U>, new() where U : IReadProxy<long>, new();
		void RequiredInt64(string name, ref long value);
		void RequiredInt64List<T>(string name, ref T value) where T : ICollection<long>, new();
		void RequiredInt64Array(string name, ref long[] value);
		void RequiredInt64Map<T>(string name, ref T value) where T : IDictionary<string,long>, new();
		void RequiredInt64Proxy<T>(string name, ref T value) where T : IReadProxy<long>, new();
		void RequiredInt64ProxyList<T,U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<long>, new();
		void RequiredInt64ProxyArray<T>(string name, ref T[] value) where T : IReadProxy<long>, new();
		void RequiredInt64ProxyMap<T,U>(string name, ref T value) where T : IDictionary<string, U>, new() where U : IReadProxy<long>, new();
		// uint
		bool OptionalUInt32(string name, ref uint value);
		bool OptionalUInt32List<T>(string name, ref T value) where T : ICollection<uint>, new();
		bool OptionalUInt32Array(string name, ref uint[] value);
		bool OptionalUInt32Map<T>(string name, ref T value) where T : IDictionary<string,uint>, new();
		bool OptionalUInt32Proxy<T>(string name, ref T value) where T : IReadProxy<uint>, new();
		bool OptionalUInt32ProxyList<T,U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<uint>, new();
		bool OptionalUInt32ProxyArray<T>(string name, ref T[] value) where T : IReadProxy<uint>, new();
		bool OptionalUInt32ProxyMap<T,U>(string name, ref T value) where T : IDictionary<string,U>, new() where U : IReadProxy<uint>, new();
		void RequiredUInt32(string name, ref uint value);
		void RequiredUInt32List<T>(string name, ref T value) where T : ICollection<uint>, new();
		void RequiredUInt32Array(string name, ref uint[] value);
		void RequiredUInt32Map<T>(string name, ref T value) where T : IDictionary<string,uint>, new();
		void RequiredUInt32Proxy<T>(string name, ref T value) where T : IReadProxy<uint>, new();
		void RequiredUInt32ProxyList<T,U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<uint>, new();
		void RequiredUInt32ProxyArray<T>(string name, ref T[] value) where T : IReadProxy<uint>, new();
		void RequiredUInt32ProxyMap<T,U>(string name, ref T value) where T : IDictionary<string, U>, new() where U : IReadProxy<uint>, new();
		// ushort
		bool OptionalUInt16(string name, ref ushort value);
		bool OptionalUInt16List<T>(string name, ref T value) where T : ICollection<ushort>, new();
		bool OptionalUInt16Array(string name, ref ushort[] value);
		bool OptionalUInt16Map<T>(string name, ref T value) where T : IDictionary<string,ushort>, new();
		bool OptionalUInt16Proxy<T>(string name, ref T value) where T : IReadProxy<ushort>, new();
		bool OptionalUInt16ProxyList<T,U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<ushort>, new();
		bool OptionalUInt16ProxyArray<T>(string name, ref T[] value) where T : IReadProxy<ushort>, new();
		bool OptionalUInt16ProxyMap<T,U>(string name, ref T value) where T : IDictionary<string,U>, new() where U : IReadProxy<ushort>, new();
		void RequiredUInt16(string name, ref ushort value);
		void RequiredUInt16List<T>(string name, ref T value) where T : ICollection<ushort>, new();
		void RequiredUInt16Array(string name, ref ushort[] value);
		void RequiredUInt16Map<T>(string name, ref T value) where T : IDictionary<string,ushort>, new();
		void RequiredUInt16Proxy<T>(string name, ref T value) where T : IReadProxy<ushort>, new();
		void RequiredUInt16ProxyList<T,U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<ushort>, new();
		void RequiredUInt16ProxyArray<T>(string name, ref T[] value) where T : IReadProxy<ushort>, new();
		void RequiredUInt16ProxyMap<T,U>(string name, ref T value) where T : IDictionary<string, U>, new() where U : IReadProxy<ushort>, new();
		// ulong
		bool OptionalUInt64(string name, ref ulong value);
		bool OptionalUInt64List<T>(string name, ref T value) where T : ICollection<ulong>, new();
		bool OptionalUInt64Array(string name, ref ulong[] value);
		bool OptionalUInt64Map<T>(string name, ref T value) where T : IDictionary<string,ulong>, new();
		bool OptionalUInt64Proxy<T>(string name, ref T value) where T : IReadProxy<ulong>, new();
		bool OptionalUInt64ProxyList<T,U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<ulong>, new();
		bool OptionalUInt64ProxyArray<T>(string name, ref T[] value) where T : IReadProxy<ulong>, new();
		bool OptionalUInt64ProxyMap<T,U>(string name, ref T value) where T : IDictionary<string,U>, new() where U : IReadProxy<ulong>, new();
		void RequiredUInt64(string name, ref ulong value);
		void RequiredUInt64List<T>(string name, ref T value) where T : ICollection<ulong>, new();
		void RequiredUInt64Array(string name, ref ulong[] value);
		void RequiredUInt64Map<T>(string name, ref T value) where T : IDictionary<string,ulong>, new();
		void RequiredUInt64Proxy<T>(string name, ref T value) where T : IReadProxy<ulong>, new();
		void RequiredUInt64ProxyList<T,U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<ulong>, new();
		void RequiredUInt64ProxyArray<T>(string name, ref T[] value) where T : IReadProxy<ulong>, new();
		void RequiredUInt64ProxyMap<T,U>(string name, ref T value) where T : IDictionary<string, U>, new() where U : IReadProxy<ulong>, new();
		// float
		bool OptionalSingle(string name, ref float value);
		bool OptionalSingleList<T>(string name, ref T value) where T : ICollection<float>, new();
		bool OptionalSingleArray(string name, ref float[] value);
		bool OptionalSingleMap<T>(string name, ref T value) where T : IDictionary<string,float>, new();
		bool OptionalSingleProxy<T>(string name, ref T value) where T : IReadProxy<float>, new();
		bool OptionalSingleProxyList<T,U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<float>, new();
		bool OptionalSingleProxyArray<T>(string name, ref T[] value) where T : IReadProxy<float>, new();
		bool OptionalSingleProxyMap<T,U>(string name, ref T value) where T : IDictionary<string,U>, new() where U : IReadProxy<float>, new();
		void RequiredSingle(string name, ref float value);
		void RequiredSingleList<T>(string name, ref T value) where T : ICollection<float>, new();
		void RequiredSingleArray(string name, ref float[] value);
		void RequiredSingleMap<T>(string name, ref T value) where T : IDictionary<string,float>, new();
		void RequiredSingleProxy<T>(string name, ref T value) where T : IReadProxy<float>, new();
		void RequiredSingleProxyList<T,U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<float>, new();
		void RequiredSingleProxyArray<T>(string name, ref T[] value) where T : IReadProxy<float>, new();
		void RequiredSingleProxyMap<T,U>(string name, ref T value) where T : IDictionary<string, U>, new() where U : IReadProxy<float>, new();
		// double
		bool OptionalDouble(string name, ref double value);
		bool OptionalDoubleList<T>(string name, ref T value) where T : ICollection<double>, new();
		bool OptionalDoubleArray(string name, ref double[] value);
		bool OptionalDoubleMap<T>(string name, ref T value) where T : IDictionary<string,double>, new();
		bool OptionalDoubleProxy<T>(string name, ref T value) where T : IReadProxy<double>, new();
		bool OptionalDoubleProxyList<T,U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<double>, new();
		bool OptionalDoubleProxyArray<T>(string name, ref T[] value) where T : IReadProxy<double>, new();
		bool OptionalDoubleProxyMap<T,U>(string name, ref T value) where T : IDictionary<string,U>, new() where U : IReadProxy<double>, new();
		void RequiredDouble(string name, ref double value);
		void RequiredDoubleList<T>(string name, ref T value) where T : ICollection<double>, new();
		void RequiredDoubleArray(string name, ref double[] value);
		void RequiredDoubleMap<T>(string name, ref T value) where T : IDictionary<string,double>, new();
		void RequiredDoubleProxy<T>(string name, ref T value) where T : IReadProxy<double>, new();
		void RequiredDoubleProxyList<T,U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<double>, new();
		void RequiredDoubleProxyArray<T>(string name, ref T[] value) where T : IReadProxy<double>, new();
		void RequiredDoubleProxyMap<T,U>(string name, ref T value) where T : IDictionary<string, U>, new() where U : IReadProxy<double>, new();
		// byte
		bool OptionalByte(string name, ref byte value);
		bool OptionalByteList<T>(string name, ref T value) where T : ICollection<byte>, new();
		bool OptionalByteArray(string name, ref byte[] value);
		bool OptionalByteMap<T>(string name, ref T value) where T : IDictionary<string,byte>, new();
		bool OptionalByteProxy<T>(string name, ref T value) where T : IReadProxy<byte>, new();
		bool OptionalByteProxyList<T,U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<byte>, new();
		bool OptionalByteProxyArray<T>(string name, ref T[] value) where T : IReadProxy<byte>, new();
		bool OptionalByteProxyMap<T,U>(string name, ref T value) where T : IDictionary<string,U>, new() where U : IReadProxy<byte>, new();
		void RequiredByte(string name, ref byte value);
		void RequiredByteList<T>(string name, ref T value) where T : ICollection<byte>, new();
		void RequiredByteArray(string name, ref byte[] value);
		void RequiredByteMap<T>(string name, ref T value) where T : IDictionary<string,byte>, new();
		void RequiredByteProxy<T>(string name, ref T value) where T : IReadProxy<byte>, new();
		void RequiredByteProxyList<T,U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<byte>, new();
		void RequiredByteProxyArray<T>(string name, ref T[] value) where T : IReadProxy<byte>, new();
		void RequiredByteProxyMap<T,U>(string name, ref T value) where T : IDictionary<string, U>, new() where U : IReadProxy<byte>, new();
		// sbyte
		bool OptionalSByte(string name, ref sbyte value);
		bool OptionalSByteList<T>(string name, ref T value) where T : ICollection<sbyte>, new();
		bool OptionalSByteArray(string name, ref sbyte[] value);
		bool OptionalSByteMap<T>(string name, ref T value) where T : IDictionary<string,sbyte>, new();
		bool OptionalSByteProxy<T>(string name, ref T value) where T : IReadProxy<sbyte>, new();
		bool OptionalSByteProxyList<T,U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<sbyte>, new();
		bool OptionalSByteProxyArray<T>(string name, ref T[] value) where T : IReadProxy<sbyte>, new();
		bool OptionalSByteProxyMap<T,U>(string name, ref T value) where T : IDictionary<string,U>, new() where U : IReadProxy<sbyte>, new();
		void RequiredSByte(string name, ref sbyte value);
		void RequiredSByteList<T>(string name, ref T value) where T : ICollection<sbyte>, new();
		void RequiredSByteArray(string name, ref sbyte[] value);
		void RequiredSByteMap<T>(string name, ref T value) where T : IDictionary<string,sbyte>, new();
		void RequiredSByteProxy<T>(string name, ref T value) where T : IReadProxy<sbyte>, new();
		void RequiredSByteProxyList<T,U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<sbyte>, new();
		void RequiredSByteProxyArray<T>(string name, ref T[] value) where T : IReadProxy<sbyte>, new();
		void RequiredSByteProxyMap<T,U>(string name, ref T value) where T : IDictionary<string, U>, new() where U : IReadProxy<sbyte>, new();
		// bool
		bool OptionalBool(string name, ref bool value);
		bool OptionalBoolList<T>(string name, ref T value) where T : ICollection<bool>, new();
		bool OptionalBoolArray(string name, ref bool[] value);
		bool OptionalBoolMap<T>(string name, ref T value) where T : IDictionary<string,bool>, new();
		bool OptionalBoolProxy<T>(string name, ref T value) where T : IReadProxy<bool>, new();
		bool OptionalBoolProxyList<T,U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<bool>, new();
		bool OptionalBoolProxyArray<T>(string name, ref T[] value) where T : IReadProxy<bool>, new();
		bool OptionalBoolProxyMap<T,U>(string name, ref T value) where T : IDictionary<string,U>, new() where U : IReadProxy<bool>, new();
		void RequiredBool(string name, ref bool value);
		void RequiredBoolList<T>(string name, ref T value) where T : ICollection<bool>, new();
		void RequiredBoolArray(string name, ref bool[] value);
		void RequiredBoolMap<T>(string name, ref T value) where T : IDictionary<string,bool>, new();
		void RequiredBoolProxy<T>(string name, ref T value) where T : IReadProxy<bool>, new();
		void RequiredBoolProxyList<T,U>(string name, ref T value) where T : ICollection<U>, new() where U : IReadProxy<bool>, new();
		void RequiredBoolProxyArray<T>(string name, ref T[] value) where T : IReadProxy<bool>, new();
		void RequiredBoolProxyMap<T,U>(string name, ref T value) where T : IDictionary<string, U>, new() where U : IReadProxy<bool>, new();
		// Vector2
		bool OptionalVector2(string name, ref Vector2 value);
		bool OptionalVector2List<T>(string name, ref T value) where T : ICollection<Vector2>, new();
		bool OptionalVector2Array(string name, ref Vector2[] value);
		bool OptionalVector2Map<T>(string name, ref T value) where T : IDictionary<string,Vector2>, new();
		void RequiredVector2(string name, ref Vector2 value);
		void RequiredVector2List<T>(string name, ref T value) where T : ICollection<Vector2>, new();
		void RequiredVector2Array(string name, ref Vector2[] value);
		void RequiredVector2Map<T>(string name, ref T value) where T : IDictionary<string,Vector2>, new();
		// Vector3
		bool OptionalVector3(string name, ref Vector3 value);
		bool OptionalVector3List<T>(string name, ref T value) where T : ICollection<Vector3>, new();
		bool OptionalVector3Array(string name, ref Vector3[] value);
		bool OptionalVector3Map<T>(string name, ref T value) where T : IDictionary<string,Vector3>, new();
		void RequiredVector3(string name, ref Vector3 value);
		void RequiredVector3List<T>(string name, ref T value) where T : ICollection<Vector3>, new();
		void RequiredVector3Array(string name, ref Vector3[] value);
		void RequiredVector3Map<T>(string name, ref T value) where T : IDictionary<string,Vector3>, new();
		// Vector4
		bool OptionalVector4(string name, ref Vector4 value);
		bool OptionalVector4List<T>(string name, ref T value) where T : ICollection<Vector4>, new();
		bool OptionalVector4Array(string name, ref Vector4[] value);
		bool OptionalVector4Map<T>(string name, ref T value) where T : IDictionary<string,Vector4>, new();
		void RequiredVector4(string name, ref Vector4 value);
		void RequiredVector4List<T>(string name, ref T value) where T : ICollection<Vector4>, new();
		void RequiredVector4Array(string name, ref Vector4[] value);
		void RequiredVector4Map<T>(string name, ref T value) where T : IDictionary<string,Vector4>, new();
		// Quaternion
		bool OptionalQuaternion(string name, ref Quaternion value);
		bool OptionalQuaternionList<T>(string name, ref T value) where T : ICollection<Quaternion>, new();
		bool OptionalQuaternionArray(string name, ref Quaternion[] value);
		bool OptionalQuaternionMap<T>(string name, ref T value) where T : IDictionary<string,Quaternion>, new();
		void RequiredQuaternion(string name, ref Quaternion value);
		void RequiredQuaternionList<T>(string name, ref T value) where T : ICollection<Quaternion>, new();
		void RequiredQuaternionArray(string name, ref Quaternion[] value);
		void RequiredQuaternionMap<T>(string name, ref T value) where T : IDictionary<string,Quaternion>, new();
		// Vector2Int
		bool OptionalVector2Int(string name, ref Vector2Int value);
		bool OptionalVector2IntList<T>(string name, ref T value) where T : ICollection<Vector2Int>, new();
		bool OptionalVector2IntArray(string name, ref Vector2Int[] value);
		bool OptionalVector2IntMap<T>(string name, ref T value) where T : IDictionary<string,Vector2Int>, new();
		void RequiredVector2Int(string name, ref Vector2Int value);
		void RequiredVector2IntList<T>(string name, ref T value) where T : ICollection<Vector2Int>, new();
		void RequiredVector2IntArray(string name, ref Vector2Int[] value);
		void RequiredVector2IntMap<T>(string name, ref T value) where T : IDictionary<string,Vector2Int>, new();
		// Vector3Int
		bool OptionalVector3Int(string name, ref Vector3Int value);
		bool OptionalVector3IntList<T>(string name, ref T value) where T : ICollection<Vector3Int>, new();
		bool OptionalVector3IntArray(string name, ref Vector3Int[] value);
		bool OptionalVector3IntMap<T>(string name, ref T value) where T : IDictionary<string,Vector3Int>, new();
		void RequiredVector3Int(string name, ref Vector3Int value);
		void RequiredVector3IntList<T>(string name, ref T value) where T : ICollection<Vector3Int>, new();
		void RequiredVector3IntArray(string name, ref Vector3Int[] value);
		void RequiredVector3IntMap<T>(string name, ref T value) where T : IDictionary<string,Vector3Int>, new();
		// Rect
		bool OptionalRect(string name, ref Rect value);
		bool OptionalRectList<T>(string name, ref T value) where T : ICollection<Rect>, new();
		bool OptionalRectArray(string name, ref Rect[] value);
		bool OptionalRectMap<T>(string name, ref T value) where T : IDictionary<string,Rect>, new();
		void RequiredRect(string name, ref Rect value);
		void RequiredRectList<T>(string name, ref T value) where T : ICollection<Rect>, new();
		void RequiredRectArray(string name, ref Rect[] value);
		void RequiredRectMap<T>(string name, ref T value) where T : IDictionary<string,Rect>, new();
		// RectInt
		bool OptionalRectInt(string name, ref RectInt value);
		bool OptionalRectIntList<T>(string name, ref T value) where T : ICollection<RectInt>, new();
		bool OptionalRectIntArray(string name, ref RectInt[] value);
		bool OptionalRectIntMap<T>(string name, ref T value) where T : IDictionary<string,RectInt>, new();
		void RequiredRectInt(string name, ref RectInt value);
		void RequiredRectIntList<T>(string name, ref T value) where T : ICollection<RectInt>, new();
		void RequiredRectIntArray(string name, ref RectInt[] value);
		void RequiredRectIntMap<T>(string name, ref T value) where T : IDictionary<string,RectInt>, new();
		// Color
		bool OptionalColor(string name, ref Color value);
		bool OptionalColorList<T>(string name, ref T value) where T : ICollection<Color>, new();
		bool OptionalColorArray(string name, ref Color[] value);
		bool OptionalColorMap<T>(string name, ref T value) where T : IDictionary<string,Color>, new();
		void RequiredColor(string name, ref Color value);
		void RequiredColorList<T>(string name, ref T value) where T : ICollection<Color>, new();
		void RequiredColorArray(string name, ref Color[] value);
		void RequiredColorMap<T>(string name, ref T value) where T : IDictionary<string,Color>, new();
		// Color32
		bool OptionalColor32(string name, ref Color32 value);
		bool OptionalColor32List<T>(string name, ref T value) where T : ICollection<Color32>, new();
		bool OptionalColor32Array(string name, ref Color32[] value);
		bool OptionalColor32Map<T>(string name, ref T value) where T : IDictionary<string,Color32>, new();
		void RequiredColor32(string name, ref Color32 value);
		void RequiredColor32List<T>(string name, ref T value) where T : ICollection<Color32>, new();
		void RequiredColor32Array(string name, ref Color32[] value);
		void RequiredColor32Map<T>(string name, ref T value) where T : IDictionary<string,Color32>, new();
	}
}
