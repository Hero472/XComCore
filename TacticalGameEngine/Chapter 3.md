# Thinking in Systems

>_Why am I still reading non important things?_

I promise this is the last chapter where we'll spend more time talking than programming, but I think it's an important one. Before we start creating classes and methods, I want us to think about how we're going to organize everything.

I'm not here to write code, in this new era of coding where AI can do it for you it's important for us to make architecture decisions that will shape the software around us and it's in our decision to make it _right_.

We can ask an LLM to gives us a algorithm for movement as we want and iterating until we feel is _perfect_ but that's not the idea here, I just want to make that clear. The idea is: Who owns the movement? and that question is something some people have never ask themself, who owns what.

In a world where an LLM can generate an A* implementation in a couple of seconds, I don't think typing the code is the interesting part anymore. The interesting part is deciding **where that algorithm belongs**.

**Who owns movement?**

That question sounds simple, but it changes everything. Should the `Grid` know how movement works? Should a `Unit` decide where it can move? Should movement even belong to either of those, or should it be its own system?

Those are the kinds of questions we'll ask throughout this book. Before we write code, we'll spend time deciding where that code belongs. I think that's a much more valuable skill than remembering the implementation of an algorithm because algorithms can always be looked up. Architecture usually can't.
## Ownership

Every piece of information should belong somewhere. 

Take the `Grid` as an example. The grid owns cells. That sounds obvious enough, but then we can keep asking questions. What does a `Cell` own? Coordinates? I would say yes. Terrain? Probably. A unit standing on it? Maybe. Environmental effects? That also makes sense.

Now let's ask different questions. Should a `Cell` know how combat works? Should it know how pathfinding is calculated? Should it know how visibility is determined?

Notice that I'm not trying to find the correct answer. I'm trying to build the habit of asking the question in the first place. Every time we introduce a new class or a new system, we'll stop for a moment and ask ourselves what problem it solves and what information it should own.

If every system owns exactly what it needs and nothing more, the architecture usually starts organizing itself.
## Dependencies

Once we know who owns what, another question appears almost immediately.

> Who depends on who?

Dependencies aren't bad. They're unavoidable. Movement needs the grid. Combat needs units. Visibility needs the world. and so on. The problem is creating dependencies everywhere until every system knows about every other system and by that start to be messy.

Imagine if the `Grid` knows about combat, combat knows about units, units know about visibility, visibility knows about movement, and movement knows about the grid again. Every system is connected to every other system, and after a while changing one class means changing five more which is inconvenient...

Whenever we need one system to interact with another, we'll stop and ask if there's a cleaner way to communicate. Sometimes that will be an interface. Sometimes it'll be a shared object. Sometimes it'll simply be a method call. There isn't one correct answer, and you'll notice that throughout the book we'll make those decisions one at a time instead of trying to invent a universal rule for everything.
## There is not a correct architecture

Before we finally start building the engine, there's one last thing I'd like to mention. Everything in this book is a design decision.

If ten different programmers sat down to design a tactical game engine, you'd probably end up with ten different architectures. Some would use inheritance everywhere. Others would avoid it completely. Some would create dozens of small systems. Others would group responsibilities into larger ones.

Throughout the book I'll explain why I made the decisions I made and why I think they fit this engine. Maybe you'll agree with all of them. Maybe you'll disagree with some of them. Maybe halfway through the book you'll realize there's a better way to solve one of the problems.

The goal of this book isn't to convince you that this is the only way to build a tactical game engine. The goal is to show you one way of thinking about software architecture, because I believe learning how to make those decisions is far more valuable than memorizing the final code.

In the next chapter we'll finally stop talking about architecture and start building the first real part of the engine: coordinates. They might look simple, but you'll quickly realize almost every other system depends on them.