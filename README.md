# Swivel Code Challenge
A simple search application that facilitates searching of json content.

# Tech stack
* .NET Core 3.0

# IDE
* Visual Studio 2019 community 
 
# Libraries used
* Newtonsoft.json
* Serilog (logging framework)
* xunit (unit testing framework)

# Solution structure
      Swivel.Search (Solution)
      |
      |_____ Swivel.Search.ConsoleApp (Main App)
      |
      |_____ Core (Core framework)
      |      |____ Swivel.Search.Service
      |      |____ Swivel.Search.Repo
      |      |____ Swivel.Search.Data
      |      |____ Swivel.Search.Model
      |      |____ Swivel.Search.Common
      |
      |_____ Test (xunit projects)
      |      |____ Swivel.Search.Service.Test
      |      |____ Swivel.Search.Test.Helper
      |  
      |_____ Data (JSON data files)
             |____ organizations.json
             |____ tickets.json
             |____ users.json
              
# Instructions to run the app
1. Download the zip file from the following location
2. Unzip the "Published.zip" and run "Swivel.Challenge.exe"

Note: You can also build & run the app on visual studio 2019 community version. The start-up project is "Swivel.Search.Console".
