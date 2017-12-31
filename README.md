# LuisInsideFormFlowBot

![Emulator Example](https://github.com/kumar-ashwin-hubert/LuisInsideFormFlow/blob/master/EmulatorExample.gif)

Once the user has typed in the name of the person or a statement describing the name of the person in the form flow, it will be sent to LUIS API and later the name entity returned from LUIS API, will be set in the form value :

```cs
return new FormBuilder<RegisterPatientForm>()
    .Field(nameof(person_name),
        validate: async (state, response) =>
        {
            var result = new ValidateResult { IsValid = true, Value = response };
            
            //Query LUIS and get the response
            LUISOutput LuisOutput = await GetIntentAndEntitiesFromLUIS((string)response);

            //Now you have the intents and entities in LuisOutput object
            //See if your entity is present in the intent and then retrieve the value
            if (LuisOutput != null && Array.Find(LuisOutput.intents, intent => intent.Intent == "GetName") != null)
            {
                LUISEntity LuisEntity = Array.Find(LuisOutput.entities, element => element.Type == "name");

                if (LuisEntity != null)
                {
                    //Store the found response in resut
                    result.Value = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(LuisEntity.Entity);
                }
                else
                {
                    //Name not found in the response
                    result.IsValid = false;
                }
            }
            else if(LuisOutput != null && Array.Find(LuisOutput.intents, intent => intent.Intent == "None") != null)
            {
                //In case of none intent assume that the user entered a name
                result.Value = LuisOutput.query;
            }
            else
            {
                result.IsValid = false;
            }
            return result;
        })
    .Field(nameof(gender))
    .Field(nameof(phone_number))
    .Field(nameof(DOB))
    .Field(nameof(cnic))
    .OnCompletion(processHotelsSearch)
    .Build();
```


