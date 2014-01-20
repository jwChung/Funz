Overview
--------
Funz is a variation of [Funq] to provide some features as the following.
Check out scenario tests to get [how to use].

 * Makes all the public methods of the Container class thead-safe.
 * Releases serivce instances when a container disposes, which fixes the out of memory issue. (see [here])
 * Supports the custom scope scenario. (see the CreateChild(scope) and the ReusedWithin(scope) methods)
 * Etc

Install
-------
To install Funz, run the following command in the Package Manager Console.

```
PM> Install-Package Funz
```

Release notes
-------------
To see release notes, run the following command.

```
$ git log --decorate --grep "Incremented"
```

[Funq]: http://funq.codeplex.com/
[here]: http://stackoverflow.com/questions/15512035/funq-and-disposing-of-child-container
[how to use]: https://github.com/jwChung/Funz/blob/master/test/Funz.UnitTest/Scenario.cs