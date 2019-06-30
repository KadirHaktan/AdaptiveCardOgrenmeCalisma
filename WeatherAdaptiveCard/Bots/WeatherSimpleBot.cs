using AdaptiveCards;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WeatherAdaptiveCard.Adaptive_Cards.Maker.Concart;

namespace WeatherAdaptiveCard.Bots
{
    public class WeatherSimpleBot : IBot
    {
        private readonly WeatherAdaptiveCardMaker maker;


        public WeatherSimpleBot()
        {
            maker = new WeatherAdaptiveCardMaker();
        }


     
        public async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (turnContext.Activity.Value != null)
            {
                await turnContext.SendActivityAsync(turnContext.Activity.Value.ToString());
            }

            if (turnContext.Activity.Type == ActivityTypes.Message)
            {
                var response = turnContext.Activity.CreateReply();
                response.Attachments = new List<Attachment>
                {
                    maker.CreateAdaptiveCardJson("weatherSample.json")
                };

                await turnContext.SendActivityAsync(response);
            }
        }
    }
}
