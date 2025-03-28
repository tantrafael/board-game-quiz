# Board Game Quiz

Simple tile-based board game with question cards on certain tiles. An exercise in separating game logic and
presentation. Focused on creating a pure game logic, independent of everything in Unity scenes or game objects. The
complete game state at any point is defined by a data structure. The game logic is updated with a pure deterministic
function, taking a game state as input and returning an updated game state as output. A complete chronological history
of states is kept, allowing skipping back and forth like on a timeline. An invalid game state is simply recreated by
inputting the preceding valid game state, all in order to facilitate debugging. Saving and loading at any time is easily
done as well. The game logic hands a state to the game presentation which then handles placement and animation of player
pieces, quiz screens and everything concerning the visual representation accordingly.
