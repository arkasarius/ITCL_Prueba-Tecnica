# ITCL_Prueba Tecnica
 Prueba técnica ITCL Documentación


### Archivo: `DeployArea.cs`

-   **Función Principal:**
    -   `OnTriggerEnter(Collider other)`: Se dispara cuando un objeto con el tag "Player" o "Npc" entra en el área de activación. Limpia el inventario del objeto que ha entrado.

----------

### Archivo: `Inventory.cs`

-   **Funciones Principales:**
    -   `AddItem(Item newItem)`: Agrega un ítem a la lista de ítems del inventario si no ha alcanzado el límite.
    -   `ClearInventory(GameObject prop)`: Limpia el inventario del objeto especificado y muestra un feedback visual.
    -   `IsInventoryFull()`: Devuelve verdadero si el inventario está lleno.

----------

### Archivo: `InventoryDisplay.cs`

-   **Funciones Principales:**
    -   `Awake()`: Encuentra y asigna el objeto hijo con la etiqueta "InventoryDisplay".
    -   `OnEnable()`: Se suscribe al evento OnChange del inventario cuando el script está habilitado.
    -   `OnDisable()`: Se cancela la suscripción al evento OnChange del inventario cuando el script está deshabilitado.
    -   `DisplayItems(List<Item> items)`: Borra los objetos hijos del GameObject y crea instancias de los objetos correspondientes a los ítems en la posición correcta.
    -   `SpawnItems(Item item, Vector3 pos)`: Instancia un objeto de visualización de ítem en la posición especificada.

----------

### Archivo: `Item.cs`

-   **Función Principal:**
    -   `Item`: Clase que representa un ítem en el juego. Contiene propiedades como nombre, descripción, valor y malla de visualización.

----------

### Archivo: `ItemControls.cs`

-   **Función Principal:**
    -   `OnTriggerEnter(Collider other)`: Se dispara cuando un objeto con el tag "Player" o "Npc" entra en el área de activación. Agrega el ítem al inventario del objeto y destruye el GameObject.

----------

### Archivo: `ItemSpawner.cs`

-   **Función Principal:**
    -   `Awake()`: Inicia una rutina para generar ítems de forma periódica.

----------

### Archivo: `AnimationState.cs`

-   **Funciones Principales:**
    -   `Awake()`: Encuentra y asigna el Animator del objeto.
    -   `SetState(GameplayState s)`: Establece el estado de animación del objeto.

----------

### Archivo: `GameplayState.cs`

-   **Enum:**
    -   `GameplayState`: Enumeración que define los estados de juego ("Idle" y "Moving").

----------

### Archivo: `NpcEventSystem.cs`

-   **Función Principal:**
    -   `Awake()`: Inicializa variables y comienza una rutina para el comportamiento del NPC.
    -   `NpcBehavior()`: Rutina que maneja el comportamiento del NPC, como depositar ítems en un área o buscar nuevos ítems.

----------

### Archivo: `ParticleController.cs`

-   **Función Principal:**
    -   `Update()`: Controla las partículas según el estado de animación del objeto.

----------

### Archivo: `PlayerController.cs`

-   **Funciones Principales:**
    -   `OnEnable()` y `OnDisable()`: Configura y desconfigura las acciones del jugador.
    -   `OnTouch(InputAction.CallbackContext context)`: Maneja las interacciones táctiles del jugador.
    -   `Pinch(InputAction.CallbackContext context)`: Maneja el gesto de pellizco para hacer zoom en la cámara.
    -   `Update()`: Actualiza la animación del jugador y verifica la presencia de UI.
    -   `MobileZoom()` y `MobileZoomCancel()`: Rutina y método para el zoom móvil.

----------

### Archivo: `UIManager.cs`

-   **Funciones Principales:**
    -   `OnEnable()` y `OnDisable()`: Suscribe y cancela la suscripción al evento de aumento de puntuación.
    -   `EventsScoreIncrease(int score)`: Maneja el aumento de puntuación y actualiza la UI.
    -   `Awake()`: Inicia una rutina para manejar el tiempo del juego y establece el tiempo de inicio.
    -   `TimeHandler()`: Rutina que maneja la cuenta regresiva del tiempo del juego.
    -   `UpdateUI()`: Actualiza la UI del tiempo.
    -   `TogglePause()`: Alterna la escala de tiempo para pausar y reanudar el juego.
    -   `ResetLevel()`: Reinicia el nivel actual.
    -   `ToggleQualitySettings()`: Alterna la visibilidad del menú de configuración de calidad.
    -   `SetQualityLevel(int qualityLevel)`: Establece el nivel de calidad del juego.
    -   `LoadMenu()`: Carga la escena del menú principal.
    -   `TimesUp()`: Maneja la lógica de finalización del juego cuando se agota el tiempo.

----------

### Archivo: `Events.cs`

-   **Eventos:**
    -   `ScoreIncrease`: Evento que se activa cuando hay una actualización en los puntos.

----------

### Archivo: `MainMenuController.cs`

-   **Funciones Principales:**
    -   `PlayButton()`, `GraphicsButton()`, `EraseDataButton()`, `ExitButton()`: Manejan las acciones del menú principal.
    -   `ReturnMenuButton()`: Restaura el menú principal.
    -   `SetQualityLevel(int qualityLevel)`: Establece el nivel de calidad del juego.
    -   `SetPoints()`: Actualiza la puntuación y la configuración de calidad según PlayerPrefs.
    -   `ErasePoints()`: Borra todos los datos almacenados en PlayerPrefs "score".
    -   `Awake()`: Configura la puntuación y el nivel de calidad al inicio.
    -   `LoadLevel(int level)`: Carga el nivel correspondiente y verifica si se cumplen los requisitos para desbloquear un nivel.

----------

### Archivo: `PointsFeedback.cs`

-   **Funciones Principales:**
    -   `SetPoints(int points)`: Modifica el texto de retroalimentación de puntos.

----------

### Archivo: `SelfDestroyDelay.cs`

-   **Funciones Principales:**
    -   `Awake()`: Destruye el objeto después de un tiempo especificado.

----------

### Archivo: `SmoothCameraFollow.cs`

-   **Funciones Principales:**
    -   `Awake()`: Configura el desplazamiento desde el jugador y el objeto de la cámara.
    -   `LateUpdate()`: Actualiza el desplazamiento de la cámara suavemente hacia el jugador.
```