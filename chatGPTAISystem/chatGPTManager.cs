using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using UnityEngine.UI;
using TMPro;
using JetBrains.Annotations;

public class ChatGPTManager : MonoBehaviour
{
    //Data Auth
    public string apiKey = "sk-proj-5YeeM1lOBNSYqCmm2iaKT3BlbkFJHb4KONZFHLPGcCauACoY";
    //Data Tunning Model
    public static string CODE_MODEL_EGOIST = "ft:gpt-3.5-turbo-0125:personal:npc-selfish:9wSuhGgL";
    public static string CODE_MODEL_SWEETY = "ft:gpt-3.5-turbo-0125:personal:npc-sweety:9yMe4qrI";
    public static string CODE_MODEL_STANDARD = "ft:gpt-3.5-turbo-0125";
    public bool is_egoist;
    public bool is_standard;
    public bool is_sweety;
    public string CODE_MODEL;
    //Data Game
    public TextMeshProUGUI responseTextUI;
    public static ChatGPTManager instance;
    public string currentSynopsis;
    public string location;
    public string family;
    public string work;
    public string uniqueFunction;
    public string additionalInfo;
    public string model_id;


    public void Start()
    {
        if (is_egoist)
        {
            CODE_MODEL = CODE_MODEL_EGOIST;
        }

        if (is_sweety)
        {
            CODE_MODEL = CODE_MODEL_SWEETY;
        }

        if (is_standard)
        {
            CODE_MODEL = CODE_MODEL_STANDARD;
        }
    }
    public void SendRequestToChatGPT(string inputText)
    {
        responseTextUI.text = "";
        string realInputText = "";
        string realInputSystem = "";
            realInputText = inputText;
        realInputSystem = $"La sin�psis de la historia presentada es: {currentSynopsis} " +
            $" Tu ubicaci�n: {location} " + $"Tu familia: {family} " + $"Tu trabajo: {work} " + $"Tu funcion es: {uniqueFunction}. Y tambien puedes responder a preguntas del contexto actual" + $"{additionalInfo} ";

        StartCoroutine(PostRequest(realInputText, realInputSystem));
    }

    private IEnumerator PostRequest(string inputText, string inputSystem)
    {
        string url = "https://api.openai.com/v1/chat/completions";
        var requestData = new
        {
            model = CODE_MODEL,
            messages = new[]
    {
        new
        {
            role = "user",
            content = inputText
        }
    },

        };
        string jsonData = @"{
    ""model"": """ + CODE_MODEL + @""",
    ""messages"": [
        {
            ""role"": ""user"",
            ""content"": """ + inputText.Replace("\"", "\\\"") + @"""
        },
        {
            ""role"": ""system"",
            ""content"": ""Eres un NPC de la historia que debe seguir el contexto proporcionado, respondiendo a las preguntas. El contexto es: " + inputSystem.Replace("\"", "\\\"") + @"""
        }
    ],
    ""max_tokens"": 50,
    ""top_p"": 0.75,
    ""temperature"": 2.0,
    ""presence_penalty"": 2.0,
    ""frequency_penalty"": 2.0
}";





        //string jsonData = JsonUtility.ToJson(requestData2);

        Debug.Log("Request JSON: " + jsonData); // Imprime el JSON para verificar su estructura
        byte[] postData = Encoding.UTF8.GetBytes(jsonData);

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        request.uploadHandler = new UploadHandlerRaw(postData);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + apiKey);
        Debug.Log("Request JSON: " + jsonData);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error: " + request.error);
        }
        else
        {
            var responseText = request.downloadHandler.text;
            Debug.Log("Response: " + responseText);
            string [] split2Response = responseText.Split(" \"content\": \"");
            string [] finalChatGPTResponse = split2Response[1].Split("\"");
            Debug.Log("final Reponse: " + finalChatGPTResponse[0]);
            if ((finalChatGPTResponse[0].Equals("\\"))) {
                responseTextUI.text = finalChatGPTResponse[1];
            }
            else
            {
                responseTextUI.text = finalChatGPTResponse[0];
            }

            
        }
    }
}


/*
        * 
        *3. temperature
Un valor entre 0 y 2 que determina la creatividad de la respuesta. Valores m�s bajos resultan en respuestas m�s deterministas.

Ejemplo:
json
Copiar c�digo
"temperature": 0.7
4. max_tokens
El n�mero m�ximo de tokens a generar en la respuesta.

Ejemplo:
json
Copiar c�digo
"max_tokens": 100
5. top_p
Usa muestreo de n�cleo, donde el modelo considera los resultados de los tokens con una probabilidad de masa acumulativa top_p. Por ejemplo, 0.1 significa que solo se consideran los tokens que comprenden el 10% de probabilidad m�s alta.

Ejemplo:
json
Copiar c�digo
"top_p": 0.9
6. n
N�mero de respuestas a generar para cada entrada.

Ejemplo:
json
Copiar c�digo
"n": 1
7. stream
Si est� establecido en true, el modelo enviar� datos de forma incremental a medida que se generan tokens.

Ejemplo:
json
Copiar c�digo
"stream": false
8. stop
Una secuencia o una lista de secuencias donde el modelo deber�a detenerse.

Ejemplo:
json
Copiar c�digo
"stop": ["\n", "Human:"]
9. presence_penalty
Un n�mero entre -2.0 y 2.0. Valores positivos aumentan la probabilidad de que el modelo hable de nuevos temas.

Ejemplo:
json
Copiar c�digo
"presence_penalty": 0.0
10. frequency_penalty
Un n�mero entre -2.0 y 2.0. Valores positivos disminuyen la probabilidad de que el modelo repita la misma l�nea palabra por palabra.

Ejemplo:
json
Copiar c�digo
"frequency_penalty": 0.0
11. logit_bias
Un mapa de ajustes de sesgo de logit para tokens espec�ficos. Esto puede hacer que el modelo sea m�s o menos propenso a ciertos resultados.

Ejemplo:
json
Copiar c�digo
"logit_bias": {"50256": -100}
        * 

        */
