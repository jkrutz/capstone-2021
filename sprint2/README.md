# Sprint 2

<!-- TABLE OF CONTENTS -->
<details open="open">
  <summary><h2 style="display: inline-block">Table of Contents</h2></summary>
  <ol>
    <li>
      <a href="#Summary">Summary</a>
    </li>
    <li>
      <a href="#Deliverables">Deliverables</a>
    </li>
    <ul>
      <li>
        <a href="#Gesture-Recognizer">Gesture Recognizer</a>
      </li>
      <li>
        <a href="#Computer-Player">Computer Player</a>
      </li>
      <li>
        <a href="#More-Spells">More Spells</a>
      </li>
      <li>
        <a href="#Aiming-System">Aiming System</a>
      </li>
      <li>
        <a href="#Debug-HUD">Debug HUD</a>
      </li>
      <li>
        <a href="#Bug-Fixes">Bug Fixes</a>
      </li>
    </ul>
    <li>
      <a href="#Tutorials">Tutorials</a>
    </li>
  </ol>
</details>



<!-- SUMMARY -->
## Summary

Sprint 2 furthers the game's natural feel. We optimize player controls and add in more abilities. This Sprint gives way to an actual playable game at its end. With some enhanced graphics the game starts to slowly feel more complete.

<!-- Deliverables -->
## Deliverables

The following are all features and concepts we are tackling in this Sprint. Each feature adds or improves on the game to overall contribute to a much more fun game to play.

### Gesture Recognizer

- [ ] Completed

Implementing the <a href="http://faculty.washington.edu/wobbrock/pubs/uist-07.01.pdf">$1 Gesture Recognizer</a>, this algorithm allows us to use Euclidean distance calculations to pretty accurately attribute a scribbled spell to a known dictionary of spells


### Computer Player

- [ ] Completed

This computer player moves around the map, can jump, and can most importantly cast spells.

### More Spells

- [ ] Fire Spell Completed
- [ ] Wall Spell Completed
- [ ] Explosion Spell Completed
- [ ] Disarm Spell Completed

Sprint 1 included one spell: Fire. This sprint improves fire mechanics, as well as adding in 3 new spells: Disarm, Wall, and Explosion. Disarm does exactly that, it prevents the player (or computer) from casting spells, until their wand or staff is retrieved. Wall summons a defensive barrier in front of players. And Explosion, manifests a ball of energy that damages and moves players across the map.

### Aiming System

- [ ] Completed

Spell casting is not necessarily easy at this point in time, and it is unclear on where a spell is being cast to. A crosshair system seeks to clear up this confusion.

### Debug HUD

- [ ] Completed

A heads-up display (HUD) provides information that will be vital for the debug process. Containing universal coordinates, rotation, and health we can communicate to the programming team exactly what a player's attributes are and provide context for why things work/don't work.

### Bug Fixes

- [ ] Gravity Normalized Completed
- [ ] Stair Collisions Completed
- [ ] Spell Casting Depth Completed

In this Sprint, we seek to correct the issues from Sprint 1, including but not limited to stair collisions, spell casting depth, and weird gravity.


<!-- TUTORIALS -->
## Tutorials

This section showcases all referenced tutorials and provides documentation on where code may have originated from.

<ul>
  <li>
    <a href="">
      Ref 1
    </a>
  </li>
</ul>
