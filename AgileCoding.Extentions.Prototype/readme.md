AgileCoding.Extensions.Prototype
================================

Overview
--------

AgileCoding.Extensions.Prototype is a .NET library that provides several useful extension methods related to the Prototype Pattern, enabling different types of deep object copying. This package offers high-speed serialization using various techniques.

Installation
------------

This library is distributed via NuGet. To install it, use the NuGet package manager console:

bashCopy code

`Install-Package AgileCoding.Extensions.Prototype -Version 2.0.5`

Features
--------

This library introduces the following extension methods:

1.  DeepBinaryCopy: Returns a deep copy of the object using binary serialization.
2.  DeepXMLCopy: Returns a deep copy of the object using XML serialization.
3.  DeepJSONNewtonsoftCopy: Returns a deep copy of the object using Newtonsoft JSON serialization.

Please note, `DeepBinaryCopy` requires that all types used in the copy are Serializable. `DeepXMLCopy` and `DeepJSONNewtonsoftCopy` require all types to have a parameterless constructor.

Usage
-----

Here's an example of how to use these features in your code:

```csharp
using AgileCoding.Extentions.Prototypes.Serialization;

// Get a deep binary copy of an object
MyClass myObject = new MyClass();
MyClass myBinaryCopy = myObject.DeepBinaryCopy();

// Get a deep XML copy of an object
MyClass myXMLCopy = myObject.DeepXMLCopy();

// Get a deep JSON copy of an object using Newtonsoft
MyClass myJSONCopy = myObject.DeepJSONNewtonsoftCopy();
```

Documentation
-------------

For more detailed information about the usage of this library, please refer to the [official documentation](https://github.com/ToolMaker/AgileCoding.Extentions.File/wiki).

License
-------

This project is licensed under the terms of the MIT license. For more information, see the [LICENSE](https://github.com/ToolMaker/AgileCoding.Extentions.File/blob/main/LICENSE) file.

Contributing
------------

Contributions are welcome! Please see our [contributing guidelines](https://github.com/ToolMaker/AgileCoding.Extentions.File/blob/main/CONTRIBUTING.md) for more details.