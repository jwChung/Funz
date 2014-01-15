Funz
----
Funz is a variation of [Funq] to provide some features as the following.

 * Makes all the public mehtods of the Container class thead-safe.
 * Releases serivce instances when a container disposes, which fixes the out of memory issue. (see [here])
 * Supports the custom scope scenario. (see the CreateChild(scope) and the ReusedWithin(object) methods)
 * Etc

To install Funz, run the following command in the Package Manager Console.

```
PM> Install-Package Funz
```

[Funq]: http://funq.codeplex.com/
[here]: http://stackoverflow.com/questions/15512035/funq-and-disposing-of-child-container
