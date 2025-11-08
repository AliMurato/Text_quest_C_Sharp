## Text Quest (Interactive Fiction)

**Author**: Ali Khudaimuratov

**Course**: Informatics — Programming

**University**: Czech University of Life Sciences Prague, FEM

**Framework**: .NET 8 Console Application

**Language**: C#

**Type**: University Project

---

## Project Overview

Text Quest is a console-based interactive fiction game.
The player wakes up in a mysterious room and must solve a series of riddles to escape.
It’s a simple text adventure with branching logic, dialogue-like narration, and two possible endings depending on the player’s choices.

## Main Menu

When the program starts, the player is greeted by the main menu:
```
1 - Start       // begin the game
2 - History     // show previous results (nickname, date, score, ending)
3 - Info        // show game description and rules
4 - Settings    // delete results, controls, show answers
5 - Exit        // quit the program
```
The player can navigate the menu using the keys ↑ ↓ to move, press Enter to select and ESC to exit.

## Gameplay

After selecting Start, the following sequence begins:
1. The player sets the text printing speed (typing effect).
2. The player enters their nickname. If no name is entered, a default one (“Daniel”) is used.
3. The current date is set automatically.
4. The intro story is displayed, introducing the setting.
5. The player faces 10 riddles, one at a time.

Each riddle must be answered correctly to continue.
Wrong answers trigger the Hangman mechanic (you have 5 mistakes allowed).
After every correct answer, the player can choose to continue or quit — quitting leads to a bad ending.

When the game ends, results are saved in a CSV file (```GameResults.csv```):

## Class Structure

The project consists of three main classes:

```Program```

- Entry point (```Main```)

- Displays the main menu

- Handles ```InfoLoader()``` and ```Settings()``` functions

```Game```

Contains the entire gameplay logic:

- ```Start()``` — initializes the game (nickname, date, intro)

- ```StoryTeller()``` — main game loop with riddles

- ```YesOrNot()``` — two-choice menu (Yes/No)

- ```Final()``` — determines ending and saves results

- ```TruthSeeker(string answer)``` — checks if the answer is correct

- ```HistorySaver()``` / ```HistoryLoader()``` — handles CSV file operations

- ```Hangman(int attempts)``` — displays the hangman (remaining tries)

- ```Writer(string words, int speed)``` — typing effect

- ```StartIntro()``` — shows introduction text

- ```SpeedOfText()``` — allows custom text speed

- ```KeyPresser()``` — custom Console.ReadKey() allowing ESC exit

```Texts```

Contains all static text data:

- Intro sequences

- Ten riddles

- Correct answers

## Data Storage
Game results are stored in a CSV file named ```GameResults.csv```.

Data is appended (not overwritten) after each game.

The file can be deleted from the Settings menu.

### Example of Saved Results
```
Date       | Nickname  | Score | Ending
----------------------------------------
11.11.2025 | Daniel    | 9     | bad
----------------------------------------
```


## Settings Menu
```
1 - Control keys          // shows input controls
2 - Delete history        // removes GameResults.csv
3 - Answers to riddles    // shows answers (password: amnesia)
4 - Return to menu
```

## Inputs and Controls

| Input Type   | Validation & Handling                                                |
| ------------ | -------------------------------------------------------------------- |
| **Nickname** | If empty → defaults to “Daniel” (`string.IsNullOrWhiteSpace()`)      |
| **Answer**   | Converted to lowercase (`ToLower()`) for case-insensitive comparison |
| **Speed**    | Must be between 15 and 50 (`int.TryParse()` + validation)            |
| **Menu**     | Navigation via arrow keys ↑ ↓ and `Enter`; exit with `ESC`           |

## Game Logic Summary

Total riddles: **10**

Allowed mistakes: **5**

Two possible endings: **good** or **bad**

Data stored automatically in ```GameResults.csv```

Controlled entirely via keyboard

Animated “typing” text for immersion

## Technologies Used
| Component          | Description              |
| ------------------ | ------------------------ |
| **.NET 8 SDK**     | Framework version        |
| **C#**             | Primary language         |
| **Console API**    | UI and keyboard handling |
| **System.IO**      | Reading/writing CSV data |
| **Thread.Sleep()** | Typing text animation    |
| **DateTime.Now**   | Automatic date handling  |

## Project Structure
```
TextQuest/
│
├── Program.cs         # Main menu and info/settings
├── Game.cs            # Core game logic
├── Texts.cs           # Intro, riddles, and answers
├── GameResults.csv    # Results file (auto-generated)
├── .gitignore
└── README.md
```

## Development Notes
- The date is set automatically using ```DateTime.Now```, removing user input errors.

- Text speed validation uses a loop with ```int.TryParse()``` and bounds checking.

- Answers are normalized to lowercase to prevent mismatches.

- The sixth riddle accepts both “emptiness” and a blank input (```string.IsNullOrWhiteSpace(answer)```).
