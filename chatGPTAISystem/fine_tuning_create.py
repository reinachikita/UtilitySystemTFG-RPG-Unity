

import langchain as langchain
import openai as openai
import tiktoken as tiktoken
import os
import numpy as np
import json
import openai



def save_to_json(dataset,file_path):
    with open(file_path, 'w') as file:
        for ejemplo in dataset:
            json_line = json.dumps(ejemplo)
            file.write(json_line + '\n')
#método para formatear el ejemplo e iterar
#asignacion de valores:
# - system: contexto
# - user: pregunta del usuario
# - assistant: respuesya de openAI
def formatear_ejemplo(lista_mensajes, system_message):
    messages = []

    # Incluir primero el mensaje de sistema
    if system_message:
        messages.append({
            "role": "system",
            "content": system_message
        })

    # Iterar por la lista de mensajes
    for mensaje in lista_mensajes:
        # Separar los mensajes por los dos puntos y el espacio
        partes = mensaje.split(': ', maxsplit=1)

        # Controlar si alguna línea no cumple el patrón
        if len(partes) < 2:
            continue

        # Identificar el rol y content
        role = partes[0].strip()
        content = partes[1].strip()

        # Formatear el mensaje
        message = {
            "role": role,
            "content": content
        }
        # Agregar el mensaje a la lista
        messages.append(message)

    # Crear diccionario final
    dict_final = {
        "messages": messages
    }

    return dict_final

def num_tokens_from_messages(messages, tokens_per_message=3, tokens_per_name=1):
    num_tokens = 0
    encoding = tiktoken.get_encoding("cl100k_base")
    for message in messages:
        num_tokens += tokens_per_message
        for key, value in message.items():
            num_tokens += len(encoding.encode(value))
            if key == "name":
                num_tokens += tokens_per_name
    num_tokens += 3
    return num_tokens

def num_assistant_tokens_from_messages(messages):
    num_tokens = 0
    encoding = tiktoken.get_encoding("cl100k_base")
    for message in messages:
        if message["role"] == "assistant":
            num_tokens += len(encoding.encode(message["content"]))
    return num_tokens

def print_distribution(values, name):
    print(f"\n#### Distribución de {name}:")
    print(f"min / max: {min(values)}, {max(values)}")
    print(f"media / mediana: {np.mean(values)}, {np.median(values)}")
    print(f"p5 / p95: {np.quantile(values, 0.1)}, {np.quantile(values, 0.9)}")



if __name__ == '__main__':
    #CARGAMOS API-KEY
    os.environ['TEST_API_KEY_CHATGPT'] = "sk-proj-5YeeM1lOBNSYqCmm2iaKT3BlbkFJHb4KONZFHLPGcCauACoY"
    #Mostramos Api key para confirmar que se ha asignado correctamente a la variable de entorno
    print(os.environ['TEST_API_KEY_CHATGPT'])
    #PASO 1 - PREPARAR DATOS: Leemos linea a linea nuestro
    #Archivo txt lleno de diálogos con un modelo y contestaciones especificas
    #Por ejemplo, en este caso he desarrollado contestaciones típicas
    #Para un modelo de NPC, egoista, arrogante y narcisista.
    with open('_finetunning_sweety.txt') as f:
        text = [line for line in f]
    #Contexto para que el sistema de openAI entienda que se le pide.
    system_message = 'Eres un NPC de un videojuego JRPG, cuyo contexto historico ha de ser hablar de la forma mas natural, comun ' \
                     'y cercana posible proveniente de un habla comun de Espana del siglo XXI en pleno 2024.'
    falseDataBase = []
    ejemplo = []
    for line in text:
        if line == '-\n':
            ejemplo_formateado = formatear_ejemplo(lista_mensajes=ejemplo,
                                                   system_message=system_message)

            falseDataBase.append(ejemplo_formateado)
            ejemplo = []
            continue

        ejemplo.append(line)

    # Last, we can look at the results of the different formatting operations before proceeding with creating a fine-tuning job:

    # Warnings and tokens counts
    n_missing_system = 0
    n_missing_user = 0
    n_messages = []
    convo_lens = []
    assistant_message_lens = []

    for ex in falseDataBase:
        messages = ex["messages"]
        if not any(message["role"] == "system" for message in messages):
            n_missing_system += 1
        if not any(message["role"] == "user" for message in messages):
            n_missing_user += 1
        n_messages.append(len(messages))
        convo_lens.append(num_tokens_from_messages(messages))
        assistant_message_lens.append(num_assistant_tokens_from_messages(messages))

    print("Num de ejemplos sin el system message:", n_missing_system)
    print("Num de ejemplos sin el user message:", n_missing_user)
    print_distribution(n_messages, "num_mensajes_por_ejemplo")
    print_distribution(convo_lens, "num_total_tokens_por_ejemplo")
    print_distribution(assistant_message_lens, "num_assistant_tokens_por_ejemplo")
    n_too_long = sum(l > 4096 for l in convo_lens)
    print(
        f"\n{n_too_long} ejemplos que excedan el límite de tokenes de 4096, ellos serán truncados durante el fine-tuning")
    # Pricing and default n_epochs estimate
    MAX_TOKENS_PER_EXAMPLE = 4096

    MIN_TARGET_EXAMPLES = 100
    MAX_TARGET_EXAMPLES = 25000
    TARGET_EPOCHS = 4
    MIN_EPOCHS = 1
    MAX_EPOCHS = 25

    n_epochs = TARGET_EPOCHS
    n_train_examples = len(falseDataBase)
    if n_train_examples * TARGET_EPOCHS < MIN_TARGET_EXAMPLES:
        n_epochs = min(MAX_EPOCHS, MIN_TARGET_EXAMPLES // n_train_examples)
    elif n_train_examples * TARGET_EPOCHS > MAX_TARGET_EXAMPLES:
        n_epochs = max(MIN_EPOCHS, MAX_TARGET_EXAMPLES // n_train_examples)

    n_billing_tokens_in_dataset = sum(min(MAX_TOKENS_PER_EXAMPLE, length) for length in convo_lens)
    print(
        f"El conjunto de datos tiene ~{n_billing_tokens_in_dataset} tokens que serán cargados durante el entrenamiento")
    print(f"Por defecto, entrenarás para {n_epochs} epochs en este conjunto de datos")
    print(f"Por defecto, serás cargado con ~{n_epochs * n_billing_tokens_in_dataset} tokens")
    print("Revisa la página para estimar el costo total")
    #guardar base de datos en JSON
    save_to_json(falseDataBase, '_finetunning_sweety.json')