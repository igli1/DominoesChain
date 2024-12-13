# Circular Dominoes Solver

This project is a **Circular Domino Chain Solver** implemented in C#. It allows users to input a set of dominoes and checks if they can form a valid circular chain, where:
- Each domino's right number matches the left number of the next domino.
- The first domino's left number matches the last domino's right number, forming a circular chain.

### Requirements
- **.NET 8.0 SDK**: Ensure that you have the .NET 8 SDK installed on your system.

### How to Run
1. **Clone or Download the Repository**:
   ```bash
   cd CircularDominoes
   dotnet restore
   dotnet build
   dotnet run
