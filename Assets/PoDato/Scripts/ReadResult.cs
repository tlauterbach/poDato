using System.Collections.Generic;

namespace PoDato {

	public class ReadResult {
		public bool IsSuccess { get { return m_errors.Count <= 0; } }
		public bool IsError { get { return m_errors.Count > 0; } }
		public Tater Result { get { return m_result; } }
		public IReadOnlyList<ReadError> Errors { get { return m_errors; } }

		private Tater m_result;
		private IReadOnlyList<ReadError> m_errors;

		internal ReadResult(Tater result, List<ReadError> errors) {
			m_result = result;
			m_errors = errors.AsReadOnly();
		}
	}

	public class ReadResult<T> where T : IReadable, new() {
		public bool IsSuccess { get { return m_errors.Count <= 0; } }
		public bool IsError { get { return m_errors.Count > 0; } }
		public bool IsArray { get { return m_isArray; } }
		public bool IsObject { get { return !m_isArray;} }
		public T ResultObject { get { return m_resultObject; } }
		public T[] ResultArray { get { return m_resultArray; } }

		public IReadOnlyList<ReadError> Errors { get { return m_errors; } }

		private T m_resultObject;
		private T[] m_resultArray;
		private IReadOnlyList<ReadError> m_errors;
		private bool m_isArray;

		internal ReadResult(T result, List<ReadError> errors) {
			m_resultObject = result;
			m_isArray = false;
			m_errors = errors.AsReadOnly();
		}
		internal ReadResult(T[] result, List<ReadError> errors) {
			m_resultArray = result;
			m_isArray = true;
			m_errors = errors.AsReadOnly();
		}
	}

}