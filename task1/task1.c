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
