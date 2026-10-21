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





## 3. Output and explanation
 
```
1. virtual + override (DYNAMIC)
Animal: some generic sound
Dog: Woof!
Puppy: Yip!
 
2. non-virtual + new (STATIC)
Animal: moves somehow
Dog: runs on four legs
Animal: moves somehow
 
3. new on a virtual method breaks the chain
Animal: some generic sound
Cat: Meow!
Animal: some generic sound
Cat: Meow!
Kitten: Mew!
 
4. overload resolution (STATIC)
Print(Animal) was chosen
Print(Dog) was chosen
Print(Dog) was chosen
Print(Dog) was chosen
```
 
### Part 1: `virtual` + `override`
 
```csharp
Animal a2 = new Dog();
a2.MakeSound(); // Dog: Woof!
```
 
Variable has type `Animal`, but the object is a `Dog`. Because `MakeSound` is `virtual` in `Animal` and `override` in `Dog`, the program looks at the real object at run time and calls `Dog.MakeSound`. 
The same happens for `Puppy`. This is dynamic binding, and it is the base of polymorphism.
 
### Part 2: normal method + `new`
 
```csharp
Dog d = new Dog();
Animal a4 = d;
a4.Move(); // Animal: moves somehow
d.Move(); // Dog: runs on four legs
```
 
It is the same object, but we get different results. `Move` is not virtual, so the compiler chooses the method by the type of the variable:
`Animal` gives `Animal.Move`, and `Dog` gives `Dog.Move`. The keyword `new` only says "I know that I hide the base method, it is on purpose". Without it, the compiler gives warning CS0108.
 
### Part 3: `new` on a virtual method
 
`Cat` hides `MakeSound` with `new` instead of overriding it. Therefore `Cat.MakeSound` is a **new method** and has no connection to the virtual chain of `Animal`:
- `Animal c1 = new Cat(); c1.MakeSound();` prints the **Animal** version, even if the object is a cat;
- `Cat c2 = new Cat(); c2.MakeSound();` prints the Cat version.
For `Kitten` it is the same: the result depends only on the type of the variable (`Animal`, `Cat` or `Kitten`).
This is the main danger of `new`: the same object behaves differently, depending on how we look at it. Usually `override` is what we really want.
 
### Part 4: overloads
 
```csharp
Animal x = new Dog();
Print(x); // Print(Animal)
Print((dynamic)x); // Print(Dog)
```
 
Static binding is not only about hiding. When there are two overloads `Print(Animal)` and `Print(Dog)`,
the compiler chooses one by the declared type of the argument, so `x` gives `Print(Animal)`,
even if it contains a `Dog`. If we cast it, we get `Print(Dog)`. With `dynamic` the choice is moved to run time, so it is dynamic binding and the real type `Dog` is used.



 
## 4. Conclusion
 
`virtual` and `override` give dynamic binding: the object decides, and one base-class variable can work with many types. A normal method and `new` give static binding: the variable decides, and the compiler knows the answer before the program starts. Static binding is a bit faster, because there is no virtual table lookup, but the speed difference is small. I think `override` should be the default choice, and `new` should be used rarely, for example when we cannot change the base class and want a different method with the same name.
