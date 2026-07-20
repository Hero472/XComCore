# Introduction

I'm excited to start this book. This is going to be a long journey, and I hope it's just as exciting for you, the reader, as it is for me. This book is about designing and implementing a Tactical Game System. I don't have a proper name for it yet, so that's what we'll call it for now. I also hope this project becomes something people can use to learn a programming language while getting exposure to more advanced concepts as we move further into the book.

We'll take everything step by step, starting with the basic concepts and defining each element of the system before actually implementing it.

This is more of a written tutorial than a textbook. It's the kind of resource I wish I had when I started learning programming during my years at university. I want it to serve as a guide rather than a collection of theory, so that anyone can pick it up and understand why we make the design decisions we do.

My goal is to show how systems are built from scratch. Honestly, this could have been about anything: a marketplace, an inventory management system, or something completely different—but I think building a tactical game system is much more fun while still teaching the same engineering principles.

I should also mention that most of my background as a programmer has been with Rust and C#, and because of that I've grown to love Rust's `Result<T, E>` and `Option<T>`. We'll be using those concepts throughout the examples and the engine, so I expect you to already have a basic understanding of them. In short, `Result` represents either success or failure, making both paths explicit, while `Option` represents a value that may or may not exist. Since it's a type instead of `null`, we avoid an entire class of null reference errors, which is a huge win when building reliable systems.

Right now, LLMs can generate entire applications for you, but I want to go a step further than that. I want you to understand how and why we build things the way we do, and why software engineers are still valuable in the bigger picture. AI can generate code quickly, but that doesn't mean the code is well understood by the people using it. In many cases, understanding the system is far more valuable than generating it in the first place.

## Why even bother if an AI can do it for me?

Good question, if you ask me. There is something that I think is going to affect all of us in the future: software engineers who build software (or ask an AI to build it for them), and the people who end up using that software. The fact that we are forgetting _how a system is done by hand_.

I have to admit that this scares me a little. There are already plenty of books out there about software engineering, but in the AI era some of them feel outdated. So here I am, trying to write the kind of guide that I wish existed, not only to teach you, but also to challenge myself to think about these problems more deeply.

I once read a thesis that said:

> "In a computer-illiterate society, the decisions that concern us all will be made by a computer-literate few." — Calvin, 1978

I believe that's becoming more relevant than ever. From my point of view, those decisions shouldn't simply be delegated to an LLM. We should understand the systems we're building, even if an AI helps us write part of the code.

### Systems

We'll use the word _system_ a lot throughout this book, so it's worth defining what I mean by it. Cambridge defines a system as _"a set of computer equipment and programs used together for a particular purpose."_ and I would like to simplify it: A system is something that we create to solve a problem.

For example a marketplace. Has a payment system, authentication system, inventory system, recommendation system and lots others. Each one of this systems create a bigger one which is interconnected. You need the authentication to enter the marketplace you need the inventory to show what you can sell, a recommendation to show the users preferences and a payment one so the user can actually buy a product all this together and lots of more stuff is a marketplace.

As software engineers, we build systems all the time, even if we don't always call them that. Maybe you wrote a service that sends emails. Maybe you created a façade so the rest of your code didn't have to know about some complicated logic. Maybe you separated your API into layers. All this sums up to a full system even if you never called you API like that.

One of my professors explained it as a _black box_. We know what goes in, we know what comes out, and we trust the box to do its job. We don't need every other part of the application to know how it works internally. This is a concept that we will use a lot.
# Great programming exercise

If most of your experience has been building CRUD applications, this project will probably feel very different.

There isn't a tutorial telling you which table to create next or which endpoint to expose. Instead, we'll spend our time designing systems, deciding how data flows through them, and figuring out how they communicate without becoming tightly coupled (which AI tends to do a lot).

You'll find yourself thinking about grids, geometry, graphs, pathfinding, visibility, collision detection, serialization, and plenty of software architecture along the way. None of these topics are impossible to understand on their own. The interesting part is making all of them work together without creating a giant mess.

This is why I think this could be a really good exercise, because most of the time you stop at the API with CRUD and you feel you didn't learn anything new, here we are going to solve a bigger problem with something AI cant fully replicate, our way of thinking. Which is a little exciting I can't deny.
# Code

Every chapter builds on the previous one. So its important you follow along

We'll start with small pieces of code, make sure we understand why are they there and why they will work against other ways of doing it and these are going to be the foundation to the next chapter. By the end of the book every chapter have had built their part to create a tactical game engine.

Most of the code examples are gonna be written in small snippets with the exceptions of some of them being a little bigger ones. I find them easier to read and they are going to be language agnostic so its your job to be implementing them in the language of your choice, I will be focusing on the theory and the logic behind it.

I will use pseudo code before writing the real implementation. I don't want to get distracted with the syntax when here it's the least important part. So the idea 

```
// Pseudocode

System  -> Input
		-> Process
		-> Output
```

We'll worry about making it compile after we understand why we're writing it.