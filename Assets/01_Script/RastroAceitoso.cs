using UnityEngine;

public class RastroAceitoso : MonoBehaviour
{
    public float tiempoEntreRastros = 2f; // Tiempo entre la creación de rastros
    private float duracion; // Duración en segundos antes de que el rastro desaparezca
    private float tiempoVida; // Temporizador de vida del rastro
    private Material material; // Material independiente para cada instancia de rastro

    void Start()
    {
        duracion = CalcularTiempoDuracion();
        tiempoVida = duracion; // Inicializa el temporizador de vida
        // Asigna un material único para evitar interferencias entre instancias
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            material = renderer.material = new Material(renderer.material); // Material independiente
        }
    }

    void Update()
    {
        // Reducir el tiempo de vida del rastro en cada cuadro
        tiempoVida -= Time.deltaTime;
        // Desvanecer gradualmente el rastro
        if (material != null)
        {
            Color color = material.color;
            color.a = Mathf.Clamp01(tiempoVida / duracion); // Ajuste del alfa para desvanecimiento
            material.color = color;
        }
        // Destruir el rastro cuando el tiempo de vida llega a cero
        if (tiempoVida <= 0)
        {
            Destroy(gameObject);
        }
    }

    private float CalcularTiempoDuracion()
    {
        // Calcular la duración del rastro en función del tiempo entre rastros
        return tiempoEntreRastros * 2.5f; // Por ejemplo, la duración será 2.5 veces el tiempo entre rastros
    }
}




//using UnityEngine;

//public class RastroAceitoso : MonoBehaviour
//{
//    public float duracion = 5f; // Duración en segundos antes de que el rastro desaparezca
//    private float tiempoVida; // Temporizador de vida del rastro
//    private Material material; // Material independiente para cada instancia de rastro

//    void Start()
//    {
//        tiempoVida = duracion; // Inicializa el temporizador de vida

//        // Asigna un material único para evitar interferencias entre instancias
//        Renderer renderer = GetComponent<Renderer>();
//        if (renderer != null)
//        {
//            material = renderer.material = new Material(renderer.material); // Material independiente
//        }
//    }

//    void Update()
//    {
//        // Reducir el tiempo de vida del rastro en cada cuadro
//        tiempoVida -= Time.deltaTime;

//        // Desvanecer gradualmente el rastro
//        if (material != null)
//        {
//            Color color = material.color;
//            color.a = Mathf.Clamp01(tiempoVida / duracion); // Ajuste del alfa para desvanecimiento
//            material.color = color;
//        }

//        // Destruir el rastro cuando el tiempo de vida llega a cero
//        if (tiempoVida <= 0)
//        {
//            Destroy(gameObject);
//        }
//    }
//}






//// RastroAceitoso.cs
//using UnityEngine;

//public class RastroAceitoso : MonoBehaviour
//{
//    public float duracion = 5f;
//    public float velocidadDesvanecimiento = 1f;

//    private MeshRenderer meshRenderer;
//    private float tiempoVida;

//    void Start()
//    {
//        meshRenderer = GetComponent<MeshRenderer>();
//        tiempoVida = duracion;
//    }

//    void Update()
//    {
//        tiempoVida -= Time.deltaTime;

//        // Desvanecer gradualmente
//        if (meshRenderer != null)
//        {
//            Color color = meshRenderer.material.color;
//            color.a = tiempoVida / duracion;
//            meshRenderer.material.color = color;
//        }

//        if (tiempoVida <= 0)
//        {
//            Destroy(gameObject);
//        }
//    }
//}