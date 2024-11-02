import openai
FILE_ID_EGOIST = 'file-koBjdadV0yTl9aw2p7I4lz2L'
FILE_ID_SWEETY = 'file-KQVUtlGaknkX2vKNil0pvv0t'
openai.api_key = 'sk-proj-5YeeM1lOBNSYqCmm2iaKT3BlbkFJHb4KONZFHLPGcCauACoY'
#En este codigo es donde realmente se crea el modelo "tuneado"
if __name__ == '__main__':
    response = openai.fine_tuning.jobs.create(
        training_file=FILE_ID_SWEETY,
        model="gpt-3.5-turbo",
        suffix="NPC_SWEETY",
        hyperparameters={'n_epochs':4}
    )
    print(response)