#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <time.h>

#define array_size (1024 * 1024) // size of the array
#define runs 100000 // # of calls as mentioned in assignment

void keep(char *a){
    __asm__ volatile("" : : "g"(a) : "memory");
}

void use_array(char *a, int full){
    keep(a);
    if (full) {
        // if full = 1 then writes the whole array with memset()
        memset(a, 1, array_size);
    } else {
        // if full = 0 then writes only one byte per page
        for (int i = 0; i < array_size; i += 4096) {
            a[i] = 1;
        }
    }
    keep(a);
}

// 1. STATIC ARRAY
static char static_array[array_size]; // created once
 
// Why noinline is needed here? - because the compiler should move arrays into main
__attribute__((noinline))
void use_static(int full){
    use_array(static_array, full);
}

// 2. STACK ARRAY
__attribute__((noinline)) // created every time function is called
void use_stack(int full){
    char a[array_size];
    use_array(a, full);
}

// 3. HEAP ARRAY
__attribute__((noinline)) // malloc() and free() are called every time
void use_heap(int full){
    char *a = malloc(array_size);
 
    if (a == NULL) {
        printf("malloc failed\n");
        exit(1);
    }
 
    use_array(a, full);
    free(a);
}

// calling 100000 times
double measure(void (*f)(int), int full){
    f(full);
 
    clock_t start = clock();
 
    for (int i = 0; i < runs; i++) {
        f(full);
    }
 
    return (double)(clock() - start) / CLOCKS_PER_SEC;
}
