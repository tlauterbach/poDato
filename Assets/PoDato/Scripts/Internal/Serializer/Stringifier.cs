using System;
using System.Globalization;
using System.Text;

namespace PoDato {

	public class Stringifier {

		private StringBuilder m_builder;
		private string m_tab;
		private int m_depth;

		private static readonly char[] m_reserved = "{[]}:\"\n#".ToCharArray();

		public Stringifier(byte tabSize) {
			m_builder = new StringBuilder();
			m_tab = "";
			while (tabSize-- > 0) {
				m_tab = string.Concat(m_tab, " ");
			}
			m_depth = 0;
		}

		public string Stringify(Tater tater) {
			m_builder.Clear();
			if (tater.IsArray) {
				AppendArray(tater);
			} else if (tater.IsObject) {
				AppendObject(tater);
			} else {
				throw new Exception("Stringify root must be an Array or Object");
			}
			return m_builder.ToString();
		}

		private void AppendObject(Tater tater) {
			m_builder.Append("{\n");
			IncreaseIndent();
			foreach (string key in tater.Keys) {
				AppendIndent();
				m_builder.Append(key).Append(": ");
				AppendValue(tater[key]);
			}
			DecreaseIndent();
			AppendIndent();
			m_builder.Append("}\n");
		}
		private void AppendArray(Tater tater) {
			m_builder.Append("[\n");
			IncreaseIndent();
			for (int ix = 0; ix < tater.Count; ix++) {
				AppendIndent();
				AppendValue(tater[ix]);
			}
			DecreaseIndent();
			AppendIndent();
			m_builder.Append("]\n");
		}
		private void AppendValue(Tater tater) {
			if (tater.IsArray) {
				AppendArray(tater);
			} else if (tater.IsObject) {
				AppendObject(tater);
			} else if (tater.IsNull) {
				m_builder.Append("null").Append('\n');
			} else if (tater.IsBoolean) {
				m_builder.Append(tater.AsBool ? "true" : "false").Append('\n');
			} else if (tater.IsNumber) {
				m_builder.Append(tater.AsDouble.ToString(CultureInfo.InvariantCulture)).Append('\n');
			} else if (tater.IsString) {
				string str = tater.AsString;
				if (str.IndexOfAny(m_reserved) == -1 && !str.Contains("//") && !str.Contains("/*") && !str.Contains("*/")) {
					// we are safe to print without double quotes
					m_builder.Append(str).Append('\n');
				} else {
					// force us to use double quotes and replace escapes
					str = str.Replace("\n", "\\n").Replace("\"", "\\\"");
					str = str.Insert(0, "\"");
					str = str.Insert(str.Length - 1, "\"");
					m_builder.Append(str).Append('\n');
				}
			} else {
				throw new NotImplementedException($"Write of type `{tater.Type}' is not implemented");
			}
		}
		private void AppendIndent() {
			for (int ix = 0; ix < m_depth; ix++) {
				m_builder.Append(m_tab);
			}
		}
		private void IncreaseIndent() {
			m_depth++;
		}
		private void DecreaseIndent() {
			m_depth--;
		}

	}

}