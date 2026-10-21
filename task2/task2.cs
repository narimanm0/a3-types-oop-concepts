using System;

namespace Binding
{
    // Base class
    class Animal
    {
        public virtual void MakeSound() // virtual: the derived class may replace it - dynamic
        {
            Console.WriteLine("Animal: some generic sound");
        }

        
        public void Move() // not virtual: the call is fixed at compile time - static
        {
            Console.WriteLine("Animal: moves somehow");
        }
    }

    // Dog uses override for MakeSound and new for Move
    class Dog : Animal
    {
        
        public override void MakeSound() // override: replaces the method of Animal, so the real type of the object decides
        {
            Console.WriteLine("Dog: Woof!");
        }

        
        public new void Move() // hides Move of Animal, it is a different method with the same name
        {
            Console.WriteLine("Dog: runs on four legs");
        }
    }

    class Puppy : Dog // Puppy overrides the method again, the chain continues
    {
        public override void MakeSound()
        {
            Console.WriteLine("Puppy: Yip!");
        }
    }

    class Cat : Animal // Cat hides a virtual method with new, this breaks the chain
    {
        public new void MakeSound()
        {
            Console.WriteLine("Cat: Meow!");
        }
    }

    class Kitten : Cat  // Kitten overrides nothing from Cat, but Cat.MakeSound is not virtual anyway
    {
        public new void MakeSound()
        {
            Console.WriteLine("Kitten: Mew!");
        }
    }

    class Program
    {
        // 2 OVERLOADS
        // the compiler chooses by the type of the variable (static binding)
        static void Print(Animal a)
        {
            Console.WriteLine("Print(Animal) was chosen");
        }

        static void Print(Dog d)
        {
            Console.WriteLine("Print(Dog) was chosen");
        }

        static void Main()
        {
            Console.WriteLine("1. virtual + override (DYNAMIC)");
            Animal a1 = new Animal();
            Animal a2 = new Dog();
            Animal a3 = new Puppy();
            a1.MakeSound(); // Animal
            a2.MakeSound(); // Dog, decided at run time
            a3.MakeSound(); // Puppy, decided at run time

            Console.WriteLine("\n2. non-virtual + new (STATIC)");
            Dog d = new Dog();
            Animal a4 = d; // the same object, but seen as Animal
            a4.Move(); // Animal's Move, because the variable type is Animal
            d.Move(); // Dog's Move, because the variable type is Dog
            ((Animal)d).Move(); // cast to Animal, so Animal's Move again

            Console.WriteLine("\n3. new on a virtual method breaks the chain");
            Animal c1 = new Cat();
            Cat c2 = new Cat();
            c1.MakeSound(); // Animal's, because Cat did not override it
            c2.MakeSound(); // Cat's
            Animal k1 = new Kitten();
            Cat k2 = new Kitten();
            Kitten k3 = new Kitten();
            k1.MakeSound(); // Animal
            k2.MakeSound(); // Cat
            k3.MakeSound(); // Kitten

            Console.WriteLine("\n4. overload resolution (STATIC)");
            Animal x = new Dog();
            Print(x); // Animal overload, because x is declared as Animal
            Print(new Dog()); // Dog overload
            Print((Dog)x); // Dog overload, after the cast
            Print((dynamic)x); // dynamic: the overload is chosen at run time -> Dog
        }
    }
}
