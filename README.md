# PoDato
Reader/Writer for Unity/C# for easy to hand-write data file format PoDato

| Package Name | Package Version | Unity Version |
|-----|-----|-----|
| com.potatointeractive.podato | 1.3.1 | 2020.3.36f1 |

[Changelog](CHANGELOG.md)

# OpenUPM
This project is available as an Open UPM Package: [![openupm](https://img.shields.io/npm/v/com.potatointeractive.podato?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.potatointeractive.podato/)

Visit [Open UPM](https://openupm.com) to learn more about the Open Unity Package Manager project and how to install the package in your Unity Project.

# Usage
As a programmer, you may often find yourself needing to hand-enter data into files if you don't have someone who is either A) making a tool or B) willing to use a tool. *YAML* is relatively difficult to write a parser for if you don't already know how to do it, and *JSON* is not very fun to type out by hand with lots of control/flow characters. *PoDato* combines the good parts about both *JSON* and *YAML* — making it easy to write, parse, and eyeball.

| Format | Easy To Hand-Write | Easy to Parse | Easy to Eyeball |
| ----- | ----- | ----- | ----- |
| JSON | | ✓ | ✓ |
| YAML | ✓ | | ✓ |
| PoDato | ✓ | ✓ | ✓ |

Even if you don't know or have time to write a parser for an obscure language such as *PoDato*, this project already contains a light-weight one ready to go for Unity projects.

For a more in-depth diagram of the grammar for parsing purposes, take a look at the [PoDato Grammar PDF](grammar.pdf).

# Format
Like JSON, *PoDato* is comprised of **Objects**, **Arrays**, and **Values**
```
# Simple PoDato Object
{
  exampleString : A sentence or string of characters.
  exampleNumber : 0.1030
  exampleBoolean: false
  exampleNull   : null
}
```
```
# Simple PoDato Array
[
  0.2103
  0.3203
  1.0233
  2.0405
]
```

*Unlike* JSON, you don't need to separate elements using commas. Instead, *PoDato* opts to use newlines for its separation, similar to YAML. *PoDato* also eliminates the need to use double-quotes to indicate field names or string values (unless absolutely necessary). However, continuing usage of open and closing characters for Objects and Arrays eliminates needing to specifically indent like you must with YAML.

```
# Complex PoDato Example
{
  object: {
    boolean: true
    single: 1.0202
    noQuotes: This is an example string that does not use double quotes.
    withQuotes: "If you need reserved characters like the `:' you can double quote your string."
  }
  arrayOfStrings: [
    example string # with a comment to the side
    another string
    yet another string
    "a double quoted string"
    "double quotes with escaped \" character"
  ]
  arrayOfObjects: [
    {
      sample: 1
      another: false
    }
    {
      sample: 3
      another: true
    }
  ]
}
```

# API
To get you started with reading and writing *PoDato*, this project includes a parser and writer.

## Reading
```
# Example PoDato for Reading
{
  a: -3092.302
  b: 20930
  c: true
  d: an optional string value
}
```

Implement the `IReadable` interface to allow your data object to read `PoDato` information, and use the `IReader` parameter of your `Deserialize` function to assign values to your object. Values that you specify in the function can be either **Required** or **Optional**, meaning the reader will error if the value doesn't exist or you can specify your own value respectively.

```csharp
using PoDato;

public class MyClass : IReadable {

    private float  m_float;
    private int    m_int32;
    private bool   m_boolean;
    private string m_string;
    private byte   m_missingByte;

    public void Deserialize(IReader reader) {
        // the name value should correspond to your data
        reader.RequiredSingle("a", ref m_float);
        reader.RequiredInt32("b", ref m_int32);
        reader.RequiredBool("c", ref m_boolean);
        if (!reader.OptionalString("d", ref m_string)) {
            // if the optional call returns false, it is 
            // letting us know that the value didn't exist
            // when reading
            m_string = "My default string value";
        }
        if (!reader.OptionalByte("e", ref m_missingByte)) {
            m_missingByte = 34;
        }
    }
}
```

To actually read your data into the object, you will need an instance of `TaterReader` and to call `Read`. Upon reading, you will get back a `ReadResult` or `ReadResult<T>` object with information about your deserialized result.

```csharp
using PoDato;

public class MyReader : MonoBehaviour {

    [SerializeField]
    private TextAsset m_asset;

    private TaterReader m_reader = new TaterReader();

    private void Awake() {
        ReadResult<MyClass> result = m_reader.Read<MyClass>(m_asset.Text);
        if (result.IsSuccess) {
            Debug.Log("Reading was successful!");
            MyClass myClass = result.ResultObject;
            // if the value was an array, it would 
            // be in result.ResultArray
        } else {
            // If any errors occur during the Reading
            // process, they will be stored in the
            // Errors property of your result 
            foreach (string error in result.Errors) {
                Debug.LogWarning(error);
            }
        }
    }
}
```

The `TaterReader` can also handle more complex objects, letting you define other `IReadable` objects within `IReadable` objects. It also has support for *Proxy Values*, meaning you can convert simple types into structs or more complex objects during reading.

```csharp
using PoDato;

public class OtherDataObject : IReadable {

    private struct ProxyByte : IReadProxy<byte> {
        private byte m_value;
        public void SetProxyValue(byte value) {
            m_value = value;
        }
    }

    private MyClass m_object1;
    private MyClass m_object2;
    private AnotherClass m_object3;
    private Dictionary<string,float> m_floatMap;
    private ProxyByte m_proxyByte;
    private List<ProxyByte> m_proxyList;

    public void Deserialize(IReader reader) {
        reader.RequiredObject("object1", ref m_object1);
        reader.RequiredObject("object2", ref m_object2);
        reader.RequiredObject("object3", ref m_object3);
        reader.RequiredSingleMap("floatMap", ref m_floatMap);
        reader.RequiredByteProxy("proxyByte", ref m_proxyByte);
        reader.RequiredByteProxyList("proxyList", ref m_proxyList);
    }
}
```

## Writing
There are cases where you might want to write data to a *PoDato* string, possibly for generating data. Writing works very similarly to Reading in that you must implement the `IWritable` interface and use the `IWriter` in the `Serialize` function. Objects can be both Readable _and_ Writable as well.

```csharp
using PoDato;

public class MyClass : IWritable {

    private float  m_float;
    private int    m_int32;
    private bool   m_boolean;
    private string m_string;
    private byte   m_missingByte;

    public void Serialize(IWriter writer) {
        // the name value should correspond to your data
        writer.Write("float", m_float);
        writer.Write("int32", m_int32);
        writer.Write("boolean", m_boolean);
        writer.Write("string", m_string);
        writer.Write("missingByte", m_missingByte);
    }
}
```
```csharp
using PoDato;

public class MyWriter : MonoBehaviour {

    private TaterWriter m_writer = new TaterWriter();

    private void Awake() {
        string result = m_writer.Write(new MyClass());
        Debug.Log(result);
    }
}
```