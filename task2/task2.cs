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

}
