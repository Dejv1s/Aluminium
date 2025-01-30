
<img align="center" src="https://i.imgur.com/kDSqUQr.png" width="256"></img>
<h1 align="center" style="font-weight: bold;">Aluminium</h1>

<p align="center">
<a href="#tech">Technologies</a>
<a href="#started">Getting Started</a>
<a href="#routes">API Endpoints</a>
<a href="#colab">Collaborators</a>
<a href="#contribute">Contribute</a> 
</p>


<p align="center">A .NET MAUI mobile application that allows users to control Keyboard on their PC through a custom C# server.</p>


<p align="center">
<a href="https://github.com/Dejv1s/Aluminium">📱 Visit this Project</a>
</p>

<h2 id="technologies">💻 Technologies</h2>

- .NET 8
- .NET MAUI
- C#
- WindowsInput Library

<h2 id="started">🚀 Getting started</h2>

Instructions to run the project locally.

<h3>Prerequisites</h3>

Here you list all prerequisites necessary for running:

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- [Visual Studio 2022 (with .NET MAUI workload)](https://visualstudio.microsoft.com/cs/vs/)
- [Git](https://github.com)

<h3>Cloning</h3>

Clone project

```bash
git clone https://github.com/Dejv1s/Aluminium
```

<h3>Starting</h3>

How to start your project
(MAUI APP)
```bash
cd project-name
dotnet build
dotnet run
```
(SERVER CLIENT)
```bash
cd project-name
dotnet build
dotnet run
```

<h2 id="routes">📍 API Endpoints</h2>

Here you can list the main routes of your API, and what are their expected request bodies.
​
| route               | description                                          
|----------------------|-----------------------------------------------------
| <kbd>GET /play_pause</kbd>     | Toggles play/pause 
| <kbd>GET /fullscreen	</kbd>     | Enters or exits fullscreen mode	
| <kbd>GET /forward </kbd>     | Skips forward	
| <kbd>GET /backward </kbd>     | Skips backward

<h3>GET /play_pause</h3>

**RESPONSE**
```json
{
  "status": "success",
  "message": "Playback toggled"
}
```

<h3>GET /fullscreen</h3>

**RESPONSE**
```json
{
  "status": "success",
  "message": "Fullscreen toggled"
}
```
<h3>GET /forward</h3>

**RESPONSE**
```json
{
  "status": "success",
  "message": "Skipped forward"
}
```
<h3>GET /backward</h3>

**RESPONSE**
```json
{
  "status": "success",
  "message": "Skipped backward"
}
```

<h2 id="contribute">📫 Contribute</h2>

Guidelines for contributing to the project:

1.  Fork the repository.
2. Create a new branch: `git checkout -b Aluminium/Dejv1s`
3. Follow commit patterns
4. Open a Pull Request explaining the problem solved or feature made, if exists, append screenshot of visual modifications and wait for the review!

<h3>Documentations that might help</h3>

Coming soon.
