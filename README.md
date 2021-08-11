<!-- PROJECT LOGO -->
<br />
<p align="center">
  <a href="https://github.com/jkrutz/capstone-2021">
    <img src="images/logo.png" alt="Logo" width="80" height="80">
  </a>

  <h3 align="center">Capstone 2021</h3>

  <p align="center">
    A third-person fantasy combat game that positions players in the middle of a magic arena armed with nothing except a spell-casting wand. Team up and battle other mages. Become champion.
  </p>
</p>



<!-- TABLE OF CONTENTS -->
<details open="open">
  <summary><h2 style="display: inline-block">Table of Contents</h2></summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
        <li><a href="#inspiration">Inspiration</a></li>
        <li><a href="#devs">The Developers</a></li>
      </ul>
    </li>
    <li>
      <a href="#core-concepts">Core Concepts</a>
      <ul>
        <li><a href="#graphics">Graphics</a></li>
        <li><a href="#multiplayer">Multiplayer</a></li>
        <li><a href="#artificial-intelligence">Artificial Intelligence</a></li>
      </ul>
    </li>
    <li><a href="#game">The Game</a>
      <ul>
        <li><a href="#player">The Player</a></li>
        <li><a href="#arena">The Arena</a></li>
        <li><a href="#spells">Spellcasting</a></li>
        <li><a href="#consumables">Consumables</a></li>
        <li><a href="#match-mode">Match Mode</a></li>
        <li><a href="#sandbox-mode">Sandbox Mode</a></li>
      </ul>
    </li>
    <li><a href="#extras">Extras</a></li>
    <li><a href="#process">Our Process</a></li>
  </ol>
</details>



<!-- ABOUT THE PROJECT -->
## About The Project

### Built With

* <a href="https://www.unrealengine.com/en-US/">Unreal Engine 4</a>
* <a href="https://www.blender.org/">Blender</a>
* Love

### Inspiration

The magic in this game is inspired by <a href="https://www.youtube.com/watch?v=02pr2W7FT-c">this scene</a> from Harry Potter and the Order of the Pheonix. In this battle, no spell is necessarily more powerful than the next, moves take a conscious effort and wit. We wanted to emulate this same kind of mechanic and throw it into an arena. In most conventional magic games, there seems to be a <a href="https://en.wikipedia.org/wiki/Rock_paper_scissors">rock-paper-scissors</a> relationship between casted spells; most often water beats fire, wind beats fire, etc. This greatly simplifies the thought process behind making moves and we believe this diminishes enjoyment. The complex and limitless interactions between two spells is what makes this game unique.

### The Developers

<ul>
  <li><a href="mailto:C22Holden.Caraway@afacademy.af.edu">Holden Caraway</a>: Game developer and network manager. Holden is a cyber major at the United States Air Force Academy</li>
  <li><a href="mailto:C22Caleb.Price@afacademy.af.edu">Caleb Price</a>: Lead developer and graphics manager. Caleb is a computer science major with a Russian minor also at USAFA</li>
  <li><a href="mailto:C22Joshua.Krutz@afacademy.af.edu">Joshua Krutz</a>: Game developer and AI manager. Joshua is a computer science and cyber science major at USAFA.</li>
</ul>


<!-- CORE CONCEPTS -->
## Core Concepts

### Graphics

Initially this development team will employ Unreal Engine Assets and free public use avatars, environments, and sounds. Later on we will explore creating our own assets using a combination of blender, cadet voice actors, and ambient sound recordings.

### Multiplayer

Players will be able to join the same game via a local host connection. If time permits, this developer team will pursue hosting games on a local server. 

### Artificial Intelligence

Spell casting movements will be appropriately classified into a spell type. The movement of casted entities will move and attack aided by AI. Computer players may be added if time permits.


<!-- THE GAME -->
## The Game

### Player

Players are gifted with magical abilities, basically wizards. Using a wand or staff, players can manifest entities or weather events into the world. Players have a set health amount, healing is possible, and will die if their HP hits zero. Players will respawn at the end of a round. There is no mana or cool down for attacks or movement, instead players are as fast or slow as they can draw our spells. Players are agile and can jump, run, walk, or crouch. 

### Arena

Players spawn in an enclosed environment, where falling off the map is not possible. To the best of our abilities we will manage glitching through buildings and the ground. This is the location where both game modes will take place. Ambient sounds will make the game feel more real and the addition of a day/night cycle will keep each game feeling a little different.

### Spells

Spells are cast via a specific mouse movement. For example, drawing a star would summon a specific spell, whereas a square would summon a different one. Spells are not projectiles but instead add entities or events into the world. Depending on the strategy of the player, a spell can be either offensive or defensive. Spells are not as simple as rock, paper, scissors; ideally any spell has the potential to undo or weaken an opponent's attack. There is no perfect defense, water does not always beat out fire and vice versa. 

### Consumables

The addition of items found around the arena will require the creation of a player inventory. Consumables have buffing effects, which will boost the effectiveness of spells, player movement speed, or heal the player. 

### Match Mode

Players are evenly assigned to one of two teams. Battling 1v1 up to 4v4, players compete to wipe out the opposing team. The first team to win four of these rounds wins!

### Sandbox Mode

Players will spawn alone in front of dummy players or targets. The idea of this game mode is to practice spell casting and get familiar with movement. This sandbox will also act as our preliminary testing environment as we add additional spells and mechanics.

<!-- EXTRAS -->
## Extras

If we proceed faster than estimated, we will pull additional features from the non exhaustive list below.

### Server Hosted Multiplayer

Hosting on a server placed at USAFA or from a third party, this option would enable multiplayer from any location. Players no longer need to be connected to the same network.

### Computer Players

This option would enable us to fill in teams with Bots. Computer Players would operate under the same abilities and restrictions as normal players and could be set up to operate at a variety of difficulties.

### Interactive Environment

Spells cast at buildings and the ground could now physically affect the environment.  Buildings can collapse, trees can fall, craters can be formed. All of these can damage a player and add a new dimension into our gameplay and strategy.

### Cyber City Render

Model the entirety of Cyber City in Blender as the battle arena. The buildings would be open for players to enter, and similar to above, may be destroyed or modified during game play. 


<!-- PROCESS -->
## Our Process

We are implementing an agile framework, Scrum, to organize this project. The time period of August to December 2021 is broken up into four Sprints. Details about each Sprint can be found below. The purpose of a Sprint is to continuously improve our product and build out content in manageable chunks.

<ul>
  <li><a href="https://github.com/jkrutz/capstone-2021/tree/main/sprint1">Sprint 1</a>
    <ul>
      <li>Research and planning phase</li>
      <li>Familiarize team with Unreal Engine</li>
      <li>Synthesize a basic 3D "<a href=https://en.wikipedia.org/wiki/Mario">Mario</a>" game</li>
    </ul>
  </li>
