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




## 2. Results
 
Every program was run 3 times and the table shows the range. The time is for 100,000 calls, and in brackets is the time of one call.
 
**C, `-O2`**
 
| Workload    | Static       | Stack        | Heap         |
|-------------|--------------|--------------|--------------|
| light use   | 0.104-0.107s | 0.211-0.216s | 0.107-0.119s |
| full use    | 2.36-2.52s   | 2.48-2.66s   | 2.36-2.57s   |
 
**C, `-O0`**
 
| Workload    | Static       | Stack        | Heap         |
|-------------|--------------|--------------|--------------|
| light use   | 0.220-0.270s | 0.325-0.378s | 0.221-0.228s |
| full use    | 2.34-2.38s   | 2.46-2.49s   | 2.35-2.46s   |
 
Of course, numbers can differ on other computers, because they depend on the CPU, the compiler and the operating system. In my runs the results changed about 10% from run to run.
