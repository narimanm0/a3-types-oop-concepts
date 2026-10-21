# Task 2: Static & Dynamic Binding in C#
 
## 1. Idea

By saying binding, we mean deciding which method body is executed when we write `obj.Method()`:
- **static (early) binding:** the compiler decides, using the type of the variable
- **dynamic (late) binding:** the program decides while it is running, using the type of the object.

In C# three keywords control this:

| Keyword    | Where?                       | What it does?                                                       | Binding |
|------------|------------------------------|---------------------------------------------------------------------|---------|
| `virtual`  | method in the base class     | allows derived classes to replace the method                        | dynamic |
| `override` | method in the derived class  | replaces the virtual method, the object decides                     | dynamic |
| `new`      | method in the derived class  | hides the base method, it is a different method with the same name  | static  |
 
A normal method (no `virtual`) is always bound statically.




## 2. Implementation
 
The classes are:
 
```
Animal (virtual MakeSound, normal Move)
 ├── Dog (override MakeSound, new Move)
 │    └── Puppy (override MakeSound)
 └── Cat (new MakeSound)
      └── Kitten (new MakeSound)
```
 
`Main` has four parts, and each part calls methods through a variable of the base type, so we can see which version is chosen.
