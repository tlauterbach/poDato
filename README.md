# PoDato
Reader/Writer for Unity/C# for easy to hand-write data file format PoDato

| Package Name | Package Version | Unity Version |
|-----|-----|-----|
| com.potatointeractive.podato | 1.0.0 | 2020.3.36f1 |

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
    example string
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

For a more in-depth look at the grammar for parsing purposes, view the [PoDato Grammar File](grammar.pdf).