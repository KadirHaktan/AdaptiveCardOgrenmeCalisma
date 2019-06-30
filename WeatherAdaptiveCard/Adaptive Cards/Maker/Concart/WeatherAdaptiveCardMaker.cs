using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdaptiveCards;
using Microsoft.Bot.Schema;
using WeatherAdaptiveCard.Adaptive_Cards.Abstract;

namespace WeatherAdaptiveCard.Adaptive_Cards.Maker.Concart
{
    public class WeatherAdaptiveCardMaker : ConvertJsonToCard,IAdaptiveCardMaker
    {
        public readonly AdaptiveCard card;

        private readonly List<string> Days = new List<string>() { "Fri", "Sat", "Sun", "Mon" };
        private readonly List<string> FirstNumbers = new List<string>() { "62", "60", "59", "64" };
        private readonly List<string> SecondNumbers = new List<string>() { "52", "48", "49", "51" };


        public WeatherAdaptiveCardMaker()
        {
            card = new AdaptiveCard();
        }
        
           
        

        private void CreatingTodayWeather()
        {
            card.Body.Add(new AdaptiveTextBlock()
            {
                Text = "Monday April1",
                Weight = AdaptiveTextWeight.Bolder,
                Color = AdaptiveTextColor.Light,
                Size = AdaptiveTextSize.Large
            });

            card.Body.Add(new AdaptiveTextBlock()
            {
                Text = "63/42",
                Size = AdaptiveTextSize.Medium,
                Spacing = AdaptiveSpacing.None,

            });

            card.Body.Add(new AdaptiveTextBlock()
            {
                Text = "20% chance of rain",
                Spacing = AdaptiveSpacing.None,
                IsSubtle = true

            });

            card.Body.Add(new AdaptiveTextBlock()
            {
                Text = "Winds 5 mph Ne",
                Spacing = AdaptiveSpacing.None,
                IsSubtle = true
            });

        }
        //-----------------------------------------//
   

        private void AddNextDayText(bool wrap=false)
        {
            foreach(var day in Days)
            {
                card.Body.Add(new AdaptiveTextBlock()
                {
                    Text = day,
                    Wrap = wrap,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center

                });
            }
            
        }

        private void AddNextDayFirstValue(bool wrap = false)
        {
            foreach(var number in FirstNumbers)
            {
                card.Body.Add(new AdaptiveTextBlock()
                {
                    Text = number,
                    Wrap = wrap,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center
                });
            } 

        }

        private void AddNextDaySecondValue(bool wrap=false,bool IsSubtle = true)
        {
            foreach(var number in SecondNumbers)
            {
                card.Body.Add(new AdaptiveTextBlock()
                {
                    Text = number,
                    Wrap = wrap,
                    HorizontalAlignment = AdaptiveHorizontalAlignment.Center,
                    IsSubtle=IsSubtle,
                    Spacing=AdaptiveSpacing.None
                });
            }
        }


        private void CreateToNextDaysWeather()
        {
            AddNextDayFirstValue();
            AddNextDaySecondValue();
            AddNextDayText();
        }


        



        public Attachment CreateAdaptiveCard()
        {
            CreatingTodayWeather();
            CreateToNextDaysWeather();

            return new Attachment
            {
                ContentType = AdaptiveCard.ContentType,
                Content = card

            };       
        }
    }
}
