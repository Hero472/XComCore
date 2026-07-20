# What are We Building

So, what are we actually going to build? Simple question, but I think it deserves its own chapter.

By now you already know the short answer: a _Tactical Game Engine_. The problem is that saying the name doesn't really explain much. If I told someone I'm building a game engine, they could imagine a renderer, a physics engine, an audio engine, networking, input handling... and they'd all be reasonable guesses.

Before we write a single line of code, we need to define the scope of this project because that's a question that should always be in the back of our minds:

> _What is the scope of the program?_

I believe its the question that all software engineer have to know by hand and do it always when doing any software. In the future as programmer you can add features but first we need to define what the program will do in the first place.

We are not interested in the graphics, animations, menus, or anything that is associated with games. Those are topics that are totally out of the scope of this book and even more about the _Tactical Game Engine_.

How does a game decide that a movement is valid? How does it know a wall blocks vision? What tells a unit it can attack from one position but not another? Where do we even store all of this information so that every system agrees on the same world?

To do that, we first need to separate the engine into systems. The grid is one system. Cells are another. Terrain has its own responsibilities. So do movement, cover, visibility, combat, serialization, and everything else we'll build throughout this book.

We'll start with the smallest building blocks: cells, grids, terrain, and the world itself. Once those are in place, we'll move on to the more interesting systems like cover, visibility, pathfinding, and combat. Every chapter builds on the previous one, and every system exists because another one depends on it.

By the end of the book, all of those independent systems will come together to form a complete tactical game engine or at least that is what I expect to do.
## Why split everything into systems

Because trying to solve everything at once is how software becomes messy and tightly coupled. At first it works, then the project grows, and suddenly changing one thing breaks three others, and later refactoring the code becomes impossible because you will break more things than you fix.

Instead, I'd rather have small systems that each solve one problem. That's all a system should be responsible for. It doesn't mean those systems will never communicate with each other. They absolutely will. A movement system might need to ask the grid for neighbouring cells. Combat might need visibility to know if a target can be attacked. Those interactions are completely normal.

So... who should actually know if a movement is valid? The movement system? The grid? The cell? Those are the kinds of questions we're going to answer before writing code.

The important part is that every system knows what it owns and what it doesn't, and who it should ask when it needs information instead of trying to do everything itself.

You'll notice that throughout this book I'll spend a lot of time deciding _where_ something belongs before writing any code. I think that's one of the most important parts of software design. Once we're happy with the architecture, writing the code becomes much easier. It also saves us from constantly going back and refactoring the core of the engine because we put something in the wrong place.

## Where do we start?

It would be pretty strange to start by writing combat before we even know what a cell is. Even though that's the most exciting part if you ask me but also is the last thing or one of the last we are going to talk in the book.

Like any other project, we need a foundation first. Every chapter after that will build on top of the previous one, so nothing should feel like it suddenly appeared out of nowhere.

We'll start with coordinates and cells. Those become a grid. The grid becomes the world. Once we have a world, we can start placing terrain and structures. After that, movement starts making sense. Then collision, cover, visibility, pathfinding, units, combat, and everything else that depends on those systems.

The reason the chapters are in this order is because every chapter depends on the previous one. I don't want to suddenly introduce combat if we don't even know what a cell is yet. Every new concept depends on something we've already built, so instead of trying to understand the whole engine at once, we'll build it piece by piece.

Hopefully, by the time we reach the end of the book, you'll look back and realize that the engine wasn't one giant project after all. It was just a collection of smaller systems that, together, solved a much bigger problem.