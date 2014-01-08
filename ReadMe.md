#Exportable

This library came about from a scenario a fellow colleague of mine had. Essentially
a scenario had arisen where a given class had more fields than desired for exporting to 
Excel. Reflection and a list of properties ended up being the path of least resistence,
but I felt that it could go one step further and essentially create a cached result set.

The library works by caching the desired valid properties of a class, thereby 
removing the need to re-reflect through the types properties on multiple runs.

```csharp
using Magana;
...
    List<PropertyInfo> exportableProperties = 
	Exportable.ValidProperties(typeof(<Class>), "<PropertyA>", "<PropertyB>", "<PropertyZ>", "<PropertyC>");
    // exportableProperties will contain a list PropertyInfo items for the properties named
    // "PropertyA", "PropertyB", "PropertyZ" and "PropertyC" in the original order

```

The real value only comes from multiple calls to this static library.
