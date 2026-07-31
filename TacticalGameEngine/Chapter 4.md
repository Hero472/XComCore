# The World

Wow finally we are over with the talking lets talk about what is the book about, so let's go!

So Before a soldier can take cover or any kind of unit like a wizard can cast a fireball (That honestly would be pretty awesome). The world needs a concept of _where_. Every mechanic we build throughout this book depends on reliable way to represent positions in the world.

We'll begin by creating the project that will contain our engine. Open a terminal and run the following command:

```bash
dotnet new classlib -n XComCore
```

This creates a .NET class library that will contain every gameplay system we develop. By keeping the engine independent of rendering, audio and user interface code, we are ensuring that the logic can be reused in any application, whether that's Unity, Godot, MonoGame, or even a completely custom engine.

> Take note that we are going to be using .NET standard 2.1 which is what Unity uses as for 2026

Unlike the real world, a tactical game's world is **discrete** rather than continuous. In mathematics, continuous spaces allow infinitely many positions between two points. A discrete space, on the other hand, is made up of individual locations that can be counted. If you've ever played chess, you've already interacted with a discrete world. A knight is never halfway between two squares; it is always occupying exactly one square on the board.

Because the world is discrete, our coordinate system can also remain discrete. We don't need floating-point numbers or complicated geometric representations just to describe where something is. A pair of integer coordinates is enough to identify every tile on the battlefield, and every other system in the engine will build on top of that simple idea.

That simplicity can be deceptive. Coordinates may look like nothing more than two numbers, but they are arguably the most frequently used type in the entire engine. Every movement request creates new positions. Every pathfinding search evaluates thousands of them. Vision algorithms compare them constantly, and nearly every gameplay mechanic, from explosions to cover calculations, depends on them in one way or another.

Because of that, small design decisions have consequences that extend far beyond this chapter. If our coordinate type is inefficient, every system built on top of it becomes inefficient. If it performs unnecessary allocations, those costs accumulate throughout the entire engine. If its API encourages mistakes, every future subsystem inherits those mistakes as well.

For that reason, we're not going to rush into writing a simple `Position` struct and moving on. Instead, we're going to design a coordinate system that is lightweight, expressive, and pleasant to work with, because it will become one of the foundations upon which the rest of XComCore is built.

## Representing a Position

Let's start with the simplest possible representation. A position in a two-dimensional grid is simply an **X** and a **Y** coordinate.

```cs
namespace XComCore.World.Grid
{ 
	public readonly struct GridPosition
	{ 
		public uint X { get; }
		public uint Y { get; }
		public GridPosition(uint x, uint y) 
		{ 
			X = x;
			Y = y;
		}
	} 
}
```