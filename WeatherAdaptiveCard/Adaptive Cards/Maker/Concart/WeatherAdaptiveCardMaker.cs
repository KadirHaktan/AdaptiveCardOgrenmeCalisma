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
        //Kart oluştururken bir tane kart nesnesine ithiyacımız var ve bu nesne sadece okunabilir yaptık.Çünkü
        //tekrar tekrar nesne üzerinde değişiklik yapılmayacaktır.Program boyunca nerde newleniyor ise o newlediği
        //şekilde kullanacağımıza dair bir önlem almış olduk.Singleton mantığı gibi düşünelebilir

        private readonly List<string> Days = new List<string>() { "Fri", "Sat", "Sun", "Mon" };
        private readonly List<string> FirstNumbers = new List<string>() { "62", "60", "59", "64" };
        private readonly List<string> SecondNumbers = new List<string>() { "52", "48", "49", "51" };

        //Kartımızda görünecek olan günler,sıcaklık değerleri ilki ve ikinci değerleri bir listede tuttuk.
        //Readonly yaparak da sadece okunabilir hale getirdik bir sefer newleyip tanımladık.Aynı zamanda Bir daha da üzerinde
        //değişiklikler gerekmediği içinde de readonly keyword kullandık.Programızın hafızası ve performansı açısından da
        //mantıklı olacaktır.


        public WeatherAdaptiveCardMaker()
        {
            card = new AdaptiveCard();
        }

        //WeatherAdaptiveCardMaker sınıfından bir nesne oluşturulduğunda AdaptiveCard'dan bir nesne oluşturulacaktır
        
           
        

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

        //Hava Durumu kartımızda bugünün hava durumu ve diğer günlerin hava durumu bilgileri yer alacaktır.İlk olarak CreateTodayWeather()
        //metodu ile sadece karttaki bugünün hava durumu ile alakalı bilgileri oluşturuyoruz.Dikkat edin  sadece bugünkü hava durumu ile 
        //alakalı düzenlemeler yapıldı.Yani bir metotta sadece tek işi yaptırılıyor.Bu da SOLID Prensiplerin Single Responsibility Principles
        //prensibine uygun olacaktır.Çünkü bu prensip der ki:Bir sınıf ya da metot sadece bir işi yapar.Birden fazla işi yapmaz.Bu açıdan
        //biz de burda bu prensibe uygun olarak Bugünün hava durum bilgileri ya da kartın üstü kısmı için ayrı bir metot tanımladık.


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

        //Kartımızın Üst Kısmı ile ilgili işlemlerinden sonra Single Responsibility yine uygun olması için de kartımızın alt kısımları
        //için de ayrı bir işlem ya da metot tanımladık.Ama dikkat edin içinde sadece üç tane metotu çağırmış ve kullanmış.
        //Peki bu metotlar ne:
        //1)İlk Sıcaklık değerlerin yapısını olusturacak metot
        //2)İkinci Sıcaklık değerlerin yapısını olusturacak metot
        //3)Günlerin ismini olusturacak metot

        //Neden peki her biri için böyle bir metot oluşturup bir metot içinde hepsini çağırdık???
        //Şöyle açıklayabiliriz yine aslında:
        //1)İlgili işlem diğer işlem etkilenmeden yine kendi ile ilgili işlemleri
        //gerçekleştirsin yani bir metot kendi işi hariç başka işleri yapmasın dedik.Single Responsibiltiy Principle prensibine 
        //uyulmaya çalışıldı diyebiliriz.

        //2)Her bir metot sadece kendi işi yapacak dedik ve birbirinden etkilenmeyecek dedik ama bu işlemler sonuçta kartımızdaki
        //aşağı kısımları oluşturan işlemlerdir.İşte bu ayrı ayrı oluşturulan işlemleri bi ara çağırmamız gerekebilir.İşte CreateToNextDaysWeather()
        //metodu aslında aynı amaç için uğraşan ama içlerinde sorumlulukları farklı işlemleri bi arada toplayan yapıdır.Kısacası metottaki
        //işlemleri ayrı kısımları modüleştirdik ve sonrasında oluşturdan her bir modül ya da işlem birimi büyük birimde çağrılmış olundu
        //Builder Tasarım Desenindeki mantığına da bir bakıma benzetilebilir aslında.Çünkü bir nesne oluştururken ayrı ayrı yapılar
        //adım adım oluşturularak bütüne ulaşılarak bütün yapı aslında parçalar ile beraber oluşturulmuş olunuyor.


        



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


        //Alt ve üst kısımları olutşurulduktan sonra bunları yine biz bir kartın modülü gibi düşünüyoruz ve o modüller ya da iş birimlerini
        //Kartı oluşturacak metotta çağırıyoruz.Tabi yine Attachment tipinde bir nesne döndüreceği için kartımızı
        //Content ve Content tipini burda belirtiyoruz ve return new Attachment diyerek de ilgili nesneyi döndürüyoruz.
    }
}
