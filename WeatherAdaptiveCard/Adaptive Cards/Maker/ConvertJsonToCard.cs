using AdaptiveCards;
using Microsoft.Bot.Schema;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherAdaptiveCard.Adaptive_Cards.Maker
{
    public class ConvertJsonToCard
    {
        public Attachment CreateAdaptiveCardJson(string path)
        {
            return new Attachment()
            {
                ContentType = AdaptiveCard.ContentType,
                Content = AdaptiveCard.FromJson(File.ReadAllText(path)).Card
            };
        }

        
    }
}
