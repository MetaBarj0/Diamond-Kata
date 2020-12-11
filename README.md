# Code Kata: Diamond

A diamond for a letter!

Given a single letter, print a diamond starting with the 'A' letter (lower or upper case) with the supplied letter at the widest point.

Example for 'A':
<pre>
A
</pre>
Example for 'B':
<pre>
 A
B B
 A
</pre>
Example for 'C':
<pre>
  A
 B B
C   C
 B B
  A
</pre>

## Usage

Use the Diamond program in command line as following :

```
dotnet run --project Program F
```

You will get this result.
<pre>
     A
    B B
   C   C
  D     D
 E       E
F         F
 E       E
  D     D
   C   C
    B B
     A
</pre>
The Diamond program works for uppercase letters as for lowercase letters. For example, the diamond printed for 'c':
<pre>
  a
 b b
c   c
 b b
  a
</pre>

## edge cases

Don't forget them! For instance, an empty or non-letter input must advise the user how to use the feature properly.

## library target

The diamond feature must be useable as a library and be used as a dependency in any project

## extensions

- Make use of the new nullable reference type of c#8.0 to clarify even more your intents and remove an entire category of bugs
- Make the feature able to draw diamonds with numbers (beware of numbers composed of several digits)
- output in a file instead of the console (think about a design decoupling the medium used to display and the feature by itself)

### number support

Use the Diamond program in command line as following :

```
dotnet run --project Program 2
```

You will get this result.
<pre>
  0
 1 1
2   2
 1 1
  0
</pre>

```
dotnet run --project Program 11
```

You will get this result.
<pre>
           0
          1 1
         2   2
        3     3
       4       4
      5         5
     6           6
    7             7
   8               8
  9                 9
 10                 10
11                   11
 10                 10
  9                 9
   8               8
    7             7
     6           6
      5         5
       4       4
        3     3
         2   2
          1 1
           0
</pre>
