# (Fantasmas en la Mira)
Es un juego estilo *Pac-Man* en 3D donde los jugadores enfrentan el reto de escapar de tres enigmáticos fantasmas que se deslizan astutamente por el laberinto, evitando obstáculos y paredes. 
### Tecnologías:  
- Unity GameEngine
- C#
- Photon PUN 2
### Paquetes de Assets
Estos paquetes de Assets son utilizados en el juego y pueden o no estar incorporados en el repositorio[^1].
- [Chomp Man - Kit de juego 3D y tutorial](https://assetstore.unity.com/packages/templates/tutorials/chomp-man-3d-game-kit-tutorial-174982#reviews)
- [Photon PUN 2](https://assetstore.unity.com/packages/tools/network/pun-2-free-119922)

### Documento de Diseño
[VDD](/VDD/README.md)  
Contiene el plan completo del juego: concepto, mecánicas, flujo de escenas, referencias visuales y roles del equipo.

### Tareas Pendientes (ToDo)
- [ ] Crear wireframes de la UI
- [ ] Crear escena de inicio con botón *Comenzar*
- [ ] Diseñar la primera escena (Level_01)
- [ ] Diseñar escena de **Game Over**
- [ ] Diseñar escena de **Ganaste**
- [ ] Diseñar escena **Level_02**
- [ ] Implementar lógica de colisión entre jugador y fantasmas
- [ ] Programar acumulación de puntos y aparición de Cherry
- [ ] Preparar conexión Photon PUN 2 para multijugador
- [ ] Pruebas y pulido final

### Notas
- Cada escena debe incluir UI clara para mostrar puntaje, estados de *Game Over* y *Ganaste*.
- Asegurarse de configurar el **Build Settings** para incluir todas las escenas.


