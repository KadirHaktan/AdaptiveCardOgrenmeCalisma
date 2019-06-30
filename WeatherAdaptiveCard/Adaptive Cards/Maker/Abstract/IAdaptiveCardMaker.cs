using Microsoft.Bot.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherAdaptiveCard.Adaptive_Cards.Maker;

namespace WeatherAdaptiveCard.Adaptive_Cards.Abstract
{
    public interface IAdaptiveCardMaker
    {
        Attachment CreateAdaptiveCard();       
    }

    //WeatherAdaptiveCardMaker olmak üzere ilerde başka AdaptiveCard tipleri olusturulduğunda
    //programın bağımlılık yönetimi oldukça bağımsızlaştırmada(Depency Injection uygulamak amaciyla) ve 
    //Yeni oluşturulan tipin önceki oluşturduğumuz sistemi etkilememesi için.Yani diyelim ki eğer hava durumu
    //yerine sporla ilgili bir card oluşturduğumuzda hava cardındaki kodları tek tek değiştirmektense bir tane arayüz ya da
    //görev olarak referans sağlayacak temel bir yapı oluşturup mevcut sistemi dokunmadan hızlı bir şekilde yeni bir sistem
    //oluşturarak implementasyonumuzu gerçekleştirebilmemiz için böyle bir interface tanımladık.Open Closed Principle prensibine
    //uyulabilmesi açısında işlev görecek bir yapıdır.Ayrıca Depency Injection uygulamak için de ideal bir yapı.
}
