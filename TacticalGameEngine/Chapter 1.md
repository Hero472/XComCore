# Introduction

I'm excited to start this book and I hope you feel thrilled to read it. This book is a guided tour through the design and construction of a **Tactical Game Engine**. I don't have a name for it yet, so that is what we will call it for now.

If your programming background consists mostly of shuttling JSON payloads between a database and a web frontend, this project is going to feel delightfully different. There are no web frameworks here. No object-relational mappers. No database tables to migrate.

Instead, we are going to build a core rules engine from scratch. We will wrestle with spatial geometry, pathfinding graphs, field-of-view algorithms, and deterministic combat resolution.

This is not a textbook filled with dry academic theory, nor is it a quick-start guide that glosses over the hard architectural choices. It is a book I wish I had read during my early years as a developer: a hands-on guide that builds a complete, nontrivial system step by step, explaining _why every line of code exists_.

We could have applied these software architecture principles to a banking backend, an inventory manager, or a compiler. But a tactical game engine gives us a domain that is visually intuitive, rigorously logical, and genuinely fun to build.

## The AI Age and the Craft of Engineering

We are living through a strange, fascinating shift in our industry. Large Language Models can write a working system or an API rest with enough tokens.

This leads me to one question: _Why bother learning how to build these systems by hand?_

I have to admit that this scares me a little. There are already plenty of books out there about software engineering, but in the AI era some of them feel outdated. So here I am, trying to write the kind of guide that I wish existed, not only to teach you, but also to challenge myself to think about these problems more deeply.

I once read a thesis that I believe it's really accurate even now almost 50 years later that said:

> "In a computer-illiterate society, the decisions that concern us all will be made by a computer-literate few." — Calvin, 1978

When we delegate all our architectural thinking to AI tools, we trade deep understanding for speed. An LLM can spit out an implementation of _A*_ pathfinding instantaneously. But it cannot evaluate your domain boundaries. It cannot tell you whether pathfinding logic belongs inside a `Grid`, a `Unit`, or a dedicated `NavigationSystem`. It cannot take responsibility for the maintainability of your system when requirements inevitably shift.

Right now writing code is easy. Designing systems and deciding _who owns what and how components communicate_. is where true software engineering lives.

## The C# ToolChain

While the architectural lessons in this book apply to any language, we will write our implementation in modern C#.

We will leverage C#'s strong type system, value-type optimization, and immutability features. Along the way, we will also embrace functional design patterns that make domain logic clean and resilient:

- `Result<T,E>`: Explicitly represents either a successful operation returning `T`, or an error holding `E`. No throwing exceptions for expected domain failures.
- `Option<T>`: Explicitly represents a value that may or may not exist, eliminating null-reference exceptions by design.

You don't need to be a C# expert to follow along, but you should be comfortable with basic object-oriented programming concepts. We will build the rest together.
## What is a System?

We are going to use the word _system_ a lot, so let's establish what we mean by it.

A system is a cohesive, self-contained unit built to solve one specific problem. It behaves like a **blackbox**: it accepts explicit inputs, produces deterministic outputs, and hides its internal implementation from the rest of the application.

When you look at a large application like an e-commerce platform, there are a lot of interconnected system functioning together:

- An Authentication System to establish identity.
- An Inventory System to manage stock
- A Payment System to handle money.

Out _Tactical Game Engine_ works the exact same way. Rather than writing one gigantic `GameManager` class that handles everything, we will break out application down into focused, independent systems: a Grid System, a Movement System, a Visibility System, and a Combat System.
# Great programming exercise

If most of your experience has been building CRUD applications, this project will probably feel very different.

There isn't a tutorial telling you which table to create next or which endpoint to expose. Instead, we'll spend our time designing systems, deciding how data flows through them, and figuring out how they communicate without becoming tightly coupled (which AI tends to do a lot).

You'll find yourself thinking about grids, geometry, graphs, pathfinding, visibility, collision detection, serialization, and plenty of software architecture along the way. None of these topics are impossible to understand on their own. The interesting part is making all of them work together without creating a giant mess.

This is why I think this could be a really good exercise, because most of the time you stop at the API with CRUD and you feel you didn't learn anything new, here we are going to solve a bigger problem with something AI cant fully replicate, our way of thinking. Which is a little exciting I can't deny.
# Code

Every chapter builds on the previous one. So its important you follow along

We'll start with small pieces of code, make sure we understand why are they there and why they will work against other ways of doing it and these are going to be the foundation to the next chapter. By the end of the book every chapter have had built their part to create a tactical game engine.

Most of the code examples are going be written in small snippets with the exceptions of some of them being a little bigger ones. I find them easier to read and they are going to be language agnostic so its your job to be implementing them in the language of your choice, I will be focusing on the theory and the logic behind it.

I will use pseudo code before writing the real implementation. I don't want to get distracted with the syntax when here it's the least important part. So the idea 

```
// Pseudocode

System  -> Input
		-> Process
		-> Output
```

We'll worry about making it compile after we understand why we're writing it.