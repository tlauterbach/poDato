using System;
using System.Collections.Generic;
using UnityEngine;

namespace PoDato {

	public interface IWriter {

		// IWritable
		void Write(string name, IWritable value);
		void Write(string name, IEnumerable<IWritable> value);
		void Write(string name, IDictionary<string, IWritable> value);

		// enum
		void Write(string name, Enum value);
		void Write(string name, IEnumerable<Enum> value);
		void Write(string name, IDictionary<string, Enum> value);

		// string
		void Write(string name, string value);
		void Write(string name, IEnumerable<string> value);
		void Write(string name, IDictionary<string, string> value);
		void Write(string name, IWriteProxy<string> value);
		void Write(string name, IEnumerable<IWriteProxy<string>> value);
		void Write(string name, IDictionary<string, IWriteProxy<string>> value);

		// int
		void Write(string name, int value);
		void Write(string name, IEnumerable<int> value);
		void Write(string name, IDictionary<string, int> value);
		void Write(string name, IWriteProxy<int> value);
		void Write(string name, IEnumerable<IWriteProxy<int>> value);
		void Write(string name, IDictionary<string, IWriteProxy<int>> value);

		// uint
		void Write(string name, uint value);
		void Write(string name, IEnumerable<uint> value);
		void Write(string name, IDictionary<string, uint> value);
		void Write(string name, IWriteProxy<uint> value);
		void Write(string name, IEnumerable<IWriteProxy<uint>> value);
		void Write(string name, IDictionary<string, IWriteProxy<uint>> value);

		// short
		void Write(string name, short value);
		void Write(string name, IEnumerable<short> value);
		void Write(string name, IDictionary<string, short> value);
		void Write(string name, IWriteProxy<short> value);
		void Write(string name, IEnumerable<IWriteProxy<short>> value);
		void Write(string name, IDictionary<string, IWriteProxy<short>> value);

		// ushort
		void Write(string name, ushort value);
		void Write(string name, IEnumerable<ushort> value);
		void Write(string name, IDictionary<string, ushort> value);
		void Write(string name, IWriteProxy<ushort> value);
		void Write(string name, IEnumerable<IWriteProxy<ushort>> value);
		void Write(string name, IDictionary<string, IWriteProxy<ushort>> value);

		// long
		void Write(string name, long value);
		void Write(string name, IEnumerable<long> value);
		void Write(string name, IDictionary<string, long> value);
		void Write(string name, IWriteProxy<long> value);
		void Write(string name, IEnumerable<IWriteProxy<long>> value);
		void Write(string name, IDictionary<string, IWriteProxy<long>> value);

		// ulong
		void Write(string name, ulong value);
		void Write(string name, IEnumerable<ulong> value);
		void Write(string name, IDictionary<string, ulong> value);
		void Write(string name, IWriteProxy<ulong> value);
		void Write(string name, IEnumerable<IWriteProxy<ulong>> value);
		void Write(string name, IDictionary<string, IWriteProxy<ulong>> value);

		// double
		void Write(string name, double value);
		void Write(string name, IEnumerable<double> value);
		void Write(string name, IDictionary<string, double> value);
		void Write(string name, IWriteProxy<double> value);
		void Write(string name, IEnumerable<IWriteProxy<double>> value);
		void Write(string name, IDictionary<string, IWriteProxy<double>> value);

		// float
		void Write(string name, float value);
		void Write(string name, IEnumerable<float> value);
		void Write(string name, IDictionary<string, float> value);
		void Write(string name, IWriteProxy<float> value);
		void Write(string name, IEnumerable<IWriteProxy<float>> value);
		void Write(string name, IDictionary<string, IWriteProxy<float>> value);

		// byte
		void Write(string name, byte value);
		void Write(string name, IEnumerable<byte> value);
		void Write(string name, IDictionary<string, byte> value);
		void Write(string name, IWriteProxy<byte> value);
		void Write(string name, IEnumerable<IWriteProxy<byte>> value);
		void Write(string name, IDictionary<string, IWriteProxy<byte>> value);

		// sbyte
		void Write(string name, sbyte value);
		void Write(string name, IEnumerable<sbyte> value);
		void Write(string name, IDictionary<string, sbyte> value);
		void Write(string name, IWriteProxy<sbyte> value);
		void Write(string name, IEnumerable<IWriteProxy<sbyte>> value);
		void Write(string name, IDictionary<string, IWriteProxy<sbyte>> value);

		// bool
		void Write(string name, bool value);
		void Write(string name, IEnumerable<bool> value);
		void Write(string name, IDictionary<string, bool> value);
		void Write(string name, IWriteProxy<bool> value);
		void Write(string name, IEnumerable<IWriteProxy<bool>> value);
		void Write(string name, IDictionary<string, IWriteProxy<bool>> value);

		// Vector2
		void Write(string name, Vector2 value);
		void Write(string name, IEnumerable<Vector2> value);
		void Write(string name, IDictionary<string, Vector2> value);

		// Vector3
		void Write(string name, Vector3 value);
		void Write(string name, IEnumerable<Vector3> value);
		void Write(string name, IDictionary<string, Vector3> value);

		// Vector4
		void Write(string name, Vector4 value);
		void Write(string name, IEnumerable<Vector4> value);
		void Write(string name, IDictionary<string, Vector4> value);

		// Vector2Int
		void Write(string name, Vector2Int value);
		void Write(string name, IEnumerable<Vector2Int> value);
		void Write(string name, IDictionary<string, Vector2Int> value);

		// Vector3Int
		void Write(string name, Vector3Int value);
		void Write(string name, IEnumerable<Vector3Int> value);
		void Write(string name, IDictionary<string, Vector3Int> value);

		// Quaternion
		void Write(string name, Quaternion value);
		void Write(string name, IEnumerable<Quaternion> value);
		void Write(string name, IDictionary<string, Quaternion> value);

		// Color
		void Write(string name, Color value);
		void Write(string name, IEnumerable<Color> value);
		void Write(string name, IDictionary<string, Color> value);

		// Color32
		void Write(string name, Color32 value);
		void Write(string name, IEnumerable<Color32> value);
		void Write(string name, IDictionary<string, Color32> value);

		// Rect
		void Write(string name, Rect value);
		void Write(string name, IEnumerable<Rect> value);
		void Write(string name, IDictionary<string, Rect> value);

		// RectInt
		void Write(string name, RectInt value);
		void Write(string name, IEnumerable<RectInt> value);
		void Write(string name, IDictionary<string, RectInt> value);

	}

}