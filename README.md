# phÿnd [![Build Status](https://travis-ci.org/halomademeapc/phyndotnet.png?branch=master)](https://travis-ci.org/halomademeapc/phyndotnet)
rudimentary "machine learning" tic-tac-toe.

Phynd is a tic-tac-toe game based on MENACE (Machine-Educable Naughts and Crosses Engine).  One key difference is that humans are allowed to go first in this, drastically increasing the system's learning curve.
The app is build on .NET Core Razor Components.


## Quickstart
```powershell
docker run --name phynd -d -p 80:80 halomademeapc/phyndweb
```

## Requirements
**To build**
* .NET Core 3.0 SDK Preview

**To run**
* .NET Core 2.0 Runtime Preview

## Building
```powershell
dotnet publish ./PhyndWeb.csproj
```

## Running
```powershell
dotnet ./PhyndWeb.dll
```