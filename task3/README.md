# Task 3: Library Catalog in Ruby
 
## 1. Implementation
 
The classes are at the top, and below them is the test from the assignment and a few extra checks. 
The class diagram:
```
LibraryItem  (title, author, year, display_info)
 ├── Book   (+ genre)
 ├── DVD    (+ genre, duration)
 └── CD     (+ genre, tracks)
 
Library      (add_item, remove_item, display_item, display_all_items)
```
 
### Base class `LibraryItem`
 
It keeps the common properties `title`, `author` and `year`, and the method `display_info`,
which prints them. Also it has `creator_label`, which returns `"Author"`. I added this method because a DVD has a director and a CD has an artist,
so the derived classes can change just the label and not the whole output.
 
### Derived classes
 
| Class  | Inherits from | Extra properties          | Special name for `author` |
|--------|---------------|---------------------------|---------------------------|
| `Book` | `LibraryItem` | `genre`                   | author                    |
| `DVD`  | `LibraryItem` | `genre`, `duration`       | `director`                |
| `CD`   | `LibraryItem` | `genre`, `tracks`         | `artist`                  |
 
In the test from the assignment, the 2nd argument of `DVD.new` is the director and the 2nd argument of `CD.new` is the artist,
and the 4th one is the genre. Therefore, I am not keeping a separate field for the director.
The director is the `author` field, and `alias director author` gives it a good name. The same is done for `artist`.
Every derived class has its own `display_info`. It prints the type, then calls `super` to print the common part, and then prints its own fields:
 
```ruby
def display_info
  puts "[DVD]"
  super
  puts "Genre: #{genre}"
  puts "Duration: #{duration} min" if duration
end
```
 
### Class `Library`
 
It stores the items in an array and has these methods:
- `add_item(item)`: adds an item. It raises `ArgumentError` if the object is not a `LibraryItem`
- `remove_item(title)`: removes the first item with this title and prints a message if there is no such item
- `display_all_items`: prints all items, or a message if the library is empty
- `display_item(item)`: displays one item.






## 2. OOP principles
 
**1) Inheritance:**

`Book`, `DVD` and `CD` get `title`, `author`, `year` and `to_s` from `LibraryItem`, and they call `super` in `initialize` and in `display_info`, so the common code is written only once.
 
**2) Encapsulation:**

The properties have only `attr_reader`, so they can be read but not changed from outside. The constructor checks the data: an empty title or a year which is not an integer raises `ArgumentError`. The array `@items` in `Library` is not available from outside, we can use it only through the methods. Therefore, nobody can put a wrong object in it.
 
**3) Polymorphism:** 

`display_item` is not checking the type of the object. There is no `if item.is_a?(Book)` anywhere in the code:
 
```ruby
def display_item(item)
  item.display_info
end
```
 
Ruby chooses the right `display_info` at run time, using the real class of the object. `display_all_items` calls `display_item` for each item, so the list can contain books, DVDs and CDs together. Moreover, if we add a new class, for example `Magazine`, the `Library` class does not need any change. Ruby uses duck typing, so it would work even for an object which is not a `LibraryItem` but has `display_info`. I still check `is_a?(LibraryItem)` in `add_item`, because I prefer to reject wrong objects when they are added, and not later.
