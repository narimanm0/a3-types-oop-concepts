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
}
