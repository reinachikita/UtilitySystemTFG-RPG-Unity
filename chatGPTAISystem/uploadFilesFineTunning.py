import openai

openai.api_key = 'sk-proj-5YeeM1lOBNSYqCmm2iaKT3BlbkFJHb4KONZFHLPGcCauACoY'

if __name__ == '__main__':
    response_file = openai.files.create(
        file=open('_finetunning_sweety.json', 'rb'),
        purpose= 'fine-tune'
    )

    print(f' id: {response_file.id}')