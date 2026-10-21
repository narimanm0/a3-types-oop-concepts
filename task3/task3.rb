# Your implеmеntation goеs hеrе
# 1. base class
class LibraryItem
  attr_reader :title, :author, :year

  # Initialize method
  def initialize(title, author, year)
    raise ArgumentError, "title cannot be empty" if title.to_s.strip.empty?
    raise ArgumentError, "year must be an integer" unless year.is_a?(Integer)
 
    @title = title
    @author = author
    @year = year
  end

  # Display Info Method
  def display_info # shared output: derived classes call it with super and add their own lines
    puts "Title: #{title}"
    puts "#{creator_label}: #{author}"
    puts "Year: #{year}"
  end

  def creator_label # the name of the "author" field. Derived classes can change it (Director, Artist)
    "Author"
  end
 
  def to_s
    "#{self.class.name}: #{title} (#{year})"
  end
end

# 2. Book class
class Book < LibraryItem
  attr_reader :genre
 
  def initialize(title, author, year, genre)
    super(title, author, year)
    @genre = genre
  end
 
  def display_info
    puts "[Book]"
    super
    puts "Genre: #{genre}"
  end
end

# 3. DVD Class 
class DVD < LibraryItem
  attr_reader :genre, :duration
 
  def initialize(title, director, year, genre, duration = nil) # author of a DVD is its director, durationis optional
    super(title, director, year)
    @genre = genre
    @duration = duration
  end
 
  alias director author
 
  def creator_label
    "Director"
  end
 
  def display_info
    puts "[DVD]"
    super
    puts "Genre: #{genre}"
    puts "Duration: #{duration} min" if duration
  end
end

# 4. CD Class
class CD < LibraryItem
  attr_reader :genre, :tracks
  
  def initialize(title, artist, year, genre, tracks = nil) # author of a CD is its artist, number of tracks
    super(title, artist, year)
    @genre = genre
    @tracks = tracks
  end
 
  alias artist author
 
  def creator_label
    "Artist"
  end
 
  def display_info
    puts "[CD]"
    super
    puts "Genre: #{genre}"
    puts "Tracks: #{tracks}" if tracks
  end
end

# 5. Library Class
class Library
  def initialize
    @items = [] # list is private, we work with it only through the methods
  end
 
  def add_item(item)
    raise ArgumentError, "only LibraryItem objects can be added" unless item.is_a?(LibraryItem)
 
    @items << item
    puts "Added: #{item}"
  end
 
  def remove_item(title)
    index = @items.index { |item| item.title == title }
 
    if index.nil?
      puts "Not found: #{title}"
      return nil
    end
 
    removed = @items.delete_at(index)
    puts "Removed: #{removed}"
    removed
  end
 
  def display_item(item) # polymorphism: we do not check the type, every item knows how to display itself
    item.display_info
  end
 
  def display_all_items
    if @items.empty?
      puts "The library is empty."
      return
    end
 
    puts "=== Library catalog (#{@items.size} items) ==="
    @items.each do |item|
      display_item(item)
      puts
    end
  end
end
 
# Crеatе instancеs of diffеrеnt library itеms
book = Book.new("Concеpts of Programming Languagеs", "Robеrt Sеbеsta", 2015, "Programming")
dvd = DVD.new("Incеption", "Christophеr Nolan", 2010, "Sciеncе Fiction")
cd = CD.new("A Day at thе Racеs", "Quееn", 1976, "Rock")
 
# Display itеms individually
book.display_info
dvd.display_info
cd.display_info
 
 
# Crеatе a library and add itеms to it
library = Library.nеw
library.add_itеm(book)
library.add_itеm(dvd)
library.add_itеm(cd)
 
# Display information about all itеms in thе library
library.display_all_itеms
