using Microsoft.Bot.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherAdaptiveCard.Adaptive_Cards.Abstract
{
    public interface IAdaptiveCardMaker
    {
        Attachment CreateAdaptiveCard();
       
    }
}
