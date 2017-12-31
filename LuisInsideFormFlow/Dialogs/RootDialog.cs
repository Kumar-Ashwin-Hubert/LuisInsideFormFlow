using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.FormFlow;
using LuisInsideFormFlow.Form;

namespace LuisInsideFormFlow.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var estimatorForm = new FormDialog<RegisterPatientForm>(new RegisterPatientForm(), RegisterPatientForm.BuildForm, FormOptions.PromptInStart, null);
            context.Call<RegisterPatientForm>(estimatorForm, FormCompleteCallbackAsync);
            //var activity = await result as Activity;

            //// calculate something for us to return
            //int length = (activity.Text ?? string.Empty).Length;

            //// return our reply to the user
            //await context.PostAsync($"You sent {activity.Text} which was {length} characters");

            //context.Wait(MessageReceivedAsync);
        }

        private async Task FormCompleteCallbackAsync(IDialogContext context, IAwaitable<RegisterPatientForm> result)
        {
            var message = await result;
            string text = $"## Patient Details : \n\n* **Name** : {  message.person_name} " +
                            $"\n* **Gender** : { message.gender.ToString() } " +
                            $"\n* **Phone Number** : { message.phone_number} " +
                            $"\n* **Date of birth** : { message.DOB } " +
                            $"\n* **CNIC Number** : { message.cnic } ";
            
            await context.PostAsync(text);
            context.Wait(MessageReceivedAsync);
        }
    }
}