# Task 1: Static, Stack and Heap Arrays

## 1. Implementation

The C program `task1.c` has three functions. Each of them uses the same array of 1 MiB (1024 * 1024 bytes), but the array lives in a different place:

| Function     | Where is the array?                       | What happens at every call?              |
|--------------|-------------------------------------------|------------------------------------------|
| `use_static` | `static char static_array[...]` (`.bss`)  | nothing, it is created once at the start |
| `use_stack`  | `char a[...]` local variable              | the stack pointer is moved               |
| `use_heap`   | `malloc()`                                | `malloc()` and `free()` are called       |


The array size and the number of runs are defined with `#define`, so we can change them in one place easily. The function `measure()` calls a function 100,000 times and returns the total time,
which is taken using `clock()`. One call before the timer is a warm-up. There is no `printf()` inside the measured loop.

Of course, if function creates only an array and does not do anything with it, there is obviously nothing to measure. Therefore, every function uses the array in two ways:
- **light use:** write 1 byte per page (every 4096 bytes), so 256 writes
- **full use:** write whole array with `memset()`


There are 2 details in the code which may look strange:
- `__attribute__((noinline))` --> compiler should move function into the loop. Then stack array would be created only once and not at every call
- `keep()`: empty `asm` line which tells the compiler that somebody may use this memory. 

 
I ran the code with GCC 14.3.0, 64-bit Linux, 8 CPU cores and the default stack limit of 8 MiB.
