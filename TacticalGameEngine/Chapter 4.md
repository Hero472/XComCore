# Grid Coordinates

Wow finally we are over with the talking lets talk about what is the book about, so let's go!

So Before a soldier can take cover or any kind of unit like a wizard can cast a fireball (That honestly would be pretty awesome). The world needs a concept of _where_.

In a tactical  game, space is not continuous, so its discrete, this means it's in the reign of natural numbers. Everything that we will be working on will depend on the grid, pathfinding, vision, where units can be and more.

If out coordinate implementation is slow, allocates memory carelessly, or suffers from rounding errors, those flaws will ripple outward and compromise every system upstream.

In this chapter, we will design and implement a coordinates system in C#.

## Class vs. Struct

Before writing code, we face a fundamental design question. Should a coordinate be a `class` or a `struct`?

In C#, a `class` is a reference type. Every time you instantiate a class using new, the memory is allocated on the heap and a pointer is passed around.

A `struct`, on the other hand, is a value type. It's data lives inline on the execution stack or embedded directly inside the object that holds it. Considering a map can have a 100 x 100 cells. That grid will contain 10,000 individual positions.

If coordinates were references types, placing or moving objects across the map would trigger thousands of heap allocations, making the Garbage Collector work more than it should be.

