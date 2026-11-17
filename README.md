# TaskOnHand

A minimal WPF to-do app for Windows. Each day gets its own list, saved as a dated JSON file.

![C#](https://img.shields.io/badge/C%23-512BD4?style=flat-square&logo=dotnet&logoColor=white)
![WPF](https://img.shields.io/badge/WPF-.NET_Framework_4.7.2-0C54C2?style=flat-square)

## Features

- Shows today's date and loads today's task list automatically
- Add and remove tasks
- Saves each day's list to `MM_dd_yyyy.json` with Newtonsoft.Json, so past days stay on disk

## Build

Open `TaskOnHand.sln` in Visual Studio, restore NuGet packages, and run.

## License

GPL-3.0
