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
