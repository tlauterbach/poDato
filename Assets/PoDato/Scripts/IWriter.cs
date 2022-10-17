// Generated from ReaderGenerator.cs
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PoDato {
	public interface IWriter {
		void Write(string name, IWritable value);
		void Write(string name, IEnumerable<IWritable> value);
		void Write(string name, IDictionary<string, IWritable> value);
		void Write(string name, Enum value);
		void Write(string name, IEnumerable<Enum> value);
		void Write(string name, IDictionary<string, Enum> value);
		// string
		void Write(string name, string value);
		void Write(string name, IEnumerable<string> value);
		void Write(string name, IDictionary<string,string> value);
		void Write(string name, IWriteProxy<string> value);
		void WriteStringProxyList<T>(string name, IEnumerable<T> value) where T : IWriteProxy<string>;
		void WriteStringProxyMap<T>(string name, IDictionary<string,T> value) where T : IWriteProxy<string>;
		// int
		void Write(string name, int value);
		void Write(string name, IEnumerable<int> value);
		void Write(string name, IDictionary<string,int> value);
		void Write(string name, IWriteProxy<int> value);
		void WriteInt32ProxyList<T>(string name, IEnumerable<T> value) where T : IWriteProxy<int>;
		void WriteInt32ProxyMap<T>(string name, IDictionary<string,T> value) where T : IWriteProxy<int>;
		// short
		void Write(string name, short value);
		void Write(string name, IEnumerable<short> value);
		void Write(string name, IDictionary<string,short> value);
		void Write(string name, IWriteProxy<short> value);
		void WriteInt16ProxyList<T>(string name, IEnumerable<T> value) where T : IWriteProxy<short>;
		void WriteInt16ProxyMap<T>(string name, IDictionary<string,T> value) where T : IWriteProxy<short>;
		// long
		void Write(string name, long value);
		void Write(string name, IEnumerable<long> value);
		void Write(string name, IDictionary<string,long> value);
		void Write(string name, IWriteProxy<long> value);
		void WriteInt64ProxyList<T>(string name, IEnumerable<T> value) where T : IWriteProxy<long>;
		void WriteInt64ProxyMap<T>(string name, IDictionary<string,T> value) where T : IWriteProxy<long>;
		// uint
		void Write(string name, uint value);
		void Write(string name, IEnumerable<uint> value);
		void Write(string name, IDictionary<string,uint> value);
		void Write(string name, IWriteProxy<uint> value);
		void WriteUInt32ProxyList<T>(string name, IEnumerable<T> value) where T : IWriteProxy<uint>;
		void WriteUInt32ProxyMap<T>(string name, IDictionary<string,T> value) where T : IWriteProxy<uint>;
		// ushort
		void Write(string name, ushort value);
		void Write(string name, IEnumerable<ushort> value);
		void Write(string name, IDictionary<string,ushort> value);
		void Write(string name, IWriteProxy<ushort> value);
		void WriteUInt16ProxyList<T>(string name, IEnumerable<T> value) where T : IWriteProxy<ushort>;
		void WriteUInt16ProxyMap<T>(string name, IDictionary<string,T> value) where T : IWriteProxy<ushort>;
		// ulong
		void Write(string name, ulong value);
		void Write(string name, IEnumerable<ulong> value);
		void Write(string name, IDictionary<string,ulong> value);
		void Write(string name, IWriteProxy<ulong> value);
		void WriteUInt64ProxyList<T>(string name, IEnumerable<T> value) where T : IWriteProxy<ulong>;
		void WriteUInt64ProxyMap<T>(string name, IDictionary<string,T> value) where T : IWriteProxy<ulong>;
		// float
		void Write(string name, float value);
		void Write(string name, IEnumerable<float> value);
		void Write(string name, IDictionary<string,float> value);
		void Write(string name, IWriteProxy<float> value);
		void WriteSingleProxyList<T>(string name, IEnumerable<T> value) where T : IWriteProxy<float>;
		void WriteSingleProxyMap<T>(string name, IDictionary<string,T> value) where T : IWriteProxy<float>;
		// double
		void Write(string name, double value);
		void Write(string name, IEnumerable<double> value);
		void Write(string name, IDictionary<string,double> value);
		void Write(string name, IWriteProxy<double> value);
		void WriteDoubleProxyList<T>(string name, IEnumerable<T> value) where T : IWriteProxy<double>;
		void WriteDoubleProxyMap<T>(string name, IDictionary<string,T> value) where T : IWriteProxy<double>;
		// byte
		void Write(string name, byte value);
		void Write(string name, IEnumerable<byte> value);
		void Write(string name, IDictionary<string,byte> value);
		void Write(string name, IWriteProxy<byte> value);
		void WriteByteProxyList<T>(string name, IEnumerable<T> value) where T : IWriteProxy<byte>;
		void WriteByteProxyMap<T>(string name, IDictionary<string,T> value) where T : IWriteProxy<byte>;
		// sbyte
		void Write(string name, sbyte value);
		void Write(string name, IEnumerable<sbyte> value);
		void Write(string name, IDictionary<string,sbyte> value);
		void Write(string name, IWriteProxy<sbyte> value);
		void WriteSByteProxyList<T>(string name, IEnumerable<T> value) where T : IWriteProxy<sbyte>;
		void WriteSByteProxyMap<T>(string name, IDictionary<string,T> value) where T : IWriteProxy<sbyte>;
		// bool
		void Write(string name, bool value);
		void Write(string name, IEnumerable<bool> value);
		void Write(string name, IDictionary<string,bool> value);
		void Write(string name, IWriteProxy<bool> value);
		void WriteBoolProxyList<T>(string name, IEnumerable<T> value) where T : IWriteProxy<bool>;
		void WriteBoolProxyMap<T>(string name, IDictionary<string,T> value) where T : IWriteProxy<bool>;
		// Vector2
		void Write(string name, Vector2 value);
		void Write(string name, IEnumerable<Vector2> value);
		void Write(string name, IDictionary<string,Vector2> value);
		// Vector3
		void Write(string name, Vector3 value);
		void Write(string name, IEnumerable<Vector3> value);
		void Write(string name, IDictionary<string,Vector3> value);
		// Vector4
		void Write(string name, Vector4 value);
		void Write(string name, IEnumerable<Vector4> value);
		void Write(string name, IDictionary<string,Vector4> value);
		// Quaternion
		void Write(string name, Quaternion value);
		void Write(string name, IEnumerable<Quaternion> value);
		void Write(string name, IDictionary<string,Quaternion> value);
		// Vector2Int
		void Write(string name, Vector2Int value);
		void Write(string name, IEnumerable<Vector2Int> value);
		void Write(string name, IDictionary<string,Vector2Int> value);
		// Vector3Int
		void Write(string name, Vector3Int value);
		void Write(string name, IEnumerable<Vector3Int> value);
		void Write(string name, IDictionary<string,Vector3Int> value);
		// Rect
		void Write(string name, Rect value);
		void Write(string name, IEnumerable<Rect> value);
		void Write(string name, IDictionary<string,Rect> value);
		// RectInt
		void Write(string name, RectInt value);
		void Write(string name, IEnumerable<RectInt> value);
		void Write(string name, IDictionary<string,RectInt> value);
		// Color
		void Write(string name, Color value);
		void Write(string name, IEnumerable<Color> value);
		void Write(string name, IDictionary<string,Color> value);
		// Color32
		void Write(string name, Color32 value);
		void Write(string name, IEnumerable<Color32> value);
		void Write(string name, IDictionary<string,Color32> value);
	}
}
