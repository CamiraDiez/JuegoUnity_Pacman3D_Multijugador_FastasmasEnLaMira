# Videogame Design Document
## Sección 1 - Generalidades
1. Título:  
Fantastas a la Mira

2. Género del juego:  
Arcade – Aventura – Plataformas Clásico

3. Perspectiva:  
Tercera persona 3D  

4. Modo(s) de juego:
- Single player  
- Multi player (opcional)  

5. Audiencia objetivo:
- Edades 16-30
- Fans de los juegos de plataforma retro
- Fans de juegos casuales y cooperativos

6. Idea central:  
El jugador controla un personaje estilo Pac-Man que debe recolectar DOTs dispersos por un laberinto mientras evita a tres fantasmas enigmáticos que se mueven astutamente, evadiendo paredes y obstáculos. Al recolectar suficientes puntos, aparece una Cherry especial como símbolo de victoria.

7. Objetivo del juego:  
Recolectar 20 DOTs para activar la Cherry.
Atrapar la Cherry para ganar la partida.
Evitar colisionar con los fantasmas.
Si el jugador choca con un fantasma, pierde y se activa Game Over con una animación de destrucción.

8. Tópico del juego:  
Juego de Plataforma Arcade Heróico, con temática retro y fantasmas animados

9. Plataformas disponibles:
- PC (Windows, Linux, Mac)


## Sección 2 - Background & Flujo del juego
1. Contexto:  
El juego se desarrolla en un laberinto colorido inspirado en los clásicos arcades de los 80’s, donde el jugador debe mostrar reflejos rápidos y estrategia para recolectar todos los puntos y burlar a los fantasmas.

2. Historia del personaje:  
El protagonista es un Pac-Héroe anónimo, un aventurero hambriento de puntos que recorre laberintos místicos para demostrar su destreza y escapar de los fantasmas guardianes.

3. Historia de los enemigos:  
Los tres fantasmas misteriosos son criaturas espectrales que protegen el laberinto. Cada uno tiene su propia “personalidad” animada y se desplazan con astucia, rodeando esquinas y evitando paredes para atrapar al jugador.

4. Desarrollo de la historia:  
_describir cómo se desarrolla la historia del juego a medida que le personaje avanza en el mismo_  
La historia se desarrolla a través del progreso del jugador:
Nivel 1: Primer laberinto básico.
Nivel 2: Segundo laberinto más complicado, con más obstáculos.
El progreso es simple: recolectar, esquivar, sobrevivir.

## Seccción 3 - Juego
1. Objectivo(s):
- Recolectar todos los DOTs para desbloquear la Cherry.
- Escapar de los fantasmas evitando colisiones.
- Capturar la Cherry para ganar.

2. Reglas del juego:
- Si el jugador colisiona con un fantasma → Game Over.
- Cada DOT recolectado suma +1 al puntaje.
- Al llegar a 20 DOTs, aparece la Cherry.
- Al tocar la Cherry → Puntaje cambia a “WIN” y se termina el nivel.
- El jugador puede reiniciar desde el menú Game Over o avanzar desde la pantalla Ganaste.

3. Mecánica del juego:
- Movimientos del personaje: Desplazamiento libre en 4 direcciones (arriba, abajo, izquierda, derecha).
- Movimientos de los enemigos: IA simple para patrullar y evitar obstáculos.
- Obstáculos y trampas: Paredes del laberinto. Los fantasmas rodean esquinas evitando paredes.

4. Completar con varios o todos de los siguientes (a necesidad)
• Game options: Reiniciar nivel, menú principal.
• Modes: Un solo modo clásico, con opción a multijugador.
• Game levels: Level_01 y Level_02. 
• Player’s controls:
• Winning: Al recolectar la Cherry después de 20 DOTs.
• Losing: Colisión con fantasmas → animación de destrucción + Game Over.
• End: Pantalla Ganaste o Game Over.
• Why is all this fun? - Nostalgia retro, reflejos rápidos, fantasmas impredecibles, sensación de logro al burlar enemigos.

## Section 4 – Game Elements
1. Environment:
Laberinto 3D colorido y estilizado, con paredes sólidas que forman corredores angostos y esquinas cerradas. 

2. Personajes:  
   2.1 Personajes Jugadores:
      - Pac-Héroe Verde: Uno de los dos jugadores controlará este personaje. Es ágil, su misión es recolectar puntos DOT mientras esquiva fantasmas. Puede colaborar con el Pac-Héroe Azul para recolectar más rápido.
      - Pac-Héroe Azul: Igual de rápido y ágil. Comparte la misma meta: recolectar puntos y sobrevivir a los fantasmas. Ambos personajes pueden moverse de forma independiente pero colaboran para alcanzar la Cherry final.

   2.2 Personajes NO Jugadores:
      - Fantasma Azul: Fantasma rápido y directo. Persigue a los jugadores por los corredores principales, buscando interceptarlos. Evita paredes y obstáculos con movimientos inteligentes.
      - Fantasma Rosa: Se mueve de forma impredecible, patrullando rutas diferentes cada cierto tiempo. Cambia de dirección sorpresivamente para emboscar jugadores.
      - Fantasma Blanco: Patrulla zonas específicas del laberinto. Es más lento, pero cubre rutas estrechas donde puede atrapar jugadores desprevenidos.

3. Armas o elementos colectables:  
- DOT: Esferas brillantes que suman 1 punto cada una. Se encuentran dispersas en el laberinto.
- Cherry: Elemento especial que aparece al sumar 20 puntos. Al recoger la Cherry, la partida termina con un mensaje de **“WIN”** para ambos jugadores.

## Sección 5 – Game Play I/O Controls & UI Interfaces
1. Game Play I/O Controls:
- Teclado  
  Movimiento del personaje:
  - Arrow keys <- ->
  - A and D keys  
  Special keys:
  - Space = jump
  - E = interaction (e.g. open doors)
  - Esc = pause
- Control de Xbox  
  _describir los controles_  
2. GUI Interfaces:

![alt text][wireframe]  
![alt text][Level1]  
<!-- Referencias para las imagenes -->
[wireframe]: /Assets/Readme_Img/wireframes_UI.jpg "Wireframe de las UI" 
[Level1]: /Assets/Readme_Img/Level1.png "Esquema para el nivel 1"  

   2.1 Main Menu Interface:  
       _describir las opciones del manú ppal_  
   2.2 Pause Menu:  
       _describir las opciones del menú de pausa_  
   2.3 Options Menu:  
       _describir las opciones del menú de opciones_  

## Sección 6 – Características Visuales y de Audio 
_describir las características visuales y de sonido del videojuego_

## TODAS LAS OTRAS SECCIONES QUE PUEDAN SER NECESARIAS