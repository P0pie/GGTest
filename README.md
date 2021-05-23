# GGTest
 Grover Gaming Proficiency Test
 
 
# Setup
 
**Implemented using Unity 2020.2.7f1**

GameManager: Implements Singleton & Observer Patterns to control all actions in-game. Contains a MoneyManager class.
- IncreaseBet(), DecreaseBet(): calls MoneyManager.ChangeBet() & updates bet display.
- Event OnPlay(): calls RoundSetup(): Hides appropriate UI, calculates and 'distributes' winnings using MoneyManager, and begins game.
- Event Pooper(): calls RoundEnd(): Hides game & restores UI. Adds Winnings to Balance.
- OpenChest(): Opens chest, Adds contained winnings. If no winnings triggers Pooper event.

MoneyManager: Handles all Financials and calculations.
-  GetBet(): Uses Enum to convert (int)_bet to appropriate (float)Bet
-  GetBalance(), GettWinnings(): Return respective _var value. Note: _winnings contains total winnings. Not yet distributed.
-  AddFunds(funds): Increases Funds by input
-  PlaceBet(): Uses GetBet(), GetNewMultiplier(), & SeperateWinnings() to generate and distribute winnings.
-  GetNewMultiplier(): returns multiplier according to GG provided specifications (50% bust, 30% low, 15% mid, 5% high)
-  SeperateWinnings(): Takes input float and seperates into array. Last value is always 0. **Warning: Assumes input float is multiple of .25**
-  NextWin(): Itterates through serperated winnings and returns next value. Caution: Can technically go past valid index, but is prevented from doing so by game.
