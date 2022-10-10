using PoDato;
using System;

public class Evidence : IReadable {

	private struct EvidenceID : IEquatable<EvidenceID>, IReadProxy<string> {

		private int m_hash;
		private string m_value;

		public EvidenceID(string value) {
			m_hash = value.GetHashCode();
			m_value = value;
		}
		public bool Equals(EvidenceID obj) {
			return obj.m_hash == m_hash;
		}
		public void SetProxyValue(string value) {
			m_hash = value.GetHashCode();
			m_value = value;
		}
		public override bool Equals(object obj) {
			if (obj is EvidenceID id) {
				return Equals(id);
			} else {
				return false;
			}
		}
		public override int GetHashCode() {
			return m_hash;
		}
		public override string ToString() {
			return m_value;
		}
	}

	private enum EvidenceType {
		Location,
		Motive,
		Weapon,
		Person
	}

	private class ArticleInfo : IReadable {
		private string m_string;
		private bool m_isPlural;

		public void Deserialize(IReader reader) {
			reader.RequiredString("string", ref m_string);
			reader.RequiredBool("isPlural", ref m_isPlural);
		}
	}

	private EvidenceID m_id;
	private string m_name;
	private EvidenceType m_type;
	private string m_prefab;
	private string m_icon;
	private ArticleInfo m_articleUnique;
	private ArticleInfo m_articleGeneric;

	public void Deserialize(IReader reader) {
		reader.RequiredStringProxy("id", ref m_id);
		reader.RequiredString("name", ref m_name);
		reader.RequiredEnum("type", ref m_type);
		reader.RequiredString("prefab", ref m_prefab);
		reader.RequiredString("icon", ref m_icon);
		reader.RequiredObject("articleUnique", ref m_articleUnique);
		reader.RequiredObject("articleGeneric", ref m_articleGeneric);
	}
}

/*
public class ManyTypes : IReadable {

	private string m_string;
	private int m_int32;
	private uint m_uint32;
	private short m_int16;
	private ushort m_uint16;
	private long m_int64;
	private ulong m_uint64;
	private byte m_byte;
	private sbyte m_sbyte;
	private double m_double;
	private float m_single;
	private bool m_boolean;
	private Rect m_rect;
	private Vector2 m_vector2;
	private Vector3 m_vector3;
	private Vector4 m_vector4;
	private Quaternion m_quaternion;
	private Color m_color;

	public void Deserialize(IReader reader) {
		m_string = reader.ReadString("string");
		m_int16 = reader.ReadInt16("int16");
		m_int32 = reader.ReadInt32("int32");
		m_int64 = reader.ReadInt64("int64");
		m_uint16 = reader.ReadUInt16("uint16");
		m_uint32 = reader.ReadUInt32("uint32");
		m_uint64 = reader.ReadUInt64("uint64");
		m_byte = reader.ReadByte("byte");
		m_sbyte = reader.ReadSByte("sbyte");
		m_double = reader.ReadDouble("double");
		m_single = reader.ReadSingle("single");
		m_boolean = reader.ReadBoolean("boolean");
		m_rect = reader.ReadRect("rect");
		m_vector2 = reader.ReadVector2("vector2");
		m_vector3 = reader.ReadVector3("vector3");
		m_vector4 = reader.ReadVector4("vector4");
		m_quaternion = reader.ReadQuaternion("quaternion");
		m_color = reader.ReadColor("color");
	}

}
*/