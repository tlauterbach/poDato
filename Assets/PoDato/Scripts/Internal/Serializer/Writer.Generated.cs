// Generated from ReaderGenerator.cs
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PoDato {
	internal sealed partial class Writer : IWriter {
		public void Write(string name, string value) {
			DoValue(name, value, StringToTater);
		}
		public void Write(string name, IEnumerable<string> value) {
			DoCollection(name, value, StringToTater);
		}
		public void Write(string name, IDictionary<string,string> value) {
			DoDictionary(name, value, StringToTater);
		}
		public void Write(string name, IWriteProxy<string> value) {
			DoValue(name, value, StringProxyToTater);
		}
		public void WriteStringProxyList<T>(string name, IEnumerable<T> value) where T : IWriteProxy<string> {
			DoCollection(name, value, StringProxyToTater);
		}
		public void WriteStringProxyMap<T>(string name, IDictionary<string,T> value) where T : IWriteProxy<string> {
			DoDictionary(name, value, StringProxyToTater);
		}
		public void Write(string name, int value) {
			DoValue(name, value, Int32ToTater);
		}
		public void Write(string name, IEnumerable<int> value) {
			DoCollection(name, value, Int32ToTater);
		}
		public void Write(string name, IDictionary<string,int> value) {
			DoDictionary(name, value, Int32ToTater);
		}
		public void Write(string name, IWriteProxy<int> value) {
			DoValue(name, value, Int32ProxyToTater);
		}
		public void WriteInt32ProxyList<T>(string name, IEnumerable<T> value) where T : IWriteProxy<int> {
			DoCollection(name, value, Int32ProxyToTater);
		}
		public void WriteInt32ProxyMap<T>(string name, IDictionary<string,T> value) where T : IWriteProxy<int> {
			DoDictionary(name, value, Int32ProxyToTater);
		}
		public void Write(string name, short value) {
			DoValue(name, value, Int16ToTater);
		}
		public void Write(string name, IEnumerable<short> value) {
			DoCollection(name, value, Int16ToTater);
		}
		public void Write(string name, IDictionary<string,short> value) {
			DoDictionary(name, value, Int16ToTater);
		}
		public void Write(string name, IWriteProxy<short> value) {
			DoValue(name, value, Int16ProxyToTater);
		}
		public void WriteInt16ProxyList<T>(string name, IEnumerable<T> value) where T : IWriteProxy<short> {
			DoCollection(name, value, Int16ProxyToTater);
		}
		public void WriteInt16ProxyMap<T>(string name, IDictionary<string,T> value) where T : IWriteProxy<short> {
			DoDictionary(name, value, Int16ProxyToTater);
		}
		public void Write(string name, long value) {
			DoValue(name, value, Int64ToTater);
		}
		public void Write(string name, IEnumerable<long> value) {
			DoCollection(name, value, Int64ToTater);
		}
		public void Write(string name, IDictionary<string,long> value) {
			DoDictionary(name, value, Int64ToTater);
		}
		public void Write(string name, IWriteProxy<long> value) {
			DoValue(name, value, Int64ProxyToTater);
		}
		public void WriteInt64ProxyList<T>(string name, IEnumerable<T> value) where T : IWriteProxy<long> {
			DoCollection(name, value, Int64ProxyToTater);
		}
		public void WriteInt64ProxyMap<T>(string name, IDictionary<string,T> value) where T : IWriteProxy<long> {
			DoDictionary(name, value, Int64ProxyToTater);
		}
		public void Write(string name, uint value) {
			DoValue(name, value, UInt32ToTater);
		}
		public void Write(string name, IEnumerable<uint> value) {
			DoCollection(name, value, UInt32ToTater);
		}
		public void Write(string name, IDictionary<string,uint> value) {
			DoDictionary(name, value, UInt32ToTater);
		}
		public void Write(string name, IWriteProxy<uint> value) {
			DoValue(name, value, UInt32ProxyToTater);
		}
		public void WriteUInt32ProxyList<T>(string name, IEnumerable<T> value) where T : IWriteProxy<uint> {
			DoCollection(name, value, UInt32ProxyToTater);
		}
		public void WriteUInt32ProxyMap<T>(string name, IDictionary<string,T> value) where T : IWriteProxy<uint> {
			DoDictionary(name, value, UInt32ProxyToTater);
		}
		public void Write(string name, ushort value) {
			DoValue(name, value, UInt16ToTater);
		}
		public void Write(string name, IEnumerable<ushort> value) {
			DoCollection(name, value, UInt16ToTater);
		}
		public void Write(string name, IDictionary<string,ushort> value) {
			DoDictionary(name, value, UInt16ToTater);
		}
		public void Write(string name, IWriteProxy<ushort> value) {
			DoValue(name, value, UInt16ProxyToTater);
		}
		public void WriteUInt16ProxyList<T>(string name, IEnumerable<T> value) where T : IWriteProxy<ushort> {
			DoCollection(name, value, UInt16ProxyToTater);
		}
		public void WriteUInt16ProxyMap<T>(string name, IDictionary<string,T> value) where T : IWriteProxy<ushort> {
			DoDictionary(name, value, UInt16ProxyToTater);
		}
		public void Write(string name, ulong value) {
			DoValue(name, value, UInt64ToTater);
		}
		public void Write(string name, IEnumerable<ulong> value) {
			DoCollection(name, value, UInt64ToTater);
		}
		public void Write(string name, IDictionary<string,ulong> value) {
			DoDictionary(name, value, UInt64ToTater);
		}
		public void Write(string name, IWriteProxy<ulong> value) {
			DoValue(name, value, UInt64ProxyToTater);
		}
		public void WriteUInt64ProxyList<T>(string name, IEnumerable<T> value) where T : IWriteProxy<ulong> {
			DoCollection(name, value, UInt64ProxyToTater);
		}
		public void WriteUInt64ProxyMap<T>(string name, IDictionary<string,T> value) where T : IWriteProxy<ulong> {
			DoDictionary(name, value, UInt64ProxyToTater);
		}
		public void Write(string name, float value) {
			DoValue(name, value, SingleToTater);
		}
		public void Write(string name, IEnumerable<float> value) {
			DoCollection(name, value, SingleToTater);
		}
		public void Write(string name, IDictionary<string,float> value) {
			DoDictionary(name, value, SingleToTater);
		}
		public void Write(string name, IWriteProxy<float> value) {
			DoValue(name, value, SingleProxyToTater);
		}
		public void WriteSingleProxyList<T>(string name, IEnumerable<T> value) where T : IWriteProxy<float> {
			DoCollection(name, value, SingleProxyToTater);
		}
		public void WriteSingleProxyMap<T>(string name, IDictionary<string,T> value) where T : IWriteProxy<float> {
			DoDictionary(name, value, SingleProxyToTater);
		}
		public void Write(string name, double value) {
			DoValue(name, value, DoubleToTater);
		}
		public void Write(string name, IEnumerable<double> value) {
			DoCollection(name, value, DoubleToTater);
		}
		public void Write(string name, IDictionary<string,double> value) {
			DoDictionary(name, value, DoubleToTater);
		}
		public void Write(string name, IWriteProxy<double> value) {
			DoValue(name, value, DoubleProxyToTater);
		}
		public void WriteDoubleProxyList<T>(string name, IEnumerable<T> value) where T : IWriteProxy<double> {
			DoCollection(name, value, DoubleProxyToTater);
		}
		public void WriteDoubleProxyMap<T>(string name, IDictionary<string,T> value) where T : IWriteProxy<double> {
			DoDictionary(name, value, DoubleProxyToTater);
		}
		public void Write(string name, byte value) {
			DoValue(name, value, ByteToTater);
		}
		public void Write(string name, IEnumerable<byte> value) {
			DoCollection(name, value, ByteToTater);
		}
		public void Write(string name, IDictionary<string,byte> value) {
			DoDictionary(name, value, ByteToTater);
		}
		public void Write(string name, IWriteProxy<byte> value) {
			DoValue(name, value, ByteProxyToTater);
		}
		public void WriteByteProxyList<T>(string name, IEnumerable<T> value) where T : IWriteProxy<byte> {
			DoCollection(name, value, ByteProxyToTater);
		}
		public void WriteByteProxyMap<T>(string name, IDictionary<string,T> value) where T : IWriteProxy<byte> {
			DoDictionary(name, value, ByteProxyToTater);
		}
		public void Write(string name, sbyte value) {
			DoValue(name, value, SByteToTater);
		}
		public void Write(string name, IEnumerable<sbyte> value) {
			DoCollection(name, value, SByteToTater);
		}
		public void Write(string name, IDictionary<string,sbyte> value) {
			DoDictionary(name, value, SByteToTater);
		}
		public void Write(string name, IWriteProxy<sbyte> value) {
			DoValue(name, value, SByteProxyToTater);
		}
		public void WriteSByteProxyList<T>(string name, IEnumerable<T> value) where T : IWriteProxy<sbyte> {
			DoCollection(name, value, SByteProxyToTater);
		}
		public void WriteSByteProxyMap<T>(string name, IDictionary<string,T> value) where T : IWriteProxy<sbyte> {
			DoDictionary(name, value, SByteProxyToTater);
		}
		public void Write(string name, bool value) {
			DoValue(name, value, BoolToTater);
		}
		public void Write(string name, IEnumerable<bool> value) {
			DoCollection(name, value, BoolToTater);
		}
		public void Write(string name, IDictionary<string,bool> value) {
			DoDictionary(name, value, BoolToTater);
		}
		public void Write(string name, IWriteProxy<bool> value) {
			DoValue(name, value, BoolProxyToTater);
		}
		public void WriteBoolProxyList<T>(string name, IEnumerable<T> value) where T : IWriteProxy<bool> {
			DoCollection(name, value, BoolProxyToTater);
		}
		public void WriteBoolProxyMap<T>(string name, IDictionary<string,T> value) where T : IWriteProxy<bool> {
			DoDictionary(name, value, BoolProxyToTater);
		}
		public void Write(string name, Vector2 value) {
			DoValue(name, value, Vector2ToTater);
		}
		public void Write(string name, IEnumerable<Vector2> value) {
			DoCollection(name, value, Vector2ToTater);
		}
		public void Write(string name, IDictionary<string,Vector2> value) {
			DoDictionary(name, value, Vector2ToTater);
		}
		public void Write(string name, Vector3 value) {
			DoValue(name, value, Vector3ToTater);
		}
		public void Write(string name, IEnumerable<Vector3> value) {
			DoCollection(name, value, Vector3ToTater);
		}
		public void Write(string name, IDictionary<string,Vector3> value) {
			DoDictionary(name, value, Vector3ToTater);
		}
		public void Write(string name, Vector4 value) {
			DoValue(name, value, Vector4ToTater);
		}
		public void Write(string name, IEnumerable<Vector4> value) {
			DoCollection(name, value, Vector4ToTater);
		}
		public void Write(string name, IDictionary<string,Vector4> value) {
			DoDictionary(name, value, Vector4ToTater);
		}
		public void Write(string name, Quaternion value) {
			DoValue(name, value, QuaternionToTater);
		}
		public void Write(string name, IEnumerable<Quaternion> value) {
			DoCollection(name, value, QuaternionToTater);
		}
		public void Write(string name, IDictionary<string,Quaternion> value) {
			DoDictionary(name, value, QuaternionToTater);
		}
		public void Write(string name, Vector2Int value) {
			DoValue(name, value, Vector2IntToTater);
		}
		public void Write(string name, IEnumerable<Vector2Int> value) {
			DoCollection(name, value, Vector2IntToTater);
		}
		public void Write(string name, IDictionary<string,Vector2Int> value) {
			DoDictionary(name, value, Vector2IntToTater);
		}
		public void Write(string name, Vector3Int value) {
			DoValue(name, value, Vector3IntToTater);
		}
		public void Write(string name, IEnumerable<Vector3Int> value) {
			DoCollection(name, value, Vector3IntToTater);
		}
		public void Write(string name, IDictionary<string,Vector3Int> value) {
			DoDictionary(name, value, Vector3IntToTater);
		}
		public void Write(string name, Rect value) {
			DoValue(name, value, RectToTater);
		}
		public void Write(string name, IEnumerable<Rect> value) {
			DoCollection(name, value, RectToTater);
		}
		public void Write(string name, IDictionary<string,Rect> value) {
			DoDictionary(name, value, RectToTater);
		}
		public void Write(string name, RectInt value) {
			DoValue(name, value, RectIntToTater);
		}
		public void Write(string name, IEnumerable<RectInt> value) {
			DoCollection(name, value, RectIntToTater);
		}
		public void Write(string name, IDictionary<string,RectInt> value) {
			DoDictionary(name, value, RectIntToTater);
		}
		public void Write(string name, Color value) {
			DoValue(name, value, ColorToTater);
		}
		public void Write(string name, IEnumerable<Color> value) {
			DoCollection(name, value, ColorToTater);
		}
		public void Write(string name, IDictionary<string,Color> value) {
			DoDictionary(name, value, ColorToTater);
		}
		public void Write(string name, Color32 value) {
			DoValue(name, value, Color32ToTater);
		}
		public void Write(string name, IEnumerable<Color32> value) {
			DoCollection(name, value, Color32ToTater);
		}
		public void Write(string name, IDictionary<string,Color32> value) {
			DoDictionary(name, value, Color32ToTater);
		}
	}
}
